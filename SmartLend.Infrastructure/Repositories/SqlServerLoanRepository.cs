using Microsoft.EntityFrameworkCore;
using SmartLend.Domain.Entities;
using SmartLend.Domain.Repositories;
using SmartLend.Infrastructure.Persistence;

namespace SmartLend.Infrastructure.Repositories;

public class SqlServerLoanRepository : ILoanRepository
{
    private readonly LoanDbContext _context;

    public SqlServerLoanRepository(LoanDbContext context)
    {
        _context = context;
    }

    public void Add(LoanApplication loan)
    {
        _context.Loans.Add(loan);
        _context.SaveChanges();
    }

    public void AddLog(AuditLog log)
    {
        _context.AuditLogs.Add(log);
        _context.SaveChanges();
    }

    public LoanApplication? GetById(Guid id)
    {
        return _context.Loans.FirstOrDefault(x => x.Id == id);
    }
}