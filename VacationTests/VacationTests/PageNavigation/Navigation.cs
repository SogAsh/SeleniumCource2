using OpenQA.Selenium;
using VacationTests.Infrastructure;
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
    }
}