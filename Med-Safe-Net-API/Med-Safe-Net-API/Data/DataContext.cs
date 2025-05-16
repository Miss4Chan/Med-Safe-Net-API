using System;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<AppUser> Users {get;set;} 
    public DbSet<HeartRate> HeartRates { get; set; }
    public DbSet<SuddenMovement> SuddenMovements { get; set; }
    public DbSet<HighHeartRate> HighHeartRates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppUser>()
            .HasMany(u => u.HeartRates)
            .WithOne(hr => hr.User)
            .HasForeignKey(hr => hr.UserId);

        modelBuilder.Entity<AppUser>()
            .HasMany(u => u.SuddenMovements)
            .WithOne(sm => sm.User)
            .HasForeignKey(sm => sm.UserId);

        modelBuilder.Entity<AppUser>()
            .HasMany(u => u.HighHeartRates)
            .WithOne(hhr => hhr.User)
            .HasForeignKey(hhr => hhr.UserId);

    }
}
