using SmartLend.Application.DTOs;
using SmartLend.Domain.Entities;
using SmartLend.Domain.Repositories;
using SmartLend.Domain.Services;

namespace SmartLend.Application.Services;

public class LoanService
{
    private readonly ILoanRepository _repository;
    private readonly ILoanAdvisor _advisor;
    public LoanService(ILoanRepository repository, ILoanAdvisor advisor)
    {
        _repository = repository;
        _advisor = advisor;
    }

    public async Task<LoanApplication> ProcessLoan(LoanRequestDto request)
    {

        var loan = new LoanApplication(request.Amount, request.Salary, request.CreditScore);
        loan.Evaluate();
        _repository.Add(loan);
        var advice = await _advisor.GenerateFinancialAdviceAsync(loan);
        var log = new AuditLog(loan.Id, $"Status: {loan.Status} | AI Advice: {advice}");
        _repository.AddLog(log);

        return loan;
    }
}