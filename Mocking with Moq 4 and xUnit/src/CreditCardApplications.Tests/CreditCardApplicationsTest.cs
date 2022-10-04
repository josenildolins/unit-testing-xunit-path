using Xunit;

namespace CreditCardApplications.Tests
{
    public class CreditCardApplicationsTest
    {
        [Fact]
        public void ShouldAcceptHighIncomeApplications()
        {
            // Arrange
            CreditCardApplicationEvaluator sut = new( null );
            
            CreditCardApplication application = new(){ GrossAnnualIncome = 100_000 };

            //Act
            CreditCardApplicationDecision decision = sut.Evaluate(application);

            // Assert
            Assert.Equal(CreditCardApplicationDecision.AutoAccepted, decision);
        }

        [Fact]
        public void ShouldReferYoungApplications()
        {
            // Arrange
            CreditCardApplicationEvaluator sut = new( null );

            CreditCardApplication application = new() { Age = 19 };

            //Act
            CreditCardApplicationDecision decision = sut.Evaluate(application);

            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
        }
    }
}