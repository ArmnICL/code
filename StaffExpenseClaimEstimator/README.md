# Staff Expense Claim Estimator - Assignment 2 Proof-of-Concept

BBIS 5101 Introduction to Object-Oriented Programming - Assignment 2
Group number: [ ]   Student A: [name, ID]   Student B: [name, ID]

## What it does
A small C# console program that demonstrates the core logic of the Staff Expense Claim
Estimator case: enter claim details, validate them, apply the policy rules, and display an
estimated reimbursable amount. It is a logic proof-of-concept only, not a complete application.

## How to run (Visual Studio)
1. Open `StaffExpenseClaimEstimator.sln`.
2. Press F5 (or Ctrl+F5) to run.
3. Follow the prompts. Dates use yyyy-mm-dd. Category: 1 Mileage, 2 Meal, 3 Parking.
(Target framework is .NET 8; retarget in project properties if your Visual Studio uses another version.)

## Files
| File | Purpose |
|---|---|
| Program.cs | Input prompts, calls validation and calculation, displays the claim summary |
| ExpenseCategory.cs | Enum: Mileage, Meal, Parking |
| ExpenseClaim.cs | Data entered for one claim |
| ClaimValidator.cs | Feature 1 - input validation |
| ClaimCalculator.cs | Feature 2 - reimbursement calculation with limits |
| ClaimResult.cs | Calculation outcome (eligible amount, capped flag, message) |

## Business rules (from the case briefing - teaching case only)
- Mileage: $0.85 per km; kilometres must be greater than 0
- Meal: maximum $35.00 per meal
- Parking: maximum $40.00 per expense
- If the amount exceeds the limit, the estimate is adjusted to the maximum and the user is told

## Out of scope (per case briefing)
Login, database/storage, claim history, approvals, receipts, notifications, payroll, GST/tax, reporting.

## Note on AI use
Parts of this project were drafted with AI assistance. [Describe how you reviewed, understood and
changed the code - state this honestly.]
