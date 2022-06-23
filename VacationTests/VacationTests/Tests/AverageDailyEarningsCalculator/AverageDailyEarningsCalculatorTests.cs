using System.Linq;
using NUnit.Framework;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;

namespace VacationTests.Tests.AverageDailyEarningsCalculator
{
    [Description("123")]
    public class AverageDailyEarningsCalculatorTests : VacationTestBase
    {
        [Test]
        [Description("Сценарий 1. При заполнении всех полей, среднедневной заработок считается корректно.")]
        public void FillAllFields_EarningsCalculatorShouldSuccess()
        {
            var page = Navigation.OpenAverageDailyEarningsCalculator();
            page.SalaryCalculatorTab.Text.Wait().That(Contains.Substring("Калькулятор"));

            page.YearEarningsTable.Items.ElementAt(0).YearSelect.SelectValueByText("2020");
            page.YearEarningsTable.Items.ElementAt(1).YearSelect.SelectValueByText("2021");
            page.YearEarningsTable.Items.ElementAt(1).SalaryCurrencyInput.ClearAndInputCurrency(10000m);
            page.YearEarningsTable.Items.ElementAt(1).SalaryCurrencyInput.ClearAndInputCurrency(20000m);
            page.CountOfExcludeDaysInput.InputText("100");
            page.AverageDailyEarningsCurrencyLabel.Sum.Wait().EqualTo(370.85m);
        }
        
        [Test]
        [Description("Сценарий 2. Если заработок за год больше базы для расчета, то база для расчёта берется по умолчанию из года.")]
        public void WhenEarningsMoreThanBase_BaseShouldBeDefault()
        {
            var page = Navigation.OpenAverageDailyEarningsCalculator();

            page.YearEarningsTable.Items.ElementAt(0).YearSelect.SelectValueByText("2020");
            page.YearEarningsTable.Items.ElementAt(0).SalaryCurrencyInput.ClearAndInputCurrency(2000000m);
            page.YearEarningsTable.Items.ElementAt(0).CountBaseCurrencyLabel.Sum.Wait().EqualTo(912000m);
            page.YearEarningsTable.Items.ElementAt(0).YearSelect.SelectValueByText("2021");
            page.YearEarningsTable.Items.ElementAt(0).CountBaseCurrencyLabel.Sum.Wait().EqualTo(966000m);
        }

        [Test]
        [Description("Сценарий 3. Если заработок за год МЕНЬШЕ базы для расчета, то база для расчёта == заработку.")]
        public void WhenEarningsLessThanBase_BaseShouldBeEqualEarnings()
        {
            var page = Navigation.OpenAverageDailyEarningsCalculator();

            page.YearEarningsTable.Items.ElementAt(0).SalaryCurrencyInput.ClearAndInputCurrency(100000.1m);
            page.YearEarningsTable.Items.ElementAt(1).SalaryCurrencyInput.ClearAndInputCurrency(200000.2m);
            page.YearEarningsTable.Items.ElementAt(0).CountBaseCurrencyLabel.Sum.Wait().EqualTo(100000.1m);
            page.YearEarningsTable.Items.ElementAt(1).CountBaseCurrencyLabel.Sum.Wait().EqualTo(200000.2m);
            page.TotalEarningsCurrencyLabel.Sum.Wait().EqualTo(300000.3m);
        }

        [Test]
        [Description("Сценарий 4. При выборе високосного года количество дней для расчета считается корректно.")]
        public void WhenLeapYear_DaysForCountShouldBeRight()
        {
            var page = Navigation.OpenAverageDailyEarningsCalculator();
            
            page.YearEarningsTable.Items.ElementAt(0).YearSelect.SelectValueByText("2020");
            page.YearEarningsTable.Items.ElementAt(1).YearSelect.SelectValueByText("2021");
            page.DaysInTwoYearsLabel.Text.Wait().EqualTo("731");
            page.YearEarningsTable.Items.ElementAt(0).YearSelect.SelectValueByText("2019");
            page.DaysInTwoYearsLabel.Text.Wait().EqualTo("730");
        }
        
        [Test]
        [Description("Сценарий 5. При указании исключенных дней, они должны корректно исключаться из расчета.")]
        public void ExcludeDays_ShouldExcludeFromCount()
        {
            var page = Navigation.OpenAverageDailyEarningsCalculator();
            
            page.YearEarningsTable.Items.ElementAt(0).YearSelect.SelectValueByText("2020");
            page.YearEarningsTable.Items.ElementAt(1).YearSelect.SelectValueByText("2021");
            
            page.DaysInTwoYearsLabel.Text.Wait().EqualTo("731"); 
            page.CountOfExcludeDaysInput.ClearAndInputCurrency(100m);
            page.TotalDaysForCalcLabel.Text.Wait().EqualTo("631");
        }
    }
}