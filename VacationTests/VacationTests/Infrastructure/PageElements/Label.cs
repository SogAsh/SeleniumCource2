using Kontur.Selone.Extensions;
using Kontur.Selone.Properties;
using OpenQA.Selenium;

namespace VacationTests.Infrastructure.PageElements
{
    public class Label : ControlBase, IClickable
    {
        public Label(ISearchContext searchContext, By by) : base(searchContext, by)
        {
        }

        public IProp<string> Text => container.Text();
    }
}