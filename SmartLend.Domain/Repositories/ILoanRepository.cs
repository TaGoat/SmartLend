using SmartLend.Domain.Entities;

namespace SmartLend.Domain.Repositories;

public interface ILoanRepository
{
    void Add(LoanApplication loan);

    LoanApplication? GetById(Guid id);

    void AddLog(AuditLog log);
}