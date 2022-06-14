using System.Globalization;
using Kontur.Selone.Properties;
using OpenQA.Selenium;
using VacationTests.Infrastructure.Properties;

namespace VacationTests.Infrastructure.PageElements
{
    public class CurrencyInput : Input
    {
        public CurrencyInput(ISearchContext searchContext, By by) : base(searchContext, by)
        {
        }

        public IProp<decimal> Sum => Value.Currency();

        public void ClearAndInputCurrency(decimal value)
        {
            ClearAndInputText(value.ToString(CultureInfo.InvariantCulture));
        }
    }
}