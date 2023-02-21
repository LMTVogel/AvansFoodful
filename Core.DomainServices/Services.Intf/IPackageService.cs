using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain;

namespace Core.DomainServices.Services.Intf
{
    public interface IPackageService
    {
        Task<string> PackageChecker(int packageId, int studentId);
        Dictionary<string, string> PackageErrorMessages(Package package);
        Task<string> CreatePackage(Package package);
        Task<string> UpdatePackage(Package package);
        Task<string> DeletePackage(Package package);
    }
}
