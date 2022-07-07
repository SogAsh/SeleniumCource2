using System;
using System.Linq;
using Kontur.Selone.Pages;
using Kontur.Selone.Properties;
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
            claimCreationPage.ClaimTypeSelect.SelectValueByText(ClaimType.Child.GetDescription());
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
            ClaimStorage.Add(new[] { claim });

            page.Refresh();
            page.ClaimList.Items.Count.Wait().EqualTo(1);
        }

        [Test]
        public void CreateClaimFromRecord()
        {
            var page = Navigation.OpenEmployeeVacationList("2");
            var claim = Claim.CreateChildType() with { UserId = "2" };
            ClaimStorage.Add(new[] { claim });

            page.Refresh();
            page.ClaimList.Items.Count.Wait().EqualTo(1);
        }

        [Test]
        public void Scenario_1()
        {
            var page = Navigation.OpenEmployeeVacationList();
            var claimCreationPage = page.CreateButton.ClickAndOpen<ClaimCreationPage>();
            claimCreationPage.ClaimTypeSelect.SelectValueByText(ClaimType.Child.GetDescription());
            claimCreationPage.ChildAgeInput.InputText("3");
            claimCreationPage.ClaimStartDatePicker.SetValue(DateTime.Now.AddDays(5));
            claimCreationPage.ClaimEndDatePicker.SetValue(DateTime.Now.AddDays(10));
            claimCreationPage.DirectorFioCombobox.SelectValue("Захаров Максим Николаевич");
            claimCreationPage.SendButton.Click();

            page = Navigation.OpenEmployeeVacationList("2");
            claimCreationPage = page.CreateButton.ClickAndOpen<ClaimCreationPage>();
            claimCreationPage.ClaimTypeSelect.SelectValueByText(ClaimType.Child.GetDescription());
            claimCreationPage.ChildAgeInput.InputText("3");
            claimCreationPage.ClaimStartDatePicker.SetValue(DateTime.Now.AddDays(5));
            claimCreationPage.ClaimEndDatePicker.SetValue(DateTime.Now.AddDays(10));
            claimCreationPage.DirectorFioCombobox.SelectValue("Захаров Максим Николаевич");
            claimCreationPage.SendButton.Click();

            var adminPage = Navigation.OpenAdminVacationList();
            adminPage.ClaimList.Items.Select(x => x.TitleLink.Text)
                .Wait().EquivalentTo(new[] { "Заявление 1", "Заявление 2" });
        }

        [Test]
        public void Scenario_2()
        {
            string ClaimsKeyName = "Vacation_App_Claims";

            // значение, которое будем добавлять для ключа ClaimsKeyName
            string FirstVacation =
                "[{\"endDate\":\"14.08.2020\",\"id\":\"1\",\"paidNow\":true,\"startDate\":\"01.08.2020\",\"status\":2,\"type\":\"Основной\",\"userId\":\"1\",\"director\":{\"id\":24939,\"name\":\"Захаров Максим Николаевич\",\"position\":\"Руководитель направления тестирования\"}}, {\"endDate\":\"14.08.2020\",\"id\":\"2\",\"paidNow\":true,\"startDate\":\"01.08.2020\",\"status\":2,\"type\":\"Основной\",\"userId\":\"1\",\"director\":{\"id\":24939,\"name\":\"Петров Петр Петрович\",\"position\":\"Руководитель направления тестирования\"}}]";

            var expected = new[]
            {
                ("Заявление 1", "Иванов Петр Семенович", "01.08.2020 - 14.08.2020", "На согласовании"),
                ("Заявление 2", "Иванов Петр Семенович", "01.08.2020 - 14.08.2020", "На согласовании")
            };
            
            var page = Navigation.OpenEmployeeVacationList();
            LocalStorage.SetItem(ClaimsKeyName, FirstVacation);

            var adminPage = Navigation.OpenAdminVacationList();
            adminPage.ClaimList.Items.Select
                (claim => Props.Create(claim.TitleLink.Text, claim.UserFioLabel.Text, claim.PeriodLabel.Text, claim.StatusLabel.Text))
                .Wait().EquivalentTo(expected);
            
            //или так
            // adminPage.ClaimList.Items.Select(x => x.TitleLink.Text)
            //     .Wait().EqualTo(new[] { "Заявление 1", "Заявление 2" });
            // adminPage.ClaimList.Items.Select(x => x.UserFioLabel.Text)
            //     .Wait().EqualTo(new[] { "Иванов Петр Семенович", "Иванов Петр Семенович" });
            // adminPage.ClaimList.Items.Select(x => x.PeriodLabel.Text)
            //     .Wait().EqualTo(new[] { "01.08.2020 - 14.08.2020", "01.08.2020 - 14.08.2020" });
            // adminPage.ClaimList.Items.Select(x => x.StatusLabel.Text)
            //     .Wait().EqualTo(new[] { "На согласовании", "На согласовании" });
        }
    }
}