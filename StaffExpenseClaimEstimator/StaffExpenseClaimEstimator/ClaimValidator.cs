namespace StaffExpenseClaimEstimator;

/// <summary>
/// FEATURE 1 - Input validation.
/// Checks the claim and returns a list of error messages (empty list = valid).
/// </summary>
public static class ClaimValidator
{
    public static List<string> Validate(ExpenseClaim claim)
    {
        List<string> errors = new List<string>();

        // 1. Required text fields
        if (string.IsNullOrWhiteSpace(claim.StaffName))
        {
            errors.Add("Staff name is required.");
        }

        if (string.IsNullOrWhiteSpace(claim.Description))
        {
            errors.Add("Description / purpose of the expense is required.");
        }

        // 2. Date must be entered and not in the future (assumption A3)
        if (claim.ExpenseDate == default(DateTime))
        {
            errors.Add("Expense date is required.");
        }
        else if (claim.ExpenseDate.Date > DateTime.Today)
        {
            errors.Add("Expense date cannot be in the future.");
        }

        // 3. Category-specific numeric checks
        if (claim.Category == ExpenseCategory.Mileage)
        {
            if (claim.Kilometres <= 0)
            {
                errors.Add("Kilometres must be greater than 0.");
            }
        }
        else // Meal or Parking
        {
            if (claim.Amount <= 0)
            {
                errors.Add("Expense amount must be greater than 0.");
            }
        }

        return errors;
    }
}
