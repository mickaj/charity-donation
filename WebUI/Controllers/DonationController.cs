using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Authorize]
    public class DonationController : Controller
    {
        [HttpGet]
        public IActionResult Donate()
        {
            ViewBag.ShowFormSlogan = true;
            return View();
        }

        [HttpPost]
        public IActionResult Donate(string x)
        {
            return Content("donate form- post");
        }
    }
}