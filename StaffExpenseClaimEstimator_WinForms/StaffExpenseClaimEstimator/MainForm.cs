using System.Drawing;
using System.Globalization;

namespace StaffExpenseClaimEstimator;

public class MainForm : Form
{
    private readonly TextBox txtStaffName = new();
    private readonly DateTimePicker dtpExpenseDate = new();
    private readonly ComboBox cmbCategory = new();
    private readonly Label lblValue = new();
    private readonly NumericUpDown nudValue = new();
    private readonly TextBox txtDescription = new();
    private readonly Button btnEstimate = new();
    private readonly Button btnClear = new();
    private readonly Panel pnlResult = new();
    private readonly Label lblResultAmount = new();
    private readonly Label lblResultDetails = new();
    private readonly Label lblStatus = new();

    public MainForm()
    {
        Text = "Staff Expense Claim Estimator";
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(720, 650);
        Size = new Size(800, 720);
        BackColor = Color.FromArgb(245, 247, 250);
        Font = new Font("Segoe UI", 10F);
        InitializeUi();
        UpdateValueField();
    }

    private void InitializeUi()
    {
        var header = new Panel
        {
            Dock = DockStyle.Top,
            Height = 105,
            BackColor = Color.FromArgb(35, 47, 62)
        };

        var title = new Label
        {
            Text = "Staff Expense Claim Estimator",
            ForeColor = Color.White,
            Font = new Font("Segoe UI Semibold", 22F),
            Location = new Point(32, 20),
            AutoSize = true
        };

        var subtitle = new Label
        {
            Text = "Estimate only — not a formal claim.",
            ForeColor = Color.FromArgb(205, 214, 224),
            Font = new Font("Segoe UI", 10F),
            Location = new Point(35, 62),
            AutoSize = true
        };

        header.Controls.Add(title);
        header.Controls.Add(subtitle);

        var content = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(32, 25, 32, 25),
            ColumnCount = 1,
            RowCount = 5
        };
        content.RowStyles.Add(new RowStyle(SizeType.Absolute, 55));
        content.RowStyles.Add(new RowStyle(SizeType.Absolute, 55));
        content.RowStyles.Add(new RowStyle(SizeType.Absolute, 55));
        content.RowStyles.Add(new RowStyle(SizeType.Absolute, 90));
        content.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        AddField(content, 0, "Staff name", txtStaffName);
        dtpExpenseDate.Format = DateTimePickerFormat.Custom;
        dtpExpenseDate.CustomFormat = "dd MMM yyyy";
        dtpExpenseDate.MaxDate = DateTime.Today;
        dtpExpenseDate.Value = DateTime.Today;
        AddField(content, 1, "Expense date", dtpExpenseDate);

        cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbCategory.Items.AddRange(new object[] { "Mileage", "Meal", "Parking" });
        cmbCategory.SelectedIndex = 0;
        cmbCategory.SelectedIndexChanged += (_, _) => UpdateValueField();
        AddField(content, 2, "Category", cmbCategory);

