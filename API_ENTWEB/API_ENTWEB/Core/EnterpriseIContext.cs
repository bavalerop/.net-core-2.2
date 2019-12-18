using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API_ENTWEB.Core
{
    public partial class EnterpriseIContext : DbContext
    {
        public EnterpriseIContext()
        {
        }

        public EnterpriseIContext(DbContextOptions<EnterpriseIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Inf001> Inf001 { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=CLTBVALERO;Database=EnterpriseI;User ID=sa;Password=12345");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inf001>(entity =>
            {
                entity.HasKey(e => e.Inf001C6);

                entity.Property(e => e.Inf001C2)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Inf001C3).HasColumnType("datetime");

                entity.Property(e => e.Inf001C5)
                    .IsRequired()
                    .HasColumnType("image");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
