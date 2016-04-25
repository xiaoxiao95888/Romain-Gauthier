using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Romain_Gauthier.Web.Infrastructure.Utilites
{
    public static class MailHelp
    {
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="empName"></param>
        /// <param name="filePath"></param>
        public static void SendMailForImage(string mail,string empName,string filePath)
        {
            var username = ConfigurationManager.AppSettings["MailUser"];
            var pwd = ConfigurationManager.AppSettings["MailPWD"];
            var host = ConfigurationManager.AppSettings["Mailhost"];
            var sub = string.Format("Hi {0},感谢您对Romain-Gauthier的支持", empName);
            var body = string.Format("Hi {0},感谢您对Romain-Gauthier的支持，下载的文件详见附件。本邮件系自动产生，请勿直接回复。", empName);
            SendMail(username, pwd, mail, host, sub, body, filePath);
        }

        /// <summary>
        /// 发送邮件,返回true表示发送成功
        /// </summary>
        /// <param name="userName">发件人邮箱地址；发件人用户名</param>
        /// <param name="pwd">密码</param>
        /// <param name="mailaddress">接受者邮箱地址</param>
        /// <param name="host">SMTP服务器的主机名</param>
        /// <param name="sub">邮件主题行</param>
        /// <param name="body">邮件主体正文</param>
        /// <param name="filePath">附件路径</param>
        /// <param name="ccMailAddresses">CC的地址</param>
        public static bool SendMail(string userName, string pwd, string mailaddress, string host, string sub, string body, string filePath, MailAddress[] ccMailAddresses = null)
        {
            var client = new SmtpClient
            {
                Host = host,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(userName, pwd),
                DeliveryMethod = SmtpDeliveryMethod.Network,

            };
            try
            {
                var message = new MailMessage(userName, mailaddress)
                {
                    Subject = sub,
                    Body = body,
                    BodyEncoding = System.Text.Encoding.UTF8,
                    IsBodyHtml = true
                };
                if (ccMailAddresses != null)
                {
                    foreach (var ccMailAddress in ccMailAddresses)
                    {
                        message.CC.Add(ccMailAddress);
                    }
                }

                if (!string.IsNullOrEmpty(filePath))
                {
                    var attachment = new Attachment(filePath) { Name = filePath.Split('/').LastOrDefault() };
                    message.Attachments.Add(attachment);
                }
                client.Send(message);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
       
    }
}