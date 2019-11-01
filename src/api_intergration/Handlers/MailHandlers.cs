using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
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

                Attachment att = new Attachment(img, "image.jpg");
                att.ContentDisposition.Inline = true;

                LinkedResource inline = new LinkedResource(img, MediaTypeNames.Image.Jpeg);
                inline.ContentId = Guid.NewGuid().ToString();

                MailMessage mail = new MailMessage();
                string htmlBody = "<html><body><h1>Cảm ơn quý khách đã đăng ký hồ sơ mới với FServices.</h1><br><img src=\"cid:"+ inline.ContentId + "\"></body></html>";
                AlternateView avHtml = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);

                avHtml.LinkedResources.Add(inline);
                mail.AlternateViews.Add(avHtml);

                mail.From = new MailAddress("cus_support@fservices.com.vn");
                mail.To.Add(toEmail);
                mail.Subject = "Chúc mừng quý khách đã đăng kí thành công với FServices!";
                mail.Body = String.Format("Cảm ơn quý khách đã đăng ký hồ sơ mới với FServices." +
                    @"<img src=""cid:{0}"" />", inline.ContentId);
                mail.Attachments.Add(new Attachment(img, "image.jpg"));

                var client = new SmtpClient("mail.fservices.com.vn", 587)
                {
                    Credentials = new NetworkCredential("cus_support@fservices.com.vn", "FS.Ucchau10A"),
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