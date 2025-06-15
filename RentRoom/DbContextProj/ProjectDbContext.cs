using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentRoom.Models;


namespace RentRoom.DbContextProj
{
    public class ProjectDbContext : IdentityDbContext<User>
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options) { }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Room> Rooms { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Name = "User" , NormalizedName = "USER",Id="KOLKO3452"},
                new IdentityRole { Name = "Admin" , NormalizedName = "ADMIN",Id= "KOLKO4533"}
            );
            builder.Entity<Booking>().HasOne(F => F.RentRoom).WithMany(d => d.Bookings);
            builder.Entity<User>().HasOne(F => F.Booking).WithOne(d => d.RentUser).HasForeignKey<Booking>(d => d.RentUserID);

         
        }

    }
}
