using System.Linq;
using System.Reflection;
using Kontur.Selone.Extensions;
using Kontur.Selone.Properties;
using Kontur.Selone.Selectors.Context;
using OpenQA.Selenium;
using VacationTests.Infrastructure.Properties;

namespace VacationTests.Infrastructure.PageElements
{
    // Об интерфейсах https://ulearn.me/course/basicprogramming/Interfeysy_3df89dfb-7f0f-4123-82ac-364c3a426396
    public class ControlBase : IHaveContainer
    {
        protected IWebElement container;

        protected ControlBase(ISearchContext searchContext, By by)
        {
            container = searchContext.SearchElement(by);
            
            ControlExtensions.InitializeControls(this, container);
        }

        public IProp<bool> Present => container.Present(); // Typo IsPreset. Expression reflection
        public IProp<bool> Visible => container.Visible();
        public IProp<bool> Disabled => container.Disabled();
        IWebElement IHaveContainer.Container => container;

        public override string ToString()
        {
            try
            {
                return $"{container.TagName} {container.Text}";
            }
            catch (StaleElementReferenceException)
            {
                return "StaleElement (not found in DOM)";
            }
        }
    }
}