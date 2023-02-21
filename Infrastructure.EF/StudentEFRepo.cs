using Core.Domain;
using Core.DomainServices.Repos.Intf;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF
{
    public class StudentEFRepo : IStudentRepo
    {
        private readonly MainDbContext _context;

        public StudentEFRepo(MainDbContext context)
        {
            _context = context;
        }

        public Student GetStudent(int id)
        {
            return _context.Students.Find(id);
        }

        public Student GetStudentByEmail(string email)
        {
            return _context.Students.Where(student => 
                student.Email == email).FirstOrDefault();
        }

        public IEnumerable<Package> GetStudentReservations(string email)
        {
            var packages = from package in _context.Packages!.Include(p => p.Canteen).Include(p => p.Products)
                where package.Student!.Email == email
                select package;

            return packages.ToList();
        }
    }
}
