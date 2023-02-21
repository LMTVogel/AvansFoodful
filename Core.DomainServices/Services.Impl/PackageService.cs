using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain;
using Core.DomainServices.Services.Intf;
using Core.DomainServices.Repos.Intf;

namespace Core.DomainServices.Services.Impl
{
    public class PackageService : IPackageService
    {
        private readonly IPackageRepo _packageRepo;
        private readonly IStudentRepo _studentRepo;

        public PackageService(IPackageRepo packageRepo, IStudentRepo studentRepo)
        {
            _packageRepo = packageRepo;
            _studentRepo = studentRepo;
        }

        public async Task<string> PackageChecker(int packageId, int studentId)
        {
            var student = _studentRepo.GetStudent(studentId);
            var requestedPackage = _packageRepo.GetPackage(packageId);
            var reservedPackages = _studentRepo.GetStudentReservations(student.Email);
            var packageAvailable = false;

            if (requestedPackage.Student == null)
            {
                if (reservedPackages.Count() != 0)
                {
                    foreach (var reservedPackage in reservedPackages)
                    {
                        if (requestedPackage.MaxPickupTime.Date != reservedPackage.MaxPickupTime.Date)
                        {
                            packageAvailable = true;
                        }
                        else
                        {
                            return "limited";
                        }
                    }
                }
                else
                {
                    packageAvailable = true;
                }
            }

            if (packageAvailable)
            {
                if (requestedPackage.ContainsAlcohol)
                {
                    if (student.BirthDate.AddYears(18).Date <= requestedPackage.MaxPickupTime.Date)
                    {
                        requestedPackage.Student = student;
                        return await _packageRepo.ReservePackage(requestedPackage);
                    }

                    return "minor";
                }

                requestedPackage.Student = student;
                return await _packageRepo.ReservePackage(requestedPackage);
            }

            return "taken";
        }

        public Dictionary<string, string> PackageErrorMessages(Package package)
        {
            var errorsList = new Dictionary<string, string>();

            if (package.Name == null || package.Name == "")
            {
                errorsList.Add("name", "Pakketnaam is nog niet ingevuld");
            }

            if (package.Products.Count == 0)
            {
                errorsList.Add("products", "Pakket bevat nog geen producten");
            }

            return errorsList;
        }

        public async Task<string> CreatePackage(Package package)
        {
            var errors = PackageErrorMessages(package);

            if (errors.Count == 0)
            {
                foreach (var product in package.Products)
                {
                    if (product.ContainsAlcohol)
                    {
                        package.ContainsAlcohol = true;
                    }
                }

                if (package is { IsMealHot: true, Canteen.HotMeals: false })
                {
                    return "Hot meals not allowed";
                }
                
                _packageRepo.CreatePackage(package);
                return "success";
            }

            return "error";
        }

        public async Task<string> UpdatePackage(Package package)
        {
            var entityToUpdate = _packageRepo.GetPackage(package.Id);
            string deletePackageResult = await DeletePackage(entityToUpdate);
            
            if (deletePackageResult == "success")
            {
                _packageRepo.CreatePackage(package);
                return deletePackageResult;
            }
            
            if (deletePackageResult == "already-reserved")
            {
                return deletePackageResult;
            }

            return "error";
        }

        public async Task<string> DeletePackage(Package package)
        {
            if (package != null)
            {
                if (package.Student == null)
                {
                    if (_packageRepo.DeletePackage(package) != null)
                    {
                        return "success";
                    }
                }
                
                return "already-reserved";
            }
            
            return "error";
        }
    }
}
