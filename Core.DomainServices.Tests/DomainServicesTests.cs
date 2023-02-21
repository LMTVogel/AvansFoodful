using System.Collections;
using Core.Domain;
using Core.DomainServices.Repos.Intf;
using Core.DomainServices.Services.Impl;
using Core.DomainServices.Services.Intf;
using Moq;

namespace Core.DomainServices.Tests
{
    public class DomainServicesTests
    {
        [Fact]
        public void US1_Get_All_Unreserved_Packages_And_Get_Own_Reserved_Packages()
        {
            // Arrange
            var packageRepo = new Mock<IPackageRepo>();
            var studentRepo = new Mock<IStudentRepo>();

            var student = new Student
            {
                Id = 1,
                Name = "Luuk Vogel",
                BirthDate = new DateTime(2001, 08, 27),
                StudentNr = 2181163,
                Email = "lmt.vogel@student.avans.nl",
                City = "Breda",
                PhoneNumber = "+31640942653"
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
                    IsMealHot = false,
                    Products = new List<Product>(),
                    StudentId = student.Id,
                    Student = student
                },
                new Package
                {
                    Id = 2,
                    Name = "Lekker in de avond",
                    CanteenId = 2,
                    MaxPickupTime = new DateTime(2022, 12, 22, 15, 00, 00),
                    ContainsAlcohol = false,
                    Price = 7,
                    IsMealHot = true,
                    Products = new List<Product>()
                }
            };

            // Act
            packageRepo.Setup(x => x.GetAllUnreservedPackages())
                .Returns(packages.Where(p => p.Student == null).AsQueryable());
            studentRepo.Setup(x => x.GetStudentByEmail(student.Email)).Returns(student);
            studentRepo.Setup(x => x.GetStudentReservations(student.Email))
                .Returns(packages.Where(p => p.StudentId == student.Id).AsEnumerable());

            // Assert
            Assert.Equal(1, packageRepo.Object.GetAllUnreservedPackages().Count());
            Assert.Equal(1, studentRepo.Object.GetStudentReservations(student.Email).Count());
        }

        [Fact]
        public void US3_Employee_Can_Delete_Package_When_Unreserved()
        {
            // Arrange
            var packageRepo = new Mock<IPackageRepo>();
            var studentRepo = new Mock<IStudentRepo>();
            var packageService = new PackageService(packageRepo.Object, studentRepo.Object);
            var student = new Student
            {
                Id = 1,
                Name = "Luuk Vogel",
                BirthDate = new DateTime(2001, 08, 27),
                StudentNr = 2181163,
                Email = "lmt.vogel@student.avans.nl",
                City = "Breda",
                PhoneNumber = "+31640942653"
            };
            var package = new Package
            {
                Id = 1,
                Name = "Gezond pakket",
                CanteenId = 1,
                MaxPickupTime = new DateTime(2022, 12, 22, 15, 00, 00),
                ContainsAlcohol = false,
                Price = 6,
                IsMealHot = false,
                Products = new List<Product>(),
            };

            // Act
            packageRepo.Setup(x => x.DeletePackage(package)).Returns(package);
            var response = packageService.DeletePackage(package);

            // Assert
            Assert.Equal("success", response.Result);
        }

        [Fact]
        public void US3_Employee_Cant_Delete_Package_When_Reserved()
        {
            // Arrange
            var packageRepo = new Mock<IPackageRepo>();
            var studentRepo = new Mock<IStudentRepo>();
            var packageService = new PackageService(packageRepo.Object, studentRepo.Object);
            var student = new Student
            {
                Id = 1,
                Name = "Luuk Vogel",
                BirthDate = new DateTime(2001, 08, 27),
                StudentNr = 2181163,
                Email = "lmt.vogel@student.avans.nl",
                City = "Breda",
                PhoneNumber = "+31640942653"
            };
            var package = new Package
            {
                Id = 1,
                Name = "Gezond pakket",
                CanteenId = 1,
                MaxPickupTime = new DateTime(2022, 12, 22, 15, 00, 00),
                ContainsAlcohol = false,
                Price = 6,
                IsMealHot = false,
                Products = new List<Product>(),
                StudentId = student.Id,
                Student = student
            };

            // Act
            packageRepo.Setup(x => x.DeletePackage(package)).Returns(package);
            var response = packageService.DeletePackage(package);

            // Assert
            Assert.Equal("already-reserved", response.Result);
        }

