using System;

namespace TaxCalculator.Services
{
    public class TaxCalculatorService : ITaxCalculatorService
    {
        public decimal CalculateTax(string postalCode, decimal annualIncome)
        {
            switch (postalCode)
            {
                case "7441":
                case "1000":
                    return CalculateProgressiveTax(annualIncome);
                case "A100":
                    return CalculateFlatValueTax(annualIncome);
                case "7000":
                    return CalculateFlatRateTax(annualIncome);
                default:
                    throw new InvalidOperationException("Invalid postal code.");
            }
        }

        private decimal CalculateProgressiveTax(decimal annualIncome)
        {
            if (annualIncome <= 8350)
                return annualIncome * 0.10m;
            else if (annualIncome <= 33950)
                return 8350 * 0.10m + (annualIncome - 8350) * 0.15m;
            else if (annualIncome <= 82250)
                return 8350 * 0.10m + (33950 - 8350) * 0.15m + (annualIncome - 33950) * 0.25m;
            else if (annualIncome <= 171550)
                return 8350 * 0.10m + (33950 - 8350) * 0.15m + (82250 - 33950) * 0.25m + (annualIncome - 82250) * 0.28m;
            else if (annualIncome <= 372950)
                return 8350 * 0.10m + (33950 - 8350) * 0.15m + (82250 - 33950) * 0.25m + (171550 - 82250) * 0.28m + (annualIncome - 171550) * 0.33m;
            else
                return 8350 * 0.10m + (33950 - 8350) * 0.15m + (82250 - 33950) * 0.25m + (171550 - 82250) * 0.28m + (372950 - 171550) * 0.33m + (annualIncome - 372950) * 0.35m;
        }

        private decimal CalculateFlatValueTax(decimal annualIncome)
        {
            if (annualIncome < 200000)
                return annualIncome * 0.05m;
            else
                return 10000;
        }

        private decimal CalculateFlatRateTax(decimal annualIncome)
        {
            return annualIncome * 0.175m;
        }
    }
}
