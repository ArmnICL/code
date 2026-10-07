namespace StaffExpenseClaimEstimator;

public class ExpenseClaim
{
    public string StaffName { get; set; } = "";
    public DateTime ExpenseDate { get; set; }
    public ExpenseCategory Category { get; set; }
    public double Kilometres { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; } = "";
}
