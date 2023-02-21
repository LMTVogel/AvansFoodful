using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF
{
    public class IFDbContext : IdentityDbContext
    {
        public IFDbContext(DbContextOptions<IFDbContext> contextOptions) : base(contextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seeding a 'Employee' and 'Student' role to AspNetRoles table
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "8dbf2938-3dbe-472d-8ee9-3596f95e7931", Name = "Employee", NormalizedName = "EMPLOYEE".ToUpper()
            });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "4c7fc7d2-87b3-4edd-bb68-4f1d69fc7529", Name = "Student", NormalizedName = "STUDENT".ToUpper()
            });

            //a hasher to hash the password before seeding the user to the db
            var hasher = new PasswordHasher<IdentityUser>();


            //Seeding the User to AspNetUsers table
            modelBuilder.Entity<IdentityUser>().HasData(
                // Student user
                new IdentityUser
                {
                    Id = "8e445865-a24d-4543-a6c6-9443d048cdb9", // primary key
                    Email = "lmt.vogel@student.avans.nl",
                    UserName = "lmt.vogel@student.avans.nl",
                    NormalizedEmail = "lmt.vogel@student.avans.nl".ToUpper(),
                    NormalizedUserName = "lmt.vogel@student.avans.nl".ToUpper(),
                    PasswordHash = hasher.HashPassword(null, "1234")
                },
                // Student user
                new IdentityUser
                {
                    Id = "7b445865-a24d-4543-a6c6-9443d048cd1c", // primary key
                    Email = "rm.vandergaag@student.avans.nl",
                    UserName = "rm.vandergaag@student.avans.nl",
                    NormalizedEmail = "rm.vandergaag@student.avans.nl".ToUpper(),
                    NormalizedUserName = "rm.vandergaag@student.avans.nl".ToUpper(),
                    PasswordHash = hasher.HashPassword(null, "1234")
                },
                // Canteen employee user
                new IdentityUser
                {
                    Id = "d90704e7-510d-4df0-a9ff-aaa8ed58f1ae", // primary key
                    Email = "arend@avans.nl",
                    UserName = "arend@avans.nl",
                    NormalizedEmail = "arend@avans.nl".ToUpper(),
                    NormalizedUserName = "arend@avans.nl".ToUpper(),
                    PasswordHash = hasher.HashPassword(null, "1234")
                },
                // Canteen employee user
                new IdentityUser
                {
                    Id = "b10704e7-510d-4df0-a9ff-aaa8ed58f1cd", // primary key
                    Email = "gerard@avans.nl",
                    UserName = "gerard@avans.nl",
                    NormalizedEmail = "gerard@avans.nl".ToUpper(),
                    NormalizedUserName = "gerard@avans.nl".ToUpper(),
                    PasswordHash = hasher.HashPassword(null, "1234")
                }
            );


            //Seeding the relation between our user and role to AspNetUserRoles table
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "4c7fc7d2-87b3-4edd-bb68-4f1d69fc7529",
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "8dbf2938-3dbe-472d-8ee9-3596f95e7931",
                    UserId = "d90704e7-510d-4df0-a9ff-aaa8ed58f1ae"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "4c7fc7d2-87b3-4edd-bb68-4f1d69fc7529",
                    UserId = "7b445865-a24d-4543-a6c6-9443d048cd1c"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "8dbf2938-3dbe-472d-8ee9-3596f95e7931",
                    UserId = "b10704e7-510d-4df0-a9ff-aaa8ed58f1cd"
                }
            );
        }
    }
}