        [Fact]
        public async void US4_Package_Has_Alcohol_Indication()
        {
            // Arrange
            var packageRepo = new Mock<IPackageRepo>();
            var productRepo = new Mock<IProductRepo>();
            var studentRepo = new Mock<IStudentRepo>();
            var packageService = new PackageService(packageRepo.Object, studentRepo.Object);
            var canteen = new Canteen
            {
                Id = 1,
                CanteenName = "LA",
                City = "Breda",
                HotMeals = true
            };
            var product1 = new Product
            {
                Id = 5,
                Name = "Plant based burger",
                ContainsAlcohol = false,
                ProductImage = "https://i.imgur.com/K1LfUCP.jpg"
            };
            var product2 = new Product
            {
                Id = 6,
                Name = "Gluhwein",
                ContainsAlcohol = true,
                ProductImage = "https://i.imgur.com/T5rn9hV.jpg"
            };
            var package = new Package
            {
                Id = 1,
                Name = "Gezond pakket",
                CanteenId = canteen.Id,
                Canteen = canteen,
                MaxPickupTime = new DateTime(2023, 01, 23),
                ContainsAlcohol = false,
                Price = 6,
                IsMealHot = false,
                Products = new List<Product> { product1, product2 },
                StudentId = null,
                Student = null
            };

            // Act
            packageRepo.Setup(x => x.CreatePackage(package)).Returns(package);
            await packageService.CreatePackage(package);

            // Assert
            Assert.True(package.ContainsAlcohol);
        }

        [Fact]
        public async void US4_Student_Cant_Reserve_Package_With_Alcohol_When_Underage()
        {
            // Arrange
            var packageRepo = new Mock<IPackageRepo>();
            var studentRepo = new Mock<IStudentRepo>();
            var packageService = new PackageService(packageRepo.Object, studentRepo.Object);
            var canteen = new Canteen
            {
                Id = 1,
                CanteenName = "LA",
                City = "Breda",
                HotMeals = true
            };
            var student = new Student
            {
                Id = 1,
                Name = "Luuk Vogel",
                BirthDate = new DateTime(2005, 01, 25),
                StudentNr = 2181163,
                Email = "lmt.vogel@student.avans.nl",
                City = "Breda",
                PhoneNumber = "+31640942653"
            };
            var product1 = new Product
            {
                Id = 5,
                Name = "Plant based burger",
                ContainsAlcohol = false,
                ProductImage = "https://i.imgur.com/K1LfUCP.jpg"
            };
            var product2 = new Product
            {
                Id = 6,
                Name = "Gluhwein",
                ContainsAlcohol = true,
                ProductImage = "https://i.imgur.com/T5rn9hV.jpg"
            };
            var package = new Package
            {
                Id = 1,
                Name = "Gezond pakket",
                CanteenId = canteen.Id,
                Canteen = canteen,
                MaxPickupTime = new DateTime(2023, 01, 23),
                ContainsAlcohol = true,
                Price = 6,
                IsMealHot = false,
                Products = new List<Product> { product1, product2 },
                StudentId = null,
                Student = null
            };

            // Act
            studentRepo.Setup(x => x.GetStudent(student.Id)).Returns(student);
            packageRepo.Setup(x => x.GetPackage(package.Id)).Returns(package);
            
            var response = packageService.PackageChecker(package.Id, student.Id);

            // Assert
            Assert.Equal("minor", await response);
        }

