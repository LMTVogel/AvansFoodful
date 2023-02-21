using Core.Domain;
using Core.DomainServices.Repos.Intf;

namespace Infrastructure.EF
{
    public class CanteenEFRepo : ICanteenRepo
    {
        private readonly MainDbContext _context;

        public Canteen GetCanteen(int id)
        {
            return _context.Canteens.Find(id);
        }
    }
}
