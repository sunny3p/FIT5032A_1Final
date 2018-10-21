using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;



namespace FIT5032A_1Final.Controllers
{
    // https://github.com/sendgrid/sendgrid-csharp

    //The code is taken from Week 8 and the github document has been followed
    public class EmailController : Controller
    {
        // API KEY of SendGrid is here.
        private const String API_KEY = "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";

        // The below method just used to send an email with 3 parameter pass to it
        public void Send(String toEmail, String subject, String contents)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("sunny33p@gmail.com", "");
            var to = new EmailAddress(toEmail, "admin@ihealth.com");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg);
        }
        // GET: Email
        public ActionResult Index()
        {
            return View();
        }
    }
}