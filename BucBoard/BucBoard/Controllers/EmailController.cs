using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BucBoard.Controllers
{
    public class EmailVM
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

    }

    public class EmailController : Controller
    {

        public IActionResult Create()
        {
            // Prepopulate the VM with address Data
            var vm = new EmailVM()
            {
                To = "luserUser5@gmail.com",
                From = "yourAddress@etsu.edu"
            };

            return View(vm);
        }
        [HttpPost]
        public IActionResult Create(EmailVM vm)
        {
            if (ModelState.IsValid)
            {
                if (SendMessage(vm))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Code for when there is an error sending message
                }
            }

            return View(vm);
        }

        #region Helper Methods

        [NonAction]
        private bool SendMessage(EmailVM vm)
        {
            bool ret = SendMessage(
                to: vm.To,
                from: vm.From,
                subject: vm.Subject,
                body: vm.Body
                );

            return ret;
        }

        [NonAction]
        private bool SendMessage(string to, string from, string body, string subject)
        {
            const string OUTGOING_SMTP_SERVER_LOGON = "luserUser5@gmail.com";
            const string OUTGOING_SMTP_SERVER_PASSWORD = "Passw0rd!Passw0rd!";
            const string OUTGOING_SMTP_SERVER_URL = "smtp.gmail.com";
            const int OUTGOING_SMTP_SERVER_PORT = 587;

            bool wasSuccessful;

            try
            {
                // Prepare Mail Client
                SmtpClient client = new SmtpClient()
                {
                    Host = OUTGOING_SMTP_SERVER_URL,
                    Port = OUTGOING_SMTP_SERVER_PORT,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(OUTGOING_SMTP_SERVER_LOGON, OUTGOING_SMTP_SERVER_PASSWORD),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };

                // Prepare Message
                MailMessage msg = new MailMessage(to, from)
                {
                    Body = body,
                    Subject = "From " + from + " :: " + subject
                };

                // Add a handler if you wanna know if the message went through
                //client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);

                // Send the message
                client.Send(msg);
                wasSuccessful = true;
            }
            catch (Exception)
            {
                wasSuccessful = false;
            }

            return wasSuccessful;
        }
        #endregion
    }
}
