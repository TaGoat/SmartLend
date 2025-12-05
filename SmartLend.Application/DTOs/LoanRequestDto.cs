namespace SmartLend.Application.DTOs;

public record LoanRequestDto(
    string ApplicantName,
    decimal Amount,
    decimal Salary,
    int CreditScore
);