using System;
using System.Windows.Forms;

namespace StaffExpenseClaimEstimator;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }
}
