using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;

namespace ProjectExample.Models.ModelView
{
    public class EmailSender
    {
        public void SendEmail(string from, string to, string subject, string body)
        {
            //Khởi tạo email 

            MailMessage mail = new MailMessage(from, to, subject,"");

            //Đính kèm Tệp PDF
            Attachment attachment = new Attachment(body);
            mail.Attachments.Add(attachment);

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.UseDefaultCredentials = false;
            //smtpClient.UseDefaultCredentials = false; bxtkvcybvgcjcdjp  
            smtpClient.Credentials = new NetworkCredential("dvo31666@gmail.com", "bxtkvcybvgcjcdjp");
            smtpClient.EnableSsl = true;
            try
            {
                smtpClient.Send(mail);
                Console.WriteLine("Email send success");
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to send email. Error message: " + e.Message);
            }

        }
        public void SendEmailSales(string from, string to, string subject, string body)
        {
            //Khởi tạo email 

            MailMessage mail = new MailMessage(from, to, subject, body);
            mail.IsBodyHtml = true;//khai báo nội dụng email là html
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.UseDefaultCredentials = false;

            smtpClient.Credentials = new NetworkCredential(from, "bxtkvcybvgcjcdjp");
            smtpClient.EnableSsl = true;
            try
            {
                smtpClient.Send(mail);
                Console.WriteLine("Email send success");
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to send email. Error message: " + e.Message);
            }

        }
     
    }
}