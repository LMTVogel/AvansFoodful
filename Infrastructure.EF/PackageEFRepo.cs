using Core.Domain;
using Core.DomainServices.Repos.Intf;
using Core.DomainServices.Services.Intf;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF
{
    public class PackageEFRepo : IPackageRepo
    {
        private readonly MainDbContext _context;

        public PackageEFRepo(MainDbContext context)
        {
            _context = context;
        }
        
        public Package CreatePackage(Package package)
        {
            package.Id = 0;

            _context.Packages.Add(package);
            _context.SaveChanges();
            return package;
        }

        public Package DeletePackage(Package package)
        {
            _context.Packages.Remove(package);
            _context.SaveChanges();
            return package;
        }

        public async Task<string> ReservePackage(Package package)
        {
            
            _context.Update(package);
            if (_context.SaveChanges() > 0)
            {
                return "success";
            }

            return "error";
        }

        public IQueryable<Package> GetAllPackages()
        {
            return _context.Packages.Include(p => p.Products).Include(c => c.Canteen);
        }

        public IQueryable<Package> GetAllUnreservedPackages()
        {
            return _context.Packages.Where(p => p.Student == null).Include(c => c.Canteen);
        }

        public Package GetPackage(int id)
        {
            return _context.Packages.Include(c => c.Canteen)
                .Include(i => i.Products)
                .FirstOrDefault(p => p.Id == id);
        }
    }
}
