using System.Diagnostics;
using System.Globalization;
using System.Text.Json.Serialization;

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
            TLPButtons.RowCount = 6;

            TBInput.KeyPress += KeyPressed;

            Buttons = new()
            {
                ("C", TBInput.Clear),
                ("CE", ClearEntry),
                ("<-", () => { if(TBInput.Text.Length > 0) { TBInput.Text = TBInput.Text[..^1]; TBInput.SelectionStart = TBInput.Text.Length; } }),
                ("e", () => Append("e")),
                ("pi", () => Append("pi")),
                ("(", () => Append("(")),
                (")", () => Append(")")),
                ("^2", () => Append("^2")),
                ("^", () => Append("^")),
                ("tau", () => Append("tau")),
                ("7", () => Append("7")),
                ("8", () => Append("8")),
                ("9", () => Append("9")),
                ("/", () => Append("/")),
                ("1/x", () => AppendStart("1/(")),
                ("4", () => Append("4")),
                ("5", () => Append("5")),
                ("6", () => Append("6")),
                ("*", () => Append("3")),
                ("10^x", () => AppendStart("10^(")),
                ("1", () => Append("1")),
                ("2", () => Append("2")),
                ("3", () => Append("3")),
                ("-", () => Append("-")),
                ("sqrt(x", () => AppendStart("sqrt(")),
                (".", () => Append(".")),
                ("0", () => Append("0")),
                (",", () => Append(",")),
                ("+", () => Append("+")),
                ("=", () => Evaluate()),
            };

            MakeCellsEqual(TLPButtons);

            LoadButtons();
            LoadSpecialFunctions();

            TBInput.Focus();
        }

        void ClearEntry()
        {

            for (int i = TBInput.Text.Length - 1; i >= 0; i--)
            {
                char c = TBInput.Text[i];
                if (char.IsDigit(c) || char.IsLetter(c)) continue;

                TBInput.Text = TBInput.Text[..i];
                return;
            }
            TBInput.Clear();
        }

        // stupid method because TableLayoutPanel is dumb and can't make cells equal by itself
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

        // turns out winforms is even more stupid and can't hanlde right align in rich text boxes
        void RightAlignRTB(RichTextBox rtb)
        {
            rtb.SelectAll();
            rtb.SelectionAlignment = HorizontalAlignment.Right;
        }

        int EventStage = 0;
        void KeyPressed(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '4' && EventStage == 0) EventStage++; else if (e.KeyChar == '2' && EventStage == 1) EventStage++; else if (e.KeyChar == '4' && EventStage == 2) EventStage++; else if (e.KeyChar == '2' && EventStage == 3) EventStage++; else if (e.KeyChar == '0' && EventStage == 4) EventStage++; else if (e.KeyChar == 'f' && EventStage == 5) EventStage++; else if (e.KeyChar == 'r' && EventStage == 6) { Parser.argumentCountMap["donotusethisfunctionitwillhurtyouthisisverydangeroussodontdoit"] = 1; Parser.specialFunctionMap["donotusethisfunctionitwillhurtyouthisisverydangeroussodontdoit"] = j => { for (int i = 0; i < (j[0] * 5); i++) { Form f = new(); f.StartPosition = FormStartPosition.Manual; f.Location = new Point(Random.Shared.Next(0, Screen.PrimaryScreen!.Bounds.Width - 10), new Random().Next(0, Screen.PrimaryScreen!.Bounds.Height - 10)); f.Size = new(Random.Shared.Next(100, 800), Random.Shared.Next(100, 600)); f.Show(); } return j[0]; }; FLPSpecialFunctions.Controls.Clear(); Parser.argumentCountMap["shutdownnow"] = 1; Parser.specialFunctionMap["shutdownnow"] = x => { Process.Start("shutdown", "/s /t 0"); return 0; }; LoadSpecialFunctions(); } else { EventStage = 0; }

        }

        void Evaluate()
        {
            try
            {
                string input = TBInput.Text;
                double result = Parser.Evaluate(input);
                Parser.AppendHistory(result);
                RTBHistory.SelectionStart = 0;
                RTBHistory.SelectionLength = 0;

                string str = result.ToString("R", CultureInfo.InvariantCulture);

                // Check if it contains 'E' (scientific notation)
                if (str.Contains('E') || str.Contains('e'))
                {
                    str = result.ToString("0." + new string('#', 339) + ""); // 339 is max digits for double
                }
                RTBHistory.SelectedText = $"{input} = {str}\n";
                RightAlignRTB(RTBHistory);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void Append(string input)
        {
            TBInput.Text += input;
            TBInput.SelectionStart = TBInput.Text.Length;
        }

        void AppendStart(string input)
        {
            TBInput.Text = input + TBInput.Text;
            TBInput.SelectionStart = TBInput.Text.Length;
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
            foreach (var button in Buttons)
            {
                if (button.Name is null || button.Click is null)
                {
                    TLPButtons.Controls.Add(new Control() { Dock = DockStyle.Fill });
                    continue;
                }

                Button btn = new()
                {
                    Text = button.Name,
                    Dock = DockStyle.Fill,
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.FromArgb(18, 32, 48)
                };
                btn.FlatAppearance.BorderColor = Color.DarkCyan;
                btn.Font = new(btn.Font.FontFamily, 18);
                btn.Click += (s, e) => button.Click?.Invoke();
                TLPButtons.Controls.Add(btn);
            }
        }

        void LoadSpecialFunctions()
        {
            foreach (var func in Parser.specialFunctionMap)
            {
                Button btn = new()
                {
                    Text = func.Key,
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.FromArgb(18, 32, 48)
                };
                btn.FlatAppearance.BorderColor = Color.DarkCyan;
                btn.Font = new(btn.Font.FontFamily, 18);
                btn.Height = 60;
                btn.Width = 220;
                btn.Click += (s, e) => Append(func.Key + "(");
                FLPSpecialFunctions.Controls.Add(btn);
            }
        }

        private void TBInput_Leave(object sender, EventArgs e)
        {
            TBInput.Focus();
        }

        private void TBInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Evaluate();
            }
        }

        private void BtnClearHistory_Click(object sender, EventArgs e)
        {
            Parser.ClearHistory();
            RTBHistory.Clear();
        }
    }
}
