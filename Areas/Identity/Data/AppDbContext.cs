using BeautyLuxe.Areas.Identity.Data;
using BeautyLuxe.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BeautyLuxe.Areas.Identity.Data;

public class AppDbContext : IdentityDbContext<User, IdentityRole, string>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Service> Services { get; set; }
    
    public DbSet<Slot> Slot { get; set; }

    public DbSet<Booking> Bookings { get; set; }
    public DbSet<BeautyTip> BeautyTips { get; set; }



    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);


        builder.Entity<Booking>()
            .HasOne(b => b.Slot)
            .WithOne(s => s.Booking)
            .HasForeignKey<Booking>(b => b.SlotId)
            .OnDelete(DeleteBehavior.Restrict);

            // builder.Entity<Booking>()
            //     .HasOne(b => b.Slot)
            //     .WithMany(s => s.Booking)
            //     .HasForeignKey(b => b.SlotId)
            //     .OnDelete(DeleteBehavior.Restrict);
    }
}
