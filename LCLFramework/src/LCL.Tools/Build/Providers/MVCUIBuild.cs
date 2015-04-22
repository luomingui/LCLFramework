using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace LCL.Tools
{
    public class MVCUIBuildBase
    {
        public void GenerateControllers(string path)
        {
            List<TableModel> tableNames = BLLFactory.Instance.idb.GetTableModelList(Utils.dbName, true);
            for (int i = 0; i < tableNames.Count; i++)
            {
                TableModel tm = tableNames[i];
                string tablename = tm.TableName;
                string tableInfo = tm.TableNameRemark;
                if (tablename == "__MigrationHistory" && tablename == "sysdiagrams")
                {
                    continue;
                }
                StringBuilder builder = new StringBuilder();
                builder.AppendLine("/******************************************************* ");
                builder.AppendLine("*  ");
                builder.AppendLine("* 作者：罗敏贵 ");
                builder.AppendLine("* 说明： " + tableInfo);
                builder.AppendLine("* 运行环境：.NET 4.5.0 ");
                builder.AppendLine("* 版本号：1.0.0 ");
                builder.AppendLine("*  ");
                builder.AppendLine("* 历史记录： ");
                builder.AppendLine("*    创建文件 罗敏贵 " + DateTime.Now.ToLongDateString());
                builder.AppendLine("*  ");
                builder.AppendLine("*******************************************************/ ");
                builder.AppendLine("using LCL.MvcExtensions; ");
                builder.AppendLine("using LCL.Repositories;");
                builder.AppendLine("using LCL;");
                builder.AppendLine("using System; ");
                builder.AppendLine("using System.Collections.Generic; ");
                builder.AppendLine("using System.Linq; ");
                builder.AppendLine("using System.Web; ");
                builder.AppendLine("using System.Web.Mvc; ");
                builder.AppendLine("using UIShell.RbacPermissionService; ");
                builder.AppendLine(" ");
                builder.AppendLine("namespace UIShell.RbacManagementPlugin.Controllers ");
                builder.AppendLine("{ ");
                builder.AppendLine("    public class " + tablename + "Controller : RbacController<" + tablename + "> ");
                builder.AppendLine("    { ");
                var list = tm.Columns.Where(e => e.ColumnName.Contains("_"));
                StringBuilder ddl = new StringBuilder();
                StringBuilder crto = new StringBuilder();
                foreach (var item in list)
                {
                    crto.AppendLine("       ddl" + item.ColumnName.Remove(item.ColumnName.Length - 3) + "(Guid.Empty); ");

                    ddl.AppendLine("        public void ddl" + item.ColumnName.Remove(item.ColumnName.Length - 3) + "(Guid dtId) ");
                    ddl.AppendLine("        { ");
                    ddl.AppendLine("            var repo = RF.FindRepo<" + item.ColumnName.Remove(item.ColumnName.Length - 3) + ">(); ");
                    ddl.AppendLine("            var list = repo.FindAll(); ");
                    ddl.AppendLine(" ");
                    ddl.AppendLine("            List<SelectListItem> selitem = new List<SelectListItem>(); ");
                    ddl.AppendLine("            if (list.Count() > 0) ");
                    ddl.AppendLine("            { ");
                    if (tm.IsTree)
                    {
                        ddl.AppendLine("                var roots = list.Where(e => e.ParentId == Guid.Empty); ");
                        ddl.AppendLine("                foreach (var item in roots) ");
                        ddl.AppendLine("                { ");
                        ddl.AppendLine("                    selitem.Add(new SelectListItem { Text = item.Name, Value = item.ID.ToString() }); ");
                        ddl.AppendLine("                    var child = list.Where(p => p.ParentId == item.ID); ");
                        ddl.AppendLine("                    foreach (var item1 in child) ");
                        ddl.AppendLine("                    { ");
                        ddl.AppendLine("                        if (dtId == item1.ID) ");
                        ddl.AppendLine("                        { ");
                        ddl.AppendLine("                            selitem.Add(new SelectListItem { Text = \"----\" + item1.Name, Value = item1.ID.ToString(), Selected = true }); ");
                        ddl.AppendLine("                            this.ViewData[\"selected\"] = item1.ID.ToString(); ");
                        ddl.AppendLine("                        } ");
                        ddl.AppendLine("                        else ");
                        ddl.AppendLine("                        { ");
                        ddl.AppendLine("                            selitem.Add(new SelectListItem { Text = \"----\" + item1.Name, Value = item1.ID.ToString() }); ");
                        ddl.AppendLine("                        } ");
                        ddl.AppendLine("                    } ");
                        ddl.AppendLine("                } ");
                        ddl.AppendLine("            } ");
                    }
                    else
                    {
                        ddl.AppendLine("                var roots = list; ");
                        ddl.AppendLine("                foreach (var item in roots) ");
                        ddl.AppendLine("                { ");
                        ddl.AppendLine("                    selitem.Add(new SelectListItem { Text = item.Name, Value = item.ID.ToString() }); ");
                        ddl.AppendLine("                } ");
                        ddl.AppendLine("            } ");
                    }
                    ddl.AppendLine("            selitem.Insert(0, new SelectListItem { Text = \"==" + item.ColumnRemark + "==\", Value = \"-1\" }); ");
                    ddl.AppendLine("            ViewData[\"ddl" + tablename + "\"] = selitem; ");
                    ddl.AppendLine("        } ");
                }
                builder.AppendLine("        public " + tablename + "Controller() ");
                builder.AppendLine("        { ");
                builder.AppendLine(crto.ToString());
                builder.AppendLine("        } ");
                builder.AppendLine(ddl.ToString());
                builder.AppendLine(" ");
                builder.AppendLine("    } ");
                builder.AppendLine("} ");

                if (path.Length > 0)
                {
                    string folder = path + @"\LCL\Controllers\";
                    Utils.FolderCheck(folder);
                    string filename = folder + @"\" + tablename + "Controller.cs";
                    Utils.CreateFiles(filename, builder.ToString());
                }
            }

        }
    }
    public class MVCUIBuild : MVCUIBuildBase
    {
        //bootstrapAdmin
        public void GenerateBootstrapAdminViews(string path)
        {
            List<TableModel> tableNames = BLLFactory.Instance.idb.GetTableModelList(Utils.dbName, true);
            for (int i = 0; i < tableNames.Count; i++)
            {
                TableModel tm = tableNames[i];
                string tablename = tm.TableName;
                string tableInfo = tm.TableNameRemark;

                if (tablename == "__MigrationHistory" && tablename == "sysdiagrams")
                {
                    continue;
                }

                string fileListPath = path + @"\LCL\Views\" + tablename + @"\Index.cshtml";
                string fileAddOrEditPath = path + @"\LCL\Views\" + tablename + @"\AddOrEdit.cshtml";
                Utils.FolderCheck(path + @"\LCL\Views\" + tablename);

                StringBuilder builder = new StringBuilder();

                #region Index.cshtml
                builder.AppendLine("@using LCL.MvcExtensions;  ");
                builder.AppendLine("@{  ");
                builder.AppendLine("    ViewBag.Title = \"" + tableInfo + "\";  ");
                builder.AppendLine("}  ");
                builder.AppendLine("<!-- Page title --> ");
                builder.AppendLine("<div class=\"page-title\"> ");
                builder.AppendLine("    <h2><i class=\"icon-desktop color\"></i> " + tableInfo + " <small>显示所有" + tableInfo + "</small></h2> ");
                builder.AppendLine("    <hr /> ");
                builder.AppendLine("</div> ");
                builder.AppendLine("<!-- Page title --> ");
                builder.AppendLine("<div class=\"awidget\"> ");
                builder.AppendLine("    <div class=\"awidget-head\"> ");
                builder.AppendLine("        <div class=\"row\"> ");
                builder.AppendLine("            <div class=\"col-md-10\"> ");
                builder.AppendLine("                <span>总数：@Model.TotalCount</span> ");
                builder.AppendLine("            </div> ");
                builder.AppendLine("            <div class=\"col-md-2\"> ");
                builder.AppendLine("                <button type=\"button\" class=\"btn btn-warning pull-right\" onclick=\"javascript: location.href='@Url.Action(\"AddOrEdit\", ");
                builder.AppendLine("    new { currentPageNum = Model.CurrentPageNum, pageSize = Model.PageSize })';\"> ");
                builder.AppendLine("                    新 增 ");
                builder.AppendLine("                </button>  ");
                builder.AppendLine("            </div> ");
                builder.AppendLine("        </div> ");
                builder.AppendLine("    </div> ");
                builder.AppendLine("    <div class=\"awidget-body\"> ");
                builder.AppendLine("        <div class=\"row\"> ");
                builder.AppendLine("            <div class=\"col-md-12\"> ");
                builder.AppendLine("                <table class=\"table table-hover table-bordered\"> ");
                builder.AppendLine("                    <thead> ");
                builder.AppendLine("                        <tr> ");
                builder.AppendLine("                           <th>操作</th> ");
                foreach (var item in tm.Columns)
                {
                    if (item.ColumnName == "ID")
                    {

                    }
                    else if (item.ColumnName == "AddDate")
                    {

                    }
                    else
                    {
                        builder.AppendLine("            <th>" + item.ColumnRemark + "</th> ");
                    }
                }
                builder.AppendLine("                        </tr> ");
                builder.AppendLine("                    </thead> ");
                builder.AppendLine("                    <tbody> ");
                builder.AppendLine("                        @{ ");
                builder.AppendLine("                            foreach (var view in Model.PagedModels) ");
                builder.AppendLine("                            { ");
                builder.AppendLine("                                <tr> ");
                foreach (var item in tm.Columns)
                {
                    if (item.ColumnName == "ID")
                    {
                        builder.AppendLine("                                    <td> ");
                        builder.AppendLine("                                        <button type=\"button\" class=\"btn btn-xs btn-warning\" ");
                        builder.AppendLine("                                                onclick=\"javascript: location.href = '@Url.Action(\"AddOrEdit\", new { ID = view.ID, currentPageNum = Model.CurrentPageNum, pageSize = Model.PageSize })';\"> ");
                        builder.AppendLine("                                            <i class=\"icon-pencil\"></i> ");
                        builder.AppendLine("                                        </button> ");
                        builder.AppendLine("                                        <button type=\"button\" class=\"btn btn-xs btn-danger\" ");
                        builder.AppendLine("                                                onclick=\"javacript: if (confirm('确认要删除这条数据吗？')) {  location.href = '@Url.Action(\"Delete\", new { ID = view.ID, currentPageNum = Model.CurrentPageNum, pageSize = Model.PageSize })'; };\"> ");
                        builder.AppendLine("                                            <i class=\"icon-remove\"></i> ");
                        builder.AppendLine("                                        </button> ");
                        builder.AppendLine("                                    </td> ");
                    }
                    else if (item.ColumnName == "AddDate")
                    {

                    }
                    else
                    {
                        builder.AppendLine("                    <td>@view." + item.ColumnName + "</td> ");
                    }
                }
                builder.AppendLine("                                </tr> ");
                builder.AppendLine("                            } ");
                builder.AppendLine("                        } ");
                builder.AppendLine("                    </tbody> ");
                builder.AppendLine("                </table> ");
                builder.AppendLine("            </div> ");
                builder.AppendLine("        </div> ");
                builder.AppendLine("        @Html.Partial(\"_PagedListBottom\") ");
                builder.AppendLine("    </div> ");
                builder.AppendLine("</div>  ");
                builder.AppendLine("  ");
                builder.AppendLine(" ");
                builder.AppendLine("  ");

                File.AppendAllText(fileListPath, builder.ToString(), Encoding.UTF8);
                #endregion

                #region AddOrEdit.cshtml
                builder = new StringBuilder();
                builder.AppendLine("@{ ");
                builder.AppendLine("    ViewBag.Title = \"编辑" + tableInfo + "\"; ");
                builder.AppendLine("} ");
                builder.AppendLine(" ");
                builder.AppendLine("@using LCL.MvcExtensions ");
                builder.AppendLine("@using UIShell.RbacPermissionService ");
                builder.AppendLine("@model AddOrEditViewModel<" + tablename + "> ");
                builder.AppendLine(" ");
                builder.AppendLine("<!-- Page title --> ");
                builder.AppendLine("<div class=\"page-title\"> ");
                builder.AppendLine("    <h2><i class=\"icon-desktop color\"></i> " + tableInfo + " <small>  ");
                builder.AppendLine("          @{ ");
                builder.AppendLine("            string actionTag=\"\"; ");
                builder.AppendLine("            if (Model.Added) { actionTag = \"新增一个" + tableInfo + "\"; } else { actionTag = \"编辑一个" + tableInfo + "\"; } ");
                builder.AppendLine("            }@actionTag ");
                builder.AppendLine("   </small></h2> <hr /> ");
                builder.AppendLine("</div> ");
                builder.AppendLine("<!-- Page title --> ");
                builder.AppendLine(" ");
                builder.AppendLine("<div class=\"row\"> ");
                builder.AppendLine("    <!--col-md-6 start--> ");
                builder.AppendLine("    <div class=\"col-md-12\"> ");
                builder.AppendLine("        <!--box-info start--> ");
                builder.AppendLine("        <div class=\"col-md-12\"> ");
                builder.AppendLine("            <!--box-info start--> ");
                builder.AppendLine("            <div class=\"awidget\"> ");
                builder.AppendLine("                <div class=\"awidget-head\"> ");
                builder.AppendLine("                    <h3>" + tableInfo + "</h3> ");
                builder.AppendLine("                </div> ");
                builder.AppendLine("                <div class=\"awidget-body\"> ");
                builder.AppendLine("                    <!--form-horizontal row-border start--> ");
                builder.AppendLine("                    @using (Html.BeginForm(Model.Added ? \"Add\" : \"Edit\", \"" + tablename + "\", FormMethod.Post, new { @class = \"form-horizontal\", @role = \"form\" })) ");
                builder.AppendLine("                    { ");
                builder.AppendLine("                        @Html.HiddenFor(c => c.CurrentPageNum) ");
                builder.AppendLine("                        @Html.HiddenFor(c => c.PageSize) ");
                builder.AppendLine("                        @Html.HiddenFor(c => c.Entity.ID) ");
                builder.AppendLine(" ");
                foreach (var item in tm.Columns)
                {
                    if (item.ColumnName != "ID")
                    {
                        if (item.ColumnName.Contains("_"))
                        {
                            builder.AppendLine("                        <div class=\"form-group\"> ");
                            builder.AppendLine("                            <label class=\"col-sm-2 control-label\">上一级</label> ");
                            builder.AppendLine("                            <div class=\"col-sm-10\"> ");
                            builder.AppendLine("                                 @Html.DropDownList(\"ddl" + tablename + "\", ViewData[\"ddl" + tablename + "\"] as IEnumerable<SelectListItem>) ");
                            builder.AppendLine("                            </div> ");
                            builder.AppendLine("                        </div> ");
                        }
                        else
                        {
                            if (!item.ColumnName.Equals("AddDate") && !item.ColumnName.Equals("UpdateDate"))
                            {
                                builder.AppendLine("                        <div class=\"form-group\"> ");
                                builder.AppendLine("                            <label class=\"col-sm-2 control-label\">@Html.LabelFor(c => c.Entity." + item.ColumnName + ")</label> ");
                                builder.AppendLine("                            <div class=\"col-sm-10\"> ");
                                builder.AppendLine("                                @Html.TextBoxFor(c => c.Entity." + item.ColumnName + ", new { @class = \"form-control\", @placeholder = \"请输入" + item.ColumnRemark + "\" }) ");
                                builder.AppendLine("                                @Html.ValidationMessageFor(c => c.Entity." + item.ColumnName + ") ");
                                builder.AppendLine("                            </div> ");
                                builder.AppendLine("                        </div> ");
                            }
                        }
                    }
                }
                builder.AppendLine(" ");
                builder.AppendLine("                        <div class=\"form-group\"> ");
                builder.AppendLine("                            <label class=\"col-sm-2 control-label\"></label> ");
                builder.AppendLine("                            <div class=\"col-sm-10\"> ");
                builder.AppendLine("                                <button type=\"submit\" class=\"btn btn-primary\">保存</button>&nbsp;&nbsp; ");
                builder.AppendLine("                                <button type=\"button\" class=\"btn btn-default\" onclick=\"javascript: window.history.back();\">返回</button> ");
                builder.AppendLine("                            </div> ");
                builder.AppendLine("                        </div> ");
                builder.AppendLine("                         ");
                builder.AppendLine("                         ");
                builder.AppendLine("                    } ");
                builder.AppendLine("                </div> ");
                builder.AppendLine("            </div> ");
                builder.AppendLine(" ");
                builder.AppendLine("        </div> ");
                builder.AppendLine("    </div> ");
                builder.AppendLine("</div> ");
                File.AppendAllText(fileAddOrEditPath, builder.ToString(), Encoding.UTF8);
                #endregion
            }
        }
    }
}
