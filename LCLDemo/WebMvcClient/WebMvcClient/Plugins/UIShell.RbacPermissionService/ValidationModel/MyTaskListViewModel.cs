using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIShell.RbacPermissionService
{
    public class MyTaskListViewModel
    {
        public Guid TaskID { get; set; }
        public string TaskName { get; set; }
        public string BillName { get; set; }
        public string RoutName { get; set; }
        public int TaskState { get; set; }
        public int ItemSstate { get; set; }
        public DateTime AddDate { get; set; }
    }
}
