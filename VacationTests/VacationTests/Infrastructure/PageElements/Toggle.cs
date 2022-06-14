using Kontur.Selone.Extensions;
using Kontur.Selone.Properties;
using OpenQA.Selenium;
using VacationTests.Infrastructure.Properties;

namespace VacationTests.Infrastructure.PageElements
{
    public class Toggle : ControlBase
    {
        public Toggle(ISearchContext searchContext, By by) : base(searchContext, by)
        {
        }

        public IProp<string> Text => container.Text();
        public IProp<bool> Checked => container.Checked();
    }
}