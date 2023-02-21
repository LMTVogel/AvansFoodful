
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain
{
    public class Package
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Canteen Canteen { get; set; }
        public int CanteenId { get; set; }
        public DateTime MaxPickupTime { get; set; }
        public bool ContainsAlcohol { get; set; }
        [Column(TypeName = "smallmoney")]
        public double Price { get; set; }
        public bool IsMealHot { get; set; }
        public Student? Student { get; set; }
        public int? StudentId { get; set; }
        public List<Product> Products { get; set; }
    }
}
