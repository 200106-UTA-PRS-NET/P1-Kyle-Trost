using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PizzaBox.Domain.Models
{
    public partial class PizzaBoxDbContext : DbContext
    {
        public PizzaBoxDbContext()
        {
        }

        public PizzaBoxDbContext(DbContextOptions<PizzaBoxDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CrustTypes> CrustTypes { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<PizzasSold> PizzasSold { get; set; }
        public virtual DbSet<PresetPizzas> PresetPizzas { get; set; }
        public virtual DbSet<Sizes> Sizes { get; set; }
        public virtual DbSet<Stores> Stores { get; set; }
        public virtual DbSet<Users> Users { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=KYLE\\SQLEXPRESS01; Database=PizzaBoxDb; Trusted_Connection=true;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CrustTypes>(entity =>
            {
                entity.HasKey(e => e.CrustId)
                    .HasName("PK__CrustTyp__C583F8D1B3588948");

                entity.ToTable("CrustTypes", "PizzaBox");

                entity.HasIndex(e => e.CrustName)
                    .HasName("UQ__CrustTyp__05BEBD67DA87F78B")
                    .IsUnique();

                entity.Property(e => e.CrustId).HasColumnName("crustId");

                entity.Property(e => e.CrustCost)
                    .HasColumnName("crustCost")
                    .HasColumnType("money");

                entity.Property(e => e.CrustName)
                    .IsRequired()
                    .HasColumnName("crustName")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__Orders__0809335DA2359090");

                entity.ToTable("Orders", "PizzaBox");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.OrderTimestamp)
                    .HasColumnName("orderTimestamp")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.StoreId).HasColumnName("storeId");

                entity.Property(e => e.TotalCost)
                    .HasColumnName("totalCost")
                    .HasColumnType("money");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK__Orders__storeId__14270015");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Orders__userId__151B244E");
            });

            modelBuilder.Entity<PizzasSold>(entity =>
            {
                entity.HasKey(e => e.PizzaId)
                    .HasName("PK__PizzasSo__4D4C90EFFA752A97");

                entity.ToTable("PizzasSold", "PizzaBox");

                entity.Property(e => e.PizzaId).HasColumnName("pizzaId");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.PizzaCrust).HasColumnName("pizzaCrust");

                entity.Property(e => e.PizzaName)
                    .HasColumnName("pizzaName")
                    .IsUnicode(false);

                entity.Property(e => e.PizzaSize).HasColumnName("pizzaSize");

                entity.Property(e => e.TotalCost)
                    .HasColumnName("totalCost")
                    .HasColumnType("money");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.PizzasSold)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__PizzasSol__order__18EBB532");

                entity.HasOne(d => d.PizzaCrustNavigation)
                    .WithMany(p => p.PizzasSold)
                    .HasForeignKey(d => d.PizzaCrust)
                    .HasConstraintName("FK__PizzasSol__pizza__1AD3FDA4");

                entity.HasOne(d => d.PizzaSizeNavigation)
                    .WithMany(p => p.PizzasSold)
                    .HasForeignKey(d => d.PizzaSize)
                    .HasConstraintName("FK__PizzasSol__pizza__19DFD96B");
            });

            modelBuilder.Entity<PresetPizzas>(entity =>
            {
                entity.HasKey(e => e.PresetId)
                    .HasName("PK__PresetPi__A691E6F0A5D378FD");

                entity.ToTable("PresetPizzas", "PizzaBox");

                entity.Property(e => e.PresetId).HasColumnName("presetId");

                entity.Property(e => e.PresetName)
                    .HasColumnName("presetName")
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Sizes>(entity =>
            {
                entity.HasKey(e => e.SizeId)
                    .HasName("PK__Sizes__55B1E5571DE5000C");

                entity.ToTable("Sizes", "PizzaBox");

                entity.HasIndex(e => e.SizeName)
                    .HasName("UQ__Sizes__DE4E1556B734DE20")
                    .IsUnique();

                entity.Property(e => e.SizeId).HasColumnName("sizeId");

                entity.Property(e => e.SizeCost)
                    .HasColumnName("sizeCost")
                    .HasColumnType("money");

                entity.Property(e => e.SizeName)
                    .IsRequired()
                    .HasColumnName("sizeName")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Stores>(entity =>
            {
                entity.HasKey(e => e.StoreId)
                    .HasName("PK__Stores__1EA71613D2D347E7");

                entity.ToTable("Stores", "PizzaBox");

                entity.Property(e => e.StoreId).HasColumnName("storeId");

                entity.Property(e => e.StoreLocation)
                    .HasColumnName("storeLocation")
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Users__CB9A1CFF96364131");

                entity.ToTable("Users", "PizzaBox");

                entity.HasIndex(e => e.UserName)
                    .HasName("UQ__Users__66DCF95C27629808")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.StoreId).HasColumnName("storeId");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("userName")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserPass)
                    .IsRequired()
                    .HasColumnName("userPass")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
