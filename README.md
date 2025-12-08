# SmartLend 🏦
### AI-Powered Financial Loan Decision Engine

> **Enterprise-grade Loan Processing System built with Clean Architecture, Domain-Driven Design (DDD), and .NET 10.**

---

## 🚀 Overview
**SmartLend** is a compliance-ready financial engine designed to automate loan risk assessment. It moves beyond simple CRUD to demonstrate how **Deterministic Logic** and **Generative AI** can work together in a secure banking environment.

The system enforces strict financial rules using C# (safe), while leveraging **Google Gemini 2.5 Flash** to generate empathetic, human-readable explanations for customers (smart).

---

## 🏗️ Architecture & Design Decisions

The solution follows strict **Clean Architecture** principles to ensure that Business Logic is isolated from Technical Details.

### The 4 Layers (The Onion)
1.  **🟡 Domain (The Core):** Contains the Enterprise Business Rules.
    * *Rich Domain Model:* Logic is encapsulated in `LoanApplication.Evaluate()`, ensuring a loan can never exist in an invalid state.
    * *Pure:* Zero dependencies on external libraries.
2.  **🔴 Application (The Orchestrator):** Contains the Use Cases.
    * Orchestrates the flow: `DTO -> Domain Logic -> Database -> AI Advisor`.
3.  **🔵 Infrastructure (The Plugins):** Contains the external tools.
    * *Persistence Strategy:* Implements the Repository Pattern with **Dual Support** (In-Memory & SQL Server).
    * *AI Service:* Google Gemini 2.5 Flash implementation via `HttpClient`.
4.  **🟢 API (The Entry Point):** The REST API/Swagger UI.
    * Wires everything together using Dependency Injection.

---

## 🌟 Key Features

### 🤖 1. AI Financial Advisor (Google Gemini 2.5)
I integrated **Google Gemini 2.5 Flash** into the Infrastructure layer to act as a "Financial Advisor."
* **Workflow:** When a loan is rejected by the C# math engine, the AI analyzes the user's data (Salary, Credit Score) and generates a polite, personalized financial tip.
* **Safety:** The AI **never** makes the approval decision. It only explains it. This prevents "AI Hallucinations" from approving risky loans.

### 💾 2. Dual Persistence Strategy (SQL & In-Memory)
To demonstrate architectural flexibility, I implemented **two** database providers in the Infrastructure layer:
* **`InMemoryLoanRepository`:** Optimized for rapid development, unit testing, and demos (Currently Active).
* **`SqlServerLoanRepository`:** Production-ready implementation using **Entity Framework Core**.
* *Note:* The application is currently configured to run in **In-Memory Mode** for ease of review (no local SQL setup required). To switch to SQL, simply uncomment the line in `Program.cs`.

### 📜 3. Compliance Audit Log
Every decision is immutable. The system records a timestamped (UTC) **Audit Log** for every action.
* *Example Log:* `[AUDIT] Action: Rejected | Time: 12/05/2025 | AI Advice: "Debt-to-Income ratio too high..."`

---

## 🛠️ Tech Stack
* **Framework:** .NET 10 (Preview)
* **Architecture:** Clean Architecture / Onion Architecture
* **AI Model:** Google Gemini 2.5 Flash 
* **Database:** In-Memory (Active) / SQL Server (Implemented)
* **Testing:** xUnit
* **Documentation:** Swagger / OpenAPI

---

## 🏃‍♂️ How to Run

### Prerequisites
* .NET 10.0 SDK installed.
* (Optional) A Google Gemini API Key. *Note: If no key is provided, the system gracefully degrades to "Offline Mode" and still processes loans.*

### Steps
1.  **Clone the repository:**
    ```bash
    git clone [https://github.com/TaGoat/SmartLend.git](https://github.com/TaGoat/SmartLend.git)
    cd SmartLend
    ```

2.  **Run the API:**
    ```bash
    dotnet run --project SmartLend.Api
    ```

3.  **Test with Swagger:**
    Open your browser to the URL shown in the terminal (e.g., `http://localhost:5xxx/swagger`).
    * **POST /api/loans:** Submit a loan request.
    * **Check Console:** Watch the VS Code terminal to see the AI Advice and Audit Logs printed in real-time.

---

## 🧪 Testing Strategy
Core business logic is covered by **Unit Tests** in the `SmartLend.Domain.Tests` project.
* **Run Tests:** `dotnet test`
* **Coverage:** Verifies that the "Debt-to-Income" and "Credit Score" rules trigger the correct Rejection status.

---

*Built by [Tareq AL-Suraihi]*
