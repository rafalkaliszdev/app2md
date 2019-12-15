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
        private readonly IPersistenceService persistenceService;
        private readonly IEmailService emailService;

        public MainController(
            IConfiguration configuration,
            IPersistenceService persistenceService,
            IEmailService emailService)
        {
            this.configuration = configuration;
            this.persistenceService = persistenceService;
            this.emailService = emailService;
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
                var contactFormId = persistenceService.InsertAndFetchId(model);
                emailService.SendMail(model, contactFormId);
                return View("ThankYou", contactFormId);
            }

            // model is not valid, redisplay
            ViewBag.AvailableInterests = new Interests().ToSelectList();
            return View(model);
        }

        public IActionResult ValidateFirstName(string firstName)
        {
            return Json(persistenceService.IsNewName(firstName, NameType.FirstName));
        }

        public IActionResult ValidateLastName(string lastName)
        {
            return Json(persistenceService.IsNewName(lastName, NameType.LastName));
        }
    }
}
