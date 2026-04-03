using System;
using Calculator.Core;

namespace Calculator.UI
{
    public class ConsoleUI
    {
        private readonly CalculatorEngine _engine;
        private readonly CalculationHistory _history;

        public ConsoleUI(CalculatorEngine engine)
        {
            _engine = engine;
            _history = new CalculationHistory();
        }

        public void Run()
        {
            ShowWelcome();

            while (true)
            {
                ShowMenu();
                string choice = Prompt("Enter your choice").Trim().ToLower();

                if (choice == "q" || choice == "quit") break;

                HandleChoice(choice);
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n  Goodbye! Thanks for using CSharp Calculator.\n");
            Console.ResetColor();
        }

        // ── Menu Display ──────────────────────────────────────────────────────────

        private void ShowWelcome()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"
  ╔═══════════════════════════════════════╗
  ║         C# CALCULATOR  v1.0           ║
  ╚═══════════════════════════════════════╝");
            Console.ResetColor();
        }

        private void ShowMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(@"
  ┌─────────────────────────────────────┐
  │            BASIC MATH               │
  │  1. Add       2. Subtract           │
  │  3. Multiply  4. Divide             │
  │  5. Modulo    6. Power              │
  ├─────────────────────────────────────┤
  │            SCIENTIFIC               │
  │  7. Square Root   8. Logarithm      │
  │  9. Natural Log  10. Factorial      │
  │ 11. Sine   12. Cosine  13. Tangent  │
  │ 14. Absolute Value                  │
  ├─────────────────────────────────────┤
  │            MEMORY                   │
  │ ms. Store   mr. Recall              │
  │ mc. Clear   m+. Add to Memory       │
  ├─────────────────────────────────────┤
  │  h. History     c. Clear Screen     │
  │  q. Quit                            │
  └─────────────────────────────────────┘");
            Console.ResetColor();

            if (_engine.Memory != 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"  Memory: {_engine.Memory}");
                Console.ResetColor();
            }
        }

        // ── Input / Output Helpers ────────────────────────────────────────────────

        private string Prompt(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"\n  {message}: ");
            Console.ResetColor();
            return Console.ReadLine() ?? "";
        }

        private double ReadNumber(string label)
        {
            while (true)
            {
                string input = Prompt(label);

                // Allow "ans" shorthand for last result
                if (input.ToLower() == "ans")
                    return _engine.LastResult;

                if (double.TryParse(input, out double value))
                    return value;

                PrintError("Invalid number. Please try again (or type 'ans' for last result).");
            }
        }

        private void PrintResult(string expression, double result)
        {
            _engine.StoreLastResult(result);
            _history.Add($"{expression} = {result}");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n  ➤  {expression} = {result}");
            Console.ResetColor();
        }

        private void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n  ✗  {message}");
            Console.ResetColor();
        }

        // ── Choice Router ─────────────────────────────────────────────────────────

        private void HandleChoice(string choice)
        {
            try
            {
                switch (choice)
                {
                    // Basic
                    case "1": BinaryOp("Add", "+", _engine.Add); break;
                    case "2": BinaryOp("Subtract", "-", _engine.Subtract); break;
                    case "3": BinaryOp("Multiply", "×", _engine.Multiply); break;
                    case "4": BinaryOp("Divide", "÷", _engine.Divide); break;
                    case "5": BinaryOp("Modulo", "%", _engine.Modulo); break;
                    case "6": BinaryOp("Power", "^", _engine.Power); break;

                    // Scientific
                    case "7":  UnaryOp("√",  _engine.SquareRoot); break;
                    case "8":  UnaryOp("log", _engine.Logarithm); break;
                    case "9":  UnaryOp("ln",  _engine.NaturalLog); break;
                    case "10": FactorialOp(); break;
                    case "11": UnaryOp("sin", _engine.Sine); break;
                    case "12": UnaryOp("cos", _engine.Cosine); break;
                    case "13": UnaryOp("tan", _engine.Tangent); break;
                    case "14": UnaryOp("|x|", _engine.Absolute); break;

                    // Memory
                    case "ms": _engine.MemoryStore(_engine.LastResult);
                               Console.WriteLine($"\n  Memory stored: {_engine.Memory}"); break;
                    case "mr": Console.WriteLine($"\n  Memory recall: {_engine.Memory}"); break;
                    case "mc": _engine.MemoryClear();
                               Console.WriteLine("\n  Memory cleared."); break;
                    case "m+": double addVal = ReadNumber("Value to add to memory");
                               _engine.MemoryAdd(addVal);
                               Console.WriteLine($"\n  Memory updated: {_engine.Memory}"); break;

                    // Utility
                    case "h": ShowHistory(); break;
                    case "c": ShowWelcome(); break;

                    default: PrintError("Unknown option. Please choose from the menu."); break;
                }
            }
            catch (Exception ex)
            {
                PrintError(ex.Message);
            }
        }

        // ── Operation Helpers ─────────────────────────────────────────────────────

        private void BinaryOp(string name, string symbol, Func<double, double, double> op)
        {
            double a = ReadNumber($"First number");
            double b = ReadNumber($"Second number");
            double result = op(a, b);
            PrintResult($"{a} {symbol} {b}", result);
        }

        private void UnaryOp(string symbol, Func<double, double> op)
        {
            double a = ReadNumber("Enter number");
            double result = op(a);
            PrintResult($"{symbol}({a})", result);
        }

        private void FactorialOp()
        {
            double raw = ReadNumber("Enter non-negative integer");
            int n = (int)raw;
            double result = _engine.Factorial(n);
            PrintResult($"{n}!", result);
        }

        private void ShowHistory()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n  ── Calculation History ──────────────────");
            if (!_history.HasHistory)
            {
                Console.WriteLine("  (No calculations yet)");
            }
            else
            {
                foreach (var entry in _history.GetAll())
                    Console.WriteLine($"  {entry}");

                string clearChoice = Prompt("Clear history? (y/n)").ToLower();
                if (clearChoice == "y")
                {
                    _history.Clear();
                    Console.WriteLine("  History cleared.");
                }
            }
            Console.ResetColor();
        }
    }
}
