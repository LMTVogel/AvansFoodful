using Core.Domain;
using Core.DomainServices.Repos.Intf;
using Core.DomainServices.Services.Intf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portal.Models;

namespace Portal.Controllers;

[Authorize(Roles = "Employee")]
public class CanteenController : Controller
{
    private readonly IPackageRepo _packageRepo;
    private readonly IEmployeeRepo _employeeRepo;
    private readonly IProductRepo _productRepo;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IPackageService _packageService;

    public CanteenController(
        IPackageRepo packageRepo,
        IEmployeeRepo employeeRepo,
        IProductRepo productRepo,
        UserManager<IdentityUser> userManager,
        IPackageService packageService)
    {
        _packageRepo = packageRepo;
        _employeeRepo = employeeRepo;
        _productRepo = productRepo;
        _userManager = userManager;
        _packageService = packageService;
    }

    public async Task<IActionResult> PackagesOverview(string filter)
    {
        var employee = await GetEmployeeInfo();

        Console.WriteLine(filter);

        if (filter == "all")
        {
            return View(_packageRepo.GetAllPackages().OrderBy(p => p.MaxPickupTime));
        }

        return View(_packageRepo.GetAllPackages().Where(p => p.CanteenId == employee.CanteenId).OrderBy(p => p.MaxPickupTime));
    }

    public async Task<Employee> GetEmployeeInfo()
    {
        var employeeId = _userManager.GetUserId(HttpContext.User);
        var employee = await _userManager.FindByIdAsync(employeeId);
        return _employeeRepo.GetEmployeeByEmail(employee.Email);
    }

    public async Task<IActionResult> DeletePackage(Package package)
    {
        await _packageService.DeletePackage(package);
        return RedirectToAction("PackagesOverview", "Canteen");
    }

    [HttpGet]
    public async Task<IActionResult> CreatePackage()
    {
        var employee = await GetEmployeeInfo();
        ViewBag.Employee = employee;

        var packageViewModel = new CreatePackageViewModel
        {
            ProductCheckboxes = new List<ProductCheckboxViewModel>(),
            Errors = new Dictionary<string, string>()
        };

        foreach (var product in _productRepo.GetAllProducts())
        {
            packageViewModel.ProductCheckboxes.Add(
                new ProductCheckboxViewModel
                {
                    Product = product
                });
        }

        return View(packageViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePackage(CreatePackageViewModel viewModel)
    {
        var products = new List<Product>();
        var employee = await GetEmployeeInfo();
        ViewBag.Employee = employee;

        if (viewModel.SelectedProductsList != null)
        {
            foreach (var product in viewModel.SelectedProductsList)
            {
                products.Add(_productRepo.GetProduct(Int32.Parse(product)));
            }
        }

        Package package = new Package
        {
            Name = viewModel.Name,
            CanteenId = viewModel.CanteenId,
            MaxPickupTime = viewModel.MaxPickupTime,
            ContainsAlcohol = false,
            Price = viewModel.Price,
            IsMealHot = viewModel.IsMealHot,
            Products = products
        };

        viewModel.Errors = _packageService.PackageErrorMessages(package);

        if (viewModel.Errors.Count() != 0)
        {
            return View(CreateCheckboxes(viewModel));
        }
        
        await _packageService.CreatePackage(package);
        return RedirectToAction("PackagesOverview", "Canteen");
    }

    [HttpGet]
    public async Task<IActionResult> UpdatePackage(int id)
    {
        var package = _packageRepo.GetPackage(id);
        var employee = await GetEmployeeInfo();
        ViewBag.Employee = employee;

        var viewModel = new CreatePackageViewModel
        {
            Id = id,
            Name = package.Name,
            CanteenId = package.CanteenId,
            MaxPickupTime = package.MaxPickupTime,
            ContainsAlcohol = package.ContainsAlcohol,
            Price = package.Price,
            IsMealHot = package.IsMealHot,
            ProductCheckboxes = new List<ProductCheckboxViewModel>(),
            SelectedProductsList = new List<string>(),
            Errors = new Dictionary<string, string>()
        };

        foreach (var product in _productRepo.GetAllProducts())
        {
            viewModel.ProductCheckboxes.Add(
                new ProductCheckboxViewModel
                {
                    Product = product,
                    IsChecked = package.Products.Contains(product)
                });

            if (package.Products.Contains(product))
            {
                viewModel.SelectedProductsList.Add(package.Id.ToString());
            }
        }

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> UpdatePackage(int id, CreatePackageViewModel viewModel)
    {
        var products = new List<Product>();
        var employee = await GetEmployeeInfo();
        ViewBag.Employee = employee;

        if (viewModel.SelectedProductsList != null)
        {
            foreach (var product in viewModel.SelectedProductsList)
            {
                products.Add(_productRepo.GetProduct(Int32.Parse(product)));
            }
        }

        Package package = new Package
        {
            Id = id,
            Name = viewModel.Name,
            CanteenId = viewModel.CanteenId,
            MaxPickupTime = viewModel.MaxPickupTime,
            ContainsAlcohol = products.FirstOrDefault(p => p.ContainsAlcohol) != null,
            Price = viewModel.Price,
            IsMealHot = viewModel.IsMealHot,
            Products = products
        };

        viewModel.Errors = _packageService.PackageErrorMessages(package);

        if (viewModel.Errors.Count() != 0)
        {
            return View(CreateCheckboxes(viewModel));
        }

        await _packageService.UpdatePackage(package);
        return RedirectToAction("PackagesOverview", "Canteen");
    }

    private CreatePackageViewModel CreateCheckboxes(CreatePackageViewModel viewModel)
    {
        foreach (var product in _productRepo.GetAllProducts())
        {
            if (viewModel.ProductCheckboxes == null) viewModel.ProductCheckboxes = new List<ProductCheckboxViewModel>();
            if (viewModel.SelectedProductsList == null) viewModel.SelectedProductsList = new List<string>();

            viewModel.ProductCheckboxes.Add(
                new ProductCheckboxViewModel
                {
                    Product = product,
                    IsChecked = viewModel.SelectedProductsList.Contains(product.Id.ToString())
                });
        }

        return viewModel;
    }
}