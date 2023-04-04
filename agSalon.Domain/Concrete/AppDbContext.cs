using agSalon.Domain.Concrete.EntityConfiguration;
using agSalon.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace agSalon.Domain.Concrete
{
    public class AppDbContext: IdentityDbContext<Client>
	{
        public DbSet<Service> Services { get; set; }
        public DbSet<GroupOfServices> Groups { get; set; }
        public DbSet<ServiceGroup> Services_Groups { get; set; }

        //public DbSet<Client> Clients { get; set; }

        public DbSet<Worker> Workers { get; set; }

        public DbSet<Worker_Group> Workers_Groups { get; set; }

        public DbSet<Attendance> Attendances { get; set; }

        public AppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AttendanceConfiguration());
            modelBuilder.ApplyConfiguration(new GroupOfServicesConfiguration());


            /**/
            modelBuilder.Entity<ServiceGroup>().HasKey(sg => new { sg.ServiceId, sg.GroupId });

            modelBuilder.Entity<ServiceGroup>().HasOne(g => g.Group).WithMany(sg => sg.Services_Groups);

            modelBuilder.Entity<ServiceGroup>().HasOne(s => s.Service).WithOne(sg => sg.Service_Group);

            /**/
            modelBuilder.Entity<Worker_Group>().HasKey(wg => new { wg.WorkerId, wg.GroupId });

            modelBuilder.Entity<Worker_Group>().HasOne(w => w.Group).WithMany(wg => wg.Workers_Groups);



            base.OnModelCreating(modelBuilder);
        }

    }
}
