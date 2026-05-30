using AuthPlatform.Domain.Auth.Entities;
using AuthPlatform.Domain.Expenditures.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthPlatform.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<AuthUser> AuthUsers => Set<AuthUser>();
    public DbSet<AuthGroup> AuthGroups => Set<AuthGroup>();
    public DbSet<AuthUserGroup> AuthUserGroups => Set<AuthUserGroup>();
    public DbSet<AuthPermission> AuthPermissions => Set<AuthPermission>();
    public DbSet<AuthUserPermission> AuthUserPermissions => Set<AuthUserPermission>();
    public DbSet<AuthGroupPermission> AuthGroupPermissions => Set<AuthGroupPermission>();
    public DbSet<AuthRefreshToken> AuthRefreshTokens => Set<AuthRefreshToken>();
    public DbSet<AuthLoginHistory> AuthLoginHistories => Set<AuthLoginHistory>();
    public DbSet<AuthAuditActivity> AuthAuditActivities => Set<AuthAuditActivity>();

    public DbSet<ExpenditureHead> ExpenditureHeads => Set<ExpenditureHead>();
    public DbSet<ExpenditureInvoice> ExpenditureInvoices => Set<ExpenditureInvoice>();
    public DbSet<ExpenditureInvoiceDetail> ExpenditureInvoiceDetails => Set<ExpenditureInvoiceDetail>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureAuth(modelBuilder);
        ConfigureExpenditures(modelBuilder);

        SeedPermissions(modelBuilder);
        SeedGroups(modelBuilder);
        SeedUsers(modelBuilder);
        SeedUserGroups(modelBuilder);
        SeedGroupPermissions(modelBuilder);
        SeedUserPermissions(modelBuilder);
    }

    private static void ConfigureAuth(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthUser>(entity =>
        {
            entity.ToTable("AuthUsers");
            entity.HasKey(x => x.AuthUserId);

            entity.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
            entity.Property(x => x.LastName).HasMaxLength(100).IsRequired();
            entity.Property(x => x.UserName).HasMaxLength(100).IsRequired();
            entity.Property(x => x.Email).HasMaxLength(200).IsRequired();
            entity.Property(x => x.PasswordHash).IsRequired();

            entity.HasIndex(x => x.UserName).IsUnique();
            entity.HasIndex(x => x.Email).IsUnique();
        });

        modelBuilder.Entity<AuthGroup>(entity =>
        {
            entity.ToTable("AuthGroups");
            entity.HasKey(x => x.AuthGroupId);

            entity.Property(x => x.GroupName).HasMaxLength(150).IsRequired();
        });

        modelBuilder.Entity<AuthPermission>(entity =>
        {
            entity.ToTable("AuthPermissions");
            entity.HasKey(x => x.AuthPermissionId);

            entity.Property(x => x.PermissionName).HasMaxLength(150).IsRequired();
            entity.Property(x => x.PermissionCode).HasMaxLength(150).IsRequired();

            entity.HasIndex(x => x.PermissionCode).IsUnique();
        });

        modelBuilder.Entity<AuthUserGroup>(entity =>
        {
            entity.ToTable("AuthUserGroups");
            entity.HasKey(x => x.AuthUserGroupId);

            entity.HasOne(x => x.AuthUser)
                .WithMany(x => x.AuthUserGroups)
                .HasForeignKey(x => x.AuthUserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(x => x.AuthGroup)
                .WithMany(x => x.AuthUserGroups)
                .HasForeignKey(x => x.AuthGroupId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<AuthUserPermission>(entity =>
        {
            entity.ToTable("AuthUserPermissions");
            entity.HasKey(x => x.AuthUserPermissionId);

            entity.HasOne(x => x.AuthUser)
                .WithMany(x => x.UserPermissions)
                .HasForeignKey(x => x.AuthUserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(x => x.AuthPermission)
                .WithMany(x => x.UserPermissions)
                .HasForeignKey(x => x.AuthPermissionId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<AuthGroupPermission>(entity =>
        {
            entity.ToTable("AuthGroupPermissions");
            entity.HasKey(x => x.AuthGroupPermissionId);

            entity.HasOne(x => x.AuthGroup)
                .WithMany(x => x.AuthGroupPermissions)
                .HasForeignKey(x => x.AuthGroupId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(x => x.AuthPermission)
                .WithMany(x => x.AuthGroupPermissions)
                .HasForeignKey(x => x.AuthPermissionId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<AuthRefreshToken>(entity =>
        {
            entity.ToTable("AuthRefreshTokens");
            entity.HasKey(x => x.AuthRefreshTokenId);

            entity.Property(x => x.Token).IsRequired();

            entity.HasOne(x => x.AuthUser)
                .WithMany(x => x.RefreshTokens)
                .HasForeignKey(x => x.AuthUserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<AuthLoginHistory>(entity =>
        {
            entity.ToTable("AuthLoginHistories");
            entity.HasKey(x => x.AuthLoginHistoryId);
        });

        modelBuilder.Entity<AuthAuditActivity>(entity =>
        {
            entity.ToTable("AuthAuditActivities");
            entity.HasKey(x => x.AuthAuditActivityId);
        });
    }

    private static void ConfigureExpenditures(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ExpenditureHead>(entity =>
        {
            entity.ToTable("ExpenditureHeads");
            entity.HasKey(x => x.ExpenditureHeadId);

            entity.Property(x => x.ExpenditureHeadName).HasMaxLength(150).IsRequired();
            entity.Property(x => x.Description).HasMaxLength(500);
        });

        modelBuilder.Entity<ExpenditureInvoice>(entity =>
        {
            entity.ToTable("ExpenditureInvoices");
            entity.HasKey(x => x.ExpenditureInvoiceId);

            entity.Property(x => x.InvoiceNumber).HasMaxLength(100).IsRequired();
            entity.Property(x => x.TotalAmount).HasColumnType("decimal(18,2)");

            entity.HasOne(x => x.ExpenditureHead)
                .WithMany(x => x.Invoices)
                .HasForeignKey(x => x.ExpenditureHeadId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ExpenditureInvoiceDetail>(entity =>
        {
            entity.ToTable("ExpenditureInvoiceDetails");
            entity.HasKey(x => x.ExpenditureInvoiceDetailId);

            entity.Property(x => x.ItemName).HasMaxLength(200).IsRequired();
            entity.Property(x => x.Quantity).HasColumnType("decimal(18,2)");
            entity.Property(x => x.UnitPrice).HasColumnType("decimal(18,2)");

            entity.HasOne(x => x.ExpenditureInvoice)
                .WithMany(x => x.Details)
                .HasForeignKey(x => x.ExpenditureInvoiceId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    private static void SeedPermissions(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthPermission>().HasData(
            new AuthPermission { AuthPermissionId = 1, PermissionName = "View Expenditure Heads", PermissionCode = "ExpenditureHead.View", Description = "Allows user to view expenditure heads", IsActive = true },
            new AuthPermission { AuthPermissionId = 2, PermissionName = "Create Expenditure Heads", PermissionCode = "ExpenditureHead.Create", Description = "Allows user to create expenditure heads", IsActive = true },
            new AuthPermission { AuthPermissionId = 3, PermissionName = "Edit Expenditure Heads", PermissionCode = "ExpenditureHead.Edit", Description = "Allows user to edit expenditure heads", IsActive = true },
            new AuthPermission { AuthPermissionId = 4, PermissionName = "Delete Expenditure Heads", PermissionCode = "ExpenditureHead.Delete", Description = "Allows user to delete expenditure heads", IsActive = true }
        );
    }

    private static void SeedGroups(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthGroup>().HasData(
            new AuthGroup { AuthGroupId = 1, GroupName = "Admin", Description = "System administrator", IsActive = true },
            new AuthGroup { AuthGroupId = 2, GroupName = "Viewer", Description = "Read-only user", IsActive = true }
        );
    }

    private static void SeedUsers(ModelBuilder modelBuilder)
    {
        const string testPasswordHash = "$2a$11$PSakMju1u1ZzRcUAMf5N/eFytXLA8thb3FrKWDIw3SRnbIM.xeepa";

        modelBuilder.Entity<AuthUser>().HasData(
            new AuthUser { AuthUserId = 1, FirstName = "System", LastName = "Admin", UserName = "admin", Email = "admin@authplatform.com", PasswordHash = testPasswordHash, IsLocked = false, FailedLoginAttempts = 0, IsActive = true },
            new AuthUser { AuthUserId = 2, FirstName = "Normal", LastName = "Viewer", UserName = "viewer", Email = "viewer@authplatform.com", PasswordHash = testPasswordHash, IsLocked = false, FailedLoginAttempts = 0, IsActive = true }
        );
    }

    private static void SeedUserGroups(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthUserGroup>().HasData(
            new AuthUserGroup { AuthUserGroupId = 1, AuthUserId = 1, AuthGroupId = 1, IsActive = true },
            new AuthUserGroup { AuthUserGroupId = 2, AuthUserId = 2, AuthGroupId = 2, IsActive = true }
        );
    }

    private static void SeedGroupPermissions(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthGroupPermission>().HasData(
            new AuthGroupPermission { AuthGroupPermissionId = 1, AuthGroupId = 1, AuthPermissionId = 1, IsActive = true },
            new AuthGroupPermission { AuthGroupPermissionId = 2, AuthGroupId = 1, AuthPermissionId = 2, IsActive = true },
            new AuthGroupPermission { AuthGroupPermissionId = 3, AuthGroupId = 1, AuthPermissionId = 3, IsActive = true },
            new AuthGroupPermission { AuthGroupPermissionId = 4, AuthGroupId = 1, AuthPermissionId = 4, IsActive = true },
            new AuthGroupPermission { AuthGroupPermissionId = 5, AuthGroupId = 2, AuthPermissionId = 1, IsActive = true }
        );
    }

    private static void SeedUserPermissions(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthUserPermission>().HasData(
            new AuthUserPermission { AuthUserPermissionId = 1, AuthUserId = 1, AuthPermissionId = 1, IsActive = true },
            new AuthUserPermission { AuthUserPermissionId = 2, AuthUserId = 1, AuthPermissionId = 2, IsActive = true },
            new AuthUserPermission { AuthUserPermissionId = 3, AuthUserId = 1, AuthPermissionId = 3, IsActive = true },
            new AuthUserPermission { AuthUserPermissionId = 4, AuthUserId = 1, AuthPermissionId = 4, IsActive = true }
        );
    }
}