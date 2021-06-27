using Microsoft.AspNetCore.Mvc;
using PracticeCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeCore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["title"] = "Lets Go";
            ViewData["book"] = new Book() { id = 2 };
            return View();
        }
        public IActionResult GetView()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }
    }
}
