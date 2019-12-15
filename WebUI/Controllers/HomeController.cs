using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebUI.Data.Services.Interfaces;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IInstitutionsService _institutionsService;
        private readonly IDonationsService _donationsService;
        private readonly IMessagesService _messagesService;

        public HomeController(ILogger<HomeController> logger, IInstitutionsService institutionsService, IDonationsService donationsService, IMessagesService messagesService, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _institutionsService = institutionsService;
            _donationsService = donationsService;
            _messagesService = messagesService;
        }

        [HttpGet]
        public IActionResult Index([FromServices]IndexViewModel vm)
        {
            int.TryParse(_configuration["AppSettings:NumberOfInstitutionsOnIndexPage"], out int numberOfInstitutions);
            vm.WhoWeHelpViewModel.AddInstitutions(_institutionsService.GetRandomInstitutions(numberOfInstitutions));
            vm.WhoWeHelpViewModel.ShowAllLink = _institutionsService.GetInstitutionsCount() > numberOfInstitutions;
            vm.StatsViewModel.BagsDonatedQty = _donationsService.GetNumberOfDonatedBags();
            vm.StatsViewModel.RecieversQty = _donationsService.GetNumberOfRecievers();

            ViewBag.ShowSlogan = true;
            return View(vm);
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet][ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult ShowAllInstitutions([FromServices]WhoWeHelpViewModel vm)
        {
            vm.AddInstitutions(_institutionsService.GetInstitutions());
            return PartialView("_WhoWeHelpPartial", vm);
        }

        [HttpPost]
        public IActionResult PostMessage([FromForm]MessageData messageData)
        {
            _messagesService.AddMessage(messageData.Name, messageData.LastName, messageData.Message);
            return PartialView("_MessagePostedPartial");
        }
    }
}
