using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UPHC.SurveillanceDashboard.Models;

namespace UPHC.SurveillanceDashboard.Data
{
    //public class AppDbContext : IdentityDbContext
    //{
    //    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    //    public DbSet<CaseRecord> Patients { get; set; } = null!;
    //    public DbSet<Notification> Notifications { get; set; } = null!;
    //    public DbSet<UPHC.SurveillanceDashboard.Models.UPHC> UPHCs { get; set; } = null!;

    //    protected override void OnModelCreating(ModelBuilder builder)
    //    {
    //        base.OnModelCreating(builder);
    //        builder.Entity<UPHC.SurveillanceDashboard.Models.UPHC>().HasData(new UPHC.SurveillanceDashboard.Models.UPHC { Id = 1, Name = "UPHC1 Bharatpur", Location = "Bhubaneswar" });
    //    }
    //}


    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<CaseRecord> CaseRecords { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UPHC.SurveillanceDashboard.Models. UPHC> UPHCs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // 🔗 CaseRecord → UPHC (Many-to-One)
            builder.Entity<CaseRecord>()
                .HasOne(c => c.UPHC)
                .WithMany(u => u.CaseRecords)
                .HasForeignKey(c => c.UPHCId)
                .OnDelete(DeleteBehavior.Restrict);

            // 🔗 Notification → CaseRecord (Many-to-One)
            builder.Entity<Notification>()
                .HasOne(n => n.CaseRecord)
                .WithMany(c => c.Notifications)
                .HasForeignKey(n => n.CaseRecordId)
                .OnDelete(DeleteBehavior.Cascade);

            //ApplicationUser -> UPHC (Many-to-one)

            builder.Entity<ApplicationUser>()
    .HasOne(u => u.UPHC)
    .WithMany()
    .HasForeignKey(u => u.UPHCId)
    .OnDelete(DeleteBehavior.Restrict);


            //Notification -> UPHC (Many-to-one)
            builder.Entity<Notification>()
    .HasOne(n => n.UPHC)
    .WithMany()
    .HasForeignKey(n => n.UPHCId)
    .OnDelete(DeleteBehavior.Restrict);


            // Seed UPHC
            builder.Entity<UPHC.SurveillanceDashboard.Models.UPHC>().HasData(
                new UPHC.SurveillanceDashboard.Models.UPHC { Id = 1, Name = "Unit-3 UPHC", Location = "Kharvela Nagar" },
                new UPHC.SurveillanceDashboard.Models.UPHC { Id = 2, Name = "Unit-4 UPHC", Location = "Unit-4,Near AG Colony" },
                new UPHC.SurveillanceDashboard.Models.UPHC { Id = 3, Name = "Unit-8 UPHC", Location = "Unit-8" },
                new UPHC.SurveillanceDashboard.Models.UPHC { Id = 4, Name = "Unit-9 UPHC", Location = "Unit-9 Industrial Colony" },
                new UPHC.SurveillanceDashboard.Models.UPHC { Id = 5, Name = "Saheed Nagar UPHC", Location = "Saheed Nagar" },
                new UPHC.SurveillanceDashboard.Models.UPHC { Id = 6, Name = "Satya Nagar UPHC", Location = "Satya Nagar,Near Kali Mandir" },
                new UPHC.SurveillanceDashboard.Models.UPHC { Id = 7, Name = "Baramunda UPHC", Location = "Rental Colony, IRC Village" },
                new UPHC.SurveillanceDashboard.Models.UPHC { Id = 8, Name = "IRC Village UPHC", Location = "IRC Village, Nayapalli" },
                new UPHC.SurveillanceDashboard.Models.UPHC { Id = 9, Name = "Rasulgarh UPHC", Location = "GGP Colony, Rasulgarh" },
                new UPHC.SurveillanceDashboard.Models.UPHC { Id = 10, Name = "Gadakan UPHC", Location = "Gadeswar,Near RI Office Kalarahanga" },
                new UPHC.SurveillanceDashboard.Models.UPHC { Id = 11, Name = "Pokhariput UPHC", Location = "Pokhariput,Ward No-62" },
                new UPHC.SurveillanceDashboard.Models.UPHC { Id = 12, Name = "Chandrasekharpur UPHC", Location = "CS Pur HB Colony,Ward No-8" },
                new UPHC.SurveillanceDashboard.Models.UPHC { Id = 13, Name = "Niladri Vihar UPHC", Location = "Niladri Vihar, Sector-I" },
                new UPHC.SurveillanceDashboard.Models.UPHC { Id = 14, Name = "Bjb Nagar UPHC", Location = "BJB Nagar,Near BJB Nagar Hata" },
                new UPHC.SurveillanceDashboard.Models.UPHC { Id = 15, Name = "Bhimatangi UPHC", Location = "Bhimatangi Housing Board area" },
                new UPHC.SurveillanceDashboard.Models.UPHC { Id = 16, Name = "Kapilaprasad UPHC", Location = "Kapilaprasad Village area" },
                new UPHC.SurveillanceDashboard.Models.UPHC { Id = 17, Name = "Badagada UPHC", Location = "Badagada Village,Brit area" },
                new UPHC.SurveillanceDashboard.Models.UPHC { Id = 18, Name = "Jharapada UPHC", Location = "Jharapada Village area" },
                new UPHC.SurveillanceDashboard.Models.UPHC { Id = 19, Name = "Bharatpur UPHC", Location = "Bharatpur Slum,Mahalaxmi Vihar" },
                new UPHC.SurveillanceDashboard.Models.UPHC { Id = 20, Name = "Patia UPHC", Location = "Patia Village,Damana area" },
                new UPHC.SurveillanceDashboard.Models.UPHC { Id = 21, Name = "VSS Nagar UPHC", Location = "VSS Nagar Housing Board area" },
                new UPHC.SurveillanceDashboard.Models.UPHC { Id = 22, Name = "Laxmisagar UPHC", Location = "Laxmisagar Village area" }
            );
        }
    }
}
