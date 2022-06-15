using Kontur.Selone.Extensions;
using OpenQA.Selenium;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageElements;

namespace VacationTests.PageObjects
{
    public class ClaimLightbox : PageBase
    {
        public ClaimLightbox(IWebDriver webDriver) : base(webDriver)
        {
            ModalHeaderLabel = GetModalContext(webDriver).Search(x => x.WithTid("ModalHeader")).Label();
            CrossButton = GetModalContext(webDriver).Search(x => x.WithTid("modal-close")).Button();
            
            StatusLabel = GetModalContext(webDriver).Search(x => x.WithTid("StatusLabel")).Label();
            ClaimTypeLabel = GetModalContext(webDriver).Search(x => x.WithTid("ClaimTypeLabel")).Label();
            ChildAgeLabel = GetModalContext(webDriver).Search(x => x.WithTid("ChildAgeLabel")).Label();
            PeriodLabel = GetModalContext(webDriver).Search(x => x.WithTid("PeriodLabel")).Label();
            AvailableDaysMessageLabel =
                GetModalContext(webDriver).Search(x => x.WithTid("AvailableDaysMessageLabel")).Label();
            AvailableDaysLabel = GetModalContext(webDriver).Search(x => x.WithTid("AvailableDaysLabel")).Label();
            PayNowCheckbox = GetModalContext(webDriver).Search(x => x.WithTid("PayNowCheckbox")).Checkbox();
            DirectorFioLabel = GetModalContext(webDriver).Search(x => x.WithTid("DirectorFioLabel")).Label();
            
            Footer = new ClaimLightboxFooter(GetModalContext(webDriver).Search(x => x.WithTid("ModalFooter")));
        }

        public Label ModalHeaderLabel { get; private set; }
        public Button CrossButton { get; private set; }
        public Label StatusLabel { get; private set; }
        public Label ClaimTypeLabel { get; private set; }
        public Label ChildAgeLabel { get; private set; }
        public Label PeriodLabel { get; private set; }
        public Label AvailableDaysMessageLabel { get; private set; }
        public Label AvailableDaysLabel { get; private set; }
        public Checkbox PayNowCheckbox { get; private set; }
        public Label DirectorFioLabel { get; private set; }
        public ClaimLightboxFooter Footer { get; private set; }

        private static IWebElement GetModalContext(IWebDriver webDriver)
        {
            var id = webDriver.FindElement(By.CssSelector("[data-tid='ClaimModal']"))
                .GetAttribute("data-render-container-id");
            return webDriver.Root().FindElement(By.CssSelector($"[data-rendered-container-id='{id}']"));
        }
    }
}