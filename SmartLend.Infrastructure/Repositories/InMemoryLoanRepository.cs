using SmartLend.Domain.Entities;
using SmartLend.Domain.Repositories;

namespace SmartLend.Infrastructure.Repositories;

public class InMemoryLoanRepository : ILoanRepository
{
    private static readonly List<LoanApplication> _database = new();
    private static readonly List<AuditLog> _logDatabase = new();

    public void Add(LoanApplication loan)
    {
        _database.Add(loan);
    }
    public void AddLog(AuditLog log)
    {
        _logDatabase.Add(log);
        Console.WriteLine($"[AUDIT LOG] Action: {log.Action} | Time: {log.Timestamp}");
    }

    public LoanApplication? GetById(Guid id)
    {
        return _database.FirstOrDefault(x => x.Id == id);
    }
}