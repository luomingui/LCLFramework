using System;
using System.Net;
using System.Windows.Forms;

namespace LCL.Tools
{
    static class Program
    {
        [STAThread]
        static void Main( )
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool ret;
            System.Threading.Mutex mutex = new System.Threading.Mutex(true, Application.ProductName, out ret);
            if (ret)
            {
                ChkSoftwareValidity();
                #region MyRegion
                ConnectionString login = null;
                try
                {
                    login = new ConnectionString();
                    login.Text = "MinGuiLuo Develop Tool ";
                    if (login.ShowDialog() == DialogResult.OK)
                    {
                        Application.Run(new MainFrm());
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show("登陆失败，" + err.Message);
                    return;
                }
                finally
                {
                    if (login != null)
                    {
                        login.Dispose();
                        login.Close();
                        login = null;
                    }
                }
                #endregion
            }
            else
            {
                if (MessageBox.Show(null, "有一个和本程序相同的应用程序已经在运行，请不要同时运行多个本程序。\n\n这个程序即将退出。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning) == DialogResult.Yes)
                    Application.Exit();
            }
        }

        #region 软件已到期

        public static void ChkSoftwareValidity( )
        {
            DateTime localTime = DateTime.Now;
            DateTime networkTime = GetNetworkTime();
            DateTime chktime = DateTime.Parse("2016-3-15");
            if (networkTime > chktime)
            {
                MessageBox.Show(null, "软件已到期，请联系软件供应商。\n\n这个程序即将退出。",
                Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
                Environment.Exit(0);
            }
        }
        public static DateTime GetNetworkTime( )
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri("http://www.baidu.com/"));
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string date = response.Headers["Date"];
                return DateTime.Parse(date);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return DateTime.Now.AddDays(3);
            }
        }

        #endregion

        #region 自定义检测当前机器是否安装SQL2000方法
        //public static bool ExistSqlServerService()
        //{
        //    bool ExistFlag = false;
        //    ServiceController[] service = ServiceController.GetServices();
        //    for (int i = 0; i < service.Length; i++)
        //    {
        //        if (service[i].DisplayName.ToString() == "MSSQLSERVER")
        //        {
        //            ExistFlag = true;
        //        }
        //    }
        //    return ExistFlag;
        //}
        #endregion
    }
}
