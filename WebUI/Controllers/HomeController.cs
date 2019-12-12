using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebUI.Data.Services.Interfaces;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IInstitutionsService _institutionsService;

        public HomeController(ILogger<HomeController> logger, IInstitutionsService institutionsService)
        {
            _logger = logger;
            _institutionsService = institutionsService;
        }

        public IActionResult Index([FromServices]IndexViewModel vm)
        {
            vm.WhoWeHelpViewModel.AddInstitutions(_institutionsService.GetInstitutions());
            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
