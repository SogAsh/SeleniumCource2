using OpenQA.Selenium;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageObjects;

namespace VacationTests.PageNavigation
{
    public class Navigation
    {
        private readonly IWebDriver webDriver;

        public Navigation(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        public LoginPage OpenLoginPage()
        {
            return webDriver.OpenPage<LoginPage>(Urls.LoginPage);
        }

        public EmployeeVacationListPage OpenEmployeeVacationList(string employeeId = "1")
        {
            return webDriver.OpenPage<EmployeeVacationListPage>(Urls.EmployeeVacationList(employeeId));
        }
        
        public AdminVacationListPage OpenAdminVacationList()
        {
            return webDriver.OpenPage<AdminVacationListPage>(Urls.AdminVacationList);
        }
        
        public AverageDailyEarningsCalculatorPage OpenAverageDailyEarningsCalculator(string employeeId = "1")
        {
            var page = OpenEmployeeVacationList(employeeId);
            page.SalaryCalculatorTab.Visible.Wait().EqualTo(true);
            return page.SalaryCalculatorTab.ClickAndOpen<AverageDailyEarningsCalculatorPage>();
        }
        
        public ClaimCreationPage OpenClaimCreationPage(string vacation = "1")
        {
            return webDriver.OpenPage<ClaimCreationPage>(Urls.ClaimCreationList(vacation));
        }
    }
}