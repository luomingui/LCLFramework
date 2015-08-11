
using System.Collections.Generic;
using System.Windows.Forms;
namespace LCL.Tools
{
    public enum BuildType
    {
        WebBuild,
        WPFBuild,
        WCFBuild,
        WebService,
        WindowsService,
        EntityFrameworkBuild,
    }
    public interface IBuild
    {
        string Library(string path, TableModel tableModel, ProgressBar progressBar);
        //string Library(string path, TableModel tableModel, List<TableColumn> SelectWhere, List<TableColumn> ShowFields, TableModel LeftTree, List<TableColumn> CheckFields, List<TableColumn> CheckFepeat);
    }

    public class BuildHelper
    {
        public static IBuild FactoryBuild(BuildType buildtype)
        {
            IBuild ibuild = null;
            switch (buildtype)
            {
                case BuildType.WebBuild:
                    break;
                case BuildType.WPFBuild:
                    break;
                case BuildType.WCFBuild:
                    break;
                case BuildType.EntityFrameworkBuild:
                    ibuild = new EntityFrameworkBuild();
                    break;
            }
            return ibuild;
        }
    }
}
