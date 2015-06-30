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

    }
}
