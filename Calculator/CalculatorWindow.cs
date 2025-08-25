namespace Calculator
{
    public partial class CalculatorWindow : Form
    {
        List<(string? Name, Action? Click)> Buttons = [];
        Parser Parser;

        public CalculatorWindow()
        {
            InitializeComponent();

            Parser = new();

            TLPButtons.ColumnCount = 5;
            TLPButtons.RowCount = 4;

            Buttons = new()
            {
                ("CE", TBInput.Clear),
                ("C", TBInput.Clear),
                ("(", () => Append("(")),
                (")", () => Append(")")),
                ("/", () => Append("/")),
                ("7", () => Append("7")),
                ("8", () => Append("8")),
                ("9", () => Append("9")),
                ("ans", () => Append("ans(")),
                ("*", () => Append("*")),
                ("4", () => Append("4")),
                ("5", () => Append("5")),
                ("6", () => Append("6")),
                ("=", () => Evaluate()),
                ("-", () => Append("-")),
                ("1", () => Append("1")),
                ("2", () => Append("2")),
                ("3", () => Append("3")),
                ("0", () => Append("0")),
                ("+", () => Append("+")),
            };

            MakeCellsEqual(TLPButtons);

            LoadButtons();
        }

        // stupid method because TableLayoutPanel is dumb and can"t make cells equal by itself
        void MakeCellsEqual(TableLayoutPanel tlp)
        {
            int colCount = tlp.ColumnCount;
            int rowCount = tlp.RowCount;

            // Set equal percent for columns
            tlp.ColumnStyles.Clear();
            for (int i = 0; i < colCount; i++)
            {
                tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / colCount));
            }

            // Set equal percent for rows
            tlp.RowStyles.Clear();
            for (int i = 0; i < rowCount; i++)
            {
                tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / rowCount));
            }
        }

        void Evaluate()
        {
            try
            {
                string input = TBInput.Text;
                decimal result = Parser.Evaluate(input);
                Parser.AppendHistory(result);
                TBInput.Text = result.ToString();
                TBInput.SelectionStart = TBInput.Text.Length;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void Append(string input)
        {
            TBInput.Text += input;
        }

        char GetCursorRight()
        {
            if (TBInput.SelectionStart < TBInput.Text.Length)
            {
                return TBInput.Text[TBInput.SelectionStart];
            }
            return '\0';
        }

        void LoadButtons()
        {
            foreach(var button in Buttons)
            {
                if(button.Name is null || button.Click is null)
                {
                    TLPButtons.Controls.Add(new Control() { Dock = DockStyle.Fill });
                    continue;
                }

                Button btn = new()
                {
                    Text = button.Name,
                    Dock = DockStyle.Fill,
                };
                btn.Font = new(btn.Font.FontFamily, 24);
                btn.Click += (s, e) => button.Click?.Invoke();
                TLPButtons.Controls.Add(btn);
            }
        }
    }
}
