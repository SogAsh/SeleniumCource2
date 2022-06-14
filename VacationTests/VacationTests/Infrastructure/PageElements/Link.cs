using Kontur.Selone.Extensions;
using Kontur.Selone.Properties;
using OpenQA.Selenium;

namespace VacationTests.Infrastructure.PageElements
{
    public class Link : ControlBase, IClickable
    {
        public Link(ISearchContext searchContext, By by) : base(searchContext, by)
        {
        }

        public IProp<string> Text => container.Text();
    }
}