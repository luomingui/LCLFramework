using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LCL.Repositories.EntityFramework
{
    public static class DatabaseExtensions
    {

        #region 动态执行Sql语句扩展
        /// <summary>
        /// 根据sql语句和参数的值动态生成Sqlparameter
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="values">值</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        private static SqlParameter[] MakeSqlParameter(string sql, object[] values)
        {
            var matches = Regex.Matches(sql, @"@\w+");
            List<string> paramNameList = new List<string>();

            foreach (Match m in matches)
            {
                if (paramNameList.Contains(m.Value) == false)
                {
                    paramNameList.Add(m.Value);
                }
            }

            if (values.Length != paramNameList.Count)
            {
                throw new ArgumentException("values的元素数目和Sql语句不匹配");
            }

            int i = 0;
            var parameters = new SqlParameter[values.Length];
            foreach (var pName in paramNameList)
            {
                parameters[i] = new SqlParameter(pName, values[i]);
                i++;
            }
            return parameters;
        }

        /// <summary>
        /// 执行Sql命令  
        /// <example>ExecuteSqlCommandEx("delete from [Table] where ID=@0", Guid.Empty)</example>
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="sql">sql语句</param>
        /// <param name="values">值</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        public static int ExecuteSqlCommandEx(this Database database, string sql, params object[] values)
        {
            var param = MakeSqlParameter(sql, values);
            return database.ExecuteSqlCommand(sql, param);
        }

        /// <summary>
        /// 执行Sql查询
        /// <example>SqlQueryEx("select * from [Table] where name=@0 and password=@1", "abc", "123456")</example>
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="sql">sql语句</param>
        /// <param name="values">值</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        public static IEnumerable<TElement> SqlQueryEx<TElement>(this Database database, string sql, params object[] values)
        {
            var param = MakeSqlParameter(sql, values);
            return database.SqlQuery<TElement>(sql, param);
        }
        #endregion

        #region 排序扩展
        /// <summary>
        /// 排序公共方法
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="orderByKey">排序键排序键(不分大小写)</param>
        /// <param name="orderByMethod">排序方法</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        private static IOrderedQueryable<T> OrderByCommon<T>(IQueryable<T> source, string orderByKey, string orderByMethod) where T : class
        {
            if (string.IsNullOrEmpty(orderByKey))
            {
                throw new ArgumentNullException("orderByKey");
            }

            Type sourceTtype = typeof(T);
            PropertyInfo keyProperty = sourceTtype.GetProperties().FirstOrDefault(p => p.Name.ToLower().Equals(orderByKey.Trim().ToLower()));
            if (keyProperty == null)
            {
                throw new ArgumentException("orderByKey不存在...");
            }

            var param = Expression.Parameter(sourceTtype, "item");
            var body = Expression.MakeMemberAccess(param, keyProperty);
            var orderByLambda = Expression.Lambda(body, param);

            var resultExp = Expression.Call(typeof(Queryable), orderByMethod, new Type[] { sourceTtype, keyProperty.PropertyType }, source.Expression, Expression.Quote(orderByLambda));
            var ordereQueryable = source.Provider.CreateQuery<T>(resultExp) as IOrderedQueryable<T>;
            return ordereQueryable;
        }

        /// <summary>
        /// 排序
        /// 选择升序或降序
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="orderByKey">排序键排序键(不分大小写)</param>
        /// <param name="ascending">是否升序</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string orderByKey, bool ascending) where T : class
        {
            var methodName = ascending ? "OrderBy" : "OrderByDescending";
            return OrderByCommon(source, orderByKey, methodName);
        }

        /// <summary>
        /// 排序次项
        /// 选择升序或降序
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="orderByKey">排序键(不分大小写)</param>
        /// <param name="ascending">是否升序</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string orderByKey, bool ascending) where T : class
        {
            var methodName = ascending ? "ThenBy" : "ThenByDescending";
            return OrderByCommon(source, orderByKey, methodName);
        }

        /// <summary>
        /// 多字段混合排序       
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="orderByString">排序字符串：例如CreateTime desc, ID asc 不区分大小写</param>
        /// <exception cref="ArgumentNullException">orderByString</exception>
        /// <returns></returns>
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string orderByString) where T : class
        {
            Func<string[], bool> descFun = (item) => item.Length > 1 && item[1].Trim().ToLower().Equals("desc");

            var parameters = orderByString
                .Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(item => item.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries))
                .Select(item => new { Key = item.FirstOrDefault(), Asc = !descFun(item) })
                .ToList();

            if (parameters.Count == 0)
            {
                throw new ArgumentNullException("orderByString");
            }

            var firstP = parameters.FirstOrDefault();
            var orderQuery = source.OrderBy(firstP.Key, firstP.Asc);
            parameters.Skip(1).ToList().ForEach(p => orderQuery = orderQuery.ThenBy(p.Key, p.Asc));

            return orderQuery;
        }

        /// <summary>
        /// 排序
        /// 选择升序或降序
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <typeparam name="TKey">排序键</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="orderKeySelector">排序器</param>
        /// <param name="ascending">是否升序</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> OrderBy<T, TKey>(this IQueryable<T> source, Expression<Func<T, TKey>> orderKeySelector, bool ascending)
        {
            if (ascending)
            {
                return source.OrderBy(orderKeySelector);
            }
            else
            {
                return source.OrderByDescending(orderKeySelector);
            }
        }

        /// <summary>
        /// 次项排序
        /// 选择升序或降序
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <typeparam name="TKey">排序键</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="orderKeySelector">排序器</param>
        /// <param name="ascending">是否升序</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> ThenBy<T, TKey>(this IOrderedQueryable<T> source, Expression<Func<T, TKey>> orderKeySelector, bool ascending)
        {
            if (ascending)
            {
                return source.ThenBy(orderKeySelector);
            }
            else
            {
                return source.ThenByDescending(orderKeySelector);
            }
        }
        #endregion

        #region 查询方法的扩展
        /// <summary>      
        /// Select方法的补充      
        /// rpy.SelectEx(item =>new TNew(){})等同rpy.Select(item => new TNew(){f1 = item.f1, f2 = item.f2 ,...})    
        /// rpy.SelectEx(item =>new TNew(){f1 = "something"})等同
        /// rpy.Select(item => new TNew(){ f1 ="something", f2 = item.f2 ,...})
        /// 其它选择方法和原始Select方法一致
        /// </summary>        
        /// <typeparam name="T">源实体类型</typeparam>
        /// <typeparam name="TNew">新的实体类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="selector">新对象实例</param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        public static IQueryable<TNew> SelectEx<T, TNew>(this IQueryable<T> source, Expression<Func<T, TNew>> selector) where T : class
        {
            if (selector == null)
            {
                throw new ArgumentNullException();
            }

            var body = selector.Body as MemberInitExpression;
            if (body != null)
            {
                var targetType = typeof(TNew);
                var targetProperties = targetType.GetProperties().ToList();
                var sourceProperties = typeof(T).GetProperties();

                var parameter = selector.Parameters[0];
                var bindedMembers = body.Bindings.Select(b => b.Member).ToList();
                var needBindProroties = targetProperties.Where(p => bindedMembers.Exists(m => m.Name.Equals(p.Name)) == false);

                var allBindings = body.Bindings.ToList();
                foreach (var property in needBindProroties)
                {
                    var sourceProperty = sourceProperties.FirstOrDefault(item => item.Name.Equals(property.Name));
                    if (sourceProperty != null)
                    {
                        var memberExp = Expression.MakeMemberAccess(parameter, sourceProperty);
                        var binding = Expression.Bind(property, memberExp);
                        allBindings.Add(binding);
                    }
                }

                var targetNew = Expression.New(targetType);
                var bodyNew = Expression.MemberInit(targetNew, allBindings);
                selector = (Expression<Func<T, TNew>>)Expression.Lambda(bodyNew, parameter);
            }

            return source.Select(selector);
        }
        #endregion

        #region DatabaseExt
        public static IEnumerable SqlQueryForDynamic(this Database db,
                string sql,
                params object[] parameters)
        {
            IDbConnection defaultConn = new System.Data.SqlClient.SqlConnection();

            return SqlQueryForDynamicOtherDB(db, sql, defaultConn, parameters);
        }

        public static IEnumerable SqlQueryForDynamicOtherDB(this Database db,
                      string sql,
                      IDbConnection conn,
                      params object[] parameters)
        {
            conn.ConnectionString = db.Connection.ConnectionString;

            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            IDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;

            IDataReader dataReader = cmd.ExecuteReader();

            if (!dataReader.Read())
            {
                return null; //无结果返回Null
            }

            #region 构建动态字段

            TypeBuilder builder = DatabaseExtensions.CreateTypeBuilder(
                          "EF_DynamicModelAssembly",
                          "DynamicModule",
                          "DynamicType");

            int fieldCount = dataReader.FieldCount;
            for (int i = 0; i < fieldCount; i++)
            {
                //dic.Add(i, dataReader.GetName(i));

                //Type type = dataReader.GetFieldType(i);

                DatabaseExtensions.CreateAutoImplementedProperty(
                  builder,
                  dataReader.GetName(i),
                  dataReader.GetFieldType(i));
            }

            #endregion

            dataReader.Close();
            dataReader.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            Type returnType = builder.CreateType();

            if (parameters != null)
            {
                return db.SqlQuery(returnType, sql, parameters);
            }
            else
            {
                return db.SqlQuery(returnType, sql);
            }
        }

        public static TypeBuilder CreateTypeBuilder(string assemblyName,
                              string moduleName,
                              string typeName)
        {
            TypeBuilder typeBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(
              new AssemblyName(assemblyName),
              AssemblyBuilderAccess.Run).DefineDynamicModule(moduleName).DefineType(typeName,
              TypeAttributes.Public);
            typeBuilder.DefineDefaultConstructor(MethodAttributes.Public);
            return typeBuilder;
        }

        public static void CreateAutoImplementedProperty(
                            TypeBuilder builder,
                            string propertyName,
                            Type propertyType)
        {
            const string PrivateFieldPrefix = "m_";
            const string GetterPrefix = "get_";
            const string SetterPrefix = "set_";

            // Generate the field.
            FieldBuilder fieldBuilder = builder.DefineField(
              string.Concat(
                PrivateFieldPrefix, propertyName),
              propertyType,
              FieldAttributes.Private);

            // Generate the property
            PropertyBuilder propertyBuilder = builder.DefineProperty(
              propertyName,
              System.Reflection.PropertyAttributes.HasDefault,
              propertyType, null);

            // Property getter and setter attributes.
            MethodAttributes propertyMethodAttributes = MethodAttributes.Public
              | MethodAttributes.SpecialName
              | MethodAttributes.HideBySig;

            // Define the getter method.
            MethodBuilder getterMethod = builder.DefineMethod(
                string.Concat(
                  GetterPrefix, propertyName),
                propertyMethodAttributes,
                propertyType,
                Type.EmptyTypes);

            // Emit the IL code.
            // ldarg.0
            // ldfld,_field
            // ret
            ILGenerator getterILCode = getterMethod.GetILGenerator();
            getterILCode.Emit(OpCodes.Ldarg_0);
            getterILCode.Emit(OpCodes.Ldfld, fieldBuilder);
            getterILCode.Emit(OpCodes.Ret);

            // Define the setter method.
            MethodBuilder setterMethod = builder.DefineMethod(
              string.Concat(SetterPrefix, propertyName),
              propertyMethodAttributes,
              null,
              new Type[] { propertyType });

            // Emit the IL code.
            // ldarg.0
            // ldarg.1
            // stfld,_field
            // ret
            ILGenerator setterILCode = setterMethod.GetILGenerator();
            setterILCode.Emit(OpCodes.Ldarg_0);
            setterILCode.Emit(OpCodes.Ldarg_1);
            setterILCode.Emit(OpCodes.Stfld, fieldBuilder);
            setterILCode.Emit(OpCodes.Ret);

            propertyBuilder.SetGetMethod(getterMethod);
            propertyBuilder.SetSetMethod(setterMethod);
        }
        #endregion
        
    }

}
