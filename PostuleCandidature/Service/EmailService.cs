using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PostuleCandidature.Service
{
    public class EmailService
    {
        public static bool SendMail(string subject, string content, string[] recipients, string from = "riadh.hammouda@cmsplusfrance.fr", string attachementUrl = @"C:\Users\riadh\Desktop\CVs\CV_Riadh_Hammouda.docx")
        {
            if (recipients == null || recipients.Length == 0)
                throw new ArgumentException("recipients");

            var gmailClient = new System.Net.Mail.SmtpClient
            {
                Host = "SSL0.OVH.NET",
                Port = 587,
                EnableSsl = false,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(from, "Cms+2018!")

            };

            using (var msg = new System.Net.Mail.MailMessage(from, recipients[0], subject, content))
            {
                for (int i = 1; i < recipients.Length; i++)
                    msg.To.Add(recipients[i]);

                msg.CC.Add("contact@cmsplusfrance.fr");
                try
                {
                    if (attachementUrl != null)
                        msg.Attachments.Add(new Attachment(attachementUrl));
                    gmailClient.Send(msg);
                    return true;
                }
                catch (Exception e)
                {
                    throw e;
                    // TODO: Handle the exception
                    return false;
                }
            }
        }

    }
}
