using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LCL.Tools
{
    public partial class ConnectionString : Form
    {
        public ConnectionString( )
        {
            InitializeComponent();
            this.comboBoxDBType.SelectedIndex = 2;
        }
        bool errFlag = true;
        private void saveUtils( )
        {
            Utils.NameSpace = txtEntityNameSpace.Text;
            Utils.TargetFolder = txtTargetFolder.Text;
            Utils.Author = txtAuthor.Text;
            Utils.DataBaseType = (DataBaseType)Enum.Parse(typeof(DataBaseType), comboBoxDBType.Text, true);
        }
        private void btn_DbOK_Click(object sender, EventArgs e)
        {
            if (!errFlag) MessageBox.Show("数据输入有误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            string str = this.comboBoxServer.Text.Trim();
            string str2 = this.txtUser.Text.Trim();
            string str3 = this.txtPass.Text.Trim();
            #region 字符串
            Utils.User = str2;
            Utils.Pwd = str3;
            Utils.Sqlserver = str;
            #endregion
            SqlConnection connection = new SqlConnection(Utils.ConnStr);
            try
            {
                connection.Open();
                saveUtils();
                connection.Close();
                btn_DbOK.Enabled = true;
                MessageBox.Show(this, "数据库配置成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "连接服务器失败！请检查服务器地址或用户名密码是否正确！" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            finally
            {
                connection.Close();
            }
        }
        //实例化一个ErrorProvider
        ErrorProvider errorUser = new ErrorProvider();
        private void txtUser_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUser.Text) || txtUser.Text.Length == 0)
            {
                errorUser.SetError(txtUser, "数据库登陆名称不能为空!");
                errFlag = false;
            }
            else
            {
                errorUser.SetError(txtUser, "");
                errFlag = true;
            }
        }

        private void txtEntityNameSpace_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtEntityNameSpace.Text) || txtEntityNameSpace.Text.Length == 0)
            {
                errorUser.SetError(txtEntityNameSpace, "命名空间不能为空!");
                errFlag = false;
            }
            else
            {
                errorUser.SetError(txtEntityNameSpace, "");
                errFlag = true;
            }
        }
    }
}