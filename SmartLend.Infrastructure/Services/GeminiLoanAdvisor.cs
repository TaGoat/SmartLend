using System.Text;
using System.Text.Json;
using SmartLend.Domain.Entities;
using SmartLend.Domain.Services;

namespace SmartLend.Infrastructure.Services;

public class GeminiLoanAdvisor : ILoanAdvisor
{
    private const string ApiKey = "***REMOVED***"; 

    private const string ModelId = "gemini-2.5-flash"; 
    private const string Endpoint = $"https://generativelanguage.googleapis.com/v1beta/models/{ModelId}:generateContent?key={ApiKey}";

    private readonly HttpClient _httpClient;

    public GeminiLoanAdvisor()
    {
        _httpClient = new HttpClient();
    }

    public async Task<string> GenerateFinancialAdviceAsync(LoanApplication loan)
    {
        // 1. Create the Prompt
        var prompt = $@"
            You are a senior financial analyst at a bank.
            A customer applied for a loan:
            - Amount: ${loan.Amount}
            - Salary: ${loan.Salary}
            - Credit Score: {loan.CreditScore}
            - Result: {loan.Status}
            - Rejection Reason: {loan.DecisionReason}

            Write a short, helpful 2-sentence message to the customer explaining the result.
            Do not mention you are an AI.";

        var requestBody = new
        {
            contents = new[]
            {
                new { parts = new[] { new { text = prompt } } }
            }
        };

        var jsonContent = new StringContent(
            JsonSerializer.Serialize(requestBody), 
            Encoding.UTF8, 
            "application/json");

        try
        {
            var response = await _httpClient.PostAsync(Endpoint, jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return $"System Note: AI Error. Google said: {response.StatusCode} - {errorContent}";
            }

            var responseString = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseString);
            
            // Navigate the JSON: candidates[0] -> content -> parts[0] -> text
            var text = doc.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();

            return text ?? "No advice available.";
        }
        catch (Exception ex)
        {
            return $"System Note: AI Connection Failed. ({ex.Message})";
        }
    }
}