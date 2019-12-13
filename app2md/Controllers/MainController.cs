using app2md.Enums;
using app2md.Extensions;
using app2md.Models;
using app2md.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

namespace app2md.Controllers
{
    public class MainController : Controller
    {
        private readonly IConfiguration configuration;

        public MainController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet]
        public IActionResult ContactForm()
        {
            var model = new ContactFormViewModel();
            ViewBag.AvailableInterests = new Interests().ToSelectList();
            return View(model);
        }

        [HttpPost]
        public IActionResult ContactForm(ContactFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                var contactFormId = PersistenceService.InsertAndReturnID(model, configuration);
                EmailService.SendMail(model, contactFormId, configuration);
                return View("ThankYou", contactFormId);
            }

            // model is not valid, redisplay
            ViewBag.AvailableInterests = new Interests().ToSelectList();
            return View(model);
        }

        public IActionResult ValidateFirstName(string firstName)
        {
            return Json(HasOnlyAcceptedLetters(firstName));
        }

        public IActionResult ValidateLastName(string lastName)
        {
            return Json(HasOnlyAcceptedLetters(lastName));
        }

        [NonAction]
        private bool HasOnlyAcceptedLetters(string name)
        {
            // quick example of remote validation 
            // in real life there would be check against database

            // arbitrally I do not accept "xvc" letters
            var hasBannedLetters = Regex.IsMatch(name, "[xv]");
            return !hasBannedLetters;
        }
    }
}
