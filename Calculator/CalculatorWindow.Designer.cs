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
            SuspendLayout();
            // 
            // TBInput
            // 
            TBInput.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TBInput.Location = new Point(12, 33);
            TBInput.Name = "TBInput";
            TBInput.Size = new Size(776, 35);
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
            TLPButtons.Location = new Point(12, 74);
            TLPButtons.Name = "TLPButtons";
            TLPButtons.RowCount = 4;
            TLPButtons.RowStyles.Add(new RowStyle());
            TLPButtons.RowStyles.Add(new RowStyle());
            TLPButtons.RowStyles.Add(new RowStyle());
            TLPButtons.RowStyles.Add(new RowStyle());
            TLPButtons.Size = new Size(776, 364);
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
            // CalculatorWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(LabelPreviousInput);
            Controls.Add(TLPButtons);
            Controls.Add(TBInput);
            Name = "CalculatorWindow";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox TBInput;
        private TableLayoutPanel TLPButtons;
        private Label LabelPreviousInput;
    }
}
