using Core.Domain;
using Core.DomainServices.Repos.Intf;
using Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace API.GraphQL
{
    public class Query
    {
        private readonly IPackageRepo _packageRepo;

        public Query(IPackageRepo packageRepo)
        {
            _packageRepo = packageRepo;
        }

        public IQueryable<Package> GetAllPackages()
        {
            return _packageRepo.GetAllPackages();
        }
    }
}
