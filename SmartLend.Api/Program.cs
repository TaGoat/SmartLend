using SmartLend.Application.Services;
using SmartLend.Domain.Repositories;
using SmartLend.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<LoanService>();


// Option A: In-Memory DB (Currently used)
builder.Services.AddSingleton<ILoanRepository, InMemoryLoanRepository>();

// Option B: SQL Server (Ready)
// builder.Services.AddDbContext<LoanDbContext>(options => 
//     options.UseSqlServer("Server=.;Database=SmartLendDb;Trusted_Connection=True;TrustServerCertificate=True;"));
// builder.Services.AddScoped<ILoanRepository, SqlServerLoanRepository>()
// Register the AI Advisor (Infrastructure)

builder.Services.AddSingleton<SmartLend.Domain.Services.ILoanAdvisor, SmartLend.Infrastructure.Services.GeminiLoanAdvisor>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();