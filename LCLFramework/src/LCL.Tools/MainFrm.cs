using LCL.Tools;
using SF.Threading;
using LCL.Tools.WinFrm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace LCL.Tools
{
    public partial class MainFrm : Form
    {
        private string DbName = "master";
        public MainFrm()
        {
            InitializeComponent();
            DbName = Utils.dbName;
        }
        private void MainFrm_Load(object sender, EventArgs e)
        {
            BLLFactory.Instance.BindDataBaseList(this.cmbDBlist);
            if (cmbDBlist.Items.Count > 0)
            {
                string dbName = cmbDBlist.SelectedItem == null ? "" : cmbDBlist.SelectedItem.ToString();
                this.toolStripStatusLabel1.Text = "服务器： " + Utils.Sqlserver
                    + " 数据库类型：" + Utils.DataBaseType.ToString()
                    + " 数据库：" + dbName
                    + " 总表数：" + this.treeView1.Nodes.Count;
            }
        }
        private void cmbDBlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDBlist.SelectedItem == null) return;
            string dbName = cmbDBlist.SelectedItem.ToString();
            OpenDao(dbName);
        }
        private void 生成数据库文档ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FrmDbaDoc frm = new FrmDbaDoc();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void 修改字段说明ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.treeView1.SelectedNode == null) return;
                TreeNode node = this.treeView1.SelectedNode;
                FrmDbTablesColumnsLable frm = new FrmDbTablesColumnsLable(node.Text);
                frm.OnDataSaved += frm_OnDataSaved;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void frm_OnDataSaved(object sender, EventArgs e)
        {
            treeView1_AfterSelect(null, null);
        }
        private void 修改表说明ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FrmDbTablesLable frm = new FrmDbTablesLable();
                frm.OnDataSaved += frm_OnDataSaved;
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void 批量生成代码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FrmDbBuild frm = new FrmDbBuild();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (this.treeView1.SelectedNode == null) return;
                TreeNode node = this.treeView1.SelectedNode;
                this.groupBox1.Text = node.Text + "表详细信息";
                BLLFactory.Instance.BinTableInfo(this.dataGridView1, node.Text);
                this.groupBox2.Text = node.Text + "表外键信息";
                BLLFactory.Instance.BinTableRelation(this.dataGridView2, node.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void 添加表主键ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否把数据库里所有的ID列添加主键和自增列", "提示",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.ActionInvoker(delegate
                {
                    添加表主键ToolStripMenuItem.Enabled = false;
                    BLLFactory.Instance.idb.AddTableByKey(Utils.dbName);
                    MessageBox.Show("把数据库里所有的ID列添加主键和自增列成功", "提示");
                    添加表主键ToolStripMenuItem.Enabled = true;
                });
            }
        }
        private void 刷新列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cmbDBlist_SelectedIndexChanged(null, null);
        }
        private void 相同字段说明一样ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                #region 相同字段说明一样
                string filePath = "";
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Excel(*.xls)|*.xls|所有文件(*.*)|*.*";

                ofd.RestoreDirectory = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    filePath = ofd.FileName;
                }
                else
                {
                    return;
                }
                if (!BLLFactory.Instance.UpdateColumns(filePath))
                {
                    MessageBox.Show("xls文件里面必须包含 字段名称，字段说明 这两列，并且文件工作表的名字必须是Sheet1$ ");
                }
                else
                {
                    MessageBox.Show("把数据库里相同的字段描述一样修改成功！！！", "提示");
                }
                #endregion
            }
            catch (Exception ex)
            {
                StringBuilder list = new StringBuilder();
                list.AppendLine("1 : xls文件里面必须包含 字段名称，字段说明 这两列，并且文件工作表的名字必须是Sheet1$ ");
                list.AppendLine("2 : 错误信息是" + ex.Message);
                MessageBox.Show(list.ToString());
            }
        }
        private void 导出数据库里相同的字段ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("导出数据库里相同的字段", "提示",
               MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable table = BLLFactory.Instance.idb.GetEqualColumnsList(Utils.dbName);
                    MyXlsHelper.LeadOutToExcel(table);
                }
                else
                {
                    DataTable table = BLLFactory.Instance.idb.GetTablesColumnsList("", Utils.dbName);
                    MyXlsHelper.LeadOutToExcel(table);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #region 函数
        private void OpenDao(string dbName)
        {
            this.ActionInvoker(delegate
            {
                //绑定数据
                Utils.dbName = dbName;
                DbName = Utils.dbName;
                BLLFactory.Instance.BindTableNameList(this.treeView1);
                this.toolStripStatusLabel1.Text = "服务器： " + Utils.Sqlserver
                  + " 数据库：" + cmbDBlist.SelectedItem.ToString()
                  + " 总表数：" + this.treeView1.Nodes.Count;
            });
        }
        public void ActionInvoker(Action action, bool flg = true)
        {
            if (flg)
            {
                #region ThreadHelper
                ThreadHelper.AsyncMultiActions.Execute(delegate
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new MethodInvoker(delegate
                        {
                            action();
                        }));
                    }
                    else
                    {
                        action();
                    }
                });
                #endregion
            }
            else
            {
                #region InvokeRequired
                if (this.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        action();
                    }));
                }
                else
                {
                    action();
                }
                #endregion
            }
        }
        #endregion
        private void labText_Click(object sender, EventArgs e)
        {
            //访问Blogs
            System.Diagnostics.Process.Start("http://www.cnblogs.com/luomingui");
        }
        private void winFromUIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.treeView1.SelectedNode == null) return;
                TreeNode node = this.treeView1.SelectedNode;
                ShowFormToTabControl(tabControl1, new FrmDbBuildWinFromUI(node.Text), "WinFrom界面代码生成");
            }
            catch
            {

            }
        }
        private void ShowFormToTabControl(TabControl tc, Form f, string tpText)
        {// ShowFormToTabControl(tabControl1, new FrmUpdateHouseHold(), "衡水更新数据");
            foreach (TabPage t in tc.TabPages)
            {
                if (t.Text == tpText)
                {
                    tc.SelectedTab = t;
                    f.Dispose();
                    return;
                }
            }
            TabPage tp = new TabPage(tpText);
            f.TopLevel = false;
            f.Parent = tp;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Dock = DockStyle.Fill;
            f.Show();
            tc.TabPages.Add(tp);
            tc.SelectedTab = tp;
        }
        private void 设置数据表关系ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.treeView1.SelectedNode == null) return;
                TreeNode node = this.treeView1.SelectedNode;
                ShowFormToTabControl(tabControl1, new FrmDbTableRelation(), "设置数据表关系");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void 关闭其他全部ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TabPage item in tabControl1.TabPages)
            {
                if (item.Text != "表说明")
                {
                    item.Dispose();
                }
            }
        }

        private void eFContextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            List<TableModel> tablenames = BLLFactory.Instance.idb.GetTableModelList(Utils.dbName, true);
            EntityFrameworkBuild entityframework = new EntityFrameworkBuild();
            entityframework.BuildDbContext(dir, tablenames);
            MessageBox.Show("已经生成到桌面的LCL文件夹下！！！", "提示");
        }

        private void iOCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            ServiceLocatorBuild build = new ServiceLocatorBuild();
            build.BuildServiceLocator(dir);
            MessageBox.Show("已经生成到桌面的LCL文件夹下！！！", "提示");
        }

        private void entityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            EntityBuild build = new EntityBuild();
            build.BuildEntity(dir);
            MessageBox.Show("已经生成到桌面的LCL文件夹下！！！", "提示");
        }

        private void repositoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            RepositoryBuild build = new RepositoryBuild();
            build.BuildRepository(dir);
            MessageBox.Show("已经生成到桌面的LCL文件夹下！！！", "提示");
        }

        private void bootstrapAdminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //控制器，视图验证，视图模型，视图
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            MVCUIBuild build = new MVCUIBuild();
            build.GenerateControllers(dir);
            build.GenerateBootstrapAdminViews(dir);
            MessageBox.Show("已经生成到桌面的LCL文件夹下！！！", "提示");
        }

        private void 根据类库生成数据库描述ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDoc frm = new FrmDoc();
            frm.ShowDialog();
        }

        private void mVC验证模型ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            ValidationModelBuild build = new ValidationModelBuild();
            build.GenerateValidationModel(dir);
            MessageBox.Show("已经生成到桌面的LCL文件夹下！！！", "提示");
        }
        private void adoNetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            AdoNetBuild build = new AdoNetBuild();
            build.GenerateDAL(dir);
            MessageBox.Show("已经生成到桌面的LCL文件夹下！！！", "提示");
        }

        private void mVC视图模型ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            MVCViewModelBuild build = new MVCViewModelBuild();
            build.GenerateEntityViewsModeel(dir);
            MessageBox.Show("已经生成到桌面的LCL文件夹下！！！", "提示");
        }
    }
}
