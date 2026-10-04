using System.Globalization;
using StaffExpenseClaimEstimator;

// Staff Expense Claim Estimator - Assignment 2 proof-of-concept (console).
// Purpose: demonstrate input -> validation -> calculation -> output only.
// Out of scope (per case briefing): login, database, claim history, approvals, GST.

Console.WriteLine("=== Staff Expense Claim Estimator ===");
Console.WriteLine("Estimate only - not a formal claim.");

string again;
do
{
    Console.WriteLine();
    ExpenseClaim claim = ReadClaim();

    List<string> errors = ClaimValidator.Validate(claim);

    if (errors.Count > 0)
    {
        Console.WriteLine();
        Console.WriteLine("Please fix the following problem(s):");
        foreach (string error in errors)
        {
            Console.WriteLine("  - " + error);
        }
    }
    else
    {
        ClaimResult result = ClaimCalculator.Calculate(claim);
        ShowSummary(claim, result);
    }

    Console.Write("\nEstimate another claim? (y/n): ");
    again = (Console.ReadLine() ?? "n").Trim().ToLower();

} while (again == "y");

Console.WriteLine("Goodbye.");

// ---------- Input helpers ----------

static ExpenseClaim ReadClaim()
{
    ExpenseClaim claim = new ExpenseClaim();

    Console.Write("Staff name: ");
    claim.StaffName = Console.ReadLine() ?? "";

    Console.Write("Expense date (yyyy-mm-dd): ");
    string dateText = Console.ReadLine() ?? "";
    if (DateTime.TryParseExact(dateText.Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture,
                               DateTimeStyles.None, out DateTime date))
    {
        claim.ExpenseDate = date;
    }
    // if parsing fails, ExpenseDate stays default and the validator reports "date is required"

    Console.WriteLine("Category: 1 = Mileage, 2 = Meal, 3 = Parking");
    Console.Write("Choose category (1-3): ");
    string choice = (Console.ReadLine() ?? "").Trim();
    while (choice != "1" && choice != "2" && choice != "3")
    {
        Console.Write("Invalid choice. Enter 1, 2 or 3: ");
        choice = (Console.ReadLine() ?? "").Trim();
    }
    claim.Category = (ExpenseCategory)int.Parse(choice);

    if (claim.Category == ExpenseCategory.Mileage)
    {
        Console.Write("Kilometres travelled: ");
        double.TryParse(Console.ReadLine(), NumberStyles.Float, CultureInfo.InvariantCulture, out double km);
        claim.Kilometres = km;   // stays 0 if not a number -> validator reports an error
    }
    else
    {
        Console.Write("Expense amount ($): ");
        decimal.TryParse(Console.ReadLine(), NumberStyles.Number, CultureInfo.InvariantCulture, out decimal amount);
        claim.Amount = amount;   // stays 0 if not a number -> validator reports an error
    }

    Console.Write("Description / purpose: ");
    claim.Description = Console.ReadLine() ?? "";

    return claim;
}

// ---------- Output helper ----------

static void ShowSummary(ExpenseClaim claim, ClaimResult result)
{
    Console.WriteLine();
    Console.WriteLine("----- Claim Summary (estimate) -----");
    Console.WriteLine($"Staff name   : {claim.StaffName.Trim()}");
    Console.WriteLine($"Date         : {claim.ExpenseDate:dd MMM yyyy}");
    Console.WriteLine($"Category     : {claim.Category}");

    if (claim.Category == ExpenseCategory.Mileage)
    {
        Console.WriteLine($"Kilometres   : {claim.Kilometres}");
    }
    else
    {
        Console.WriteLine($"Amount entered: ${result.EnteredAmount:0.00}");
    }

    Console.WriteLine($"Description  : {claim.Description.Trim()}");
    Console.WriteLine($"Note         : {result.Message}");
    Console.WriteLine($"ESTIMATED REIMBURSABLE AMOUNT: ${result.EligibleAmount:0.00}");
}
