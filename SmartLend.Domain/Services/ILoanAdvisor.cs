using SmartLend.Domain.Entities;

namespace SmartLend.Domain.Services;
public interface ILoanAdvisor
{
    Task<string> GenerateFinancialAdviceAsync(LoanApplication loan);
}