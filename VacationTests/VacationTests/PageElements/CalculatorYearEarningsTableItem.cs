using Kontur.Selone.Selectors.Context;
using OpenQA.Selenium;
using VacationTests.Infrastructure.PageElements;

namespace VacationTests.PageElements
{
    public class CalculatorYearEarningsTableItem : ControlBase
    {
        public CalculatorYearEarningsTableItem(IContextBy contextBy) : base(contextBy.SearchContext, contextBy.By)
        {
        }
        
        public Select YearSelect { get; private set; }
        public CurrencyInput SalaryCurrencyInput { get; private set; }
        public CurrencyLabel CountBaseCurrencyLabel { get; private set; }
    }
}