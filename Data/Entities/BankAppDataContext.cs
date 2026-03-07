namespace StancaBankApi.Data.Entities;

public class BankAppDataContext(DbContextOptions<BankAppDataContext> options) : DbContext(options)
{
    public DbSet<AuthUser> AuthUsers { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<AccountType> AccountTypes { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Disposition> Dispositions { get; set; }
    public DbSet<Card> Cards { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Account>()
            .Property(a => a.Balance)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Loan>()
            .Property(l => l.Amount)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Loan>()
            .Property(l => l.Payments)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Transaction>()
            .Property(t => t.Amount)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Transaction>()
            .Property(t => t.Balance)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Card>()
            .HasOne<Disposition>()
            .WithMany()
            .HasForeignKey(c => c.DispositionId)
            .HasConstraintName("FK_Cards_Dispositions");

        modelBuilder.Entity<Account>()
            .HasOne<AccountType>()
            .WithMany()
            .HasForeignKey(a => a.AccountTypeId)
            .HasConstraintName("FK_Accounts_AccountTypes");

        modelBuilder.Entity<Disposition>()
            .HasOne<Customer>()
            .WithMany()
            .HasForeignKey(d => d.CustomerId)
            .HasConstraintName("FK_Dispositions_Customers");

        modelBuilder.Entity<AuthUser>()
            .HasOne<Customer>()
            .WithMany()
            .HasForeignKey(a => a.CustomerId)
            .HasConstraintName("FK_AuthUsers_Customers_CustomerId");

        modelBuilder.Entity<Disposition>()
            .HasOne<Account>()
            .WithMany()
            .HasForeignKey(d => d.AccountId)
            .HasConstraintName("FK_Dispositions_Accounts");

        modelBuilder.Entity<Loan>()
            .HasOne<Account>()
            .WithMany()
            .HasForeignKey(l => l.AccountId)
            .HasConstraintName("FK_Loans_Accounts");

        modelBuilder.Entity<Transaction>()
            .HasOne<Account>()
            .WithMany()
            .HasForeignKey(t => t.AccountId)
            .HasConstraintName("FK_Transactions_Accounts");
    }

    public void Seed(IPasswordService passwordService)
    {
        var seedAdminUsername = Environment.GetEnvironmentVariable("SEED_ADMIN_USERNAME") ?? "admin";
        var seedAdminPassword = Environment.GetEnvironmentVariable("SEED_ADMIN_PASSWORD") ?? "ChangeMe_Admin123!";

        var admin = AuthUsers.FirstOrDefault(a =>
            a.Role.ToLower() == "admin" &&
            a.Username.ToLower() == seedAdminUsername.ToLower());

        if (admin is null)
        {
            AuthUsers.Add(new AuthUser
            {
                Username = seedAdminUsername,
                PasswordHash = passwordService.HashPassword(seedAdminPassword),
                Role = "Admin",
                RequirePasswordChange = false
            });
        }
        else
        {
            admin.Username = seedAdminUsername;
            admin.Role = "Admin";
            admin.PasswordHash = passwordService.HashPassword(seedAdminPassword);
            admin.RequirePasswordChange = false;
        }

        SaveChanges();
    }
}
