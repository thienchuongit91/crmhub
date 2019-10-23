using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace api_intergration.Handlers
{
    public static class MailHandlers
    {
        public static void SendMail(string toEmail, Bitmap qrCode)
        {
            int mailPort = Int16.Parse(Common.Constant.MAIL_PORT);
            try
            {
                ImageConverter ic = new ImageConverter();
                Byte[] b = (Byte[])ic.ConvertTo(qrCode, typeof(Byte[]));
                MemoryStream img = new MemoryStream(b);

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("ucchau@fservices.com.vn");
                mail.To.Add(toEmail);
                mail.Subject = "Chúc mừng quý khách đã đăng kí thành công với FServices!";
                mail.Body = "Cảm ơn quý khách đã đăng ký hồ sơ mới với FServices, đính kèm là mã QRcode của quý khách:";
                mail.Attachments.Add(new Attachment(img, "image/jpg"));

                //SmtpServer.Port = mailPort;
                //SmtpServer.Credentials = new System.Net.NetworkCredential("username", "password");
                //SmtpServer.EnableSsl = true;

                //SmtpServer.Send(mail);
                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("fservicedev.system@gmail.com", "1234567a@"),
                    EnableSsl = true
                };
                client.Send(mail);
            }
            catch (Exception ex)
            {
            }
        }
    }
}