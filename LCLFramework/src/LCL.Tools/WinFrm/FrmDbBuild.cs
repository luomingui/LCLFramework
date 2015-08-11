using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LCL.Tools.WinFrm
{
    public partial class FrmDbBuild : Form
    {
        public FrmDbBuild( )
        {
            InitializeComponent();
            this.comboBox1.SelectedIndex = 0;
            this.lblServer.Text = Utils.Sqlserver;
            BLLFactory.Instance.BindDataBaseList(this.cmbDBlist);
            if (cmbDBlist.Items.Count > 0)
                cmbDBlist.SelectedIndex = 0;
        }
        List<TableModel> tablenames = new List<TableModel>();
        private void but_BuildUI_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedItem == null) return;
            but_BuildUI.Enabled = false;
            BuildType build = BuildType.EntityFrameworkBuild;
            switch (this.comboBox1.SelectedItem.ToString())
            {
                case "WebBuild":
                    build = BuildType.WebBuild;
                    break;
                case "WPFBuild":
                    build = BuildType.WPFBuild;
                    break;
                case "WCFBuild":
                    build = BuildType.WCFBuild;
                    break;
                case "EntityFrameworkBuild":
                    build = BuildType.EntityFrameworkBuild;
                    break;
                case "WebService":
                    build = BuildType.WebBuild;
                    break;
                case "WindowsService":
                    build = BuildType.WindowsService;
                    break;
                default:
                    break;
            }

            tablenames = BLLFactory.Instance.idb.GetTableModelList(Utils.dbName, true);
            GenerateLib(build);
            but_BuildUI.Enabled = true;
        }

        #region DbBuild
        public void GenerateLib(BuildType buildtype)
        {
            if (txtOutPut.Text.Length == 0)
            {
                MessageBox.Show("输出路径不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            Code(buildtype);
            //编译DLL
            txtOutPut.Enabled = true;
        }
        private void Code(BuildType buildtype)
        {
            try
            {
                //生成增删改查
                int count = tablenames.Count;
                this.SetprogressBar1Max(count);
                this.SetprogressBar1Val(1);
                for (int i = 0; i < count; i++)
                {
                    TableModel tm = tablenames[i];
                    saveCode(tm, buildtype);
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(50); //干点实际的事
                    this.SetprogressBar1Val(i + 1);
                    this.SetlblStatuText((i + 1).ToString());
                }
                this.progressBar1.Maximum = 0;
                this.progressBar2.Maximum = 0;
                saveCode(buildtype);
                this.SetlblStatuText("已完成");
                MessageBox.Show("已完成！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                but_BuildUI.Enabled = true;
            }
        }
        private void saveCode(TableModel tm, BuildType buildtype)
        {
            if (!string.IsNullOrEmpty(this.txtOutPut.Text))
            {
                switch (buildtype)
                {
                    case BuildType.WebBuild:
                        BuildHelper.FactoryBuild(BuildType.WebBuild).Library(this.txtOutPut.Text, tm, this.progressBar2);
                        break;
                    case BuildType.WPFBuild:
                        BuildHelper.FactoryBuild(BuildType.WPFBuild).Library(this.txtOutPut.Text, tm, this.progressBar2);
                        break;
                  
                    case BuildType.EntityFrameworkBuild:
                        BuildHelper.FactoryBuild(BuildType.EntityFrameworkBuild).Library(this.txtOutPut.Text, tm, this.progressBar2);
                        break;
                }
            }
        }
        private void saveCode(BuildType buildtype)
        {
            if (!string.IsNullOrEmpty(this.txtOutPut.Text))
            {
                switch (buildtype)
                {
                    case BuildType.WebBuild:
                        break;
                    case BuildType.WPFBuild:
                        break;
                    case BuildType.EntityFrameworkBuild:
                        EntityFrameworkBuild entityframework = new EntityFrameworkBuild();
                        entityframework.BuildMyMenusClass(this.txtOutPut.Text, tablenames);
                        entityframework.BuildConfig(this.txtOutPut.Text);
                        entityframework.BuildDbContext(this.txtOutPut.Text, tablenames);
                        break;
                }
            }
        }
        #endregion

        #region SetProBar1MaxCallback
        private delegate void SetProBar1MaxCallback(int val);
        private delegate void SetProBar1ValCallback(int val);
        private delegate void SetlblStatuCallback(string text);
        public void SetprogressBar1Max(int val)
        {
            if (this.progressBar1.InvokeRequired)
            {
                SetProBar1MaxCallback method = new SetProBar1MaxCallback(this.SetprogressBar1Max);
                base.Invoke(method, new object[] { val });
            }
            else
            {
                this.progressBar1.Maximum = val;
            }
        }
        public void SetprogressBar1Val(int val)
        {
            if (this.progressBar1.InvokeRequired)
            {
                SetProBar1ValCallback method = new SetProBar1ValCallback(this.SetprogressBar1Val);
                base.Invoke(method, new object[] { val });
            }
            else
            {
                this.progressBar1.Value = val;
            }
        }
        public void SetlblStatuText(string text)
        {
            if (this.labelNum.InvokeRequired)
            {
                SetlblStatuCallback method = new SetlblStatuCallback(this.SetlblStatuText);
                base.Invoke(method, new object[] { text });
            }
            else
            {
                this.labelNum.Text = text;
            }
        }
        #endregion

        private void btn_OutPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                txtOutPut.Text = dialog.SelectedPath + "\\Template";
            }
        }
        private void cmbDBlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDBlist.SelectedItem == null) return;
            Utils.dbName = cmbDBlist.SelectedItem.ToString();
        }

        private void txtOutPut_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
