using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AllCars.Models
{
    public partial class CarsContext : DbContext
    {
        public CarsContext()
        {
        }

        public CarsContext(DbContextOptions<CarsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<GetCarsAvtoria> Cars { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-Q0HU41J;Database=Cars;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GetCarsAvtoria>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CarYear).HasColumnName("car_year");

                entity.Property(e => e.Engine).HasColumnName("engine");

                entity.Property(e => e.Img)
                    .HasColumnName("img")
                    .HasMaxLength(165)
                    .IsUnicode(false);

                entity.Property(e => e.MarkName)
                    .HasColumnName("mark")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ModelName)
                    .HasColumnName("model")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Odometer).HasColumnName("odometer");

                entity.Property(e => e.PriceUSD).HasColumnName("price");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
