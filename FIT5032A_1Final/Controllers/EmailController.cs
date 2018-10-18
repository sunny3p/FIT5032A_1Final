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
    public class EmailController : Controller
    {
        // Please use your API KEY here.
        private const String API_KEY = "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";

        public void Send(String toEmail, String subject, String contents)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("sunny33p@gmail.com", "");
            var to = new EmailAddress(toEmail, "spat0013@student.monash.edu");
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