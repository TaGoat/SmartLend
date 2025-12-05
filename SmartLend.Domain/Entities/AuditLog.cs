namespace SmartLend.Domain.Entities;

public class AuditLog
{
    public Guid Id { get; private set; }
    public Guid LoanApplicationId { get; private set; } 
    public string Action { get; private set; } // "Created", "Rejected", "Approved"
    public DateTime Timestamp { get; private set; }

    public AuditLog(Guid loanId, string action)
    {
        Id = Guid.NewGuid();
        LoanApplicationId = loanId;
        Action = action;
        Timestamp = DateTime.Now; 
    }
}