using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUI.Data.DataModel;
using WebUI.Data.Services.Interfaces;
using WebUI.Models;

namespace WebUI.Controllers
{
    [Authorize]
    public class DonationController : Controller
    {
        private readonly IDonationsService _donationsService;
        private readonly IInstitutionsService _institutionsService;
        private readonly UserManager<CharityUser> _userManager;

        public DonationController(IDonationsService donationsService, IInstitutionsService institutionsService, UserManager<CharityUser> userManager)
        {
            _donationsService = donationsService;
            _institutionsService = institutionsService;
            _userManager = userManager;
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
        public IActionResult DonateExecute([FromForm]DonationFormModel vm)
        {
            var userId = _userManager.GetUserId(User);
            var cats = HttpContext.Request.Form["CategoriesString"];
            var catsInts = StringArrayToIntArray(cats);
            var date = DateTime.Parse(vm.CollectionData.Date + "T" + vm.CollectionData.Time);
            _donationsService.AddDonation(userId, vm.SelectedInstitutionId, vm.CollectionData.Street, vm.CollectionData.City, vm.CollectionData.ZipCode, date, vm.CollectionData.Notes, vm.NumberOfBags, catsInts);
            return Content("donate form- post");
        }

        private int[] StringArrayToIntArray(IEnumerable<string> strings)
        {
            var result = new List<int>();
            foreach(var s in strings)
            {
                if(int.TryParse(s, out int i)) { result.Add(i); }
            }
            return result.ToArray();
        }
    }
}