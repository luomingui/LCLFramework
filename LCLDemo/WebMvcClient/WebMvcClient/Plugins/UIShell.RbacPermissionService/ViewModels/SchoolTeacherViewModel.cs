using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIShell.RbacPermissionService
{
    /*
SELECT u.ID,ui.TName,u.Name,u.Password,ut.IsGetZGZ,DegreeID,Sex,s.Email,
Telephone,UserQQ,IdCard,ui.Birthday,IsLockedOut FROM dbo.UnitInfo s
INNER JOIN dbo.User_Teacher ut ON s.ID = ut.UnitInfo_ID
INNER JOIN dbo.[User] u ON ut.User_ID = u.ID
INNER JOIN dbo.UserInfo ui ON u.ID = ui.ID
     */
    public class SchoolTeacherViewModel
    {
        public Guid ID { get; set; }
        public string TName { get; set; }
        public string  Name { get; set; }
        public string Password { get; set; }
        public bool IsGetZGZ { get; set; }
        public int DegreeID { get; set; }
        public string Sex { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string UserQQ { get; set; }
        public string IdCard { get; set; }
        public string Birthday { get; set; }
        public bool IsLockedOut { get; set; }
    }
}
