#pragma warning disable CS8618

using Microsoft.EntityFrameworkCore;
namespace MyCRM.Models;

public class MyCRMContext : DbContext 
{ 
    public MyCRMContext(DbContextOptions options) : base(options) { }

    public DbSet<User> Users { get; set; } 

    public DbSet<Admin> Administrators { get; set; } 

    public DbSet<Activity> Activities {get; set;}

    public DbSet<Business> Businesses {get; set;}

    public DbSet<Staff> Staff {get; set;}

    public DbSet<Note> Notes {get; set;}

    public DbSet<Address> Addresses {get; set;}

    public DbSet<Purchase> Purchases {get; set;}

    public DbSet<Product> Products {get; set;}
}