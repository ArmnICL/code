namespace StaffExpenseClaimEstimator;

/// <summary>Holds the details a staff member enters for one claim.</summary>
public class ExpenseClaim
{
    public string StaffName { get; set; } = "";
    public DateTime ExpenseDate { get; set; }
    public ExpenseCategory Category { get; set; }
    public double Kilometres { get; set; }   // used for Mileage only
    public decimal Amount { get; set; }      // used for Meal and Parking only
    public string Description { get; set; } = "";
}
