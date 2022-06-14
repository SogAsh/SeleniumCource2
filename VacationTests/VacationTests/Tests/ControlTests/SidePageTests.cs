using NUnit.Framework;
using OpenQA.Selenium;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageObjects;

namespace VacationTests.Tests.ControlTests
{
    public class SidePageTests : VacationTestBase
    {
        [Test]
        public void GoToLoginPage_OpenSidePage_CheckText_Close()
        {
            var page = Navigation.OpenLoginPage();
            var sidePage = page.Footer.OurFooterLink.ClickAndOpen<InfoSidePage>();
            sidePage.HeaderLabel.Text.Wait().EqualTo("Чтение вредит");
            sidePage.BodyLabel.Text.Wait().That(Contains.Substring("Многие знания — многие скорби"));
            sidePage.BodyLabel.Text.Wait().That(Contains.Substring("Лучше жить реальностью"));
            sidePage.AgreeButton.Text.Wait().EqualTo("Точно так");
            sidePage.NotAgreeButton.Text.Wait().EqualTo("Но это неточно");
            sidePage.CloseButton.ClickAndOpen<LoginPage>();
            Assert.Catch<StaleElementReferenceException>(() => sidePage.HeaderLabel.Present.Get());
        }
    }
}