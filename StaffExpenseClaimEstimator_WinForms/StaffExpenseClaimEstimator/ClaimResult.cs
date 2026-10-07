namespace StaffExpenseClaimEstimator;

public class ClaimResult
{
    public decimal EnteredAmount { get; set; }
    public decimal EligibleAmount { get; set; }
    public bool WasCapped { get; set; }
    public string Message { get; set; } = "";
}
