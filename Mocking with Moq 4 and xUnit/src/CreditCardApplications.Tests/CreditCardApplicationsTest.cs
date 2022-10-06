using Xunit;
using Moq;

namespace CreditCardApplications.Tests
{
    public class CreditCardApplicationsTest
    {
        [Fact]
        public void ShouldAcceptHighIncomeApplications()
        {
            // Arrange
            Mock<IFrequentFlyerNumberValidator> mockValidator =
                new Mock<IFrequentFlyerNumberValidator>();

            CreditCardApplicationEvaluator sut = new(mockValidator.Object);

            CreditCardApplication application = new() { GrossAnnualIncome = 100_000 };

            //Act
            CreditCardApplicationDecision decision = sut.Evaluate(application);

            // Assert
            Assert.Equal(CreditCardApplicationDecision.AutoAccepted, decision);
        }

        [Fact]
        public void ShouldReferYoungApplications()
        {
            // Arrange
            Mock<IFrequentFlyerNumberValidator> mockValidator =
               new Mock<IFrequentFlyerNumberValidator>();

            mockValidator.DefaultValue = DefaultValue.Mock;

            mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);

            CreditCardApplicationEvaluator sut = new(mockValidator.Object);

            CreditCardApplication application = new() { Age = 19 };

            //Act
            CreditCardApplicationDecision decision = sut.Evaluate(application);

            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
        }

        [Fact]
        public void ShouldDeclineLowIncomeApplications()
        {
            // Arrange
            Mock<IFrequentFlyerNumberValidator> mockValidator =
                new();

            mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");

            mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);

            CreditCardApplicationEvaluator sut = new(mockValidator.Object);

            CreditCardApplication application = new() { GrossAnnualIncome = 19_99, Age = 42, FrequentFlyerNumber = "x" };

            // Act
            CreditCardApplicationDecision decision = sut.Evaluate(application);

            // Assert
            Assert.Equal(CreditCardApplicationDecision.AutoDeclined, decision);
        }

        [Fact]
        public void ShouldReferInvalidFrequentFlyerApplications()
        {
            // Arrange
            Mock<IFrequentFlyerNumberValidator> mockValidator = new();

            mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");

            mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(false);

            CreditCardApplicationEvaluator sut = new(mockValidator.Object);

            CreditCardApplication application = new();

            // Act
            CreditCardApplicationDecision decision = sut.Evaluate(application);

            // Assert
            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
        }

        [Fact]
        public void ShouldDeclineLowIncomeApplicationsOutDemo()
        {
            // Arrange
            Mock<IFrequentFlyerNumberValidator> mockValidator = new();

            bool isValid = true;
            mockValidator.Setup(x => x.IsValid(It.IsAny<string>(), out isValid));

            CreditCardApplicationEvaluator sut = new(mockValidator.Object);

            CreditCardApplication application = new()
            {
                GrossAnnualIncome = 19_99,
                Age = 42
            };

            // Act
            CreditCardApplicationDecision decision = sut.EvaluateUsingOut(application);

            // Assert
            Assert.Equal(CreditCardApplicationDecision.AutoDeclined, decision);
        }

        [Fact]
        public void ShouldReferWhenLicenseKeyExpired()
        {
            // Arrange
            Mock<IFrequentFlyerNumberValidator> mockValidator = new();
            mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("EXPIRED");

            CreditCardApplicationEvaluator sut = new(mockValidator.Object);

            CreditCardApplication application = new() { Age = 42 };

            // Act
            CreditCardApplicationDecision decision = sut.Evaluate(application);

            // Assert
            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
        }

        [Fact]
        public void ShouldUseDetailedLookupForOlderApplications()
        {
            //Arrange
            Mock<IFrequentFlyerNumberValidator> mockValidator = new();

            mockValidator.SetupAllProperties();

            mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");

            CreditCardApplicationEvaluator sut = new(mockValidator.Object);

            CreditCardApplication application = new() { Age= 30 };

            // Act
            sut.Evaluate(application);

            // Assert
            Assert.Equal(ValidationMode.Detailed, mockValidator.Object.ValidationMode);
        }

        [Fact]
        public void ShouldValidateFrequencyLyerNumberForLowIncomeApplications()
        {
            // Arrange
            Mock<IFrequentFlyerNumberValidator> mockValidator = new();

            mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");

            CreditCardApplicationEvaluator sut = new(mockValidator.Object);

            CreditCardApplication application = new()
            {
                FrequentFlyerNumber = "q"
            };

            // Act
            sut.Evaluate(application);

            mockValidator.Verify(x => x.IsValid(It.IsAny<string>()));
        }
    }
}