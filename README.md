# 🧮 C# Calculator

A feature-rich command-line calculator built with C# and .NET 8. Supports basic arithmetic, scientific operations, memory functions, and calculation history.

## ✨ Features

- **Basic Math** — Addition, subtraction, multiplication, division, modulo, exponentiation
- **Scientific** — Square root, log₁₀, natural log, factorial, sin, cos, tan, absolute value
- **Memory** — Store, recall, clear, and accumulate values in memory
- **History** — Timestamped log of the last 50 calculations
- **`ans` shorthand** — Reuse the last result as input in any operation
- **Unit Tests** — Full coverage via xUnit

## 📁 Project Structure

```
Calculator/
├── Program.cs                  # Entry point
├── Core/
│   ├── CalculatorEngine.cs     # All math logic
│   └── CalculationHistory.cs   # History tracking
├── UI/
│   └── ConsoleUI.cs            # Interactive terminal interface
└── Calculator.Tests/
    └── CalculatorEngineTests.cs # xUnit tests
```

## 🚀 Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Run the App

```bash
git clone https://github.com/YOUR_USERNAME/csharp-calculator.git
cd csharp-calculator
dotnet run
```

### Run Tests

```bash
dotnet test
```

## 📸 Preview

```
  ╔═══════════════════════════════════════╗
  ║         C# CALCULATOR  v1.0           ║
  ╚═══════════════════════════════════════╝

  ┌─────────────────────────────────────┐
  │            BASIC MATH               │
  │  1. Add       2. Subtract           │
  │  3. Multiply  4. Divide             │
  ...
```

## 🛠 Built With

- **C# / .NET 8**
- **xUnit** — Unit testing framework

## 📄 License

MIT License — feel free to use, fork, and extend!
