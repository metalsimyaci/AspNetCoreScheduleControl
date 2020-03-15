﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.Extensions.Logging;
using ScheduleControl.BackgroundJob.Abstract;

namespace ScheduleControl.BackgroundJob.Schedules
{
    public class EmailSendingScheduleJob : IEmailSendingSchedule
    {
        private ILogger<EmailSendingScheduleJob> _logger;

        public EmailSendingScheduleJob(ILogger<EmailSendingScheduleJob> logger)
        {
            _logger = logger;
        }

        public async Task Run(IJobCancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            await SendingAsync(new MailMessage());
        }

        public async Task SendingAsync(MailMessage mailMessage)
        {
            SmtpClient sc = new SmtpClient();
            sc.Port = 587;
            sc.Host = "smtp.gmail.com";
            sc.EnableSsl = true;
            sc.Credentials = new NetworkCredential("ademolguner@gmail.com", "SampiyonBesiktas1903");

            MailMessage mail = new MailMessage();

            mail.From = new MailAddress("ademolguner@gmail.com", "Adem Hangfire Mail");

            mail.To.Add("ademolguner@gmail.com");
            mail.CC.Add("ademolguner@gmail.com");

            mail.Subject = "Adem Medium Hangfire Mail";
            mail.IsBodyHtml = true;
            mail.Body = "Adem Medium Hangfire Mail E-Posta İçeriği";

            //mail.Attachments.Add(new Attachment(@"C:\Rapor.xlsx"));
            //mail.Attachments.Add(new Attachment(@"C:\Sonuc.pptx"));
            try
            {
                sc.Send(mail);
                await Task.FromResult("Mail Gönderildi");
            }
            catch (Exception)
            {
                await Task.FromException(new Exception("Mail Gönderildiği esnada hata oldu"));
            }

            //throw new NotImplementedException();
        }
    }
}
