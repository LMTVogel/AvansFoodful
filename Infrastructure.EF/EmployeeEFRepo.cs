using Core.Domain;
using Core.DomainServices.Repos.Intf;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF
{
    public class EmployeeEFRepo : IEmployeeRepo
    {
        private readonly MainDbContext _context;

        public EmployeeEFRepo(MainDbContext context)
        {
            _context = context;
        }

        public Employee GetEmployeeById(int id)
        {
            return _context.Employees.Find(id);
        }

        public Employee GetEmployeeByEmail(string email)
        {
            return _context.Employees.Where(a => a.Email == email)
                .Include(c => c.Canteen).FirstOrDefault();
        }
    }
}
