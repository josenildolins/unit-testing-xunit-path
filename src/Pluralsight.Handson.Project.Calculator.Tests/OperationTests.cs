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

        [Fact]
        public void SumShouldReturnFour()
        {
            // Arrange
            double value = 2;
            double value2 = 2;
            double expected = 4;

            // Act
            var actual = _sut.Add(value, value2);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
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

        //[Fact]
        //public void DivideShouldReturnTwoDotFive()
        //{
        //    // Arrange
        //    int value = 5;
        //    int value2 = 2;
        //    int expected = 2.5;

        //    // Act
        //    var actual = _sut.Divide(value, value2);

        //    // Assert
        //    Assert.Equal((double)expected, actual);
        //}
    }
}