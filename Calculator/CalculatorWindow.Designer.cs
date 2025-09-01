namespace Calculator
{
    partial class CalculatorWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            TBInput = new TextBox();
            TLPButtons = new TableLayoutPanel();
            LabelPreviousInput = new Label();
            RTBHistory = new RichTextBox();
            FLPSpecialFunctions = new FlowLayoutPanel();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // TBInput
            // 
            TBInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TBInput.BackColor = Color.FromArgb(18, 32, 48);
            TBInput.BorderStyle = BorderStyle.None;
            TBInput.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TBInput.ForeColor = Color.White;
            TBInput.Location = new Point(15, 190);
            TBInput.Name = "TBInput";
            TBInput.Size = new Size(776, 28);
            TBInput.TabIndex = 0;
            TBInput.Text = "1+2";
            TBInput.TextAlign = HorizontalAlignment.Right;
            // 
            // TLPButtons
            // 
            TLPButtons.ColumnCount = 4;
            TLPButtons.ColumnStyles.Add(new ColumnStyle());
            TLPButtons.ColumnStyles.Add(new ColumnStyle());
            TLPButtons.ColumnStyles.Add(new ColumnStyle());
            TLPButtons.ColumnStyles.Add(new ColumnStyle());
            TLPButtons.Dock = DockStyle.Fill;
            TLPButtons.Location = new Point(391, 3);
            TLPButtons.Name = "TLPButtons";
            TLPButtons.RowCount = 4;
            TLPButtons.RowStyles.Add(new RowStyle());
            TLPButtons.RowStyles.Add(new RowStyle());
            TLPButtons.RowStyles.Add(new RowStyle());
            TLPButtons.RowStyles.Add(new RowStyle());
            TLPButtons.Size = new Size(382, 365);
            TLPButtons.TabIndex = 1;
            // 
            // LabelPreviousInput
            // 
            LabelPreviousInput.AutoSize = true;
            LabelPreviousInput.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            LabelPreviousInput.Location = new Point(12, 9);
            LabelPreviousInput.Name = "LabelPreviousInput";
            LabelPreviousInput.Size = new Size(0, 21);
            LabelPreviousInput.TabIndex = 2;
            // 
            // RTBHistory
            // 
            RTBHistory.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            RTBHistory.BackColor = Color.FromArgb(18, 32, 48);
            RTBHistory.BorderStyle = BorderStyle.None;
            RTBHistory.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            RTBHistory.ForeColor = Color.White;
            RTBHistory.Location = new Point(12, 12);
            RTBHistory.Name = "RTBHistory";
            RTBHistory.ReadOnly = true;
            RTBHistory.Size = new Size(776, 172);
            RTBHistory.TabIndex = 3;
            RTBHistory.Text = "";
            // 
            // FLPSpecialFunctions
            // 
            FLPSpecialFunctions.AutoScroll = true;
            FLPSpecialFunctions.Dock = DockStyle.Fill;
            FLPSpecialFunctions.Location = new Point(3, 3);
            FLPSpecialFunctions.Name = "FLPSpecialFunctions";
            FLPSpecialFunctions.Size = new Size(382, 365);
            FLPSpecialFunctions.TabIndex = 4;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(FLPSpecialFunctions, 0, 0);
            tableLayoutPanel1.Controls.Add(TLPButtons, 1, 0);
            tableLayoutPanel1.Location = new Point(12, 224);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(776, 371);
            tableLayoutPanel1.TabIndex = 5;
            // 
            // CalculatorWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 18, 18);
            ClientSize = new Size(800, 607);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(RTBHistory);
            Controls.Add(LabelPreviousInput);
            Controls.Add(TBInput);
            ForeColor = Color.White;
            Name = "CalculatorWindow";
            Text = "Calculator";
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox TBInput;
        private TableLayoutPanel TLPButtons;
        private Label LabelPreviousInput;
        private RichTextBox RTBHistory;
        private FlowLayoutPanel FLPSpecialFunctions;
        private TableLayoutPanel tableLayoutPanel1;
    }
}
