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
            DataTable table = BLLFactory.Instance.idb.GetTablesColumnsList("", dbName);
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("                <table class=\"table\"> ");
            builder.AppendLine("                    <thead> ");
            builder.AppendLine("                        <tr> ");

            int columns = table.Columns.Count;
            for (int i = 0; i < columns; i++)
            {
                builder.AppendLine("            <th>" + table.Columns[i].ColumnName + "</th> ");
            }
            builder.AppendLine("                        </tr> ");
            builder.AppendLine("                    </thead> ");
            builder.AppendLine("                    <tbody> ");
            builder.AppendLine("                        <tr> ");

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                for (int j = 0; j < columns; j++)
                {
                    builder.AppendLine("                    <td>" + row[j].ToString() + "</td> ");
                }
            }

            builder.AppendLine("                        </tr> ");
            builder.AppendLine("                    </tbody> ");
            builder.AppendLine("                </table> ");

            File.AppendAllText(fileListPath, builder.ToString(), Encoding.UTF8);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