        var valuePanel = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1 };
        valuePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150));
        valuePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        lblValue.Text = "Kilometres";
        lblValue.TextAlign = ContentAlignment.MiddleLeft;
        valuePanel.Controls.Add(lblValue, 0, 0);
        nudValue.DecimalPlaces = 2;
        nudValue.Maximum = 100000;
        nudValue.Minimum = 0;
        nudValue.Increment = 0.5M;
        nudValue.Dock = DockStyle.Fill;
        valuePanel.Controls.Add(nudValue, 1, 0);
        content.Controls.Add(valuePanel, 0, 3);

        var bottom = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3 };
        bottom.RowStyles.Add(new RowStyle(SizeType.Absolute, 65));
        bottom.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
        bottom.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        var descPanel = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2 };
        descPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150));
        descPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        var descLabel = new Label { Text = "Description / purpose", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
        txtDescription.Multiline = true;
        txtDescription.Dock = DockStyle.Fill;
        descPanel.Controls.Add(descLabel, 0, 0);
        descPanel.Controls.Add(txtDescription, 1, 0);
        bottom.Controls.Add(descPanel, 0, 0);

        var buttons = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft };
        btnEstimate.Text = "Estimate Reimbursement";
        btnEstimate.AutoSize = true;
        btnEstimate.Height = 38;
        btnEstimate.Padding = new Padding(15, 0, 15, 0);
        btnEstimate.BackColor = Color.FromArgb(0, 120, 215);
        btnEstimate.ForeColor = Color.White;
        btnEstimate.FlatStyle = FlatStyle.Flat;
        btnEstimate.Click += (_, _) => EstimateClaim();

        btnClear.Text = "Clear";
        btnClear.AutoSize = true;
        btnClear.Height = 38;
        btnClear.Padding = new Padding(15, 0, 15, 0);
        btnClear.Click += (_, _) => ClearForm();

        buttons.Controls.Add(btnEstimate);
        buttons.Controls.Add(btnClear);
        bottom.Controls.Add(buttons, 0, 1);

        pnlResult.Dock = DockStyle.Fill;
        pnlResult.Padding = new Padding(18);
        pnlResult.BackColor = Color.White;
        pnlResult.BorderStyle = BorderStyle.FixedSingle;

        lblResultAmount.Text = "Estimated reimbursable amount: $0.00";
        lblResultAmount.Font = new Font("Segoe UI Semibold", 18F);
        lblResultAmount.AutoSize = true;
        lblResultDetails.Text = "Enter claim details and select Estimate Reimbursement.";
        lblResultDetails.AutoSize = true;
        lblResultDetails.MaximumSize = new Size(650, 60);
        lblResultDetails.Location = new Point(20, 58);
        lblResultAmount.Location = new Point(20, 18);
        lblStatus.Text = "";
        lblStatus.AutoSize = true;
        lblStatus.Location = new Point(20, 125);

        pnlResult.Controls.Add(lblResultAmount);
        pnlResult.Controls.Add(lblResultDetails);
        pnlResult.Controls.Add(lblStatus);
        bottom.Controls.Add(pnlResult, 0, 2);

        content.Controls.Add(bottom, 0, 4);

        Controls.Add(content);
        Controls.Add(header);
    }

    private void AddField(TableLayoutPanel parent, int row, string labelText, Control control)
    {
        var panel = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2 };
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150));
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

        var label = new Label
        {
            Text = labelText,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft
        };

        control.Dock = DockStyle.Fill;
        panel.Controls.Add(label, 0, 0);
        panel.Controls.Add(control, 1, 0);
        parent.Controls.Add(panel, 0, row);
    }

    private void UpdateValueField()
    {
        if (cmbCategory.SelectedIndex == 0)
        {
            lblValue.Text = "Kilometres";
            nudValue.DecimalPlaces = 2;
            nudValue.Increment = 0.5M;
            nudValue.Maximum = 100000;
        }
        else
        {
            lblValue.Text = "Expense amount ($)";
            nudValue.DecimalPlaces = 2;
            nudValue.Increment = 1M;
            nudValue.Maximum = 1000000;
        }
        nudValue.Value = 0;
    }

    private void EstimateClaim()
    {
        ExpenseCategory category = (ExpenseCategory)(cmbCategory.SelectedIndex + 1);

        var claim = new ExpenseClaim
        {
            StaffName = txtStaffName.Text,
            ExpenseDate = dtpExpenseDate.Value.Date,
            Category = category,
            Description = txtDescription.Text
        };

        if (category == ExpenseCategory.Mileage)
            claim.Kilometres = (double)nudValue.Value;
        else
            claim.Amount = nudValue.Value;

        var errors = ClaimValidator.Validate(claim);

        if (errors.Count > 0)
        {
            MessageBox.Show(
                string.Join(Environment.NewLine, errors.Select(e => "• " + e)),
                "Please fix the following problem(s)",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            lblStatus.Text = "Validation failed. Please correct the highlighted information.";
            return;
        }

        ClaimResult result = ClaimCalculator.Calculate(claim);

        lblResultAmount.Text = $"Estimated reimbursable amount: ${result.EligibleAmount:0.00}";
        lblResultDetails.Text =
            $"Staff: {claim.StaffName.Trim()}   |   Date: {claim.ExpenseDate:dd MMM yyyy}" +
            Environment.NewLine +
            $"Category: {claim.Category}   |   {result.Message}";

        lblStatus.Text = result.WasCapped
            ? $"Entered amount: ${result.EnteredAmount:0.00} — policy limit applied."
            : $"Entered/calculated amount: ${result.EnteredAmount:0.00} — fully eligible.";
    }

    private void ClearForm()
    {
        txtStaffName.Clear();
        dtpExpenseDate.Value = DateTime.Today;
        cmbCategory.SelectedIndex = 0;
        nudValue.Value = 0;
        txtDescription.Clear();
        lblResultAmount.Text = "Estimated reimbursable amount: $0.00";
        lblResultDetails.Text = "Enter claim details and select Estimate Reimbursement.";
        lblStatus.Text = "";
    }
}
