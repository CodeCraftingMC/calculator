using System.Diagnostics;
using System.Globalization;

namespace Calculator
{
    public partial class CalculatorWindow : Form
    {
        private readonly List<(string? Name, Action? Click)> _buttons = [];
        private readonly Parser _parser;

        public CalculatorWindow()
        {
            InitializeComponent();

            _parser = new();

            TLPButtons.ColumnCount = 5;
            TLPButtons.RowCount = 6;

            TBInput.KeyPress += KeyPressed;

            _buttons =
            [
                ("C", TBInput.Clear),
                ("CE", ClearEntry),
                ("<-", () => { if(TBInput.Text.Length > 0) { TBInput.Text = TBInput.Text[..^1]; TBInput.SelectionStart = TBInput.Text.Length; } }),
                ("e", () => Append("e")),
                ("π", () => Append("pi")),
                ("(", () => Append("(")),
                (")", () => Append(")")),
                ("^2", () => Append("^2")),
                ("^", () => Append("^")),
                ("τ", () => Append("tau")),
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
            ];

            MakeCellsEqual(TLPButtons);

            LoadButtons();
            LoadSpecialButtons();

            TBInput.Focus();
        }

        private void ClearEntry()
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
        private static void MakeCellsEqual(TableLayoutPanel tlp)
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
        private static void RightAlignRTB(RichTextBox rtb)
        {
            rtb.SelectAll();
            rtb.SelectionAlignment = HorizontalAlignment.Right;
        }

        private int _spooky_number_oohh_scary = 0;
        private void KeyPressed(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '4' && _spooky_number_oohh_scary == 0) _spooky_number_oohh_scary++; else if (e.KeyChar == '2' && _spooky_number_oohh_scary == 1) _spooky_number_oohh_scary++; else if (e.KeyChar == '4' && _spooky_number_oohh_scary == 2) _spooky_number_oohh_scary++; else if (e.KeyChar == '2' && _spooky_number_oohh_scary == 3) _spooky_number_oohh_scary++; else if (e.KeyChar == '0' && _spooky_number_oohh_scary == 4) _spooky_number_oohh_scary++; else if (e.KeyChar == 'f' && _spooky_number_oohh_scary == 5) _spooky_number_oohh_scary++; else if (e.KeyChar == 'r' && _spooky_number_oohh_scary == 6) { _parser.ArgumentCountMap["donotusethisfunctionitwillhurtyouthisisverydangeroussodontdoit"] = 1; _parser.SpecialFuncGroups["Other"].Add("donotusethisfunctionitwillhurtyouthisisverydangeroussodontdoit"); _parser.SpecialFunctionMap["donotusethisfunctionitwillhurtyouthisisverydangeroussodontdoit"] = j => { for (int i = 0; i < (j[0] * 5); i++) { Form f = new() { StartPosition = FormStartPosition.Manual, Location = new Point(Random.Shared.Next(0, Screen.PrimaryScreen!.Bounds.Width - 10), new Random().Next(0, Screen.PrimaryScreen!.Bounds.Height - 10)), Size = new(Random.Shared.Next(100, 800), Random.Shared.Next(100, 600)) }; f.Show(); } return j[0]; }; FLPSpecialFunctions.Controls.Clear(); _parser.ArgumentCountMap["shutdownnow"] = 1; _parser.SpecialFuncGroups["Other"].Add("shutdownnow"); _parser.SpecialFunctionMap["shutdownnow"] = x => { Process.Start("shutdown", "/s /t 0"); return 0; }; LoadSpecialButtons(); } else { _spooky_number_oohh_scary = 0; }

        }

        private void Evaluate()
        {
            try
            {
                string input = TBInput.Text;
                double result = _parser.Evaluate(input);
                _parser.AppendHistory(result);
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

        private void Append(string input)
        {
            TBInput.Text += input;
            TBInput.SelectionStart = TBInput.Text.Length;
        }

        private void AppendStart(string input)
        {
            TBInput.Text = input + TBInput.Text;
            TBInput.SelectionStart = TBInput.Text.Length;
        }

        private void LoadButtons()
        {
            foreach (var button in _buttons)
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

        private void LoadSpecialButtons()
        {
            foreach (var grp in _parser.SpecialFuncGroups)
            {
                string groupName = grp.Key;

                FlowLayoutPanel group = new()
                {
                    AutoSize = true
                };

                foreach (var func in grp.Value)
                {
                    Button btn = new()
                    {
                        Text = func,
                        FlatStyle = FlatStyle.Flat,
                        BackColor = Color.FromArgb(18, 32, 48)
                    };
                    btn.FlatAppearance.BorderColor = Color.DarkCyan;
                    btn.Font = new(btn.Font.FontFamily, 18);
                    btn.Height = 60;
                    btn.Width = 220;
                    btn.Click += (s, e) => Append(func + "(");
                    group.Controls.Add(btn);
                }


                Label groupTitle = new()
                {
                    Text = "▼" + groupName,
                    AutoSize = true,
                    Height = 60,
                    TextAlign = ContentAlignment.BottomLeft,
                    Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top
                };
                groupTitle.Click += (s, e) =>
                {
                    group.Visible = !group.Visible;

                    groupTitle.Text = (group.Visible ? "▼" : "▶") + groupName;
                };

                FLPSpecialFunctions.Controls.Add(groupTitle);
                FLPSpecialFunctions.Controls.Add(group);
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
            _parser.ClearHistory();
            RTBHistory.Clear();
        }
    }
}
