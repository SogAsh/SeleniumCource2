using OpenQA.Selenium;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageElements;

namespace VacationTests.PageObjects
{
    public class AverageDailyEarningsCalculatorPage : PageBase
    {
        public AverageDailyEarningsCalculatorPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public Link SalaryCalculatorTab { get; private set; }
        public CurrencyInput CountOfExcludeDaysInput { get; private set; } //0
        public Label DaysInTwoYearsLabel { get; private set; } //130
        public CurrencyLabel TotalEarningsCurrencyLabel { get; private set; } //0,00 ₽
        public Label TotalDaysForCalcLabel { get; private set; } //730
        public CurrencyLabel AverageDailyEarningsCurrencyLabel { get; private set; } //3700.85 P
        public CalculatorYearEarningsTable YearEarningsTable { get; private set; }
        public PageFooter Footer { get; private set; }
    }
}