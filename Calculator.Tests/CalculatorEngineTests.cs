using System;
using Xunit;
using Calculator.Core;

namespace Calculator.Tests
{
    public class CalculatorEngineTests
    {
        private readonly CalculatorEngine _engine = new();

        // Basic Operations
        [Fact] public void Add_ReturnsCorrectSum() => Assert.Equal(10, _engine.Add(3, 7));
        [Fact] public void Subtract_ReturnsCorrectDifference() => Assert.Equal(-4, _engine.Subtract(1, 5));
        [Fact] public void Multiply_ReturnsCorrectProduct() => Assert.Equal(12, _engine.Multiply(3, 4));
        [Fact] public void Divide_ReturnsCorrectQuotient() => Assert.Equal(2.5, _engine.Divide(5, 2));
        [Fact] public void Divide_ByZero_ThrowsException() =>
            Assert.Throws<DivideByZeroException>(() => _engine.Divide(5, 0));
        [Fact] public void Modulo_ReturnsCorrectRemainder() => Assert.Equal(1, _engine.Modulo(7, 3));

        // Scientific
        [Fact] public void Power_ReturnsCorrectResult() => Assert.Equal(8, _engine.Power(2, 3));
        [Fact] public void SquareRoot_ReturnsCorrectResult() => Assert.Equal(5, _engine.SquareRoot(25));
        [Fact] public void SquareRoot_Negative_ThrowsException() =>
            Assert.Throws<ArgumentException>(() => _engine.SquareRoot(-1));
        [Fact] public void Factorial_Of5_Returns120() => Assert.Equal(120, _engine.Factorial(5));
        [Fact] public void Factorial_Negative_ThrowsException() =>
            Assert.Throws<ArgumentException>(() => _engine.Factorial(-1));
        [Fact] public void Absolute_NegativeNumber_ReturnsPositive() => Assert.Equal(7, _engine.Absolute(-7));
        [Fact] public void Sine_Of90_Returns1() => Assert.Equal(1, _engine.Sine(90), 5);
        [Fact] public void Cosine_Of0_Returns1() => Assert.Equal(1, _engine.Cosine(0), 5);

        // Memory
        [Fact]
        public void Memory_StoreAndRecall_Works()
        {
            _engine.MemoryStore(42);
            Assert.Equal(42, _engine.MemoryRecall());
        }

        [Fact]
        public void Memory_Clear_ResetsToZero()
        {
            _engine.MemoryStore(99);
            _engine.MemoryClear();
            Assert.Equal(0, _engine.MemoryRecall());
        }

        [Fact]
        public void Memory_Add_AccumulatesCorrectly()
        {
            _engine.MemoryStore(10);
            _engine.MemoryAdd(5);
            Assert.Equal(15, _engine.MemoryRecall());
        }
    }
}
