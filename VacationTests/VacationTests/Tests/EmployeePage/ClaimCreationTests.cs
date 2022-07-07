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
            claimCreationPage.DirectorFioCombobox.WaitValue("Захаров Максим Николаевич");
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
            
            var adminPage = Navigation.OpenAdminVacationList();
            LocalStorage.SetItem(ClaimsKeyName, FirstVacation);
            adminPage.Refresh();
            
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

        [Test]
        public void Scenario_3()
        {
            var adminPage = Navigation.OpenAdminVacationList();
            
            var claim5 = ClaimBuilder.AClaim().WithId("5").Build();
            var claim8 = ClaimBuilder.AClaim().WithId("8").Build();
            var claim2 = ClaimBuilder.AClaim().WithId("2").Build();
            ClaimStorage.Add(new[] {claim5, claim8, claim2});
            
            adminPage.Refresh();
            adminPage.ClaimList.Items.Select(x => x.TitleLink.Text)
                .Wait().EqualTo(new[] { $"Заявление {claim5.Id}", $"Заявление {claim8.Id}", $"Заявление {claim2.Id}"});
        }
        
        [Test]
        public void Scenario_4()
        {
            var expected = new[]
            {
                (ClaimStatus.NonHandled.GetDescription(), true, true),
                (ClaimStatus.Accepted.GetDescription(), false, false),
                (ClaimStatus.Rejected.GetDescription(), false, false)
            };
            var adminPage = Navigation.OpenAdminVacationList();
            
            var claim1 = Claim.CreateDefault() with {Id = "1", Status = ClaimStatus.NonHandled};
            var claim2 = Claim.CreateDefault() with {Id = "2", Status = ClaimStatus.Accepted};
            var claim3 = Claim.CreateDefault() with {Id = "3", Status = ClaimStatus.Rejected};

            ClaimStorage.Add(new[] {claim1, claim2, claim3});
            
            adminPage.Refresh();
            adminPage.ClaimList.Items.Select
                    (claim => Props.Create(claim.StatusLabel.Text, claim.AcceptButton.Visible, claim.RejectButton.Visible))
                .Wait().EquivalentTo(expected);
        }
        
        [Test]
        public void Scenario_5()
        {
            var adminPage = Navigation.OpenAdminVacationList();
            
            var claim1 = Claim.CreateDefault() with {Id = "1", Status = ClaimStatus.NonHandled};

            ClaimStorage.Add(new[] {claim1});
            
            adminPage.Refresh();
            var el = adminPage.ClaimList.Items.Wait().Single(x => x.AcceptButton.Visible, Is.EqualTo(true));
            el.AcceptButton.Click();
            el.StatusLabel.Text.Wait().EqualTo(ClaimStatus.Accepted.GetDescription());
            el.AcceptButton.Visible.Wait().EqualTo(false);
        }
        
        [Test]
        public void Scenario_6()
        {
            var adminPage = Navigation.OpenAdminVacationList();
            
            var claim1 = Claim.CreateDefault() with {Id = "1", Status = ClaimStatus.NonHandled};

            ClaimStorage.Add(new[] {claim1});
            
            adminPage.Refresh();
            var el = adminPage.ClaimList.Items.Wait().Single(x => x.TitleLink.Visible, Is.EqualTo(true));
            var lb = el.TitleLink.ClickAndOpen<ClaimLightbox>();
            lb.Footer.AcceptButton.Click();
            el.StatusLabel.Text.Wait().EqualTo(ClaimStatus.Accepted.GetDescription());
            
        }
    }
}