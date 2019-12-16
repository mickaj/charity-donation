using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebUI.Data.Services.Interfaces;
using WebUI.Models;

namespace WebUI.Controllers
{
    [Authorize]
    public class DonationController : Controller
    {
        private readonly IDonationsService _donationsService;

        public DonationController(IDonationsService donationsService)
        {
            _donationsService = donationsService;
        }

        [HttpGet]
        public IActionResult Donate([FromServices]DonationFormModel vm)
        {
            vm.FillCategories(_donationsService.GetCategories());            

            ViewBag.ShowFormSlogan = true;
            return View(vm);
        }

        [HttpPost]
        public IActionResult Donate(string x)
        {
            return Content("donate form- post");
        }
    }
}