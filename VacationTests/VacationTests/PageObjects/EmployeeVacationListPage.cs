using Kontur.Selone.Extensions;
using Kontur.Selone.Selectors.Css;
using OpenQA.Selenium;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageElements;

namespace VacationTests.PageObjects
{
    public class EmployeeVacationListPage : PageBase
    {
        public EmployeeVacationListPage(IWebDriver webDriver) : base(webDriver)
        {
            // своя инициализация для сложного контрола
            // ClaimList = new EmployeeClaimList(webDriver.Search(x => x.WithTid("claimList")));
            CreateButton = webDriver.Search(x => x.Css().WithTid("createButton")).Button();
        }

        public Label TitleLabel { get; private set; }
        public Link ClaimsTab { get; private set; }
        public Link SalaryCalculatorTab { get; private set; }
        public Button CreateButton { get; private set; }
        public EmployeeClaimList ClaimList { get; private set; }
        public PageFooter Footer { get; private set; }
        public ClaimCreationPage CreationPage { get; set; }
    }
}