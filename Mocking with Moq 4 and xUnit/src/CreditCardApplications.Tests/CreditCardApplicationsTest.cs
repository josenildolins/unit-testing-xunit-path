using Xunit;
using Moq;
using System;
using System.Collections.Generic;

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

            CreditCardApplication application = new() { Age = 30 };

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

            mockValidator.Verify(x => x.IsValid(It.IsAny<string>()), "Frequent Flyer numbers should be validated.");
        }

        [Fact]
        public void ShouldNotValidateFrequentFlynumberForHighIncomeApplications()
        {
            // Arrange
            Mock<IFrequentFlyerNumberValidator> mockValidator = new();

            mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");

            CreditCardApplicationEvaluator sut = new(mockValidator.Object);

            CreditCardApplication application = new() { GrossAnnualIncome = 100_000 };

            // Act
            sut.Evaluate(application);

            mockValidator.Verify(x => x.IsValid(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void ShouldCheckLicenseKeyForLowIncomeApplications()
        {
            // Arrange
            Mock<IFrequentFlyerNumberValidator> mockValidator = new();

            mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");

            CreditCardApplicationEvaluator sut = new(mockValidator.Object);

            CreditCardApplication application = new() { GrossAnnualIncome = 99_000 };

            // Act
            sut.Evaluate(application);

            mockValidator.VerifyGet(x => x.ServiceInformation.License.LicenseKey, Times.Once);
        }

        [Fact]
        public void ShouldSetDetailedLookupForOlderApplications()
        {
            // Arrange
            Mock<IFrequentFlyerNumberValidator> mockValidator = new();

            mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");

            CreditCardApplicationEvaluator sut = new(mockValidator.Object);

            CreditCardApplication application = new() { Age = 30 };

            // Act
            sut.Evaluate(application);

            mockValidator.VerifySet(x => x.ValidationMode = ValidationMode.Detailed);
        }

        [Fact]
        public void ReferWhenFrequentFlyerValidationError()
        {
            // Arrange
            Mock<IFrequentFlyerNumberValidator> mockValidator = new();

            mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");

            mockValidator.Setup(x => x.IsValid(It.IsAny<string>()))
                .Throws(new Exception("Custom message"));

            CreditCardApplicationEvaluator sut = new(mockValidator.Object);

            CreditCardApplication application = new() { Age = 42 };

            // Act
            CreditCardApplicationDecision decision = sut.Evaluate(application);

            // Assert
            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
        }

        [Fact]
        public void ShouldIncrementLookupCount()
        {
            // Arrange
            Mock<IFrequentFlyerNumberValidator> mockValidator = new();

            mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("ok");

            mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true)
                .Raises(x => x.ValidatorLookupPerformed += null, EventArgs.Empty);

            CreditCardApplicationEvaluator sut = new(mockValidator.Object);

            CreditCardApplication application = new() { FrequentFlyerNumber = "x", Age = 25 };

            // Act
            sut.Evaluate(application);

            // Assert
            Assert.Equal(1, sut.ValidatorLookupCount);
        }

        [Fact]
        public void ShouldReferInvalidFrequentFlyerApplications_ReturnValuesSequence()
        {
            // Arrange
            Mock<IFrequentFlyerNumberValidator> mockValidator = new();

            mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");

            mockValidator.SetupSequence(x => x.IsValid(It.IsAny<string>()))
                .Returns(false)
                .Returns(true);

            CreditCardApplicationEvaluator sut = new(mockValidator.Object);

            CreditCardApplication application = new() { Age = 25 };

            // Act
            CreditCardApplicationDecision FirstDecision = sut.Evaluate(application);

            CreditCardApplicationDecision SecondDecision = sut.Evaluate(application);

            // Assert
            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, FirstDecision);

            Assert.Equal(CreditCardApplicationDecision.AutoDeclined, SecondDecision);
        }

        [Fact]
        public void ShouldReferInvalidFrequencyFlyerApplications_MultipleCallsSequence()
        {
            // Arrange
            List<string> frequentFlyerNumberPassed = new List<string>();

            Mock<IFrequentFlyerNumberValidator> mockValidator = new();

            mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");
            mockValidator.Setup(x => x.IsValid(Capture.In(frequentFlyerNumberPassed)));

            CreditCardApplicationEvaluator sut = new(mockValidator.Object);
            CreditCardApplication application1 = new() { Age = 25, FrequentFlyerNumber = "aa" };
            CreditCardApplication application2 = new() { Age = 25, FrequentFlyerNumber = "bb" };
            CreditCardApplication application3 = new() { Age = 25, FrequentFlyerNumber = "cc" };

            // Act
            sut.Evaluate(application1);
            sut.Evaluate(application2);
            sut.Evaluate(application3);

            // Assert
            Assert.Equal(new List<string> { "aa", "bb", "cc" }, frequentFlyerNumberPassed);
        }
    }
}