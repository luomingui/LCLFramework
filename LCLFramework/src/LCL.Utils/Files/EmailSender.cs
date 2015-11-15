using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Threading;
using System.Text;

namespace LCL
{
    /// <summary>
    /// 邮件发送
    /// 例： EmailSender es = new EmailSender(txtStmtpServer.Text.Trim(),
    ///         txtfromEmail.Text.Trim(), txtPwd.Text.Trim(), txtToEmail.Text.Trim(), txtSubject.Text.Trim(), txtBody.Text.Trim(), ckbHTML.Checked,
    ///         new string[] { txtfile1.Text.Trim(), txtfile2.Text.Trim() }, txtfromByName.Text.Trim());
    ///         es.Send();
    /// 示例：Demo/SendEmailDemo.aspx
    /// </summary>
    public class EmailSender
    {
        /// <summary>
        /// 通过线程来发送邮件
        /// </summary>
        public void Send_Thead()
        {
            Thread thr = new Thread(new ThreadStart(Send));
            thr.Start();
        }

        private string _subject = string.Empty;     //主题
        private bool _isHtml = true;                //是否以HTML发送
        private string _body = string.Empty;        //内容
        private string[] _files = null;             //附件
        private string _fromByName = string.Empty;  //发送者别名
        private string _fromEmail = string.Empty; //发送者邮箱
        private string _fromPassWord = string.Empty;//发送者密码        
        private string _stmtpServer = string.Empty; //SmtpServer
        private string _toEmail = string.Empty;         //收件人邮箱
        private Dictionary<string, string> Files = null;             //图片附件

        ///// <summary>
        ///// 请使用有参的构造方法
        ///// </summary>
        //public EmailSender()
        //{
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_stmtpServer">SmtpServer</param>
        /// <param name="_fromEmail">发送者邮箱</param>
        /// <param name="_fromPassWord">发送者密码</param>
        /// <param name="_toEmail">收件人邮箱</param>
        /// <param name="_subject">主题</param>
        /// <param name="_body">内容</param>
        /// <param name="_isHtml">是否以HTML发送</param>
        /// <param name="_files">附件</param>
        /// <param name="_fromByName">发送者别名</param>
        public EmailSender(string _stmtpServer, string _fromEmail, string _fromPassWord,
            string _toEmail, string _subject, string _body, bool _isHtml, string[] _files, string _fromByName)
        {
            this._stmtpServer = _stmtpServer;
            this._fromEmail = _fromEmail;
            this._fromPassWord = _fromPassWord;
            this._toEmail = _toEmail;
            this._subject = _subject;
            this._body = _body;
            this._isHtml = _isHtml;
            this._files = _files;
            this._fromByName = _fromByName;
        }

