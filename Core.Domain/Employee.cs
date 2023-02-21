
namespace Core.Domain
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int EmployeeNr { get; set; }
        public Canteen Canteen { get; set; }
        public int CanteenId { get; set; }
    }
}
