using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using RentRoom.Models;
using System.Reflection.Emit;


namespace RentRoom.DbContextProj
{
    public class ProjectDbContext : IdentityDbContext<User>
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options) { }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Review> Reviews { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Name = "User" , NormalizedName = "USER",Id="KOLKO3452"},
                new IdentityRole { Name = "Admin" , NormalizedName = "ADMIN",Id= "KOLKO4533"}
            );
            builder.Entity<Booking>().HasOne(F => F.RentRoom).WithMany(d => d.Bookings);
            builder.Entity<User>().HasOne(F => F.Booking).WithOne(d => d.RentUser).HasForeignKey<Booking>(d => d.RentUserID);

            builder.Entity<Review>()
               .HasOne(r => r.user)
               .WithMany(u => u.Reviews)
               .HasForeignKey(r => r.UserId);

            builder.Entity<Review>()
               .HasOne(r => r.room)
               .WithMany(rm => rm.Reviews)
               .HasForeignKey(r => r.roomId);



        }

    }
}
