using Kontur.Selone.Elements;
using Kontur.Selone.Selectors.Context;
using Kontur.Selone.Selectors.Css;
using OpenQA.Selenium;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;

namespace VacationTests.PageElements
{
    public class CalculatorYearEarningsTable : ControlBase
    {
        public CalculatorYearEarningsTable(ISearchContext searchContext, By by) : base(searchContext, by)
        {
            Items = new ElementsCollection<CalculatorYearEarningsTableItem>(container,
                x => x.Css().WithAttribute("data-comp-name", "AverageSalaryRow").FixedByAttribute("data-tid"),
                (s, b, e) => new CalculatorYearEarningsTableItem(new ContextBy(s, b)));
        }
        
        public ElementsCollection<CalculatorYearEarningsTableItem> Items { get; private set; }
    }
}