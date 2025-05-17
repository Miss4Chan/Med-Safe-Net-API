using System;
using API.Entities;
using Med_Safe_Net_API.Data.Seed;
using Med_Safe_Net_API.Entities;
using Microsoft.EntityFrameworkCore;
using Namotion.Reflection;

namespace API.Data;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<AppUser> Users {get;set;} 
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<AppRole> AppRoles { get; set; }
    public DbSet<HeartRate> HeartRates { get; set; }
    public DbSet<SuddenMovement> SuddenMovements { get; set; }
    public DbSet<HighHeartRate> HighHeartRates { get; set; }
    public DbSet<UserLink> UserLinks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppUser>(entity =>
        {
            entity
                .HasMany(u => u.HeartRates)
                .WithOne(hr => hr.User)
                .HasForeignKey(hr => hr.UserId);

            entity
                .HasMany(u => u.SuddenMovements)
                .WithOne(sm => sm.User)
                .HasForeignKey(sm => sm.UserId);

            entity
                .HasMany(u => u.HighHeartRates)
                .WithOne(hhr => hhr.User)
                .HasForeignKey(hhr => hhr.UserId);

            entity
                .HasIndex(e => e.UserCode)
                .IsUnique()
                .HasFilter("[UserCode] IS NOT NULL");

            entity
                .HasMany(e => e.UserLinks)
                .WithOne(ul => ul.CareGiver)
                .HasForeignKey(ul => ul.CareGiverId);
        });

        modelBuilder.Entity<UserLink>(entity =>
        {
            entity
                .HasIndex(e => new { e.PatientId, e.CareGiverId })
                .IsUnique();
        });

        DataSeeder.SeedData(modelBuilder);

    }
}
