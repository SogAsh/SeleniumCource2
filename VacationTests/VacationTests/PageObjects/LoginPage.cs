using Kontur.Selone.Extensions;
using Kontur.Selone.Waiting;
using OpenQA.Selenium;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageElements;

// О наследовании https://ulearn.me/course/basicprogramming/Nasledovanie_ac2b8cb6-8d63-4b81-9083-eaa77ab0c89c
namespace VacationTests.PageObjects
{
    //getType
    public class LoginPage : PageBase, ILoadable
    {
        public LoginPage(IWebDriver webDriver) : base(webDriver)
        {
            // Footer = new PageFooter(webDriver.Search(x => x.WithTid("Footer")));
        }

        //GetProperties
        public Label TitleLabel { get; private set; }
        public Button LoginAsEmployeeButton { get; private set; }
        public PageFooter Footer { get; private set; }
        public Button LoginAsAdminButton { get; private set; }
        public AdminVacationListPage LoginAsAdmin()
        {
            return LoginAsAdminButton.ClickAndOpen<AdminVacationListPage>();
        }
        public EmployeeVacationListPage LoginAsEmployee()
        {
            return LoginAsEmployeeButton.ClickAndOpen<EmployeeVacationListPage>();
        }
        public void WaitLoaded(int? timeout = null)
        {
            LoginAsAdminButton.WaitPresence();
            LoginAsEmployeeButton.Present.Wait().EqualTo(true);
        }
    }
}