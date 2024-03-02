using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class SimpleEmailSender
{
    public static EmailSettings emailSettings = new EmailSettings { UserEmail = "", UserName = "" };
    public struct EmailSettings
    {
        public string UserEmail;
      
        public string UserName;
    }

    public static void Send(string to,string name, Action<object, AsyncCompletedEventArgs> callback)
    {
        try
        {
            SmtpClient mailServer = new SmtpClient("smtp-mail.outlook.com", 587);
            mailServer.EnableSsl = true;
            mailServer.Credentials = new NetworkCredential("fluentlysa@outlook.com", "Fluentlyslfns5") as ICredentialsByHost;
            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) {
                return true;
            };
            MailMessage msg = new MailMessage("fluentlysa@outlook.com", to);
            msg.Subject = "Welcome to Fluently!";
            msg.Body = "<html>" +
                       "<head>" +
                       "<style>" +
                       "body { font-family: Arial, sans-serif; font-size: 14px; color: #000000;}" +
                       "h1 { color: #3C4F8D; }" +
                       "p { font-size: 16px; }" +
                       "</style>" +
                       "</head>" +
                       "<body>" +
                       "<h1>Welcome <strong>" + name + "</strong> To Fluently!</h1>" +
                       "<p>We are thrilled to have you join the Fluently family as it grows. " +
                       "At Fluently, our AI-powered virtual reality experience combines language practice with virtual reality to " +
                       "ensure that you fully live the experience and don't miss a thing. Prepare to embark on an exciting journey with us," +
                       " and please don't hesitate to get in touch! wish to see you once more." +
                       "<br> <br><strong>The best,</strong><br><br><strong> fluently team.</strong></p>" +
                       "</body>" +
                       "</html>";
            msg.IsBodyHtml = true;


            mailServer.SendCompleted += new SendCompletedEventHandler(callback);
            mailServer.SendAsync(msg, "");

            Debug.Log("SimpleEmail: Sending Email.");
        }
        catch (Exception ex)
        {
            Debug.LogWarning("SimpleEmail: " + ex);
            callback("", new AsyncCompletedEventArgs(ex, true, ""));
        }
    }
}