        [Fact]
        public async void US4_Student_Can_Reserve_Package_With_Alcohol_When_Of_Age()
        {
            // Arrange
            var packageRepo = new Mock<IPackageRepo>();
            var studentRepo = new Mock<IStudentRepo>();
            var packageService = new PackageService(packageRepo.Object, studentRepo.Object);
            var canteen = new Canteen
            {
                Id = 1,
                CanteenName = "LA",
                City = "Breda",
                HotMeals = true
            };
            var student = new Student
            {
                Id = 1,
                Name = "Luuk Vogel",
                BirthDate = new DateTime(2004, 01, 23),
                StudentNr = 2181163,
                Email = "lmt.vogel@student.avans.nl",
                City = "Breda",
                PhoneNumber = "+31640942653"
            };
            var product1 = new Product
            {
                Id = 5,
                Name = "Plant based burger",
                ContainsAlcohol = false,
                ProductImage = "https://i.imgur.com/K1LfUCP.jpg"
            };
            var product2 = new Product
            {
                Id = 6,
                Name = "Gluhwein",
                ContainsAlcohol = true,
                ProductImage = "https://i.imgur.com/T5rn9hV.jpg"
            };
            var package = new Package
            {
                Id = 1,
                Name = "Gezond pakket",
                CanteenId = canteen.Id,
                Canteen = canteen,
                MaxPickupTime = new DateTime(2023, 01, 23),
                ContainsAlcohol = true,
                Price = 6,
                IsMealHot = false,
                Products = new List<Product> { product1, product2 },
                StudentId = null,
                Student = null
            };

            // Act
            studentRepo.Setup(x => x.GetStudent(student.Id)).Returns(student);
            packageRepo.Setup(x => x.GetPackage(package.Id)).Returns(package);
            studentRepo.Setup(x => x.GetStudentReservations(student.Email)).Returns(new List<Package>());
            packageRepo.Setup(x => x.ReservePackage(package)).ReturnsAsync("success");

            var response = packageService.PackageChecker(package.Id, student.Id);

            // Assert
            Assert.Equal("success", await response);
        }

        [Fact]
        public async void US5_Student_Cant_Reserve_Packages_On_Same_Pickup_Date()
        {
            var packageRepo = new Mock<IPackageRepo>();
            var studentRepo = new Mock<IStudentRepo>();
            var packageService = new PackageService(packageRepo.Object, studentRepo.Object);
            var canteen = new Canteen
            {
                Id = 1,
                CanteenName = "LA",
                City = "Breda",
                HotMeals = true
            };
            var student = new Student
            {
                Id = 1,
                Name = "Luuk Vogel",
                BirthDate = new DateTime(2004, 01, 23),
                StudentNr = 2181163,
                Email = "lmt.vogel@student.avans.nl",
                City = "Breda",
                PhoneNumber = "+31640942653"
            };
            var product1 = new Product
            {
                Id = 5,
                Name = "Plant based burger",
                ContainsAlcohol = false,
                ProductImage = "https://i.imgur.com/K1LfUCP.jpg"
            };
            var product2 = new Product
            {
                Id = 6,
                Name = "Banaan",
                ContainsAlcohol = false,
                ProductImage = "https://i.imgur.com/T5rn9hV.jpg"
            };
            var package1 = new Package
            {
                Id = 1,
                Name = "Gezond pakket 1",
                CanteenId = canteen.Id,
                Canteen = canteen,
                MaxPickupTime = new DateTime(2023, 01, 23),
                ContainsAlcohol = false,
                Price = 6,
                IsMealHot = false,
                Products = new List<Product> { product1, product2 },
                StudentId = null,
                Student = null
            };
            var package2 = new Package
            {
                Id = 2,
                Name = "Gezond pakket 2",
                CanteenId = canteen.Id,
                Canteen = canteen,
                MaxPickupTime = new DateTime(2023, 01, 23),
                ContainsAlcohol = false,
                Price = 6,
                IsMealHot = false,
                Products = new List<Product> { product1, product2 },
                StudentId = null,
                Student = null
            };

            studentRepo.Setup(x => x.GetStudent(student.Id)).Returns(student);
            packageRepo.Setup(x => x.GetPackage(package1.Id)).Returns(package1);
            packageRepo.Setup(x => x.ReservePackage(package1)).ReturnsAsync("success");
            studentRepo.Setup(x => x.GetStudentReservations(student.Email)).Returns(new List<Package>());


            Assert.Equal("success", await packageService.PackageChecker(package1.Id, student.Id));

            studentRepo.Setup(x => x.GetStudent(student.Id)).Returns(student);
            packageRepo.Setup(x => x.GetPackage(package2.Id)).Returns(package2);
            IEnumerable<Package> reservationList = new List<Package> { package1 };
            studentRepo.Setup(x => x.GetStudentReservations(student.Email)).Returns(reservationList);

            Assert.Equal("limited", await packageService.PackageChecker(package2.Id, student.Id));
        }

