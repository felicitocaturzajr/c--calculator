using System;

namespace Calculator.Core
{
    public class CalculatorEngine
    {
        public double Memory { get; private set; } = 0;
        public double LastResult { get; private set; } = 0;

        // Basic Operations
        public double Add(double a, double b) => a + b;
        public double Subtract(double a, double b) => a - b;
        public double Multiply(double a, double b) => a * b;

        public double Divide(double a, double b)
        {
            if (b == 0)
                throw new DivideByZeroException("Cannot divide by zero.");
            return a / b;
        }

        public double Modulo(double a, double b)
        {
            if (b == 0)
                throw new DivideByZeroException("Cannot modulo by zero.");
            return a % b;
        }

        // Scientific Operations
        public double Power(double baseVal, double exponent) => Math.Pow(baseVal, exponent);

        public double SquareRoot(double value)
        {
            if (value < 0)
                throw new ArgumentException("Cannot take square root of a negative number.");
            return Math.Sqrt(value);
        }

        public double Logarithm(double value)
        {
            if (value <= 0)
                throw new ArgumentException("Logarithm is undefined for non-positive values.");
            return Math.Log10(value);
        }

        public double NaturalLog(double value)
        {
            if (value <= 0)
                throw new ArgumentException("Natural log is undefined for non-positive values.");
            return Math.Log(value);
        }

        public double Sine(double degrees) => Math.Sin(DegreesToRadians(degrees));
        public double Cosine(double degrees) => Math.Cos(DegreesToRadians(degrees));
        public double Tangent(double degrees)
        {
            double radians = DegreesToRadians(degrees);
            if (Math.Abs(Math.Cos(radians)) < 1e-10)
                throw new ArgumentException("Tangent is undefined at this angle.");
            return Math.Tan(radians);
        }

        public double Absolute(double value) => Math.Abs(value);
        public double Factorial(int n)
        {
            if (n < 0) throw new ArgumentException("Factorial is undefined for negative numbers.");
            if (n > 20) throw new ArgumentException("Input too large (max: 20).");
            double result = 1;
            for (int i = 2; i <= n; i++) result *= i;
            return result;
        }

        // Memory Operations
        public void MemoryStore(double value) => Memory = value;
        public double MemoryRecall() => Memory;
        public void MemoryClear() => Memory = 0;
        public void MemoryAdd(double value) => Memory += value;

        public void StoreLastResult(double value) => LastResult = value;

        // Helpers
        private double DegreesToRadians(double degrees) => degrees * Math.PI / 180.0;
    }
}
