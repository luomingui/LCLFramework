using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LCL.Tools.WinFrm
{
    public partial class FrmDbaDoc : Form
    {
        public FrmDbaDoc()
        {
            InitializeComponent();

            BLLFactory.Instance.BindDataBaseList(this.cmbDBlist);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cmbDBlist.SelectedItem == null) return;
            string dbName = cmbDBlist.SelectedItem.ToString();
            button1.Enabled = false;
            if (radioButton1.Checked)
            {

            }
            else if (radioButton2.Checked)
            {
                DataTable table = BLLFactory.Instance.idb.GetTablesColumnsList("", dbName);
                MyXlsHelper.LeadOutToExcel(table);
            }
            else if (radioButton3.Checked)
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Title = "保存";
                save.Filter = "HTML(*.htm)|*.htm|所有文件(*.*)|*.*";
                save.RestoreDirectory = true;
                if (save.ShowDialog() == DialogResult.OK)
                {
                    HtmlTable(save.FileName, dbName);
                    MessageBox.Show("导出成功！");
                }
            }
            else if (radioButton4.Checked)
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Title = "保存";
                save.Filter = "HTML(*.htm)|*.htm|所有文件(*.*)|*.*";
                save.RestoreDirectory = true;
                if (save.ShowDialog() == DialogResult.OK)
                {
                    HtmlTable4(save.FileName, dbName);
                    MessageBox.Show("导出成功！");
                }
            }
            button1.Enabled = true;
        }


        public void HtmlTable(string fileListPath, string dbName)
        {
            List<TableModel> list = DALFactory.Factory().GetTableModelList(dbName);
            StringBuilder sbuder = new StringBuilder();
            foreach (var item in list)
            {
                DataTable table = BLLFactory.Instance.idb.GetTablesColumnsList(item.TableName, dbName);
                int columns = table.Columns.Count;
                string context = ConvertDatatableToHtml(table);
                sbuder.Append(context);
            }
            File.AppendAllText(fileListPath, sbuder.ToString(), Encoding.UTF8);

        }
        public string ConvertDatatableToHtml(DataTable filedt)
        {
            string desc = "";
            if (filedt != null && filedt.Rows.Count != 0)
            {
                for (int j = 0; j < filedt.Rows.Count; j++)
                {
                    if (j == 0)
                    {
                        desc += "<table border=1 cellspacing=0 cellpadding=0 style='width:100%;border-collapse:collapse;border:none;table-layout:fixed;empty-cells:show;margin:0 auto;'>";
                        for (int k = 0; k < filedt.Columns.Count; k++)
                        {
                            if (k == 0)
                            {
                                desc += "<tr>";
                            }
                            desc += "    <td style='background-color:#ccddff;border:.5pt solid windowtext;border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;'>";
                            desc += "        <p class=MsoNormal align=center style='text-align:left'>&nbsp;  " + filedt.Columns[k].ColumnName.ToString() + "</p></td>";
                            if (k == filedt.Columns.Count - 1)
                            {
                                desc += "</tr>";
                            }
                        }
                        for (int k = 0; k < filedt.Columns.Count; k++)
                        {
                            if (k == 0)
                            {
                                desc += "<tr>";
                            }
                            desc += "    <td style='border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;'>";
                            desc += "        <p class=MsoNormal align=center style='text-align:left'>&nbsp;  " + filedt.Rows[j][k].ToString() + "</p></td>";
                            if (k == filedt.Columns.Count - 1)
                            {
                                desc += "</tr>";
                            }
                        }
                    }
                    else
                    {
                        for (int k = 0; k < filedt.Columns.Count; k++)
                        {
                            if (k == 0)
                            {
                                desc += "<tr>";
                            }
                            desc += "    <td style='border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;'>";
                            desc += "        <p class=MsoNormal align=center style='text-align:left'>&nbsp;  " + filedt.Rows[j][k].ToString() + "</p></td>";
                            if (k == filedt.Columns.Count - 1)
                            {
                                desc += "</tr>";
                            }
                        }
                    }
                }
                desc += "<tr><td width=580 colspan=" + filedt.Columns.Count.ToString() + " valign=top align='left' style='border:solid windowtext 1.0pt; padding:0cm 5.4pt 0cm 5.4pt'>";
                desc += "        <p class=MsoNormal align=left style='text-align:left'>Total Qty: " + filedt.Rows.Count + "</p></td></tr>";
                desc += "</table>";
            }
            return desc;
        }

        public void HtmlTable4(string fileListPath, string dbName)
        {
            List<TableModel> tableNames = BLLFactory.Instance.idb.GetTableModelList(dbName, true);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < tableNames.Count; i++)
            {
                TableModel tm = tableNames[i];
                string tablename = tm.TableName;
                string tableInfo = tm.TableNameRemark;
               
                builder.AppendLine("    <table class=\"tb_titlebar\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"> ");
                builder.AppendLine("        <tbody> ");
                builder.AppendLine("            <tr> ");
                builder.AppendLine("                <td valign=\"center\" nowrap align=\"left\" width=\"100%\"> ");
                builder.AppendLine("                    <img onclick=\"ShowTable(this);\" id=\"Img" + i + "\" src=\"icon_collapseall.gif\" class=\"icon\" ");
                builder.AppendLine("                        alt=\"表名\" width=\"16\" height=\"16\">" + tablename + "(" + tableInfo + ") ");
                builder.AppendLine("                </td> ");
                builder.AppendLine("            </tr> ");
                builder.AppendLine("            <tr> ");
                builder.AppendLine("                <td align=\"left\"> ");
                builder.AppendLine("                    <table cellspacing=\"1\" class=\"tb_datalist\" id=\"Table" + i + "\" style=\"display: one; width: 100%\"> ");
                builder.AppendLine("                        <tbody> ");
                builder.AppendLine("                            <tr class=\"tr_head\"> ");
                builder.AppendLine("                                <td  style=\"width: 12%\">标识</td> ");
                builder.AppendLine("                                <td style=\"width: 12%\">字段名称</td> ");
                builder.AppendLine("                                <td  style=\"width: 12%\">字段类型</td> ");
                builder.AppendLine("                                <td  style=\"width: 12%\">长度</td> ");
                builder.AppendLine("                                <td  style=\"width: 12%\">允许空</td> ");
                builder.AppendLine("                                <td  style=\"width: 80%\">字段描述</td> ");
                builder.AppendLine("                            </tr> ");

                for (int j = 0; j < tm.Columns.Count; j++)
                {
                    var col = tm.Columns[j];
                    string trclass = "";
                    if (j % 2 == 0)
                    {
                        trclass = " class=\"even\" ";
                    }
                    builder.AppendLine("                            <tr" + trclass + "> ");
                    builder.AppendLine("                                <td>" + col.PK + "</td> ");
                    builder.AppendLine("                                <td>" + col.ColumnName + "</td> ");
                    builder.AppendLine("                                <td>" + col.ColumnType + "</td> ");
                    builder.AppendLine("                                <td>" + col.MaxLength + "</td> ");
                    builder.AppendLine("                                <td>" + col.Isnullable + "</td> ");
                    builder.AppendLine("                                <td>" + col.ColumnRemark + "</td> ");
                    builder.AppendLine("                            </tr> ");
                }
                builder.AppendLine("                        </tbody> ");
                builder.AppendLine("                    </table> ");
                builder.AppendLine("                </td> ");
                builder.AppendLine("            </tr> ");
                builder.AppendLine("        </tbody> ");
                builder.AppendLine("    </table> ");
            }
            File.AppendAllText(fileListPath, HtmlTop(builder.ToString()), Encoding.UTF8);
        }

        public static string HtmlTop(string htmltables)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("<!DOCTYPE html> ");
            builder.AppendLine("<html> ");
            builder.AppendLine("<head> ");
            builder.AppendLine("    <meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /> ");
            builder.AppendLine("    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=7\" /> ");
            builder.AppendLine("    <title>数据字典</title> ");
            builder.AppendLine("    <style type=\"text/css\"> ");
            builder.AppendLine("        body {margin-left: 0px; margin-top: 0px;margin-right: 0px;margin-bottom: 0px;padding: 0px; }  ");
            builder.AppendLine("        BODY { ");
            builder.AppendLine("	        PADDING-RIGHT: 0px; PADDING-LEFT: 0px; FONT-SIZE: 12px; BACKGROUND: white; PADDING-BOTTOM: 0px; MARGIN: 0px;  ");
            builder.AppendLine("	        PADDING-TOP: 0px; FONT-FAMILY: \"Verdana\",\"Arial\",\"Helvetica\",\"sans-serif\"; COLOR: #111111 ");
            builder.AppendLine("        } ");
            builder.AppendLine("      .tb_datalist { ");
            builder.AppendLine("	        BORDER-RIGHT: silver 1px solid; BACKGROUND: #C6D5F5; BORDER-LEFT: silver 1px solid; WIDTH: 100%; BORDER-BOTTOM: silver 1px solid ");
            builder.AppendLine("        } ");
            builder.AppendLine("        .tb_datalist .select { ");
            builder.AppendLine("	        MARGIN-TOP: 19px! important; TEXT-ALIGN: left; ");
            builder.AppendLine("        } ");
            builder.AppendLine("        .tb_datalist TD { ");
            builder.AppendLine("	        PADDING-RIGHT: 1px; PADDING-LEFT: 4px; BACKGROUND: white; PADDING-BOTTOM: 1px; PADDING-TOP: 3px ");
            builder.AppendLine("        } ");
            builder.AppendLine("        .tb_datalist TH { ");
            builder.AppendLine("	        BORDER-TOP: white 1px solid; BACKGROUND: #B9D0FE; BORDER-LEFT: white 1px solid ");
            builder.AppendLine("        } ");
            builder.AppendLine("        .tb_datalist .tr_head TD { ");
            builder.AppendLine("	        BORDER-TOP: white 1px solid; BACKGROUND: #EAF0FB; BORDER-LEFT: white 1px solid ; height:25PX ; ");
            builder.AppendLine("        } ");
            builder.AppendLine("        .tb_datalist .tr_head { ");
            builder.AppendLine("	        BORDER-TOP: white 1px solid; BACKGROUND: #EAF0FB; BORDER-LEFT: white 1px solid ; height:25PX ; ");
            builder.AppendLine("        } ");
            builder.AppendLine("        .tb_datalist .tr_title TD { ");
            builder.AppendLine("	        BORDER-TOP: white 1px solid; BACKGROUND: #D2E1FD; BORDER-LEFT: white 1px solid ; ");
            builder.AppendLine("        } ");
            builder.AppendLine("        .tb_datalist .tr_title { ");
            builder.AppendLine("	        BORDER-TOP: white 1px solid; BACKGROUND: #D2E1FD; BORDER-LEFT: white 1px solid ");
            builder.AppendLine("        } ");
            builder.AppendLine("        .tb_datalist .tr_detail TD { ");
            builder.AppendLine("	        BORDER-TOP: white 1px solid; BACKGROUND: #D2E1FD; BORDER-LEFT: white 1px solid ; ");
            builder.AppendLine("        } ");
            builder.AppendLine("        .tb_datalist .tr_detail { ");
            builder.AppendLine("	        BORDER-TOP: white 1px solid; BACKGROUND: #D2E1FD; BORDER-LEFT: white 1px solid ");
            builder.AppendLine("        } ");
            builder.AppendLine("        .tb_datalist .tr_pagenumber { ");
            builder.AppendLine("	        BACKGROUND: #f5f5f5 no-repeat 50% bottom; FLOAT: right; COLOR: #5e5e5e; WIDTH: 100%;  ");
            builder.AppendLine("	        BORDER-top: silver 1px solid ; BORDER-left: silver 1px solid; BORDER-right: buttonshadow 1px solid; VERTICAL-ALIGN: bottom; TEXT-ALIGN: right; } ");
            builder.AppendLine("        .tb_datalist .tr_pagenumber  TD { ");
            builder.AppendLine("	        BORDER-TOP: white 1px solid; BACKGROUND: #E8E8E8; BORDER-LEFT: white 1px solid ; BORDER-right: white 1px solid ");
            builder.AppendLine("        } ");
            builder.AppendLine("        .tb_datalist .tr_odd { ");
            builder.AppendLine("        } ");
            builder.AppendLine("        .tb_datalist .tr_odd  TD { ");
            builder.AppendLine("        } ");
            builder.AppendLine("        .tb_datalist .tr_even { ");
            builder.AppendLine("	        BACKGROUND-COLOR: #F7F9FE ");
            builder.AppendLine("        } ");
            builder.AppendLine("        .tb_datalist .tr_even  TD { ");
            builder.AppendLine("	        BACKGROUND-COLOR: #F7F9FE ");
            builder.AppendLine("        } ");
            builder.AppendLine("        .tb_datalist .tr_selected { ");
            builder.AppendLine("	        BACKGROUND-COLOR: #FFFFD0 ");
            builder.AppendLine("        } ");
            builder.AppendLine("        .tb_datalist .tr_selected  TD { ");
            builder.AppendLine("	        BACKGROUND-COLOR: #FFFFD0 ");
            builder.AppendLine("        } ");
            builder.AppendLine("        tr_selected{ ");
            builder.AppendLine("  ");
            builder.AppendLine("        } ");
            builder.AppendLine("        .tb_datalist .tr_tail { ");
            builder.AppendLine("	        BORDER-TOP: white 1px solid; BACKGROUND: ; BORDER-LEFT: white 1px solid ; text-align : Right; ");
            builder.AppendLine("        } ");
            builder.AppendLine("        .tb_datalist .tr_tail  TD { ");
            builder.AppendLine("	        BORDER-TOP: white 1px solid; BACKGROUND: ; BORDER-LEFT: white 1px solid ; text-align : Right; ");
            builder.AppendLine("        } ");
            builder.AppendLine("        TR.even { background-color  : #F7F9FE } ");
            builder.AppendLine("        TR.even TD {BACKGROUND-COLOR: #F7F9FE } ");
            builder.AppendLine("        .even TEXTAREA {  BACKGROUND-COLOR: #F7F9FE } ");
            builder.AppendLine("        .icon { BORDER-RIGHT: 0px; BORDER-TOP: 0px; MARGIN-BOTTOM: -3px; MARGIN-LEFT: 1px; BORDER-LEFT: 0px; MARGIN-RIGHT: 1px; BORDER-BOTTOM: 0px} ");
            builder.AppendLine("        .tb_titlebar { ");
            builder.AppendLine("	        border-right : buttonshadow 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: buttonshadow 1px solid; PADDING-LEFT: 2px; PADDING-BOTTOM: 2px;  ");
            builder.AppendLine("	        BORDER-LEFT: buttonshadow 1px solid; WIDTH: 100%; PADDING-TOP: 2px; BORDER-BOTTOM: buttonshadow 1px solid; WHITE-SPACE: nowrap ; MARGIN-top: 3px; ");
            builder.AppendLine("         } ");
            builder.AppendLine("    </style> ");
            builder.AppendLine("    <script type=\"text/javascript\"> ");
            builder.AppendLine("        function ShowTable(imgCtrl) { ");
            builder.AppendLine("            var ImgPlusScr = \"icon_expandall.gif\";      	// pic Plus  + ");
            builder.AppendLine("            var ImgMinusScr = \"icon_collapseall.gif\";     // pic Minus -  ");
            builder.AppendLine("            var TableID = imgCtrl.id.replace(\"Img\", \"Table\"); ");
            builder.AppendLine("            var tableCtrl = document.getElementById(TableID); ");
            builder.AppendLine("            if (imgCtrl.src.indexOf(\"icon_expandall\") != -1) { ");
            builder.AppendLine("                tableCtrl.style.display = \"\"; ");
            builder.AppendLine("                imgCtrl.src = ImgMinusScr; ");
            builder.AppendLine("            } ");
            builder.AppendLine("            else { ");
            builder.AppendLine("                tableCtrl.style.display = \"none\"; ");
            builder.AppendLine("                imgCtrl.src = ImgPlusScr; ");
            builder.AppendLine("            } ");
            builder.AppendLine("        } ");
            builder.AppendLine("    </script> ");
            builder.AppendLine("</head> ");
            builder.AppendLine("<body> ");
            builder.AppendLine(htmltables);
            builder.AppendLine("</body> ");
            builder.AppendLine("</html> ");

            return builder.ToString();
        }
    }
}



