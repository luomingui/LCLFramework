using EFDemo.Document;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LCL.Tools
{
    public partial class FrmDoc : Form
    {
        public FrmDoc()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog1 = new OpenFileDialog();
            fileDialog1.Filter = "文件类型(*.dll,*.exe,*.dll)|*.dll;*.exe;*.dll|All files (*.*)|*.*";
            fileDialog1.FilterIndex = 1;
            fileDialog1.RestoreDirectory = true;
            if (fileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtdllpath.Text = fileDialog1.FileName;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                this.txtcodepath.Text = dialog.SelectedPath;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.txtdllpath.Text.Length == 0)
            {
                MessageBox.Show("找不到DLL文件");
                return;
            }
            if (this.txtcodepath.Text.Length == 0)
            {
                MessageBox.Show("找不到DLL源码文件");
                return;
            }
            if (this.txtcondition.Text.Length == 0)
            {
                MessageBox.Show("查询条件不能为空");
                return;
            }
            button1.Enabled = false;
            button3.Enabled = false;
            button2.Enabled = false;
            string condition = this.txtcondition.Text;
            string[] arr = condition.Split(',');
            Assembly assembly = Assembly.LoadFile(this.txtdllpath.Text);
            foreach (var str in arr)
            {
                Type modelBase = assembly.GetTypes().FirstOrDefault(t => t.Name == str);
                var types = assembly.GetTypes().Where(t => modelBase == t.BaseType && t != modelBase);
                if (types.Count() > 0)
                {
                    DbHelper.ConnectionString = Utils.ConnStr;
                    DocumentHelper.GenerateDbTableFieldDescribe(types.ToArray(), this.txtcodepath.Text);
                }
            }
            button2.Enabled = true;
            button1.Enabled = true;
            button3.Enabled = true;
            MessageBox.Show("根据模型生成数据库表字段说明完成！！！");
        }
    }
}
