using Microsoft.AspNetCore.Mvc;
using Portal.Models;
using System.Diagnostics;
using Core.DomainServices.Repos.Intf;
using Core.DomainServices.Services.Intf;
using Microsoft.AspNetCore.Authorization;

namespace Portal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPackageRepo _packageRepo;
        private readonly IStudentRepo _studentRepo;
        private readonly IPackageService _packageService;

        public HomeController(
            ILogger<HomeController> logger,
            IPackageRepo packageRepo,
            IStudentRepo studentRepo,
            IPackageService packageService)
        {
            _logger = logger;
            _packageRepo = packageRepo;
            _studentRepo = studentRepo;
            _packageService = packageService;
        }

        [AllowAnonymous]
        public IActionResult Index(string response = null)
        {
            string alertDanger = null;
            string alertSuccess = null;

            switch (response)
            {
                case "limited":
                    alertDanger = "Op de afhaaldatum van het gekozen pakket is al een pakket gereserveerd.";
                    break;
                case "taken":
                    alertDanger = "Pakket is al gereserveerd. Excuses voor het ongemak.";
                    break;
                case "minor":
                    alertDanger = "Je moet minimaal 18 jaar oud zijn voor dit pakket.";
                    break;
                case "success":
                    alertSuccess = "Pakket is gereserveerd!";
                    break;
                case "login":
                    alertSuccess = "Je bent ingelogd!";
                    break;
                case "logout":
                    alertDanger = "Je bent uitgelogd!";
                    break;
                case "error":
                    alertDanger = "Er is iets fout gegaan. Probeer het later opnieuw.";
                    break;
            }
            if (alertDanger != null)
            {
                ViewBag.AlertDanger = alertDanger;
            }
            else if (alertSuccess != null)
            {
                ViewBag.AlertSuccess = alertSuccess;
            }

            return View(_packageRepo.GetAllUnreservedPackages());
        }

        [AllowAnonymous]
        public IActionResult PackageDetails(int id)
        {
            return View(_packageRepo.GetPackage(id));
        }

        public async Task<IActionResult> ReservePackage(int packageId)
        {
            var student = _studentRepo.GetStudentByEmail(User.Identity.Name);
            
            // Geeft een string terug met de status van het reserveren van het pakket.
            var response = await _packageService.PackageChecker(packageId, student.Id);

            return RedirectToAction("Index", "Home", new { response });
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