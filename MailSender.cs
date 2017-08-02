using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;

namespace Chata_IS
{
    public class MailSender
    {
        
        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
                System.Diagnostics.Debug.WriteLine("[{0}] Send canceled.", token);
            }
            if (e.Error != null)
            {
                System.Diagnostics.Debug.WriteLine("[{0}] {1}", token, e.Error.ToString());
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Message sent.");
            }
            
        }

        public static void SendMail(string Subject,string Message, string From,
            string To, int smtpPort, string smtpHost, bool enableSsl, string from_login, string from_password)
        {
            // Command line argument must the the SMTP host.
            MailMessage mail = new MailMessage(new MailAddress(From,"Rezervační systém chaty"), new MailAddress(To));
            

            mail.Subject = Subject;
            mail.Body = Message;
            mail.IsBodyHtml = true;

            SmtpClient smtpMail = new SmtpClient(smtpHost);
            smtpMail.Port = 587;
            smtpMail.EnableSsl = true;
            smtpMail.Credentials = new NetworkCredential(from_login, from_password);
            // and then send the mail
            smtpMail.Send(mail);
        }
    }
}