        [Fact]
        public async void US7_Student_Cant_Reserve_A_Reserved_Package()
        {
            // Arrange
            var packageRepo = new Mock<IPackageRepo>();
            var studentRepo = new Mock<IStudentRepo>();
            var packageService = new PackageService(packageRepo.Object, studentRepo.Object);
            var canteen = new Canteen
            {
                Id = 1,
                CanteenName = "LA",
                City = "Breda",
                HotMeals = true
            };
            var student = new Student
            {
                Id = 1,
                Name = "Luuk Vogel",
                BirthDate = new DateTime(2004, 01, 23),
                StudentNr = 2181163,
                Email = "lmt.vogel@student.avans.nl",
                City = "Breda",
                PhoneNumber = "+31640942653"
            };
            var product1 = new Product
            {
                Id = 5,
                Name = "Plant based burger",
                ContainsAlcohol = false,
                ProductImage = "https://i.imgur.com/K1LfUCP.jpg"
            };
            var product2 = new Product
            {
                Id = 6,
                Name = "Banaan",
                ContainsAlcohol = false,
                ProductImage = "https://i.imgur.com/T5rn9hV.jpg"
            };
            var package = new Package
            {
                Id = 1,
                Name = "Gezond pakket 1",
                CanteenId = canteen.Id,
                Canteen = canteen,
                MaxPickupTime = new DateTime(2023, 01, 23),
                ContainsAlcohol = false,
                Price = 6,
                IsMealHot = false,
                Products = new List<Product> { product1, product2 },
                StudentId = student.Id,
                Student = student
            };

            // Act
            studentRepo.Setup(x => x.GetStudent(student.Id)).Returns(student);
            packageRepo.Setup(x => x.GetPackage(package.Id)).Returns(package);

            // Assert
            Assert.Equal("taken", await packageService.PackageChecker(package.Id, student.Id));
        }

