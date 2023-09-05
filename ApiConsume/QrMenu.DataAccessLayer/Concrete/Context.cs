using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QrMenu.EntityLayer.Concrete;

namespace HotelAndTours.DataAccessLayer.Concrete;

public class Context : IdentityDbContext<AppUser, AppRole, int>
{
    public virtual DbSet<AppRole> AppRoles { get; set; }
    public virtual DbSet<AppUser> AppUsers { get; set; }
    public virtual DbSet<Logs> Logs { get; set; }
    public virtual DbSet<Meal> Meals { get; set; }
    public virtual DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=localhost\\SQLEXPRESS;Database=QrMenu; Integrated Security=True;TrustServerCertificate=True;Trusted_Connection=True;MultipleActiveResultSets=true");
    }
}