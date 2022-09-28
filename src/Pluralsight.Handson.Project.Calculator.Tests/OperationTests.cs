using System;
using Xunit;

namespace Pluralsight.Handson.Project.Calculator.Tests
{
    public class OperationTests
    {
        private readonly Operation _sut;

        public OperationTests()
        {
            _sut = new Operation();
        }

        [Fact, Trait("Category", "Add")]
        public void SumShouldReturnFour()
        {
            // Arrange
            double firsValue = 2;
            double secondValue = 2;
            double expected = 4;

            // Act
            var actual = _sut.Add(firsValue, secondValue);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact, Trait("Category", "Add")]
        public void SumShouldReturnTwo()
        {
            // Arrange
            double firsValue = 2;
            double secondValue = 0;
            double expected = 2;

            // Act
            var actual = _sut.Add(firsValue, secondValue);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact, Trait("Category", "Subtract")]
        public void SubtractShouldReturnMinusOne()
        {
            // Arrange
            double firsValue = 2;
            double secondValue = 3;
            double expected = -1;

            // Act
            var actual = _sut.Subtract(firsValue, secondValue);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact, Trait("Category", "Subtract")]
        public void SubtractShouldReturnTwo()
        {
            // Arrange
            double firsValue = 4;
            double secondValue = 2;
            double expected = 2;

            // Act
            var actual = _sut.Subtract(firsValue, secondValue);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact, Trait("Category", "Divide")]
        public void DivideShouldThrowDivideByZeroExcepttion()
        {
            // Arrange
            int value = 10;
            int value2 = 0;

            // Assert
            Assert.Throws<DivideByZeroException>(() =>
            {
                // Act
                var result = _sut.Divide(value, value2);
            });
        }

        [Fact, Trait("Category", "Divide")]
        public void DivideShouldReturnTwoDotFive()
        {
            // Arrange
            double value = 5;
            double value2 = 2;
            double expected = 2.5;

            // Act
            var actual = _sut.Divide(value, value2);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact, Trait("Category", "Divide")]
        public void DivideShouldReturnZeroDotThirtyTwo()
        {
            // Arrange
            double value = 32;
            double value2 = 99;
            double expected = 0.32;

            // Act
            var actual = _sut.Divide(value, value2);

            // Assert
            Assert.Equal(expected, actual, 2);
        }

        [Fact, Trait("Category", "Multiply")]
        public void MultiplyShouldReturnFive()
        {
            // Arrange
            double firstValue = 5;
            double secondValue = 1;
            double expected = 5;

            // Act
            var actual = _sut.Multiply(firstValue, secondValue);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact, Trait("Category", "Multiply")]
        public void MultiplyShouldReturnZero()
        {

            // Arrange
            double firstValue = 5;
            double secondValue = 0;
            double expected = 0;

            // Act
            var actual = _sut.Multiply(firstValue, secondValue);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}