        public EmailSender(string _stmtpServer, string _fromEmail, string _fromPassWord,
            string _toEmail, string _subject, string _body, bool _isHtml, string _fromByName, Dictionary<string, string> files)
        {
            this._stmtpServer = _stmtpServer;
            this._fromEmail = _fromEmail;
            this._fromPassWord = _fromPassWord;
            this._toEmail = _toEmail;
            this._subject = _subject;
            this._body = _body;
            this._isHtml = _isHtml;
            this.Files = files;
            this._fromByName = _fromByName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_toEmail"></param>
        /// <param name="_subject"></param>
        /// <param name="_body"></param>
        /// <param name="_isHtml"></param>
        public EmailSender(string _toEmail, string _subject, string _body, bool _isHtml)
        {
            //Model.SysEmail model = new DAL.SysEmail().GetModel(" status=1 ");
            //this._stmtpServer = model.SmtpServer;
            //this._fromEmail = model.FromEmail;
            //this._fromPassWord = model.FromPwd;

            this._isHtml = _isHtml;
            this._toEmail = _toEmail;
            this._subject = _subject;
            this._body = _body;
        }

        public void Send()
        {
            System.Net.Mail.SmtpClient client = new SmtpClient(_stmtpServer);
            //client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(_fromEmail, _fromPassWord);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Timeout = 100000;

            //System.Net.Mail.MailMessage message = new MailMessage("aaaaaaaaaaaa1<asd@asdf.com>", strto, strSubject, strBody);
            System.Net.Mail.MailMessage message = new MailMessage();
            //for (int i = 0; i < _toEmail.Count; i++)
            {
                message.To.Add(_toEmail);
            }
            message.From = new MailAddress(_fromEmail, _fromByName);
            message.Subject = _subject;
            message.BodyEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            message.Body = _body;
            message.SubjectEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            message.IsBodyHtml = _isHtml;

            //发送附件               
            if (_files != null && _files.Length != 0)
            {
                for (int i = 0; i < _files.Length; ++i)
                {
                    if (_files[i].Trim() != "" && System.IO.File.Exists(_files[i].Trim()))
                    {
                        Attachment attach = new Attachment(_files[i]);
                        message.Attachments.Add(attach);
                    }
                }
            }

            string UserName = _fromEmail;
            if (_stmtpServer.Trim().ToLower() == "smtp.gmail.com")
            {
                //UserName = _fromEmail.Substring(0, _fromEmail.IndexOf('@'));
                client.EnableSsl = true;
            }
            //else if (_stmtpServer.Trim().ToLower() == "smtp.163.com")
            //{
            //    UserName = _fromEmail.Substring(0, _fromEmail.IndexOf('@'));
            //}

            client.Credentials = new System.Net.NetworkCredential(UserName.Trim(), _fromPassWord);

            client.Send(message);
        }

        public void SendImg()
        {
            System.Net.Mail.SmtpClient client = new SmtpClient(_stmtpServer);
            //client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(_fromEmail, _fromPassWord);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Timeout = 100000;

            //System.Net.Mail.MailMessage message = new MailMessage("aaaaaaaaaaaa1<asd@asdf.com>", strto, strSubject, strBody);
            System.Net.Mail.MailMessage message = new MailMessage();
            //for (int i = 0; i < _toEmail.Count; i++)
            {
                message.To.Add(_toEmail);
            }
            message.From = new MailAddress(_fromEmail, _fromByName);
            message.Subject = _subject;
            message.BodyEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            message.Body = _body;
            message.SubjectEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            message.IsBodyHtml = _isHtml;

            //发送附件               
            if (Files != null && Files.Count != 0)
            {
                string file;
                foreach (string key in Files.Keys)
                {
                    file = Files[key];
                    if (file != "" && System.IO.File.Exists(file))
                    {
                        Attachment att = new Attachment(file);
                        att.ContentType.Name = "image/jpg";
                        att.ContentId = string.Format("codeImg{0}", key);
                        att.ContentDisposition.Inline = true;
                        att.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                        message.Attachments.Add(att);
                    }
                }
            }

            string UserName = _fromEmail;
            if (_stmtpServer.Trim().ToLower() == "smtp.gmail.com")
            {
                //UserName = _fromEmail.Substring(0, _fromEmail.IndexOf('@'));
                client.EnableSsl = true;
            }
            //else if (_stmtpServer.Trim().ToLower() == "smtp.163.com")
            //{
            //    UserName = _fromEmail.Substring(0, _fromEmail.IndexOf('@'));
            //}

            client.Credentials = new System.Net.NetworkCredential(UserName.Trim(), _fromPassWord);

            client.Send(message);
        }
    }

    /// <summary>
    /// 发送邮件信息
    /// </summary>
    public static class EmailMsg
    {
        //private static EmailService service;
        //private static MemberManageService memberManageService;

        //static EmailMsg()
        //{
        //    service = (EmailService)ComponentUtil.GetComponent(QuillInjector.GetInstance().Container, typeof(EmailService));
        //    QuillInjector.GetInstance().Inject(service);

