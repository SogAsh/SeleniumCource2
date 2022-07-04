using System;
using NUnit.Framework;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageObjects;

namespace VacationTests.Tests.EmployeePage
{
    [TestFixture]
    public class ClaimCreationTests : VacationTestBase
    {
        [SetUp]
        public void SetUp()
        {
            page = Navigation.OpenEmployeeVacationList();
        }
        [Test]
        public void CreateClaimFromPage()
        {
            page.CreateButton.Click();
            var claimCreationPage = Navigation.OpenClaimCreationPage();
            claimCreationPage.ClaimTypeSelect.SelectValueByText("По уходу за ребенком");
            claimCreationPage.ChildAgeInput.InputText("3");
            claimCreationPage.ClaimStartDatePicker.SetValue(DateTime.Now.AddDays(5)); 
            claimCreationPage.ClaimEndDatePicker.SetValue(DateTime.Now.AddDays(10));
            claimCreationPage.DirectorFioCombobox.SelectValue("Захаров Максим Николаевич");
            claimCreationPage.SendButton.Click();
            page.ClaimList.Items.Count.Wait().EqualTo(1);
        }

        [Test]
        public void CreateClaimFromBuilder()
        {
            
        }

        [Test]
        public void CreateClaimFromRecord()
        {
        }

        private EmployeeVacationListPage page;
    }
}