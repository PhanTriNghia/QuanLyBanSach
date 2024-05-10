using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace BaiTapLon.Common
{
    public class Common
    {
        [Obsolete]
        private static string Password = ConfigurationSettings.AppSettings["PasswordEmail"];
        [Obsolete]
        private static string Email = ConfigurationSettings.AppSettings["Email"];

        [Obsolete]
        public static bool SenEmail(string name, string subject, string content, string toMail)
        {
            bool rs = false;
            try
            {
                MailMessage message = new MailMessage();
                var smtp = new System.Net.Mail.SmtpClient();
                {
                    smtp.Host = "smtp.gmail.com"; // đia chỉ email server
                    smtp.Port = 587;
                    smtp.EnableSsl = true; // mã hóa dữ liệu
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential()
                    {
                        UserName = Email,
                        Password = Password,
                    };

                }
                MailAddress fromAddress = new MailAddress(Email, name);
                message.From = fromAddress;
                message.To.Add(toMail);
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = content;
                smtp.Send(message);
                rs = true;
            }
            catch (Exception)
            {
                rs = false;
            }
            return rs;
        }
    }
}