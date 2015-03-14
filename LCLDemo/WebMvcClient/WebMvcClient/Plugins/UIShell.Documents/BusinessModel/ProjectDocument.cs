using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UIShell.Documents.Model
{
    public partial class ProjectDocument : BaseModel
    {
        public string ProjectName { get; set; }
        public string Title { get; set; }
        public string  TypeName { get; set; }
        public string  Version { get; set; }
        public string Content { get; set; }
    }
}