using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Text;
using UIShell.Documents.Model;

namespace UIShell.Documents
{
    public partial class DocContext : DbContext
    {
        public DocContext()
            : base("LCL_Documents")
        {

        }
        public DbSet<ProjectDocument> ProjectDocument { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //表前缀
            //modelBuilder.Types().Configure(entity => entity.ToTable("Edms_" + entity.ClrType.Name));
        }
    }
    /// <summary>
    /// 数据库初始化操作类
    /// </summary>
    public static class DatabaseInitializer
    {
        /// <summary>
        /// 数据库初始化
        /// </summary>
        public static void Initialize()
        {
            try
            {
                Database.SetInitializer<DocContext>(new DropCreateDatabaseIfModelChanges<DocContext>());
                Database.SetInitializer(new SampleData());
                using (var db = new DocContext())
                {
                    db.Database.Initialize(false);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
    }
    /// <summary>
    /// 数据库初始化策略
    /// </summary>
    internal class SampleData : CreateDatabaseIfNotExists<DocContext>
    {
        protected override void Seed(DocContext context)
        {
            //单位信息
            context.Set<ProjectDocument>().Add(new ProjectDocument
            {
                ID = Guid.NewGuid(),
                ProjectName = "LCL",
                Title = "架构图",
                TypeName = "帮助",
                Version = "1.0",
                Content = "题材挺好的。可惜演员的演技太恶心了。太做作。"
            });
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder errors = new StringBuilder();
                IEnumerable<DbEntityValidationResult> validationResult = ex.EntityValidationErrors;
                foreach (DbEntityValidationResult result in validationResult)
                {
                    errors.Append(result.Entry + ":" + result.Entry + "\r\n");
                    ICollection<DbValidationError> validationError = result.ValidationErrors;
                    foreach (DbValidationError err in validationError)
                    {
                        errors.Append(err.PropertyName + ":" + err.ErrorMessage + "\r\n");
                    }
                }
                throw new Exception(errors.ToString());
            }
        }
    }
}
