using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF
{
    public class MainDbContext : DbContext
    {
        public DbSet<Canteen>? Canteens { get; set; }
        public DbSet<Employee>? Employees { get; set; }
        public DbSet<Package>? Packages { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<Student>? Students { get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> contextOptions) : base(contextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            IEnumerable<Canteen> canteens = new List<Canteen>
            {
                new Canteen { Id = 1, CanteenName = "LA", City = "Breda", HotMeals = true },
                new Canteen { Id = 2, CanteenName = "LD", City = "Breda", HotMeals = false },
                new Canteen { Id = 3, CanteenName = "HA", City = "Breda", HotMeals = true },
                new Canteen { Id = 4, CanteenName = "TB", City = "Tilburg", HotMeals = false },
                new Canteen { Id = 5, CanteenName = "DB", City = "Den Bosch", HotMeals = false }
            };
            IEnumerable<Employee> employees = new List<Employee>
            {
                new Employee { Id = 1, Name = "Arend Vliet", Email = "arend@avans.nl", EmployeeNr = 1000, CanteenId = canteens.ToList()[0].Id },
                new Employee { Id = 2, Name = "Gerard Kok", Email = "gerard@avans.nl", EmployeeNr = 2000, CanteenId = canteens.ToList()[1].Id },
                new Employee { Id = 3, Name = "Thomas Trein", Email = "thomas@avans.nl", EmployeeNr = 3000, CanteenId = canteens.ToList()[2].Id }
            };
            IEnumerable<Product> products = new List<Product>
            {
                new Product { Id = 1, Name = "Banaan smoothie", ContainsAlcohol = false , ProductImage = "https://i.imgur.com/hJHKdzi.jpg"},
                new Product { Id = 2, Name = "Broodje bacon, ei, kaas", ContainsAlcohol = false , ProductImage = "https://i.imgur.com/rA8TaIL.jpg"},
                new Product { Id = 3, Name = "Broodje Unox", ContainsAlcohol = false , ProductImage = "https://i.imgur.com/BcdzaoG.jpg"},
                new Product { Id = 4, Name = "Cheeseburger", ContainsAlcohol = false , ProductImage = "https://i.imgur.com/YX5YYKB.jpg"},
                new Product { Id = 5, Name = "Plant based burger", ContainsAlcohol = false , ProductImage = "https://i.imgur.com/K1LfUCP.jpg"},
                new Product { Id = 6, Name = "Gluhwein", ContainsAlcohol = true , ProductImage = "https://i.imgur.com/T5rn9hV.jpg"},
                new Product { Id = 7, Name = "Banaan", ContainsAlcohol = false , ProductImage = "https://i.imgur.com/ccLWJP7.jpg"},
                new Product { Id = 8, Name = "Appel", ContainsAlcohol = false , ProductImage = "https://i.imgur.com/bMMCN0r.jpg"},
                new Product { Id = 9, Name = "Broodje gezond", ContainsAlcohol = false , ProductImage = "https://i.imgur.com/bUOh9Cc.jpg"},
                new Product { Id = 10, Name = "Tomatensoep", ContainsAlcohol = false , ProductImage = "https://i.imgur.com/CQKfs4O.jpg"},
                new Product { Id = 11, Name = "Kippensoep", ContainsAlcohol = false , ProductImage = "https://i.imgur.com/FOdxUth.jpg"}
            };
            IEnumerable<Package> packages = new List<Package>
            {
                new Package
                {
                    Id = 1, 
                    Name = "Gezond pakket", 
                    CanteenId = 1,
                    MaxPickupTime = new DateTime(2022, 12, 22, 15, 00, 00),
                    ContainsAlcohol = false,
                    Price = 6,
                    IsMealHot = true,
                    Products = new List<Product>()
                },
                new Package
                {
                    Id = 2, 
                    Name = "Lekker in de avond", 
                    CanteenId = 2,
                    MaxPickupTime = new DateTime(2022, 12, 22, 15, 00, 00),
                    ContainsAlcohol = false,
                    Price = 7,
                    IsMealHot = false,
                    Products = new List<Product>()
                }
            };
            IEnumerable<Student> students = new List<Student>
            {
                new Student
                {
                    Id = 1,
                    Name = "Luuk Vogel",
                    BirthDate = new DateTime(2001, 08, 27),
                    StudentNr = 2181163,
                    Email = "lmt.vogel@student.avans.nl",
                    City = "Breda",
                    PhoneNumber = "+31640942653"
                },
                new Student
                {
                    Id = 2,
                    Name = "Rogier van der Gaag",
                    BirthDate = new DateTime(2001, 04, 22),
                    StudentNr = 2181162,
                    Email = "rm.vandergaag@student.avans.nl",
                    City = "Breda",
                    PhoneNumber = "+31612345678"
                }
            };

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Canteen>().HasData(canteens);
            modelBuilder.Entity<Employee>().HasData(employees);
            modelBuilder.Entity<Product>().HasData(products);
            modelBuilder.Entity<Package>().HasData(packages);
            modelBuilder.Entity<Student>().HasData(students);

            modelBuilder.Entity<Package>()
                .HasMany(a => a.Products)
                .WithMany(b => b.Packages)
                .UsingEntity<Dictionary<string, object>>(
                    "PackageProduct",
                    c => c.HasOne<Product>().WithMany().HasForeignKey("ProductId"),
                    d => d.HasOne<Package>().WithMany().HasForeignKey("PackageId"),
                    e =>
                    {
                        e.HasKey("ProductId", "PackageId");
                        e.HasData(
                            new { ProductId = 1, PackageId = 1 },
                            new { ProductId = 7, PackageId = 1 }, 
                            new { ProductId = 8, PackageId = 1 },
                            new { ProductId = 9, PackageId = 1 },
                            new { ProductId = 3, PackageId = 2 },
                            new { ProductId = 6, PackageId = 2 },
                            new { ProductId = 10, PackageId = 2 });
                    });
        }
    }
}
