using Kontur.Selone.Properties;
using OpenQA.Selenium;
using VacationTests.Infrastructure.Properties;

namespace VacationTests.Infrastructure.PageElements
{
    public class CurrencyLabel : Label
    {
        public CurrencyLabel(ISearchContext searchContext, By by) : base(searchContext, by)
        {
        }

        public IProp<decimal> Sum => Text.Currency();
    }
}