using NUnit.Framework;
using VacationTests.Infrastructure;

namespace VacationTests.Tests.Navigation
{
    public class NavigationTests : VacationTestBase
    {
        [Test]
        public void LoginPage_AdminButtonTest()
        {
            var enterPage = Navigation.OpenLoginPage();
            var adminVacationListPage = enterPage.LoginAsAdmin();
            adminVacationListPage.TitleLabel.Text.Wait().EqualTo("Список отпусков");
        }
    }
}