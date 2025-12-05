using Xunit;
using SmartLend.Domain.Entities;

namespace SmartLend.Domain.Tests;

public class LoanApplicationTests
{
    [Fact] 
    public void Evaluate_ShouldReject_WhenLoanAmountExceeds40PercentOfSalary()
    {
        decimal amount = 500000m;
        decimal salary = 1000m;
        int creditScore = 700; 

        var loan = new LoanApplication(amount, salary, creditScore);

        loan.Evaluate();

        Assert.Equal(LoanStatus.Rejected, loan.Status);
        Assert.Contains("exceeds", loan.DecisionReason); 
    }

    [Fact]
    public void Evaluate_ShouldApprove_WhenCriteriaMet()
    {
        var loan = new LoanApplication(5000, 10000, 750);
        
        loan.Evaluate();

        Assert.Equal(LoanStatus.Approved, loan.Status);
    }
}