using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCL.Tools
{
    /*
     * 1:生成表单
     * 2：生成树形管理页面
     * 3：生成数据表管理页面
     * 4：生成父子页面
     */
    class EasyUIBuild
    {
        string[] cols = { "ID", "IsLast", "Level", "NodePath", "OrderBy", "IsDelete", "AddDate", "UpdateDate" };
        internal void GenerateEasyUIBuild(string path)
        {
            GenerateView(path);
            GenerateControllers(path);
            GenerateEasyUILanguageResource(path);
            GenerateEasyUITableJs(path);
        }
        private void GenerateView(string path)
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
                if (tm.IsTree)//是否树形表
                {
                    builder.Append(GenerateEasyUITreeTable(tm));
                }
                else
                {
                    builder.Append(GenerateEasyUITable(tm));
                }
                if (path.Length > 0)
                {
                    string folder = path + @"\LCL\EasyUI\View\" + tablename + "\\";
                    Utils.FolderCheck(folder);
                    string filename = folder + @"\Index.cshtml";
                    Utils.CreateFiles(filename, builder.ToString());
                }
            }
        }

        #region 生成树形管理页面
        private string GenerateEasyUITreeTable(TableModel tm)
        {
            var list = tm.Columns;
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("<div class=\"easyui-layout\" fit=\"true\" id=\"tb\"> ");
            builder.AppendLine("    <div data-options=\"region:'west',title:'" + tm.TableNameRemark + "'\" style=\"width: 200px;\"> ");
            builder.AppendLine("        <ul id=\"uitree_" + tm.TableName.ToLower() + "\" class=\"easyui-tree\"></ul> ");
            builder.AppendLine("    </div> ");
            builder.AppendLine("    <div data-options=\"region:'center',title:'" + tm.TableNameRemark + "'\" style=\"padding: 5px; overflow-y: hidden;\"> ");
            builder.AppendLine("        <div style=\"background: #efefef; width: 100%; border-bottom: #99bbe8 1px solid;\"> ");
            builder.AppendLine("            <a id=\"btnAdd" + tm.TableName.ToLower() + "\" href=\"javascript:;\" plain=\"true\" class=\"easyui-linkbutton\" icon=\"icon-add\">新增下级</a>&nbsp; ");
            builder.AppendLine("            <a id=\"btnDel" + tm.TableName.ToLower() + "\" href=\"javascript:;\" plain=\"true\" class=\"easyui-linkbutton\" icon=\"icon-remove\">删除</a>&nbsp; ");
            builder.AppendLine("            <a id=\"butSave" + tm.TableName.ToLower() + "\" href=\"javascript:;\" plain=\"true\" class=\"easyui-linkbutton\" icon=\"icon-save\">保存</a>&nbsp; ");
            builder.AppendLine("        </div> ");
            builder.Append(GenerateEasyUIFrom(tm));
            builder.AppendLine("    </div> ");
            builder.AppendLine("</div> ");
            builder.AppendLine("@section scripts{ ");
            builder.AppendLine("    <script> ");
            builder.AppendLine(GenerateEasyUITreeTableJs(tm));
            builder.AppendLine("    </script> ");
            builder.AppendLine("} ");

            return builder.ToString();
        }
        private string GenerateEasyUITreeTableJs(TableModel tm)
        {
            var list = tm.Columns;
            StringBuilder builder = new StringBuilder();
            StringBuilder builder1 = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            StringBuilder builder3 = new StringBuilder();
            StringBuilder builder4 = new StringBuilder();
            foreach (var item in list)
            {
                builder1.AppendLine(" $('#" + tm.TableName.ToLower() + "_Entity_" + item.ColumnName + "').val(node." + item.ColumnName + "); ");
                builder2.AppendLine(" $('#" + tm.TableName.ToLower() + "_Entity_" + item.ColumnName + "').val(''); ");
                builder3.AppendLine("var " + item.ColumnName + "= $('#" + tm.TableName.ToLower() + "_Entity_" + item.ColumnName + "').val(); ");
                builder4.Append("" + item.ColumnName + ": " + item.ColumnName + ",");
            }
            if (builder4.Length > 1)
                builder4.Remove(builder4.Length - 1, 1);

            builder.AppendLine("        var root = \"@(Url.BundlePContent(\"UIShell.RbacManagementPlugin\", \"" + tm.TableName + "\"))/\"; ");
            builder.AppendLine("        var added = true; ");
            builder.AppendLine("        var dicType; ");
            builder.AppendLine("        var LCL_" + tm.TableName.ToLower() + " = { ");
            builder.AppendLine("            showTree: function () { ");
            builder.AppendLine("                $('#uitree_" + tm.TableName.ToLower() + "').tree({ ");
            builder.AppendLine("                    url: root + 'AjaxEasyUITree_" + tm.TableName + "', ");
            builder.AppendLine("                    lines: true, ");
            builder.AppendLine("                    onClick: function (node) { ");
            builder.AppendLine("                        added = false; ");
            builder.AppendLine("                        var cc = node.id; ");
            builder.AppendLine("                        dicType = cc; ");
            builder.AppendLine("                        $('#parentid').val(node.parentId); ");
            builder.AppendLine("                        $('#parentname').val(node.parentName); ");
            builder.AppendLine(" ");
            builder.AppendLine(builder1.ToString());
            builder.AppendLine(" ");
            builder.AppendLine(" ");
            builder.AppendLine("                    } ");
            builder.AppendLine("                }); ");
            builder.AppendLine("            }, ");
            builder.AppendLine("            reload: function () { ");
            builder.AppendLine("                $('#uitree_" + tm.TableName.ToLower() + "').tree('reload'); ");
            builder.AppendLine("            }, ");
            builder.AppendLine("            getSelected: function () { ");
            builder.AppendLine("                return $('#uitree_" + tm.TableName.ToLower() + "').tree('getSelected'); ");
            builder.AppendLine("            }, ");
            builder.AppendLine("            add: function () { ");
            builder.AppendLine("                var node = LCL_" + tm.TableName.ToLower() + ".getSelected(); ");
            builder.AppendLine("                if (node) { ");
            builder.AppendLine("                    //新增下级 ");
            builder.AppendLine("                    added = true; ");
            builder.AppendLine("                    $('#parentid').val(node.id); ");
            builder.AppendLine("                    $('#parentname').val(node.text); ");
            builder.AppendLine(" ");
            builder.AppendLine(builder2.ToString());
            builder.AppendLine(" ");
            builder.AppendLine(" ");
            builder.AppendLine("                } ");
            builder.AppendLine("            }, ");
            builder.AppendLine("            save: function () { ");
            builder.AppendLine(builder3.ToString());
            builder.AppendLine(" ");
            builder.AppendLine("                if (added) { ");
            builder.AppendLine("                    $.post(root + 'AjaxAdd', ");
            builder.AppendLine("                        { " + builder4.ToString() + " }, function (data) { ");
            builder.AppendLine("                            $.messager.alert('系统消息', data.Message); ");
            builder.AppendLine("                            //重新加载当前页 ");
            builder.AppendLine("                            LCL_" + tm.TableName.ToLower() + ".reload(); ");
            builder.AppendLine("                        }, \"json\"); ");
            builder.AppendLine("                } else { ");
            builder.AppendLine("                    $.post(root + 'AjaxEdit', ");
            builder.AppendLine("                        { " + builder4.ToString() + " }, function (data) { ");
            builder.AppendLine("                            $.messager.alert('系统消息', data.Message); ");
            builder.AppendLine("                            //重新加载当前页 ");
            builder.AppendLine("                            LCL_" + tm.TableName.ToLower() + ".reload(); ");
            builder.AppendLine("                        }, \"json\"); ");
            builder.AppendLine("                } ");
            builder.AppendLine("            }, ");
            builder.AppendLine("            del: function () { ");
            builder.AppendLine("                var node = LCL_" + tm.TableName.ToLower() + ".getSelected(); ");
            builder.AppendLine("                if (node) { ");
            builder.AppendLine("                    $.messager.confirm('确认', '确认要删除选中记录吗?', function (y) { ");
            builder.AppendLine("                        if (y) { ");
            builder.AppendLine("                            //提交 ");
            builder.AppendLine("                            $.post(root + 'AjaxDelete/', node.id, ");
            builder.AppendLine("                            function (msg) { ");
            builder.AppendLine("                                if (msg.Success) { ");
            builder.AppendLine("                                    $.messager.alert('提示', msg.Message, 'info', function () { ");
            builder.AppendLine("                                        //重新加载当前页 ");
            builder.AppendLine("                                        $('#grid_" + tm.TableName.ToLower() + "').datagrid('reload'); ");
            builder.AppendLine("                                    }); ");
            builder.AppendLine("                                } else { ");
            builder.AppendLine("                                    $.messager.alert('提示', msg.Message, 'info') ");
            builder.AppendLine("                                } ");
            builder.AppendLine("                            }, \"json\"); ");
            builder.AppendLine("                        } ");
            builder.AppendLine("                    }); ");
            builder.AppendLine("                } ");
            builder.AppendLine("                else { ");
            builder.AppendLine("                    alert('请选择'); ");
            builder.AppendLine("                } ");
            builder.AppendLine("                return false; ");
            builder.AppendLine("            } ");
            builder.AppendLine("        } ");
            builder.AppendLine("        $(document).ready(function () { ");
            builder.AppendLine("            LCL_" + tm.TableName.ToLower() + ".showTree(); ");
            builder.AppendLine("            $('#btnAdd" + tm.TableName.ToLower() + "').click(function () { LCL_" + tm.TableName.ToLower() + ".add(); }); ");
            builder.AppendLine("            $('#btnSave" + tm.TableName.ToLower() + "').click(function () { LCL_" + tm.TableName.ToLower() + ".save(); }); ");
            builder.AppendLine("            $('#btnDel" + tm.TableName.ToLower() + "').click(function () { LCL_" + tm.TableName.ToLower() + ".del(); }); ");
            builder.AppendLine("        }); ");

            return builder.ToString();
        }
        #endregion

        #region 生成数据表管理页面
        private string GenerateEasyUITable(TableModel tm)
        {
            var list = tm.Columns;
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("<div class=\"easyui-layout\" fit=\"true\" id=\"tb\"> ");
            builder.AppendLine("    <div data-options=\"region:'north'\" style=\"padding: 1px; height:70px;\"> ");
            builder.AppendLine("        <!-------------------------------搜索框-----------------------------------> ");
            builder.AppendLine("        <fieldset> ");
            builder.AppendLine("            <legend id=\"search_title\">信息查询</legend> ");
            builder.AppendLine("            <form id=\"ffSearch" + tm.TableName.ToLower() + "\" method=\"post\"> ");
            builder.AppendLine("                <div id=\"toolbar\" style=\"margin-bottom:5px\"> ");
            builder.AppendLine("                    <label for=\"Keyword\" id=\"search_key\">搜:</label> ");
            builder.AppendLine("                    <input type=\"text\" name=\"Keyword\" id=\"Keyword\" class=\"easyui-textbox\" style=\"width: 220px; height: 25px; text-align: center;\" placeholder=\"请输入\" /> ");
            builder.AppendLine("                    &nbsp; ");
            builder.AppendLine("                    <a id=\"btnSearch" + tm.TableName.ToLower() + "\" href=\"javascript:void(0)\" class=\"easyui-linkbutton\" iconcls=\"icon-search\">查询</a>&nbsp; ");
            builder.AppendLine("                </div> ");
            builder.AppendLine("            </form> ");
            builder.AppendLine("        </fieldset> ");
            builder.AppendLine("    </div> ");
            builder.AppendLine("    <div data-options=\"region:'center'\"> ");
            builder.AppendLine("        <!-------------------------------详细信息展示表格-----------------------------------> ");
            builder.AppendLine("        <table id=\"grid_" + tm.TableName.ToLower() + "\" title=\"" + tm.TableNameRemark + "\" toolbar=\"#tbar_" + tm.TableName.ToLower() + "\" data-options=\"iconCls:'icon-view'\" fit=\"true\"></table> ");
            builder.AppendLine("   <!--   弹出框       --> ");
            builder.AppendLine("        <div id=\"win_" + tm.TableName.ToLower() + "\"  style=\"padding: 5px;background: #fafafa;\" >  ");
            builder.AppendLine("            <div class=\"easyui-layout\" fit=\"true\"> ");
            builder.AppendLine("               <form id=\"ff" + tm.TableName.ToLower() + "\" method=\"post\" ajax=\"true\">");
            builder.AppendLine(GenerateEasyUIFrom(tm));
            builder.AppendLine("               </form>");
            builder.AppendLine("            </div> ");
            builder.AppendLine("        </div> ");
            builder.AppendLine("    </div> ");
            builder.AppendLine("</div> ");
            builder.AppendLine("@section scripts{ ");
            builder.AppendLine("  <script src=\"/Plugins/UIShell.RbacManagementPlugin/ViewScript/" + tm.TableName.ToLower() + ".cshtml.LanguageResource.js\" type=\"text/javascript\"></script> ");
            builder.AppendLine("  <script src=\"/Plugins/UIShell.RbacManagementPlugin/ViewScript/" + tm.TableName.ToLower() + ".cshtml.js\" type=\"text/javascript\"></script> ");
            builder.AppendLine("} ");
            return builder.ToString();
        }
        //.cshtml.LanguageResource.js
        private void GenerateEasyUILanguageResource(string path)
        {
            List<TableModel> tableNames = BLLFactory.Instance.idb.GetTableModelList(Utils.dbName, true);
            for (int i = 0; i < tableNames.Count; i++)
            {
                TableModel tm = tableNames[i];
                string tablename = tm.TableName;
                string tableInfo = tm.TableNameRemark;
                if (tablename == "__MigrationHistory" && tablename == "sysdiagrams") continue;

                StringBuilder builder = new StringBuilder();
                StringBuilder builder1 = new StringBuilder();
                StringBuilder builder2 = new StringBuilder();
                StringBuilder builder3 = new StringBuilder();
                foreach (var item in tm.Columns)
                {
                    builder1.AppendLine("    " + tm.TableName + "_Model_" + item.ColumnName + ": '" + item.ColumnRemark + "', ");
                    builder2.AppendLine("    " + tm.TableName + "_Model_" + item.ColumnName + ": '" + item.ColumnName + "', ");
                    builder3.AppendLine("    $('#ff_lab_" + tm.TableName.ToLower() + "_" + item.ColumnName.ToLower() + "').html($.LCLPageModel.Resource.PageLanguageResource." + tm.TableName + "_Model_" + item.ColumnName + "); ");
                }
                if (builder1.Length > 2)
                    builder1.Remove(builder1.Length - 1, 1);

                if (builder2.Length > 2)
                    builder2.Remove(builder2.Length - 1, 1);

                builder.AppendLine("$.LCLPageModel.Resource.InitLanguageResource = function () { ");
                builder.AppendLine("    // 资源初始化 ");
                builder.AppendLine("    $.LCLPageModel.Resource.PageLanguageResource = $.LCLPageModel.Resource.LanguageResourceInfo[pageAttr.LanguageId]; ");
                builder.AppendLine("$('#btnSavewfrout').html($.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save); ");
                builder.AppendLine("$('#btnCancelwfrout').html($.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel); ");
                builder.AppendLine("$('#btnSearchwfrout').html($.LCLPageModel.Resource.PageLanguageResource.Page_Command_Search); ");
                builder.AppendLine("$('#search_title').html($.LCLPageModel.Resource.PageLanguageResource.Page_label_Search_title); ");
                builder.AppendLine("$('#search_key').html($.LCLPageModel.Resource.PageLanguageResource.Page_label_Search_key); ");
                builder.AppendLine("$('#grid_wfrout').attr('title', $.LCLPageModel.Resource.PageLanguageResource.Page_title); ");
                builder.AppendLine(builder3.ToString());
                builder.AppendLine("} ");
                builder.AppendLine("//初始化页面中文资源 ");
                builder.AppendLine("$.LCLPageModel.Resource.LanguageResourceInfo['2052'] = { ");
                builder.AppendLine("    Page_title: '" + tableInfo + "', ");
                builder.AppendLine("    Page_Command_Grid_Operate: '操作', ");
                builder.AppendLine("    Page_Command_Add: '添加', ");
                builder.AppendLine("    Page_Command_Edit: '修改', ");
                builder.AppendLine("    Page_Command_Del: '删除', ");
                builder.AppendLine("    Page_Command_Save: '保存', ");
                builder.AppendLine("    Page_Command_Cancel: '取消', ");
                builder.AppendLine("    Page_Command_Search: '查询', ");
                builder.AppendLine("    Page_label_Search_title: '信息查询', ");
                builder.AppendLine("    Page_label_Search_key: '搜', ");
                builder.AppendLine("    LCLMessageBox_AlertTitle: '温馨提示', ");
                builder.AppendLine("    LCLMessageBox_Message1: '请选择一行', ");
                builder.AppendLine("    LCLMessageBox_Message2: '请先勾选要删除的数据', ");
                builder.AppendLine("    LCLMessageBox_Message3: '是否删除选中数据', ");

                builder.AppendLine(builder1.ToString());
                builder.AppendLine("}; ");
                builder.AppendLine("//初始化页面英文资源 ");
                builder.AppendLine("$.LCLPageModel.Resource.LanguageResourceInfo['1033'] = { ");
                builder.AppendLine("    Page_title: '" + tablename + "', ");
                builder.AppendLine("    Page_Command_Grid_Operate: 'Operate', ");
                builder.AppendLine("    Page_Command_Add: 'Add', ");
                builder.AppendLine("    Page_Command_Edit: 'Edit', ");
                builder.AppendLine("    Page_Command_Del: 'Delete', ");
                builder.AppendLine("    Page_Command_Save: 'Save', ");
                builder.AppendLine("    Page_Command_Cancel: 'Cancel', ");
                builder.AppendLine("    Page_Command_Search: 'Search', ");
                builder.AppendLine("    Page_label_Search_title: 'info query', ");
                builder.AppendLine("    Page_label_Search_key: 'key', ");
                builder.AppendLine("    LCLMessageBox_AlertTitle: 'AlertTitle', ");
                builder.AppendLine("    LCLMessageBox_Message1: 'Please select row', ");
                builder.AppendLine("    LCLMessageBox_Message2: 'Please delete data', ");
                builder.AppendLine("    LCLMessageBox_Message3: 'is delete data', ");
                builder.AppendLine(builder2.ToString());
                builder.AppendLine("}; ");

                if (path.Length > 0)
                {
                    string folder = path + @"\LCL\EasyUI\ViewScript\";
                    Utils.FolderCheck(folder);
                    string filename = folder + tablename.ToLower() + ".cshtml.LanguageResource.js";
                    Utils.CreateFiles(filename, builder.ToString());
                }
            }
        }
        //.cshtml.js
        private void GenerateEasyUITableJs(string path)
        {
            List<TableModel> tableNames = BLLFactory.Instance.idb.GetTableModelList(Utils.dbName, true);
            for (int i = 0; i < tableNames.Count; i++)
            {
                TableModel tm = tableNames[i];
                string tablename = tm.TableName;
                string tableInfo = tm.TableNameRemark;
                if (tablename == "__MigrationHistory" && tablename == "sysdiagrams") continue;

                StringBuilder builder = new StringBuilder();
                builder.AppendLine("/// <reference path=\"/Content/Code/LCL.JQuery.Base.js\" /> ");
                builder.AppendLine("/// <reference path=\"/Content/Core/LCL.JQuery.Core.js\" /> ");
                builder.AppendLine("/// <reference path=\"/Content/Core/LCL.JQuery.JSON.js\" /> ");
                builder.AppendLine("/// <reference path=\"/Content/Core/LCL.JQuery.Plugins.js\" /> ");
                builder.AppendLine(" ");
                builder.AppendLine("//页面属性PageAttr （该行不允许删除） ");
                builder.AppendLine("var pageAttr = { ");
                builder.AppendLine("    SiteRoot: '', ");
                builder.AppendLine("    LanguageId: '2052', ");
                builder.AppendLine("    JsonServerURL: '', ");
                builder.AppendLine("    Added: true, ");
                builder.AppendLine("    toolbar: '' ");
                builder.AppendLine("}; ");
                builder.AppendLine("//页面入口 ");
                builder.AppendLine("$(document).ready(function () { ");
                builder.AppendLine("    //debugger; ");
                builder.AppendLine("    InitAttribute(); ");
                builder.AppendLine("    InitLanguage(); ");
                builder.AppendLine("    InitControls(); ");
                builder.AppendLine("    InitEvent(); ");
                builder.AppendLine("}); ");
                builder.AppendLine("//初始化页面属性 ");
                builder.AppendLine("function InitAttribute() { ");
                builder.AppendLine("    pageAttr.SiteRoot = $.LCLBase.SiteConfig.GetSiteRoot(); ");
                builder.AppendLine("    pageAttr.LanguageId = $.LCLBase.SiteConfig.GetCurrLanguageID(); ");
                builder.AppendLine("    pageAttr.JsonServerURL = pageAttr.SiteRoot + 'UIShell.RbacManagementPlugin/'; ");
                builder.AppendLine("} ");
                builder.AppendLine("//初始化多语言 ");
                builder.AppendLine("function InitLanguage() { ");
                builder.AppendLine("    $.LCLPageModel.Resource.InitLanguage(); ");
                builder.AppendLine("} ");
                builder.AppendLine("//初始化控件 ");
                builder.AppendLine("function InitControls() { ");
                builder.AppendLine("    InitGrid(); ");
                builder.AppendLine("} ");
                builder.AppendLine("//初始化事件 ");
                builder.AppendLine("function InitEvent() { ");
                builder.AppendLine("    $('#btnAdd" + tm.TableName.ToLower() + "').click(function () { pageFunc_" + tm.TableName.ToLower() + "Add(); }); ");
                builder.AppendLine("    $('#btnDel" + tm.TableName.ToLower() + "').click(function () { pageFunc_" + tm.TableName.ToLower() + "Del(); }); ");
                builder.AppendLine("    $('#btnSearch" + tm.TableName.ToLower() + "').click(function () { pageFunc_SearchData" + tm.TableName.ToLower() + "(); }); ");
                builder.AppendLine("} ");
                builder.AppendLine("function InitGrid() { ");
                builder.AppendLine("    $('#grid_" + tm.TableName.ToLower() + "').datagrid({ ");
                builder.AppendLine("        url: pageAttr.JsonServerURL + '" + tm.TableName + "/AjaxGetByPage', ");
                builder.AppendLine("        iconCls: 'icon-edit', ");
                builder.AppendLine("        pagination: true, ");
                builder.AppendLine("        rownumbers: true, ");
                builder.AppendLine("        fitCloumns: true, ");
                builder.AppendLine("        idField: \"ID\", ");
                builder.AppendLine("        frozenColumns: [[ ");
                builder.AppendLine("          { field: 'ck', checkbox: true } ");
                builder.AppendLine("        ]], ");
                builder.AppendLine("        hideColumn: [[ ");
                builder.AppendLine("           { title: 'ID', field: 'ID' } ");
                builder.AppendLine("        ]], ");
                builder.AppendLine("        columns: [[ ");

                foreach (var item in tm.Columns)
                {
                    if (cols.Contains(item.ColumnName))
                    {
                        builder.AppendLine("                { field: '" + item.ColumnName + "', title: $.LCLPageModel.Resource.PageLanguageResource." + tm.TableName + "_Model_" + item.ColumnName + ", width: 100,hidden:true }, ");
                    }
                    else
                    {
                        builder.AppendLine("                { field: '" + item.ColumnName + "', title: $.LCLPageModel.Resource.PageLanguageResource." + tm.TableName + "_Model_" + item.ColumnName + ", width: 100 }, ");
                        if (item.ColumnType.ToLower() == "bool")
                        {
                            builder.AppendLine("                { ");
                            builder.AppendLine("                    field: '" + item.ColumnName + "', title: $.LCLPageModel.Resource.PageLanguageResource." + tm.TableName + "_Model_" + item.ColumnName + ", width: 100, formatter: function (value, row, index) { ");
                            builder.AppendLine("                        return value ? '<div class=\"icon-true\" style=\"width:16px; height:16px;\" >&nbsp;&nbsp;</div>' : ");
                            builder.AppendLine("                                       '<div class=\"icon-false\" style=\"width:16px; height:16px;\">&nbsp;&nbsp;</div>'; ");
                            builder.AppendLine("                    } ");
                            builder.AppendLine("                }, ");
                        }
                    }
                }
                builder.AppendLine("                { ");
                builder.AppendLine("                    field: 'opt', title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Grid_Operate, width: 120, align: 'center', ");
                builder.AppendLine("                    formatter: function (value, rec, index) { ");
                builder.AppendLine("                        return '&nbsp;<a href=\"javascript:void(0)\" onclick=\"pageFunc_" + tm.TableName.ToLower() + "Edit(\\'' + rec.ID + '\\')\">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Edit + '</a>&nbsp;' ");
                builder.AppendLine("                             + '&nbsp;<a href=\"javascript:void(0)\" onclick=\"pageFunc_" + tm.TableName.ToLower() + "Del()\">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Del + '</a>&nbsp;'; ");
                builder.AppendLine("                    } ");
                builder.AppendLine("                } ");
                builder.AppendLine("        ]], ");
                builder.AppendLine("        toolbar: grid_" + tm.TableName.ToLower() + "_toolbar(), ");
                builder.AppendLine("        onDblClickRow: function (rowIndex, rowData) { ");
                builder.AppendLine("            $('#grid_wfrout').datagrid('clearSelections').datagrid('clearChecked').datagrid('checkRow', rowIndex); ");
                builder.AppendLine("            pageFunc_" + tm.TableName.ToLower() + "Edit(rowData.ID); ");
                builder.AppendLine("        } ");
                builder.AppendLine("    }); ");
                builder.AppendLine("} ");
                builder.AppendLine("function pageFunc_SearchData" + tm.TableName.ToLower() + "() { ");
                builder.AppendLine("    $(\"#grid_" + tm.TableName.ToLower() + "\").datagrid('load', { ");
                builder.AppendLine("        Name: $('#ui_wfrout_search').find('[name=Keyword]').val() ");
                builder.AppendLine("    }); ");
                builder.AppendLine("    clearSelect('grid_" + tm.TableName.ToLower() + "'); ");
                builder.AppendLine("} ");
                builder.AppendLine("function pageFunc_" + tm.TableName.ToLower() + "Add() { ");
                builder.AppendLine("    pageAttr.Added = true; ");
                builder.AppendLine("    $('#ff" + tm.TableName.ToLower() + "').form('clear'); ");
                builder.AppendLine("    $('#win_" + tm.TableName.ToLower() + "').dialog({ ");
                builder.AppendLine("        title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Add, ");
                builder.AppendLine("        width: 500, ");
                builder.AppendLine("        height: 280, ");
                builder.AppendLine("        iconCls: 'icon-add', ");
                builder.AppendLine("        modal: true, ");
                builder.AppendLine("        buttons: [{ ");
                builder.AppendLine("            id: 'btnSave" + tm.TableName.ToLower() + "', ");
                builder.AppendLine("            iconCls: 'icon-ok', ");
                builder.AppendLine("            text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save, ");
                builder.AppendLine("            handler: function () { ");
                builder.AppendLine("                pageFunc_" + tm.TableName.ToLower() + "Save(); ");
                builder.AppendLine("            } ");
                builder.AppendLine("        }, { ");
                builder.AppendLine("            id: 'btnCancelwfrout', ");
                builder.AppendLine("            iconCls: 'icon-cancel', ");
                builder.AppendLine("            text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel, ");
                builder.AppendLine("            handler: function () { ");
                builder.AppendLine("                closeDialog('win_" + tm.TableName.ToLower() + "'); ");
                builder.AppendLine("            } ");
                builder.AppendLine("        }], ");
                builder.AppendLine("        onLoad: function () { ");
                builder.AppendLine("            $('#" + tm.TableName.ToLower() + "_Entity_Name').focus(); ");
                builder.AppendLine("        } ");
                builder.AppendLine("    }); ");
                builder.AppendLine("} ");
                builder.AppendLine("function pageFunc_" + tm.TableName.ToLower() + "Edit(ID) { ");
                builder.AppendLine("    if (ID != undefined && ID.length > 0) { ");
                builder.AppendLine("        pageAttr.Added = false; ");
                builder.AppendLine("        $('#win_" + tm.TableName.ToLower() + "').dialog({ ");
                builder.AppendLine("            title: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Edit, ");
                builder.AppendLine("            width: 550, ");
                builder.AppendLine("            height: 280, ");
                builder.AppendLine("            iconCls: 'icon-edit', ");
                builder.AppendLine("            modal: true, ");
                builder.AppendLine("            buttons: [{ ");
                builder.AppendLine("                id: \"btnSave" + tm.TableName.ToLower() + "\", ");
                builder.AppendLine("                iconCls: 'icon-ok', ");
                builder.AppendLine("                text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Save, ");
                builder.AppendLine("                handler: function () { ");
                builder.AppendLine("                    pageFunc_" + tm.TableName.ToLower() + "Save(); ");
                builder.AppendLine("                } ");
                builder.AppendLine("            }, { ");
                builder.AppendLine("                id: 'btnCancel" + tm.TableName.ToLower() + "', ");
                builder.AppendLine("                iconCls: 'icon-cancel', ");
                builder.AppendLine("                text: $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Cancel, ");
                builder.AppendLine("                handler: function () { ");
                builder.AppendLine("                    closeDialog('win_" + tm.TableName.ToLower() + "'); ");
                builder.AppendLine("                } ");
                builder.AppendLine("            }], ");
                builder.AppendLine("            onClose: function () { ");
                builder.AppendLine("                closeDialog('win_" + tm.TableName.ToLower() + "'); ");
                builder.AppendLine("            } ");
                builder.AppendLine("        }); ");
                builder.AppendLine("        var ajaxUrl = pageAttr.JsonServerURL + '" + tm.TableName.ToLower() + "/AjaxGetByModel?id=' + $.LCLCore.ValidUI.Trim(ID); ");
                builder.AppendLine("        $.post(ajaxUrl, function (resultData) { ");
                builder.AppendLine("            if (resultData.Success) { ");

                foreach (var item in tm.Columns)
                {
                    builder.AppendLine("                $('#" + tm.TableName.ToLower() + "_Entity_" + item.ColumnName + "').val(resultData.DataObject." + item.ColumnName + "); ");
                }

                builder.AppendLine("            } ");
                builder.AppendLine("        }, 'json'); ");
                builder.AppendLine(" ");
                builder.AppendLine("    } else { ");
                builder.AppendLine("        $.LCLMessageBox.Alert($.LCLPageModel.Resource.PageLanguageResource.LCLMessageBox_Message1); ");
                builder.AppendLine("    } ");
                builder.AppendLine("} ");
                builder.AppendLine("function pageFunc_" + tm.TableName.ToLower() + "Save() { ");
                builder.AppendLine("    var ajaxUrl = \"\"; ");
                builder.AppendLine("    if (pageAttr.Added) { ");
                builder.AppendLine("        ajaxUrl = pageAttr.JsonServerURL + '" + tm.TableName + "/AjaxAdd'; ");
                builder.AppendLine("    } else { ");
                builder.AppendLine("        ajaxUrl = pageAttr.JsonServerURL + '" + tm.TableName + "/AjaxEdit'; ");
                builder.AppendLine("    } ");
                builder.AppendLine(" ");
                builder.AppendLine("    $('#ff" + tm.TableName.ToLower() + "').form('submit', { ");
                builder.AppendLine("        url: ajaxUrl, ");
                builder.AppendLine("        onSubmit: function (param) { ");
                builder.AppendLine("            $('#btnSave" + tm.TableName.ToLower() + "').linkbutton('disable'); ");
                foreach (var item in tm.Columns)
                {
                    builder.AppendLine("            param." + item.ColumnName + " = $('#" + tm.TableName.ToLower() + "_Entity_" + item.ColumnName + "').val(); ");
                }
                builder.AppendLine("            if ($(this).form('validate')) ");
                builder.AppendLine("                return true; ");
                builder.AppendLine("            else { ");
                builder.AppendLine("                $('#btnSave" + tm.TableName.ToLower() + "').linkbutton('enable'); ");
                builder.AppendLine("                return false; ");
                builder.AppendLine("            } ");
                builder.AppendLine("        }, ");
                builder.AppendLine("        success: function (data) { ");
                builder.AppendLine("            var resultData = eval('(' + data + ')'); ");
                builder.AppendLine("            if (resultData.Success) { ");
                builder.AppendLine("                flashTable('grid_" + tm.TableName.ToLower() + "'); ");
                builder.AppendLine("                if (pageAttr.Added) { ");
                builder.AppendLine(" ");
                builder.AppendLine("                } else { ");
                builder.AppendLine("                    closeDialog('win_" + tm.TableName.ToLower() + "'); ");
                builder.AppendLine("                } ");
                builder.AppendLine("            } ");
                builder.AppendLine("            $('#btnSave" + tm.TableName.ToLower() + "').linkbutton('enable'); ");
                builder.AppendLine("            $.LCLMessageBox.Alert(resultData.Message); ");
                builder.AppendLine("        } ");
                builder.AppendLine("    }); ");
                builder.AppendLine("} ");
                builder.AppendLine("function pageFunc_" + tm.TableName.ToLower() + "Del() { ");
                builder.AppendLine("    var rows = $(\"#grid_" + tm.TableName.ToLower() + "\").datagrid(\"getChecked\"); ");
                builder.AppendLine("    if (rows.length < 1) { ");
                builder.AppendLine("        $.LCLMessageBox.Alert($.LCLPageModel.Resource.PageLanguageResource.LCLMessageBox_Message2); ");
                builder.AppendLine("        return; ");
                builder.AppendLine("    } ");
                builder.AppendLine("    var parm; ");
                builder.AppendLine("    $.each(rows, function (i, row) { ");
                builder.AppendLine("        if (i == 0) { ");
                builder.AppendLine("            parm = \"idList=\" + row.ID; ");
                builder.AppendLine("        } else { ");
                builder.AppendLine("            parm += \"&idList=\" + row.ID; ");
                builder.AppendLine("        } ");
                builder.AppendLine("    }); ");
                builder.AppendLine("    $.LCLMessageBox.Confirm($.LCLPageModel.Resource.PageLanguageResource.LCLMessageBox_Message3, function (r) { ");
                builder.AppendLine("        if (r) { ");
                builder.AppendLine("            $.post(pageAttr.JsonServerURL + '" + tm.TableName + "/AjaxDeleteList/', parm, ");
                builder.AppendLine("            function (resultData) { ");
                builder.AppendLine("                if (resultData.Success) { ");
                builder.AppendLine("                    $.LCLMessageBox.Alert(resultData.Message,function () { ");
                builder.AppendLine("                        InitGrid(); ");
                builder.AppendLine("                    }); ");
                builder.AppendLine("                } else { ");
                builder.AppendLine("                    $.LCLMessageBox.Alert(resultData.Message); ");
                builder.AppendLine("                } ");
                builder.AppendLine("            }, \"json\"); ");
                builder.AppendLine("        } ");
                builder.AppendLine("    }); ");
                builder.AppendLine("} ");
                builder.AppendLine("function grid_" + tm.TableName.ToLower() + "_toolbar() { ");
                builder.AppendLine("    var ihtml = '<div id=\"tbar_" + tm.TableName.ToLower() + "\">' ");
                builder.AppendLine("        + '<a id=\"btnAdd" + tm.TableName.ToLower() + "\" href=\"javascript:;\" plain=\"true\" class=\"easyui-linkbutton\" icon=\"icon-add\">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Add + '</a>&nbsp;' ");
                builder.AppendLine("        + '<a id=\"btnDel" + tm.TableName.ToLower() + "\" href=\"javascript:;\" plain=\"true\" class=\"easyui-linkbutton\" icon=\"icon-remove\">' + $.LCLPageModel.Resource.PageLanguageResource.Page_Command_Del + '</a>&nbsp;' ");
                builder.AppendLine("        + '<a href=\"javascript:void(0)\" /></div>' ");
                builder.AppendLine("    return ihtml; ");
                builder.AppendLine("} ");

                if (path.Length > 0)
                {
                    string folder = path + @"\LCL\EasyUI\ViewScript\";
                    Utils.FolderCheck(folder);
                    string filename = folder + tablename.ToLower() + ".cshtml.js";
                    Utils.CreateFiles(filename, builder.ToString());
                }
            }
        }
        #endregion

        #region 生成MVC控制器
        private void GenerateControllers(string path)
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
                builder.AppendLine("namespace " + Utils.NameSpaceUI + ".Controllers ");
                builder.AppendLine("{ ");
                builder.AppendLine("    public class " + tablename + "Controller : RbacController<" + tablename + "> ");
                builder.AppendLine("    { ");
                builder.AppendLine("        public " + tablename + "Controller() ");
                builder.AppendLine("        { ");
                builder.AppendLine("        } ");
                builder.AppendLine("    } ");
                builder.AppendLine("} ");

                if (path.Length > 0)
                {
                    string folder = path + @"\LCL\EasyUI\Controllers\";
                    Utils.FolderCheck(folder);
                    string filename = folder + @"\" + tablename + "Controller.cs";
                    Utils.CreateFiles(filename, builder.ToString());
                }
            }

        }
        #endregion

        #region 生成表单
        private string GenerateEasyUIFrom(TableModel tm)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("<table class=\"tb_searchbar\"> ");
            builder.AppendLine(" <tbody> ");
            var list = tm.Columns;
            StringBuilder crto = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                string isnull = "", validatebox = "";
                if (item.Isnullable && item.MaxLength > 0)
                {
                    isnull = "<span style=\"color:red;\">*</span>";
                    validatebox = " data-options=\"required:true\" class=\"easyui-validatebox\" ";
                }

                if (cols.Contains(item.ColumnName))
                {
                    crto.AppendLine("<input type='hidden' id='" + tm.TableName.ToLower() + "_Entity_" + item.ColumnName + "' name='" + tm.TableName.ToLower() + "_Entity" + item.ColumnName + "' value='' />");
                }
                else
                {
                    if (item.ColumnName.Contains("_"))
                    {
                        builder.AppendLine("                <tr> ");
                        builder.AppendLine("                    <td class=\"td_title\"><label style=\"color: black\" id=\"ff_lab_" + tm.TableName.ToLower() + "_" + item.ColumnName.ToLower() + "\">" + item.ColumnRemark + "" + isnull + "</label></td> ");
                        builder.AppendLine("                    <td> ");
                        builder.AppendLine("<select id=\"" + tm.TableName.ToLower() + "_Entity_" + item.ColumnName + "\" class='easyui-combotree' data-options='url:\"AjaxEasyUITree\"' style='width: 120px; height: 28px;' tabindex=\"" + i + "\" " + validatebox + "></select>");
                        builder.AppendLine("                    </td> ");
                        builder.AppendLine("                </tr> ");
                    }
                    else
                    {
                        switch (item.ColumnType.ToLower())
                        {
                            case "bool":
                                builder.AppendLine("                <tr> ");
                                builder.AppendLine("                    <td class=\"td_title\"><label style=\"color: black\" id=\"ff_lab_" + tm.TableName.ToLower() + "_" + item.ColumnName.ToLower() + "\">" + item.ColumnRemark + "" + isnull + "</label></td> ");
                                builder.AppendLine("                    <td> ");
                                builder.AppendLine("                        <input type=\"radio\" id=\"" + tm.TableName.ToLower() + "_Entity_" + item.ColumnName + "1\" name=\"" + tm.TableName.ToLower() + "_Entity_" + item.ColumnName + "\" value=\"true\" checked tabindex=\"" + i + "\" />是");
                                builder.AppendLine("                        <input type=\"radio\" id=\"" + tm.TableName.ToLower() + "_Entity_" + item.ColumnName + "2\" name=\"" + tm.TableName.ToLower() + "_Entity_" + item.ColumnName + "\" value=\"false\" tabindex=\"" + i + "\" />否");
                                builder.AppendLine("                    </td> ");
                                builder.AppendLine("                </tr> ");
                                break;
                            default:
                                builder.AppendLine("                <tr> ");
                                builder.AppendLine("                    <td class=\"td_title\"><label style=\"color: black\" id=\"ff_lab_" + tm.TableName.ToLower() + "_" + item.ColumnName.ToLower() + "\">" + item.ColumnRemark + "" + isnull + "</label></td> ");
                                builder.AppendLine("                    <td> ");
                                builder.AppendLine("                        <input type=\"text\" id=\"" + tm.TableName.ToLower() + "_Entity_" + item.ColumnName + "\" name=\"" + tm.TableName.ToLower() + "_Entity" + item.ColumnName + "\" value=\"  \" style=\"width:310px\" " + validatebox + " maxlength=\"" + item.MaxLength + "\" tabindex=\"" + i + "\" /> ");
                                builder.AppendLine("                    </td> ");
                                builder.AppendLine("                </tr> ");
                                break;
                        }
                    }
                }
            }
            builder.AppendLine("<tr><td  colspan='2'>" + crto.ToString() + "<td></tr>");
            builder.AppendLine(" </tbody> ");
            builder.AppendLine("</table> ");
            return builder.ToString();
        }
        #endregion
    }
}