        //    memberManageService = (MemberManageService)ComponentUtil.GetComponent(QuillInjector.GetInstance().Container, typeof(MemberManageService));
        //    QuillInjector.GetInstance().Inject(memberManageService);
        //}
        /// <summary>
        /// 注册发送邮件，激活帐号
        /// </summary>
        /// <param name="guid">激活码</param>
        /// <param name="toEmail"></param>
        /// <returns></returns>
        //public static int Register(string guid, string toEmail)
        //{
        //    EmailTemplates entity = service.GetTempByType(0);
        //    if (entity == null)
        //    {
        //        return 0;
        //    }
        //    string content = entity.EmailBody;
        //    string port = string.Format(":{0}", WebUtils.Port);
        //    if (port == ":80")
        //    {
        //        port = "";
        //    }
        //    content = content.Replace("{URL}", string.Format(@"http://{0}/Handle/RegisterValidate?code={1}", string.Format("{0}{1}", WebUtils.Host, port), guid));

        //    SysEmail sysEmail = service.GetSysEmail();
        //    if (sysEmail == null)
        //    {
        //        return 0;
        //    }
        //    try
        //    {
        //        EmailSender es = new EmailSender(sysEmail.SmtpServer, sysEmail.FromEmail, sysEmail.FromPwd, toEmail, entity.EmailSubject, content, entity.IsHtml == 1, null, sysEmail.FromName);
        //        es.Send();
        //    }
        //    catch(Exception ex)
        //    {
        //        string s = ex.Message.ToString();
        //        return 0;
        //    }
        //    return 1;
        //}
        ///// <summary>
        ///// 景区注册发送邮件，查询码
        ///// </summary>
        ///// <param name="guid">查询码</param>
        ///// <param name="toEmail"></param>
        ///// <returns></returns>
        //public static int RegisterSite(string guid, string toEmail)
        //{
        //    EmailTemplates entity = service.GetTempByType(1);
        //    if (entity == null)
        //    {
        //        return 0;
        //    }
        //    string content = entity.EmailBody;
        //    content = content.Replace("{URL}", string.Format(@"http://{0}/SelectSite/SelectSite?code={1}", WebUtils.Host, guid));

        //    SysEmail sysEmail = service.GetSysEmail();
        //    if (sysEmail == null)
        //    {
        //        return 0;
        //    }
        //    try
        //    {
        //        EmailSender es = new EmailSender(sysEmail.SmtpServer, sysEmail.FromEmail, sysEmail.FromPwd, toEmail, entity.EmailSubject, content, entity.IsHtml == 1, null, sysEmail.FromName);
        //        es.Send();
        //    }
        //    catch (Exception ex)
        //    {
        //        string s = ex.Message.ToString();
        //        return 0;
        //    }
        //    return 1;
        //}
        ///// <summary>
        ///// 找回密码
        ///// </summary>
        ///// <param name="guid">激活码</param>
        ///// <param name="toEmail"></param>
        ///// <param name="user_name"></param>
        ///// <returns></returns>
        //public static string GetPwd(string guid, Member memberEntity)
        //{
        //    string Res = "";
        //    EmailTemplates entity = service.GetTempByType(0);
        //    if (entity == null)
        //    {
        //        return "系统出错！";
        //    }
        //    string port = string.Format(":{0}", WebUtils.Port);
        //    if (port == ":80")
        //    {
        //        port = "";
        //    }
        //    string content = entity.EmailBody;
        //    content = content.Replace("{URL}", string.Format(@"http://{0}/Admin/ForgotPassword/Reset?username={1}&&code={2}", string.Format("{0}{1}", WebUtils.Host, port), memberEntity.Account, guid));

