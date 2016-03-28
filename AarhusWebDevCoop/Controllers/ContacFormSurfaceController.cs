using AarhusWebDevCoop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using System.Net.Mail;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;

namespace AarhusWebDevCoop.Controllers
{
    
    public class ContacFormSurfaceController : SurfaceController
    {
        // GET: ContacFormSurface
        
        public ActionResult Index()
        {
            return PartialView("ContactForm", new ContactForm());
        }
        [HttpPost]
        public ActionResult HandleFormSubmit(ContactForm model)
        {
            if (!ModelState.IsValid)
                return CurrentUmbracoPage();
            // send email

                MailMessage message = new MailMessage();
            message.To.Add("dan_claw20@yahoo.com");
            message.Subject = model.Subject;
            message.From = new MailAddress(model.Email, model.Name);
            message.Body = model.Message;
            SmtpClient smtp = new SmtpClient();
            smtp.Send(message);

            TempData["success"] = true;

            
        IContent comment = Services.ContentService.CreateContent(model.Subject, CurrentPage.Id, "Message");

            comment.SetValue("name", model.Name);
             comment.SetValue("email", model.Email);
            comment.SetValue("subject", model.Subject);
             comment.SetValue("message", model.Message);

            //Save
            Services.ContentService.Save(comment);

            return RedirectToCurrentUmbracoPage();

        }

    }
}
