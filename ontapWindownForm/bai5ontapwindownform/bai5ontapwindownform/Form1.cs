namespace bai5ontapwindownform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // initialize runtime behavior
            this.KeyPreview = true;
            this.timer1.Tick += Timer1_Tick;
            this.timer1.Start();

            this.dgvItems.CellEndEdit += DgvItems_CellEndEdit;
            this.dgvItems.CellValidating += DgvItems_CellValidating;
            this.dgvItems.RowsRemoved += DgvItems_RowsRemoved;
            this.dgvItems.UserDeletingRow += DgvItems_UserDeletingRow;

            this.KeyDown += Form1_KeyDown;
            RecalculateTotals();
        }

        private void Timer1_Tick(object? sender, EventArgs e)
        {
            this.tslTime.Text = DateTime.Now.ToString("G");
        }

        private void DgvItems_CellValidating(object? sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var dgv = this.dgvItems;
            string header = dgv.Columns[e.ColumnIndex].HeaderText;
            string val = e.FormattedValue?.ToString() ?? string.Empty;

            if (header == "Quantity" || header == "Weight (kg)")
            {
                if (!double.TryParse(val, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double d) || d <= 0)
                {
                    // show error via ErrorProvider on the editing control if available
                    if (dgv.EditingControl != null)
                    {
                        errorProvider1.SetError(dgv.EditingControl, "Value must be a number greater than 0");
                    }
                    dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "Must be > 0";
                }
                else
                {
                    if (dgv.EditingControl != null)
                    {
                        errorProvider1.SetError(dgv.EditingControl, string.Empty);
                    }
                    dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = string.Empty;
                }
            }
        }

        private void DgvItems_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
        {
            // clear any editing control errors
            if (this.dgvItems.EditingControl != null)
            {
                errorProvider1.SetError(this.dgvItems.EditingControl, string.Empty);
            }

            // recalc line total for the edited row
            var row = this.dgvItems.Rows[e.RowIndex];
            if (row.IsNewRow) return;

            double qty = ParseDoubleCell(row.Cells["colQuantity"]);
            double wt = ParseDoubleCell(row.Cells["colWeight"]);
            double price = ParseDoubleCell(row.Cells["colUnitPrice"]);

            double lineTotal = Math.Round(qty * price, 2);
            row.Cells["colLineTotal"].Value = lineTotal.ToString("F2");

            RecalculateTotals();
        }

        private void DgvItems_RowsRemoved(object? sender, DataGridViewRowsRemovedEventArgs e)
        {
            RecalculateTotals();
        }

        private void DgvItems_UserDeletingRow(object? sender, DataGridViewRowCancelEventArgs e)
        {
            // allow deletion, totals will update in RowsRemoved
        }

        private void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                // Add new row
                this.dgvItems.Rows.Add();
                var idx = this.dgvItems.Rows.Count - 1;
                if (idx >= 0)
                {
                    this.dgvItems.CurrentCell = this.dgvItems.Rows[idx].Cells[0];
                    this.dgvItems.BeginEdit(true);
                }
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                // Delete selected rows
                var dgv = this.dgvItems;
                if (dgv.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow r in dgv.SelectedRows)
                    {
                        if (!r.IsNewRow) dgv.Rows.Remove(r);
                    }
                }
                else if (dgv.CurrentCell != null && !dgv.CurrentRow.IsNewRow)
                {
                    dgv.Rows.RemoveAt(dgv.CurrentCell.RowIndex);
                }
                e.Handled = true;
            }
        }

        private double ParseDoubleCell(DataGridViewCell cell)
        {
            if (cell?.Value == null) return 0;
            var s = cell.Value.ToString();
            if (double.TryParse(s, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double d)) return d;
            return 0;
        }

        private void RecalculateTotals()
        {
            double totalQty = 0;
            double totalWeight = 0;
            double totalAmt = 0;

            foreach (DataGridViewRow row in this.dgvItems.Rows)
            {
                if (row.IsNewRow) continue;
                double qty = ParseDoubleCell(row.Cells["colQuantity"]);
                double wt = ParseDoubleCell(row.Cells["colWeight"]);
                double price = ParseDoubleCell(row.Cells["colUnitPrice"]);
                double line = qty * price;

                totalQty += qty;
                totalWeight += qty * wt;
                totalAmt += line;
            }

            this.tslTotalQuantity.Text = $"Total Qty: {totalQty}";
            this.tslTotalWeight.Text = $"Total Wt: {totalWeight:F2} kg";
            this.tslTotalAmount.Text = $"Total Amt: {totalAmt:F2}";
        }
    }
}
