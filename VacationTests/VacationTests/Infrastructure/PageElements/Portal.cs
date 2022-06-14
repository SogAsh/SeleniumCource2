using Kontur.Selone.Extensions;
using OpenQA.Selenium;

namespace VacationTests.Infrastructure.PageElements
{
    public class Portal : ControlBase
    {
        public Portal(ISearchContext searchContext, By by) : base(searchContext, by)
        {
        }

        public IWebElement GetPortalElement()
        {
            Present.Wait().EqualTo(true);
            var renderContainerId = container.GetAttribute("data-render-container-id");
            try
            {
                return container.Root()
                    .SearchElement(By.CssSelector($"[data-rendered-container-id='{renderContainerId}']"));
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }
    }
}