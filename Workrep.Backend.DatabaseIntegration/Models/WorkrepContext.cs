using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Workrep.Backend.DatabaseIntegration.Models
{
    public partial class WorkrepContext : DbContext
    {
        public WorkrepContext()
        {
        }

        public WorkrepContext(DbContextOptions<WorkrepContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EmailConfirmation> EmailConfirmation { get; set; }
        public virtual DbSet<Niche> Niche { get; set; }
        public virtual DbSet<Organization> Organization { get; set; }
        public virtual DbSet<OrganizationBio> OrganizationBio { get; set; }
        public virtual DbSet<OrganizationInNiche> OrganizationInNiche { get; set; }
        public virtual DbSet<Review> Review { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Workplace> Workplace { get; set; }
        public virtual DbSet<WorkplaceBio> WorkplaceBio { get; set; }
        public virtual DbSet<WorkplaceInNiche> WorkplaceInNiche { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<EmailConfirmation>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Email_Co__B9BE370FDBC08601");

                entity.ToTable("Email_Confirmation");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.GenKey)
                    .IsRequired()
                    .HasColumnName("gen_key")
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithOne(p => p.EmailConfirmation)
                    .HasPrincipalKey<User>(p => p.UserId)
                    .HasForeignKey<EmailConfirmation>(d => d.UserId)
                    .HasConstraintName("FK_UserConfirm");
            });

            modelBuilder.Entity<Niche>(entity =>
            {
                entity.HasKey(e => e.NicheCode)
                    .HasName("PK__Niche__95D1C435BF8159F0");

                entity.Property(e => e.NicheCode)
                    .HasColumnName("niche_code")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.HasKey(e => e.OrganizationNumber)
                    .HasName("PK__Organiza__A04220313CC43787");

                entity.Property(e => e.OrganizationNumber)
                    .HasColumnName("organization_number")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Employees)
                    .HasColumnName("employees")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Homepage)
                    .HasColumnName("homepage")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SectorCode)
                    .HasColumnName("sector_code")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Zip).HasColumnName("zip");
            });

            modelBuilder.Entity<OrganizationBio>(entity =>
            {
                entity.HasKey(e => e.OrganizationNumber)
                    .HasName("PK__Organiza__A04220314DCD4709");

                entity.ToTable("Organization_Bio");

                entity.Property(e => e.OrganizationNumber)
                    .HasColumnName("organization_number")
                    .ValueGeneratedNever();

                entity.Property(e => e.Bio)
                    .IsRequired()
                    .HasColumnName("bio")
                    .HasMaxLength(1200)
                    .IsUnicode(false);

                entity.HasOne(d => d.OrganizationNumberNavigation)
                    .WithOne(p => p.OrganizationBio)
                    .HasForeignKey<OrganizationBio>(d => d.OrganizationNumber)
                    .HasConstraintName("fk_bio_organization");
            });

            modelBuilder.Entity<OrganizationInNiche>(entity =>
            {
                entity.HasKey(e => new { e.NicheCode, e.OrganizationNumber })
                    .HasName("PK__Organiza__9FD5E636EC7CA68E");

                entity.ToTable("Organization_In_Niche");

                entity.Property(e => e.NicheCode)
                    .HasColumnName("niche_code")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.OrganizationNumber).HasColumnName("organization_number");

                entity.HasOne(d => d.NicheCodeNavigation)
                    .WithMany(p => p.OrganizationInNiche)
                    .HasForeignKey(d => d.NicheCode)
                    .HasConstraintName("fk_niche");

                entity.HasOne(d => d.OrganizationNumberNavigation)
                    .WithMany(p => p.OrganizationInNiche)
                    .HasForeignKey(d => d.OrganizationNumber)
                    .HasConstraintName("fk_organization");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.Property(e => e.ReviewId).HasColumnName("review_id");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasColumnType("text");

                entity.Property(e => e.EmploymentEnd)
                    .HasColumnName("employment_end")
                    .HasColumnType("date");

                entity.Property(e => e.EmploymentStart)
                    .HasColumnName("employment_start")
                    .HasColumnType("date");

                entity.Property(e => e.Position)
                    .HasColumnName("position")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.Timestamp)
                    .HasColumnName("timestamp")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.WorkplaceOrganizationNumber).HasColumnName("workplace_organization_number");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Review)
                    .HasPrincipalKey(p => p.UserId)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_review_user");

                entity.HasOne(d => d.WorkplaceOrganizationNumberNavigation)
                    .WithMany(p => p.Review)
                    .HasForeignKey(d => d.WorkplaceOrganizationNumber)
                    .HasConstraintName("fk_review_workplace");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Email)
                    .HasName("PK__User__AB6E6165C2CC2323");

                entity.HasIndex(e => e.UserId)
                    .HasName("UQ__User__B9BE370EC04AE2E3")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Birthdate)
                    .HasColumnName("birthdate")
                    .HasColumnType("date");

                entity.Property(e => e.Confirmed)
                    .HasColumnName("confirmed")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.RegisterDate)
                    .HasColumnName("register_date")
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Workplace>(entity =>
            {
                entity.HasKey(e => e.OrganizationNumber)
                    .HasName("PK__Workplac__A04220315F2A5CF1");

                entity.Property(e => e.OrganizationNumber)
                    .HasColumnName("organization_number")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Norway')");

                entity.Property(e => e.Employees)
                    .HasColumnName("employees")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Homepage)
                    .HasColumnName("homepage")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SuperOrganizationNumber).HasColumnName("super_organization_number");

                entity.Property(e => e.Zip).HasColumnName("zip");

                entity.HasOne(d => d.SuperOrganizationNumberNavigation)
                    .WithMany(p => p.Workplace)
                    .HasForeignKey(d => d.SuperOrganizationNumber)
                    .HasConstraintName("fk_workplace_organization");
            });

            modelBuilder.Entity<WorkplaceBio>(entity =>
            {
                entity.HasKey(e => e.OrganizationNumber)
                    .HasName("PK__Workplac__A0422031C23C0A77");

                entity.ToTable("Workplace_Bio");

                entity.Property(e => e.OrganizationNumber)
                    .HasColumnName("organization_number")
                    .ValueGeneratedNever();

                entity.Property(e => e.Bio)
                    .IsRequired()
                    .HasColumnName("bio")
                    .HasMaxLength(1200)
                    .IsUnicode(false);

                entity.HasOne(d => d.OrganizationNumberNavigation)
                    .WithOne(p => p.WorkplaceBio)
                    .HasForeignKey<WorkplaceBio>(d => d.OrganizationNumber)
                    .HasConstraintName("fk_bio_workplace");
            });

            modelBuilder.Entity<WorkplaceInNiche>(entity =>
            {
                entity.HasKey(e => new { e.NicheCode, e.OrganizationNumber })
                    .HasName("PK__Workplac__9FD5E6360C9C7E17");

                entity.ToTable("Workplace_In_Niche");

                entity.Property(e => e.NicheCode)
                    .HasColumnName("niche_code")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.OrganizationNumber).HasColumnName("organization_number");

                entity.HasOne(d => d.NicheCodeNavigation)
                    .WithMany(p => p.WorkplaceInNiche)
                    .HasForeignKey(d => d.NicheCode)
                    .HasConstraintName("fk_niche_workplace");

                entity.HasOne(d => d.OrganizationNumberNavigation)
                    .WithMany(p => p.WorkplaceInNiche)
                    .HasForeignKey(d => d.OrganizationNumber)
                    .HasConstraintName("fk_workplace");
            });
        }
    }
}
