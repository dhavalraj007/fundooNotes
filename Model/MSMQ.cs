using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Model
{
    public class MSMQ
    {
        MessageQueue messageQueue= new MessageQueue();
        public string receiverName;
        public string receiverEmail;
        public void SendMessage(string token,string Email,string name)
        {
            receiverEmail= Email;
            receiverName= name;
            messageQueue.Path = @".\private$\token";

            if(!MessageQueue.Exists(messageQueue.Path))
            {
                MessageQueue.Create(messageQueue.Path);
            }
            messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            messageQueue.ReceiveCompleted += MessageQueue_RecieveCompleted;
            messageQueue.Send(token);
            messageQueue.BeginReceive();
            messageQueue.Close();
        }

        private void MessageQueue_RecieveCompleted(object sender,ReceiveCompletedEventArgs args)
        {
            var msg = messageQueue.EndReceive(args.AsyncResult);
            string token = msg.Body.ToString();
            MailMessage mailMessage = new MailMessage();
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential("rdjcoding@gmail.com", "lwgprspgibpsnift")
            };

            mailMessage.From = new MailAddress("rdjcoding@gmail.com");
            mailMessage.To.Add(new MailAddress(receiverEmail));
            string mailBody = $"<!DOCTYPE html>" +
                $"<html>" +
                $"<style>" +
                $".blink" +
                $"</style>" +
                $"<body style=\"background-color:#DBFF73;text-align:center;padding:5px;\">" +
                $"<h1>dear {receiverName},</h1>" +
                $"<h3>To reset the password click this link :</h3>" +
                $"<a href='http://localhost:4200/resetPassword/{token}'>" +
                $"valid for 6 hours" +
                $"</body>" +
                $"</html>";
            mailMessage.Body = mailBody;
            mailMessage.IsBodyHtml= true;
            mailMessage.Subject = "Fundoo reset password";
            smtpClient.Send(mailMessage);
        }
    }
}
