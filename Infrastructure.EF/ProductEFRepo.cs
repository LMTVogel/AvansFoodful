using Core.Domain;
using Core.DomainServices.Repos.Intf;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF
{
    public class ProductEFRepo : IProductRepo
    {
        private readonly MainDbContext _context;

        public ProductEFRepo(MainDbContext context)
        {
            _context = context;
        }

        public Product GetProduct(int id)
        {
            return _context.Products.Find(id);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.Include(p => p.Packages).ToList();
        }
    }
}
