using Core.Domain;

namespace Core.DomainServices.Repos.Intf
{
    public interface IStudentRepo
    {
        Student GetStudent(int id);
        Student GetStudentByEmail(string email);
        IEnumerable<Package> GetStudentReservations(string email);
    }
}
