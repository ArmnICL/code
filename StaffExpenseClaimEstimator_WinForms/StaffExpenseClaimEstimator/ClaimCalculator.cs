namespace StaffExpenseClaimEstimator;

public static class ClaimCalculator
{
    public const decimal MileageRatePerKm = 0.85m;
    public const decimal MealLimit = 35.00m;
    public const decimal ParkingLimit = 40.00m;

    public static ClaimResult Calculate(ExpenseClaim claim)
    {
        ClaimResult result = new();

        switch (claim.Category)
        {
            case ExpenseCategory.Mileage:
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

    private static void ApplyLimit(decimal entered, decimal limit, string categoryName, ClaimResult result)
    {
        result.EnteredAmount = entered;

        if (entered > limit)
        {
            result.EligibleAmount = limit;
            result.WasCapped = true;
            result.Message = $"{categoryName} amount exceeds the ${limit:0.00} limit. " +
                             "The estimate has been adjusted to the maximum allowed amount.";
        }
        else
        {
            result.EligibleAmount = entered;
            result.WasCapped = false;
            result.Message = $"{categoryName} amount is within the ${limit:0.00} limit.";
        }
    }
}
