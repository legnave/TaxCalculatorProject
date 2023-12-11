
using NUnit.Framework;
using System;
using TaxCalculator.Services;

namespace TestProjects
{

    [TestFixture]
    public class TaxCalculatorServiceTests
    {
        private ITaxCalculatorService _taxCalculatorService;

        [SetUp]
        public void Setup()
        {
            _taxCalculatorService = new TaxCalculatorService();
        }

        [Test]
        public void CalculateProgressiveTax_ShouldReturnCorrectTax()
        {
            // Arrange
            decimal annualIncome = 50000;

            // Act
            decimal result = _taxCalculatorService.CalculateTax("7441", annualIncome);

            // Assert
            Assert.That(result, Is.EqualTo(8350 * 0.10m + (33950 - 8350) * 0.15m + (50000 - 33950) * 0.25m).Within(0.001));
        }

        [Test]
        public void CalculateFlatValueTax_ShouldReturnCorrectTax()
        {
            // Arrange
            decimal lowIncome = 50000;
            decimal highIncome = 250000;

            // Act
            decimal lowIncomeResult = _taxCalculatorService.CalculateTax("A100", lowIncome);
            decimal highIncomeResult = _taxCalculatorService.CalculateTax("A100", highIncome);

            // Assert
            Assert.That(lowIncomeResult, Is.EqualTo(lowIncome * 0.05m).Within(0.001));
            Assert.That(highIncomeResult, Is.EqualTo(10000).Within(0.001));
        }

        [Test]
        public void CalculateFlatRateTax_ShouldReturnCorrectTax()
        {
            // Arrange
            decimal annualIncome = 50000;

            // Act
            decimal result = _taxCalculatorService.CalculateTax("7000", annualIncome);

            // Assert
            Assert.That(result, Is.EqualTo(annualIncome * 0.175m).Within(0.001));
        }

        [TestCase("7441", 50000, ExpectedResult = 8350 * 0.10 + (33950 - 8350) * 0.15 + (50000 - 33950) * 0.25)]
        [TestCase("A100", 50000, ExpectedResult = 50000 * 0.05)]
        [TestCase("7000", 50000, ExpectedResult = 50000 * 0.175)]
        [TestCase("1000", 50000, ExpectedResult = 8350 * 0.10 + (33950 - 8350) * 0.15 + (50000 - 33950) * 0.25)]
        public decimal CalculateTax_ShouldReturnCorrectTax(string postalCode, decimal annualIncome)
        {
            // Act
            decimal result = _taxCalculatorService.CalculateTax(postalCode, annualIncome);

            // Assert
            return result;
        }

        [Test]
        public void CalculateTax_WithInvalidPostalCode_ShouldThrowException()
        {
            // Arrange
            string invalidPostalCode = "INVALID";

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _taxCalculatorService.CalculateTax(invalidPostalCode, 50000));
        }
    }
}
