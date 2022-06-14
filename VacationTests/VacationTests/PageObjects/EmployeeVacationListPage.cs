using Kontur.Selone.Extensions;
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
            TitleLabel = webDriver.Search(x => x.WithTid("TitleLabel")).Label();
            ClaimsTab = webDriver.Search(x => x.WithTid("ClaimsTab")).Link();
            SalaryCalculatorTab = webDriver.Search(x => x.WithTid("SalaryCalculatorTab")).Link();
            CreateButton = webDriver.Search(x => x.WithTid("CreateButton")).Button();
            ClaimList = new EmployeeClaimList(webDriver.Search(x => x.WithTid("ClaimList")));
            
            Footer = new PageFooter(webDriver.Search(x => x.WithTid("Footer")));
        }

        public Label TitleLabel { get; private set; }
        public Link ClaimsTab { get; private set; }
        public Link SalaryCalculatorTab { get; private set; }
        public Button CreateButton { get; private set; }
        public EmployeeClaimList ClaimList { get; private set; }
        
        public PageFooter Footer { get; private set; }
    }
}