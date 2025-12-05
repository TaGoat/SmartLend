using Microsoft.EntityFrameworkCore;
using SmartLend.Domain.Entities;

namespace SmartLend.Infrastructure.Persistence;

public class LoanDbContext : DbContext
{
    public LoanDbContext(DbContextOptions<LoanDbContext> options) : base(options)
    {
    }

    public DbSet<LoanApplication> Loans { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LoanApplication>().HasKey(l => l.Id);
        modelBuilder.Entity<AuditLog>().HasKey(l => l.Id);

        modelBuilder.Entity<LoanApplication>()
            .Property(l => l.Amount)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<LoanApplication>()
            .Property(l => l.Salary)
            .HasColumnType("decimal(18,2)");
    }
}