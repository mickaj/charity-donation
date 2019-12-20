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
        private readonly IInstitutionsService _institutionsService;

        public DonationController(IDonationsService donationsService, IInstitutionsService institutionsService)
        {
            _donationsService = donationsService;
            _institutionsService = institutionsService;
        }

        [HttpGet]
        public IActionResult Donate([FromServices]DonationFormModel vm)
        {
            vm.FillCategories(_donationsService.GetCategories());
            vm.FillInstitutions(_institutionsService.GetInstitutions());

            ViewBag.ShowFormSlogan = true;
            return View(vm);
        }

        [HttpPost]
        public IActionResult Donate()
        {
            var x = HttpContext.Request;
            return Content("donate form- post");
        }
    }
}