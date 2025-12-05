namespace SmartLend.Domain.Entities;

public enum LoanStatus
{
    Pending,
    Approved,
    Rejected
}

public class LoanApplication
{
    private const decimal MAX_DEBT_TO_INCOME_RATIO = 0.40m; // 40%
    private const int MINIMUM_CREDIT_SCORE = 600;

    public Guid Id { get; private set; }
    public decimal Amount { get; private set; }
    public decimal Salary { get; private set; }
    public int CreditScore { get; private set; }
    
    public LoanStatus Status { get; private set; }
    public string DecisionReason { get; private set; }
    public LoanApplication(decimal amount, decimal salary, int creditScore)
    {
        if (amount <= 0) 
            throw new ArgumentException("Loan amount must be positive.");
            
        Id = Guid.NewGuid();
        Amount = amount;
        Salary = salary;
        CreditScore = creditScore;
        
        Status = LoanStatus.Pending;
        DecisionReason = "Waiting for evaluation";
    }
    public void Evaluate()
    {
        var annualIncome = Salary * 12;
        var maxAllowedLoan = annualIncome * MAX_DEBT_TO_INCOME_RATIO;

        if (Amount > maxAllowedLoan)
        {
            Reject($"Loan amount exceeds {MAX_DEBT_TO_INCOME_RATIO:P0} of annual income.");
            return;
        }

        if (CreditScore < MINIMUM_CREDIT_SCORE)
        {
            Reject($"Credit score is below the minimum of {MINIMUM_CREDIT_SCORE}.");
            return;
        }

        Approve();
    }

    private void Approve()
    {
        Status = LoanStatus.Approved;
        DecisionReason = "Congratulations! Your loan meets all criteria.";
    }

    private void Reject(string reason)
    {
        Status = LoanStatus.Rejected;
        DecisionReason = reason;
    }
}