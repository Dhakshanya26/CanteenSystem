using System;
using CanteenSystem.Dto.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CanteenSystem.Dal
{

    public partial class CanteenSystemDbContext : IdentityDbContext<ApplicationUser>
    {

        public CanteenSystemDbContext()
        {
        }

        public CanteenSystemDbContext(DbContextOptions<CanteenSystemDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<MealMenu> MealMenus { get; set; }
        public virtual DbSet<MealMenuAvailability> MealMenuAvailabilities { get; set; }
        public virtual DbSet<MealType> MealTypes { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<ParentMapping> ParentMapping { get; set; }
        public virtual DbSet<Card> Cards { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("data source=localhost;initial catalog=CanteenSystemDb;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("Cart");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.MealAvailableDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.MealMenu)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.MealMenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cart_MealMenus");
            });

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.ValidFromDate).HasColumnType("datetime");

                entity.Property(e => e.ValidToDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MealMenu>(entity =>
            {
                entity.Property(e => e.MealName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.Discount)
                    .WithMany(p => p.MealMenus)
                    .HasForeignKey(d => d.DiscountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MealMenus_Discounts");

                entity.HasOne(d => d.MealType)
                    .WithMany(p => p.MealMenus)
                    .HasForeignKey(d => d.MealTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MealMenus_MealTypes");
            });

            modelBuilder.Entity<MealMenuAvailability>(entity =>
            {
                entity.ToTable("MealMenuAvailability");

                entity.Property(e => e.AvailabilityDate).HasColumnType("datetime");

                entity.HasOne(d => d.MealMenu)
                    .WithMany(p => p.MealMenuAvailabilities)
                    .HasForeignKey(d => d.MealMenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MealMenuAvailability_MealMenus");
            });

            modelBuilder.Entity<MealType>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.OrderReference)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.UserProfile)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_UserProfiles");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.Property(e => e.MealMenuOrderDate).HasColumnType("datetime");

                entity.HasOne(d => d.MealMenu)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.MealMenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderItems_MealMenus");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderItems_Orders");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentReference)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payments_Orders");
            });
            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.Property(e => e.Department)
                    .HasMaxLength(500);

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.ApplicationUser)
                    .WithMany(p => p.UserProfiles)
                    .HasForeignKey(d => d.ApplicationUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicationUsers_UserProfile");
            });

            modelBuilder.Entity<ParentMapping>(entity =>
            {
                entity.HasOne(d => d.ParentUserProfile)
                    .WithMany(p => p.ParentUserProfiles)
                    .HasForeignKey(d => d.ParentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserParent_ParentUserProfiles");

                entity.HasOne(d => d.StudentUserProfile)
                     .WithMany(p => p.StudentUserProfiles)
                     .HasForeignKey(d => d.StudentId)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("FK_UserParent_StudentUserProfiles");
            });

            modelBuilder.Entity<Card>(entity =>
            {
                entity.Property(e => e.CardNumber)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.AvailableBalance)
                   .IsRequired();
                entity.HasOne(d => d.UserProfile)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.UserProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cards_UserProfiles");
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
