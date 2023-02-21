using Core.Domain;

namespace Core.DomainServices.Repos.Intf
{
    public interface IPackageRepo
    {
        Package CreatePackage(Package package);
        Package DeletePackage(Package package);
        Task<string> ReservePackage( Package package);
        IQueryable<Package> GetAllPackages();
        IQueryable<Package> GetAllUnreservedPackages();
        Package GetPackage(int id);
    }
}
