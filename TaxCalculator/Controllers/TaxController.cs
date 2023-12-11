using Microsoft.AspNetCore.Mvc;
using System;
using TaxCalculator.Models;
using TaxCalculator.Services;

namespace TaxCalculator.Controllers
{
    public class TaxController : Controller
    {
        private readonly ITaxCalculatorService _taxCalculatorService;
        private readonly AppDbContext _dbContext;

        public TaxController(ITaxCalculatorService taxCalculatorService, AppDbContext dbContext)
        {
            _taxCalculatorService = taxCalculatorService;
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CalculateTax(TaxCalculation model)
        {
            if (ModelState.IsValid)
            {
                model.CalculationDate = DateTime.Now;
                model.CalculatedTax = _taxCalculatorService.CalculateTax(model.PostalCode, model.AnnualIncome);

                // Save to database
                _dbContext.TaxCalculations.Add(model);
                _dbContext.SaveChanges();

                return View("Result", model);
            }

            return View("Index", model);
        }
    }
}
