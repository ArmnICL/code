namespace StaffExpenseClaimEstimator;

/// <summary>
/// FEATURE 2 - Reimbursement calculation.
/// Applies the internal policy rules from the case briefing.
/// Assumes the claim has already passed ClaimValidator.
/// </summary>
public static class ClaimCalculator
{
    // Policy values from the case briefing (teaching case only)
    public const decimal MileageRatePerKm = 0.85m;
    public const decimal MealLimit = 35.00m;
    public const decimal ParkingLimit = 40.00m;

    public static ClaimResult Calculate(ExpenseClaim claim)
    {
        ClaimResult result = new ClaimResult();

        switch (claim.Category)
        {
            case ExpenseCategory.Mileage:
                // No cap on mileage: kilometres x rate
                result.EnteredAmount = Math.Round((decimal)claim.Kilometres * MileageRatePerKm, 2);
                result.EligibleAmount = result.EnteredAmount;
                result.WasCapped = false;
                result.Message = $"Mileage calculated at ${MileageRatePerKm:0.00} per km.";
                break;

            case ExpenseCategory.Meal:
                ApplyLimit(claim.Amount, MealLimit, "Meal", result);
                break;

            case ExpenseCategory.Parking:
                ApplyLimit(claim.Amount, ParkingLimit, "Parking", result);
                break;
        }

        return result;
    }

    /// <summary>If the entered amount is above the limit, only the limit is eligible.</summary>
    private static void ApplyLimit(decimal entered, decimal limit, string categoryName, ClaimResult result)
    {
        result.EnteredAmount = entered;

        if (entered > limit)
        {
            result.EligibleAmount = limit;
            result.WasCapped = true;
            result.Message = $"{categoryName} amount exceeds the ${limit:0.00} limit. " +
                             $"The estimate has been adjusted to the maximum allowed amount.";
        }
        else
        {
            result.EligibleAmount = entered;
            result.WasCapped = false;
            result.Message = $"{categoryName} amount is within the ${limit:0.00} limit.";
        }
    }
}
