
using System.Collections.Generic;
using System.Windows.Forms;
namespace SF.Tools
{
    public enum BuildType
    {
        WebBuild,
        WinFromBuild,
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
                case BuildType.WinFromBuild:
                    ibuild = new WinFromComplexBuild();
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
