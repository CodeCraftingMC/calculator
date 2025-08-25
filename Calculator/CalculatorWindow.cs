namespace Calculator
{
    public partial class CalculatorWindow : Form
    {
        List<(string Name, Action Click)> Buttons = [];

        public CalculatorWindow()
        {
            InitializeComponent();

            Buttons = new()
            {
                ("1", () => Append('1')),
                ("2", () => Append('2')),
                ("3", () => Append('3')),
                ("+", () => Append('+')),
                ("4", () => Append('4')),
                ("5", () => Append('5')),
                ("6", () => Append('6')),
                ("-", () => Append('-')),
                ("7", () => Append('7')),
                ("8", () => Append('8')),
                ("9", () => Append('9')),
                ("*", () => Append('*')),
                ("0", () => Append('0')),
                ("=", () => Append('=')),
                (".", () => Append('.')),
                ("/", () => Append('/')),
            };

            LoadButtons();
        }

        void Append(char input)
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
                Button btn = new()
                {
                    Text = button.Name,
                };
                btn.Click += (s, e) => button.Click?.Invoke();
                TLPButtons.Controls.Add(btn);
            }
        }
    }
}
