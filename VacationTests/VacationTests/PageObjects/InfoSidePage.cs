using Kontur.Selone.Extensions;
using OpenQA.Selenium;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;

namespace VacationTests.PageObjects
{
    public class InfoSidePage : PageBase
    {
        public InfoSidePage(IWebDriver webDriver) : base(webDriver)
        {
            HeaderLabel = GetModalContext(webDriver).Search(x => x.WithTid("SidePageHeader")).Label();
            CrossButton = GetModalContext(webDriver).Search(x => x.WithTid("SidePage__close")).Button();
            
            BodyLabel = GetModalContext(webDriver).Search(x => x.WithTid("SidePageBody")).Label();
            AgreeButton = GetModalContext(webDriver).Search(x => x.WithTid("AgreeButton")).Button();
            NotAgreeButton = GetModalContext(webDriver).Search(x => x.WithTid("NotAgreeButton")).Button();
            CloseButton = GetModalContext(webDriver).Search(x => x.WithTid("CloseButton")).Button();
        }

        public Label HeaderLabel { get; private set; }
        public Button CrossButton { get; private set; }

        public Label BodyLabel { get; private set; }
        public Button AgreeButton { get; private set; }
        public Button NotAgreeButton { get; private set; }
        public Button CloseButton { get; private set; }

        private static IWebElement GetModalContext(IWebDriver webDriver)
        {
            var id = webDriver.FindElement(By.CssSelector("[data-tid='InfoSidePage']"))
                .GetAttribute("data-render-container-id");
            return webDriver.Root().FindElement(By.CssSelector($"[data-rendered-container-id='{id}']"));
        }
    }
}