namespace StaffExpenseClaimEstimator;

/// <summary>The outcome of calculating one claim.</summary>
public class ClaimResult
{
    public decimal EnteredAmount { get; set; }     // what the staff member claimed / km x rate
    public decimal EligibleAmount { get; set; }    // estimated reimbursable amount
    public bool WasCapped { get; set; }            // true if a limit reduced the amount
    public string Message { get; set; } = "";      // notice shown to the user
}
