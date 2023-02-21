using Core.Domain;

namespace Core.DomainServices.Repos.Intf
{
    public interface IProductRepo
    {
        Product GetProduct(int id);
        IEnumerable<Product> GetAllProducts();
    }
}
