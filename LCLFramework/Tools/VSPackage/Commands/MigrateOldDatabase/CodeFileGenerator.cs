using EnvDTE;
using LCL.Data;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCL.VSPackage.Commands.MigrateOldDatabase
{
    class CodeFileGenerator
    {
        private ProjectItem _directory;
        private ProjectItem _repoDirectory;
        private string _domainBaseEntityName;

        public string DomainName { get; set; }
        public DbSetting DbSetting { get; set; }
        public ProjectItem Directory
        {
            get { return _directory; }
            set { _directory = value; }
        }
        public ProjectItem RepoDirectory
        {
            get { return _repoDirectory; }
            set { _repoDirectory = value; }
        }
        public int SuccessCount { get; private set; }

        internal string Generate()
        {
            this.SuccessCount = 0;

            var dba = new DbAccesser(this.DbSetting);


            throw new NotImplementedException();
        }
    }
}
