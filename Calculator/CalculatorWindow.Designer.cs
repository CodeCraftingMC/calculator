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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CalculatorWindow));
            TBInput = new TextBox();
            TLPButtons = new TableLayoutPanel();
            LabelPreviousInput = new Label();
            RTBHistory = new RichTextBox();
            FLPSpecialFunctions = new FlowLayoutPanel();
            tableLayoutPanel1 = new TableLayoutPanel();
            BtnClearHistory = new Button();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // TBInput
            // 
            TBInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TBInput.BackColor = Color.FromArgb(18, 32, 48);
            TBInput.BorderStyle = BorderStyle.None;
            TBInput.Font = new Font("Consolas", 16.125F);
            TBInput.ForeColor = Color.White;
            TBInput.Location = new Point(8, 190);
            TBInput.Name = "TBInput";
            TBInput.Size = new Size(784, 26);
            TBInput.TabIndex = 0;
            TBInput.TextAlign = HorizontalAlignment.Right;
            TBInput.KeyDown += TBInput_KeyDown;
            TBInput.Leave += TBInput_Leave;
            // 
            // TLPButtons
            // 
            TLPButtons.ColumnCount = 4;
            TLPButtons.ColumnStyles.Add(new ColumnStyle());
            TLPButtons.ColumnStyles.Add(new ColumnStyle());
            TLPButtons.ColumnStyles.Add(new ColumnStyle());
            TLPButtons.ColumnStyles.Add(new ColumnStyle());
            TLPButtons.Dock = DockStyle.Fill;
            TLPButtons.Location = new Point(395, 3);
            TLPButtons.Name = "TLPButtons";
            TLPButtons.RowCount = 4;
            TLPButtons.RowStyles.Add(new RowStyle());
            TLPButtons.RowStyles.Add(new RowStyle());
            TLPButtons.RowStyles.Add(new RowStyle());
            TLPButtons.RowStyles.Add(new RowStyle());
            TLPButtons.Size = new Size(386, 369);
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
            RTBHistory.Font = new Font("Consolas", 16.125F);
            RTBHistory.ForeColor = Color.White;
            RTBHistory.Location = new Point(113, 9);
            RTBHistory.Name = "RTBHistory";
            RTBHistory.ReadOnly = true;
            RTBHistory.Size = new Size(679, 175);
            RTBHistory.TabIndex = 3;
            RTBHistory.Text = "";
            // 
            // FLPSpecialFunctions
            // 
            FLPSpecialFunctions.AutoScroll = true;
            FLPSpecialFunctions.Dock = DockStyle.Fill;
            FLPSpecialFunctions.FlowDirection = FlowDirection.TopDown;
            FLPSpecialFunctions.Location = new Point(3, 3);
            FLPSpecialFunctions.Name = "FLPSpecialFunctions";
            FLPSpecialFunctions.Size = new Size(386, 369);
            FLPSpecialFunctions.TabIndex = 4;
            FLPSpecialFunctions.WrapContents = false;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(FLPSpecialFunctions, 0, 0);
            tableLayoutPanel1.Controls.Add(TLPButtons, 1, 0);
            tableLayoutPanel1.Location = new Point(8, 219);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(784, 375);
            tableLayoutPanel1.TabIndex = 5;
            // 
            // BtnClearHistory
            // 
            BtnClearHistory.BackColor = Color.FromArgb(18, 32, 48);
            BtnClearHistory.FlatAppearance.BorderColor = Color.DarkCyan;
            BtnClearHistory.FlatStyle = FlatStyle.Flat;
            BtnClearHistory.Location = new Point(8, 9);
            BtnClearHistory.Margin = new Padding(2, 1, 2, 1);
            BtnClearHistory.Name = "BtnClearHistory";
            BtnClearHistory.Size = new Size(100, 39);
            BtnClearHistory.TabIndex = 6;
            BtnClearHistory.Text = "Clear History";
            BtnClearHistory.UseVisualStyleBackColor = false;
            BtnClearHistory.Click += BtnClearHistory_Click;
            // 
            // CalculatorWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 18, 18);
            ClientSize = new Size(800, 607);
            Controls.Add(BtnClearHistory);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(RTBHistory);
            Controls.Add(LabelPreviousInput);
            Controls.Add(TBInput);
            ForeColor = Color.White;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "CalculatorWindow";
            Text = "Calcflex";
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
        private Button BtnClearHistory;
    }
}
