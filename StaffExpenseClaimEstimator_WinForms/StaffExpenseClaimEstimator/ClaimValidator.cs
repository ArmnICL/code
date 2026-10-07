namespace StaffExpenseClaimEstimator;

public static class ClaimValidator
{
    public static List<string> Validate(ExpenseClaim claim)
    {
        List<string> errors = new();

        if (string.IsNullOrWhiteSpace(claim.StaffName))
            errors.Add("Staff name is required.");

        if (string.IsNullOrWhiteSpace(claim.Description))
            errors.Add("Description / purpose of the expense is required.");

        if (claim.ExpenseDate == default)
            errors.Add("Expense date is required.");
        else if (claim.ExpenseDate.Date > DateTime.Today)
            errors.Add("Expense date cannot be in the future.");

        if (claim.Category == ExpenseCategory.Mileage)
        {
            if (claim.Kilometres <= 0)
                errors.Add("Kilometres must be greater than 0.");
        }
        else
        {
            if (claim.Amount <= 0)
                errors.Add("Expense amount must be greater than 0.");
        }

        return errors;
    }
}
