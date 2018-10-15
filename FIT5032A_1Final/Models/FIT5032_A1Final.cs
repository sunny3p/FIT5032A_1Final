namespace FIT5032A_1Final.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class FIT5032_A1Final : DbContext
    {
        public FIT5032_A1Final()
            : base("name=FIT5032_A1Final")
        {
        }

        public virtual DbSet<Employee_Info> Employee_Info { get; set; }
        public virtual DbSet<Health_Info> Health_Info { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Personal_Info> Personal_Info { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee_Info>()
                .HasMany(e => e.Reservations)
                .WithOptional(e => e.Employee_Info)
                .HasForeignKey(e => e.EId);

            modelBuilder.Entity<Health_Info>()
                .Property(e => e.Height)
                .HasPrecision(10, 3);

            modelBuilder.Entity<Health_Info>()
                .Property(e => e.Weight)
                .HasPrecision(10, 3);

            modelBuilder.Entity<Personal_Info>()
                .HasMany(e => e.Health_Info)
                .WithRequired(e => e.Personal_Info)
                .HasForeignKey(e => e.PId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Personal_Info>()
                .HasMany(e => e.Locations)
                .WithRequired(e => e.Personal_Info)
                .HasForeignKey(e => e.PId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Personal_Info>()
                .HasMany(e => e.Reservations)
                .WithOptional(e => e.Personal_Info)
                .HasForeignKey(e => e.PId);
        }
    }
}
