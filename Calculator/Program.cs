namespace Calculator
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Parser p = new Parser();
        
            p.Evaluate("arcsin(1) + arccos(1) + arctan(2) + arccot(2) + arcsec(2) + arccsc(1)");
            //p.Evaluate("(1 + 1) * (0.5 + 0.5) + (0.5 - 0.5) * 2 * (1 / 3)");
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            //Application.Run(new CalculatorWindow());
        }
    }
}