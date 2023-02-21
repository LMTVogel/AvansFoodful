using Core.Domain;

namespace Core.DomainServices.Repos.Intf
{
    public interface IEmployeeRepo
    {
        Employee GetEmployeeById(int id);
        Employee GetEmployeeByEmail(string email);
    }
}
