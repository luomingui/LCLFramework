using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;

namespace LCL.Tests.ACore
{
    public static class Helper
    { 
        public const string MongoDB_Database = @"LCLMongoTest";

        public const string EF_SQL_ConnectionString = @"Data Source=.;Database=EFTestContext;User ID=sa;Password=123456;";
        public const string EF_Table_EFCustomers = @"EFCustomers";
        public const string EF_Table_EFNotes = @"EFNotes";
        public const string EF_Table_EFCustomerNotes = @"EFCustomerNotes";
        public static void ClearEFTestDB()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(EF_SQL_ConnectionString))
                {
                    conn.Open();
                    List<string> tablesToClear = new List<string> { EF_Table_EFCustomerNotes, EF_Table_EFNotes, EF_Table_EFCustomers };
                    foreach (var table in tablesToClear)
                    {
                        using (SqlCommand command = new SqlCommand(string.Format("DELETE FROM {0}", table), conn))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                    conn.Close();
                }
            }
            catch { }
        }
        public static int ReadRecordCountFromEFTestDB(string table)
        {
            int ret = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(EF_SQL_ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(string.Format("SELECT COUNT(*) FROM {0}", table), conn))
                    {
                        ret = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    conn.Close();
                }
            }
            catch { }
            return ret;
        }

        /// <summary>
        /// 向指定的邮件地址发送邮件。
        /// </summary>
        /// <param name="to">需要发送邮件的邮件地址。</param>
        /// <param name="subject">邮件主题。</param>
        /// <param name="content">邮件内容。</param>
        public static void SendEmail(string to, string subject, string content)
        {
            //  <emailClient host="smtp.163.com" port="25" userName="byteartretail" password="byteartretail123" enableSsl="false" sender="byteartretail@163.com" />
            MailMessage msg = new MailMessage("minguiluo@163.com",to,subject,content);
            SmtpClient smtpClient = new SmtpClient("smtp.163.com");
            smtpClient.Port = 25;
            smtpClient.Credentials = new NetworkCredential("minguiluo@163.com", "4485083");
            smtpClient.EnableSsl = false;
            smtpClient.Send(msg);
        }
    }
}
