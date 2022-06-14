using Kontur.Selone.Extensions;
using Kontur.Selone.Properties;
using OpenQA.Selenium;
using VacationTests.Infrastructure.Properties;

namespace VacationTests.Infrastructure.PageElements
{
    public class Checkbox : ControlBase, IClickable
    {
        public Checkbox(ISearchContext searchContext, By by) : base(searchContext, by)
        {
        }

        public IProp<string> Text => container.Text();
        public IProp<bool> Checked => container.Checked();

        public void SetChecked()
        {
            Checked.Wait().EqualTo(false);
            this.Click();
            Checked.Wait().EqualTo(true);
        }

        public void SetUnchecked()
        {
            Checked.Wait().EqualTo(true);
            this.Click();
            Checked.Wait().EqualTo(false);
        }
    }
}