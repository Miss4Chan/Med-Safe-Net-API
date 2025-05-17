using API.Entities;
using Med_Safe_Net_API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Med_Safe_Net_API.Data.Seed
{
    public static class DataSeeder
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppRole>().HasData(
                new AppRole
                {
                    AppRoleId = AppRoleType.Administrator,
                    AppRoleName = "Administrator"
                },
               new AppRole
               {
                   AppRoleId = AppRoleType.Caregiver,
                   AppRoleName = "Caregiver"
               },
               new AppRole
               {
                   AppRoleId = AppRoleType.Patient,
                   AppRoleName = "Patient"
               }
            );
            using var hmac = new HMACSHA512();
            var defaultPassword = "sogospodnapred123";

            var admin = new AppUser
            {
                Id = -1,
                Username = "admin",
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(defaultPassword)),
                PasswordSalt = hmac.Key,
                FirstName = "Admin",
                LastName = "User",
                Email = "admin@example.com",
                DateOfBirth = new DateTime(1980, 1, 1)
            };

            var caregiver1 = new AppUser
            {
                Id = -2,
                Username = "caregiver1",
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(defaultPassword)),
                PasswordSalt = hmac.Key,
                FirstName = "Alice",
                LastName = "Smith",
                Email = "cg1@example.com",
                DateOfBirth = new DateTime(1990, 5, 10)
            };

            var caregiver2 = new AppUser
            {
                Id = -3,
                Username = "caregiver2",
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(defaultPassword)),
                PasswordSalt = hmac.Key,
                FirstName = "Bob",
                LastName = "Johnson",
                Email = "cg2@example.com",
                DateOfBirth = new DateTime(1992, 6, 15)
            };

            var patient1 = new AppUser
            {
                Id = -4,
                Username = "patient1",
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(defaultPassword)),
                PasswordSalt = hmac.Key,
                FirstName = "Eve",
                LastName = "Doe",
                Email = "patient@example.com",
                DateOfBirth = new DateTime(1952, 9, 21)
            };

            modelBuilder.Entity<AppUser>().HasData(
                admin,
                caregiver1,
                caregiver2,
                patient1
            );

            modelBuilder.Entity<UserRole>().HasData(
                new UserRole
                {
                    UserRoleId = -1,
                    AppRoleId = AppRoleType.Administrator,
                    Id = admin.Id
                },
                new UserRole
                {
                    UserRoleId = -2,
                    AppRoleId = AppRoleType.Caregiver,
                    Id = caregiver1.Id,
                    
                },
                new UserRole
                {
                    UserRoleId = -3,
                    AppRoleId = AppRoleType.Patient,
                    Id = caregiver2.Id
                }
            );
        }
    }
}
