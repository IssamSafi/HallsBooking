using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Firstproject.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AboutU> AboutUs { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<ContactU> ContactUs { get; set; }
        public virtual DbSet<Hall> Halls { get; set; }
        public virtual DbSet<Home> Homes { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Role1> Role1s { get; set; }
        public virtual DbSet<Testimonial> Testimonials { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Userhall> Userhalls { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("USER ID=JOR15_User91;PASSWORD=SaFiCo##**00;DATA SOURCE=94.56.229.181:3488/traindb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("JOR15_USER91")
                .HasAnnotation("Relational:Collation", "USING_NLS_COMP");

            modelBuilder.Entity<AboutU>(entity =>
            {
                entity.ToTable("ABOUT_US");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Image)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE");
            });

            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("ADDRESS");

                entity.Property(e => e.AddressId)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ADDRESS_ID");

                entity.Property(e => e.City)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CITY");

                entity.Property(e => e.Country)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("COUNTRY");

                entity.Property(e => e.Street)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("STREET");
            });

            modelBuilder.Entity<ContactU>(entity =>
            {
                entity.HasKey(e => e.ConId)
                    .HasName("SYS_C00269383");

                entity.ToTable("CONTACT_US");

                entity.Property(e => e.ConId)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CON_ID");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Location)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("LOCATION");

                entity.Property(e => e.Message)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("MESSAGE");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Subject)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("SUBJECT");

                entity.Property(e => e.Telephone)
                    .HasColumnType("NUMBER")
                    .HasColumnName("TELEPHONE");

                entity.Property(e => e.YEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Y_EMAIL");
            });

           
            modelBuilder.Entity<Hall>(entity =>
            {
                entity.ToTable("HALL");

                entity.Property(e => e.HallId)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("HALL_ID");

                entity.Property(e => e.AddressId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ADDRESS_ID");

                entity.Property(e => e.HallName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("HALL_NAME");

                entity.Property(e => e.HallPrice)
                    .HasPrecision(10)
                    .HasColumnName("HALL_PRICE");

                entity.Property(e => e.HallSize)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("HALL_SIZE");

                entity.Property(e => e.HallType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("HALL_TYPE");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Halls)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("HALL_ADDRESS");
            });

            modelBuilder.Entity<Home>(entity =>
            {
                entity.ToTable("HOME");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Image1)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE1");

                entity.Property(e => e.Image2)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE2");

                entity.Property(e => e.Image3)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE3");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TITLE");
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.ToTable("LOGIN");

                entity.Property(e => e.LoginId)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("LOGIN_ID");

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.RoleId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ROLE_ID");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("USER_ID");

                entity.Property(e => e.UserName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("USER_NAME");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Logins)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("LOGIN_ROLE");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Logins)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("LOGIN_USER");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("PAYMENT");

                entity.Property(e => e.PaymentId)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PAYMENT_ID");

                entity.Property(e => e.Amount)
                    .HasColumnType("NUMBER(8,2)")
                    .HasColumnName("AMOUNT");

                entity.Property(e => e.Balance)
                    .HasColumnType("NUMBER")
                    .HasColumnName("BALANCE");

                entity.Property(e => e.CardNumber)
                    .HasColumnType("LONG")
                    .HasColumnName("CARD_NUMBER");

                entity.Property(e => e.PaymentDate)
                    .HasColumnType("DATE")
                    .HasColumnName("PAYMENT_DATE");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("USER_ID");
            });

            modelBuilder.Entity<Role1>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("SYS_C00270794");

                entity.ToTable("ROLE1");

                entity.Property(e => e.RoleId)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ROLE_ID");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ROLE_NAME");
            });

            modelBuilder.Entity<Testimonial>(entity =>
            {
                entity.ToTable("TESTIMONIAL");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Approved)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("APPROVED");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Message)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("MESSAGE");

                entity.Property(e => e.Phone)
                    .HasColumnType("NUMBER(10,2)")
                    .HasColumnName("PHONE");

                entity.Property(e => e.Tname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TNAME");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USERS");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("USER_ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Fname)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("FNAME");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");

                entity.Property(e => e.Lname)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("LNAME");

                entity.Property(e => e.Phone)
                    .HasPrecision(10)
                    .HasColumnName("PHONE");
            });

            modelBuilder.Entity<Userhall>(entity =>
            {
                entity.ToTable("USERHALL");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.EndDate)
                    .HasColumnType("DATE")
                    .HasColumnName("END_DATE");

                entity.Property(e => e.HallId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("HALL_ID");

                entity.Property(e => e.IsApprove)
                    .HasPrecision(1)
                    .HasColumnName("IS_APPROVE");

                entity.Property(e => e.StartDate)
                    .HasColumnType("DATE")
                    .HasColumnName("START_DATE");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.Hall)
                    .WithMany(p => p.Userhalls)
                    .HasForeignKey(d => d.HallId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("HALL_USER");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Userhalls)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("USER_HALL");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
