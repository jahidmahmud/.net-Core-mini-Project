using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PracticeCore.Models;
using PracticeCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeCore.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly IEmailService _emailService;

        public HomeController(IEmailService emailService)
        {
            _emailService = emailService;
        }
        [Route("~/")]
        public async Task<IActionResult> Index()
        {
            //mail send
            //UserEmailOptions options = new UserEmailOptions()
            //{
            //    ToEmails = new List<string>() { "test@gmail.com" },
            //    PlaceHolders = new List<KeyValuePair<string, string>>()
            //    {
            //        new KeyValuePair<string, string>("{{UserName}}","Jahid Mahmud")
            //    }
            //};
            //await _emailService.SendTestEmail(options);
            ViewData["title"] = "Lets Go";
            ViewData["book"] = new Book() { id = 2 };
            return View();
        }
        //[Route("Contact-us")]
        public IActionResult GetView()
        {
            return View();
        }
        [Authorize(Roles ="Admin")]
        //[Route("about-us")]
        public IActionResult AboutUs()
        {
            return View();
        }
    }
}
