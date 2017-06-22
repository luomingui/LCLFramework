
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Options;
using System;
using System.Reflection;

namespace LCL.Repositories.MongoDB.Conventions
{
    public class UseLocalDateTimeConvention : IMemberMapConvention
    {
        #region IMemberMapConvention Members
        public void Apply(BsonMemberMap memberMap)
        {
            IBsonSerializationOptions options = null;
            switch (memberMap.MemberInfo.MemberType)
            {
                case MemberTypes.Property:
                    PropertyInfo propertyInfo = (PropertyInfo)memberMap.MemberInfo;
                    if (propertyInfo.PropertyType == typeof(DateTime) ||
                        propertyInfo.PropertyType == typeof(DateTime?))
                        options = new DateTimeSerializationOptions(DateTimeKind.Local);
                    break;
                case MemberTypes.Field:
                    FieldInfo fieldInfo = (FieldInfo)memberMap.MemberInfo;
                    if (fieldInfo.FieldType == typeof(DateTime) ||
                        fieldInfo.FieldType == typeof(DateTime?))
                        options = new DateTimeSerializationOptions(DateTimeKind.Local);
                    break;
                default:
                    break;
            }
            memberMap.SetSerializationOptions(options);
        }

        #endregion

        #region IConvention Members
        public string Name
        {
            get { return this.GetType().Name; }
        }

        #endregion
    }
}
