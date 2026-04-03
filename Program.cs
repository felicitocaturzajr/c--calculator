using Calculator.Core;
using Calculator.UI;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var engine = new CalculatorEngine();
            var ui = new ConsoleUI(engine);
            ui.Run();
        }
    }
}
