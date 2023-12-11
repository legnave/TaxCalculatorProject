namespace TaxCalculator.Services
{
    public interface ITaxCalculatorService
{
    decimal CalculateTax(string postalCode, decimal annualIncome);
}
}