        //    SysEmail sysEmail = service.GetSysEmail();
        //    if (sysEmail == null)
        //    {
        //        return "系统出错！";
        //    }
        //    memberEntity.ActivationCode = guid;
        //    bool update=memberManageService.updateMember(memberEntity);
        //    if (!update)
        //    {
        //        return "系统出错！";
        //    }
        //    try
        //    {
        //        EmailSender es = new EmailSender(sysEmail.SmtpServer, sysEmail.FromEmail, sysEmail.FromPwd, memberEntity.Email, entity.EmailSubject, content, entity.IsHtml == 1, null, sysEmail.FromName);
        //        es.Send();
        //        Res = "ok";
        //    }
        //    catch
        //    {
        //        Res = "发送邮件出错，请验证邮箱是否有效！";
        //    }
        //    return Res;
        //}
        ///// <summary>
        ///// 修改邮箱
        ///// </summary>
        ///// <param name="guid"></param>
        ///// <param name="memberEntity"></param>
        ///// <returns></returns>
        //public static string ResetEmail(string guid,Member memberEntity,string newemail,string encodecode,string encodemail)
        //{
        //    string Res = "";
        //    EmailTemplates entity = service.GetTempByType(4);
        //    if (entity == null)
        //    {
        //        return "系统出错！";
        //    }
        //    string port = string.Format(":{0}", WebUtils.Port);
        //    if (port == ":80")
        //    {
        //        port = "";
        //    }
        //    string content = entity.EmailBody;
        //    content = content.Replace("{url}", string.Format(@"http://{0}/Admin/ResetEmail/?id={1}&code={2}&email={3}"
        //        , string.Format("{0}{1}", WebUtils.Host, port), memberEntity.MemberId
        //        , encodecode, encodemail))
        //        .Replace("{account}", memberEntity.Account + "：")
        //        .Replace("{datetime}", DateTime.Now.ToLongDateString().ToString()).Replace("{newemail}", newemail);
        //    SysEmail sysEmail = service.GetSysEmail();
        //    if (sysEmail == null)
        //    {
        //        return "系统出错！";
        //    }
        //    memberEntity.ActivationCode = guid;
        //    bool update = memberManageService.updateMember(memberEntity);
        //    if (!update)
        //    {
        //        return "系统出错！";
        //    }
        //    try
        //    {
        //        EmailSender es = new EmailSender(sysEmail.SmtpServer, sysEmail.FromEmail, sysEmail.FromPwd, newemail
        //            , entity.EmailSubject, content, entity.IsHtml == 1, null, sysEmail.FromName);
        //        es.Send();
        //        Res = "ok";
        //    }
        //    catch
        //    {
        //        Res = "发送邮件出错，请验证邮箱是否有效！";
        //    }
        //    return Res;
        //}
        
        //public static int SendTicketCode(List<OrderCodeModel> listCode, string toEmail, long orderID)
        //{
        //    EmailTemplates entity = service.GetTempByType(3);
        //    if (entity == null)
        //    {
        //        return 0;
        //    }
        //    Dictionary<string, string> files = new Dictionary<string, string>();
        //    StringBuilder sb = new StringBuilder();//内容
        //    string fileName;
        //    string filePath = string.Format("{0}{1}_{2}", System.Configuration.ConfigurationManager.AppSettings["AuthCodePath"].ToString(), orderID, DateTime.Now.ToShortDateString());
        //    foreach (OrderCodeModel item in listCode)
        //    {
        //        fileName = string.Format("{0}_{1}.jpg", filePath, item.SITE_ID);
        //        if (!System.IO.File.Exists(fileName))
        //        {
        //            QRCodeManager.EncodeImg(item.AUTH_CODE).Save(fileName);
        //        }
        //        files.Add(item.SITE_ID.ToString(), fileName);
        //        sb.AppendFormat("<br/><img alt='二维码' src=cid:codeImg{0} /><br/>{1}，付款：{2}<br/>{3}<br/>", item.SITE_ID, item.SITE_NAME, item.PAID_IN_PRICE, item.AUTH_CODE);
        //    }

        //    string content = entity.EmailBody;


        //    content = content.Replace("{CONTENT}", sb.ToString());

        //    SysEmail sysEmail = service.GetSysEmail();
        //    if (sysEmail == null)
        //    {
        //        return 0;
        //    }
        //    try
        //    {
        //        EmailSender es = new EmailSender(sysEmail.SmtpServer, sysEmail.FromEmail, sysEmail.FromPwd, toEmail, entity.EmailSubject, content, entity.IsHtml == 1, sysEmail.FromName, files);
        //        es.SendImg();
        //    }
        //    catch (Exception ex)
        //    {
        //        string s = ex.Message.ToString();
        //        return 0;
        //    }
        //    return 1;
        //}
    }
}