        [Fact]
        public async void US7_Student_Can_Reserve_An_Unreserved_Package()
        {
            // Arrange
            var packageRepo = new Mock<IPackageRepo>();
            var studentRepo = new Mock<IStudentRepo>();
            var packageService = new PackageService(packageRepo.Object, studentRepo.Object);
            var canteen = new Canteen
            {
                Id = 1,
                CanteenName = "LA",
                City = "Breda",
                HotMeals = true
            };
            var student = new Student
            {
                Id = 1,
                Name = "Luuk Vogel",
                BirthDate = new DateTime(2004, 01, 23),
                StudentNr = 2181163,
                Email = "lmt.vogel@student.avans.nl",
                City = "Breda",
                PhoneNumber = "+31640942653"
            };
            var product1 = new Product
            {
                Id = 5,
                Name = "Plant based burger",
                ContainsAlcohol = false,
                ProductImage = "https://i.imgur.com/K1LfUCP.jpg"
            };
            var product2 = new Product
            {
                Id = 6,
                Name = "Banaan",
                ContainsAlcohol = false,
                ProductImage = "https://i.imgur.com/T5rn9hV.jpg"
            };
            var package = new Package
            {
                Id = 1,
                Name = "Gezond pakket 1",
                CanteenId = canteen.Id,
                Canteen = canteen,
                MaxPickupTime = new DateTime(2023, 01, 23),
                ContainsAlcohol = false,
                Price = 6,
                IsMealHot = false,
                Products = new List<Product> { product1, product2 },
                StudentId = null,
                Student = null
            };

            // Act
            studentRepo.Setup(x => x.GetStudent(student.Id)).Returns(student);
            packageRepo.Setup(x => x.GetPackage(package.Id)).Returns(package);
            packageRepo.Setup(x => x.ReservePackage(package)).ReturnsAsync("success");
            studentRepo.Setup(x => x.GetStudentReservations(student.Email)).Returns(new List<Package>());

            // Assert
            Assert.Equal("success", await packageService.PackageChecker(package.Id, student.Id));
        }

        [Fact]
        public async void US9_Hot_Meal_Package_Cant_Be_Created_When_Canteen_Doesnt_Have_Hot_Meals()
        {
            var packageRepo = new Mock<IPackageRepo>();
            var studentRepo = new Mock<IStudentRepo>();
            var packageService = new PackageService(packageRepo.Object, studentRepo.Object);
            var canteen = new Canteen
            {
                Id = 1,
                CanteenName = "LD",
                City = "Breda",
                HotMeals = false
            };
            var product1 = new Product
            {
                Id = 5,
                Name = "Plant based burger",
                ContainsAlcohol = false,
                ProductImage = "https://i.imgur.com/K1LfUCP.jpg"
            };
            var product2 = new Product
            {
                Id = 6,
                Name = "Soep",
                ContainsAlcohol = false,
                ProductImage = "https://i.imgur.com/T5rn9hV.jpg"
            };
            var package = new Package
            {
                Id = 1,
                Name = "Gezond pakket 1",
                CanteenId = canteen.Id,
                Canteen = canteen,
                MaxPickupTime = new DateTime(2023, 01, 23),
                ContainsAlcohol = false,
                Price = 6,
                IsMealHot = true,
                Products = new List<Product> { product1, product2 },
                StudentId = null,
                Student = null
            };

            Assert.Equal("Hot meals not allowed", await packageService.CreatePackage(package));
        }

        [Fact]
        public async void US9_Hot_Meal_Package_Can_Be_Created_When_Canteen_Has_Hot_Meals()
        {
            var packageRepo = new Mock<IPackageRepo>();
            var studentRepo = new Mock<IStudentRepo>();
            var packageService = new PackageService(packageRepo.Object, studentRepo.Object);
            var canteen = new Canteen
            {
                Id = 1,
                CanteenName = "LA",
                City = "Breda",
                HotMeals = true
            };
            var product1 = new Product
            {
                Id = 5,
                Name = "Plant based burger",
                ContainsAlcohol = false,
                ProductImage = "https://i.imgur.com/K1LfUCP.jpg"
            };
            var product2 = new Product
            {
                Id = 6,
                Name = "Soep",
                ContainsAlcohol = false,
                ProductImage = "https://i.imgur.com/T5rn9hV.jpg"
            };
            var package = new Package
            {
                Id = 1,
                Name = "Gezond pakket 1",
                CanteenId = canteen.Id,
                Canteen = canteen,
                MaxPickupTime = new DateTime(2023, 01, 23),
                ContainsAlcohol = false,
                Price = 6,
                IsMealHot = true,
                Products = new List<Product> { product1, product2 },
                StudentId = null,
                Student = null
            };

            Assert.Equal("success", await packageService.CreatePackage(package));
        }
    }
}