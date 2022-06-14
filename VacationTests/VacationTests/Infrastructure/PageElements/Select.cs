using Kontur.Selone.Properties;
using OpenQA.Selenium;
using VacationTests.Infrastructure.Properties;

namespace VacationTests.Infrastructure.PageElements
{
    public class Select : ControlBase
    {
        public Select(ISearchContext searchContext, By by) : base(searchContext, by)
        {
            Portal = new Portal(container, By.TagName("noscript"));
        }

        public IProp<string> Value => container.ReactValue();
        private Portal Portal { get; }

        public void SelectValueByText(string text)
        {
            container.Click();
            var itemSelector = $".//*[contains(@data-comp-name,'MenuItem CommonWrapper')][contains(.,'{text}')]";
            var item = new Button(Portal.GetPortalElement(), By.XPath(itemSelector));
            item.Visible.Wait().EqualTo(true);
            item.Click();
        }
    }
}