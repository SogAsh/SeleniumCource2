using System;
using Kontur.Selone.Pages;
using NUnit.Framework;
using VacationTests.Claims;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageObjects;

namespace VacationTests.Tests.EmployeePage
{
    [TestFixture]
    public class ClaimCreationTests : VacationTestBase
    {
        [Test]
        public void CreateClaimFromPage()
        {
            var page = Navigation.OpenEmployeeVacationList();
            var claimCreationPage = page.CreateButton.ClickAndOpen<ClaimCreationPage>();
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
            var page = Navigation.OpenEmployeeVacationList();
            var claim = ClaimBuilder.AChildClaim();
            ClaimStorage.Add(new[] {claim});
            
            page.Refresh();
            page.ClaimList.Items.Count.Wait().EqualTo(1);
        }

        [Test]
        public void CreateClaimFromRecord()
        {
            var page = Navigation.OpenEmployeeVacationList();
            var claim = Claim.CreateChildType() with{ UserId = "1"};
            ClaimStorage.Add(new[] {claim});
            
            page.Refresh();
            page.ClaimList.Items.Count.Wait().EqualTo(1);
        }
    }
}