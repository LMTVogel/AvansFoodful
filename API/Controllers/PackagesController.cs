using API.Models;
using Core.Domain;
using Core.DomainServices.Repos.Intf;
using Core.DomainServices.Services.Intf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackagesController : ControllerBase
    {
        private readonly IPackageRepo _packageRepo;
        private readonly IPackageService _packageService;

        public PackagesController(IPackageRepo packageRepo, IPackageService packageService)
        {
            _packageRepo = packageRepo;
            _packageService = packageService;
        }

        [HttpGet]
        public IQueryable<Package> GetAllPackages()
        {
            return _packageRepo.GetAllPackages();
        }

        [HttpPost(Name = "ReservePackage")]
        public async Task<PackageViewModel> ReservePackage(int packageId, int studentId)
        {
            var response = await _packageService.PackageChecker(packageId, studentId);
            PackageViewModel package = new PackageViewModel();
            
            switch (response)
            {
                case "success":
                    package.ResponseCode = StatusCodes.Status200OK;
                    package.ResponseMessage = "Pakket is succesvol gereserveerd!";
                    package.Package = _packageRepo.GetPackage(packageId);
                    return package;
                case "limited":
                    package.ResponseCode = StatusCodes.Status400BadRequest;
                    package.ResponseMessage = "Op de afhaaldatum van het gekozen pakket is al een pakket gereserveerd.";
                    return package;
                case "minor":
                    package.ResponseCode = StatusCodes.Status400BadRequest;
                    package.ResponseMessage = "Je moet minimaal 18 jaar oud zijn voor dit pakket.";
                    return package;
                case "error":
                    package.ResponseCode = StatusCodes.Status500InternalServerError;
                    package.ResponseMessage = "Er is iets fout gegaan. Probeer het later opnieuw.";
                    return package;
                case "taken":
                    package.ResponseCode = StatusCodes.Status400BadRequest;
                    package.ResponseMessage = "Pakket is al gereserveerd. Excuses voor het ongemak.";
                    return package;
                default:
                    package.ResponseCode = StatusCodes.Status500InternalServerError;
                    package.ResponseMessage = "Er is iets fout gegaan. Probeer het later opnieuw.";
                    return package;
            }
        }
    }
}
