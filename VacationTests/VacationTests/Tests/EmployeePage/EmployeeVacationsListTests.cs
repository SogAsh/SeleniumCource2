using System;
using System.Linq;
using Kontur.Selone.Pages;
using Kontur.Selone.Properties;
using NUnit.Framework;
using VacationTests.Claims;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;

namespace VacationTests.Tests.EmployeePage
{
    public class EmployeeVacationsListTests : VacationTestBase
    {
        private string employeeId => TestContext.CurrentContext.Test.Name;

        [Test]
        public void CreateVacations_ShouldAddItemsToClaimsList()
        {
            var page = Navigation.OpenEmployeeVacationList(employeeId);
            // проверяем, что список пустой
            page.ClaimList.Items.Count.Wait().EqualTo(0);

            // создаем 3 отпуска с дефолтными данными - для теста важно только количество отпусков
            var claims = new[] {CreateDefaultClaim(), CreateDefaultClaim(), CreateDefaultClaim()};
            // todo  после реализации record заменить код создания на код ниже
            // var claims = new[] {Claim.CreateDefault(), Claim.CreateDefault(), Claim.CreateDefault()}; // создаем абстракции
            ClaimStorage.Add(claims); // записываем их в базу

            // обновляем страницу, чтобы обновить список
            page.Refresh();

            // ожидание трёх строк в таблице
            page.ClaimList.Items.Count.Wait().EqualTo(3);
        }

        [Test]
        public void ClaimsList_ShouldDisplayRightTitles()
        {
            var page = Navigation.OpenEmployeeVacationList(employeeId);

            // создаем 3 отпуска с опредленными номерами, чтобы дальше их проверить
            // остальные данные не важны для теста
            var claims = new[] {CreateDefaultClaim("1"), CreateDefaultClaim("2"), CreateDefaultClaim("3")};
            // todo  после реализации record заменить код создания на код ниже
            // var claims = new[]
            // {
            //     Claim.CreateDefault() with {Id = "1"},
            //     Claim.CreateDefault() with {Id = "2"},
            //     Claim.CreateDefault() with {Id = "3"}
            // };
            ClaimStorage.Add(claims);
            page.Refresh();

            // проверка данных по первому столбцу с учётом порядка
            page.ClaimList.Items.Select(x => x.TitleLink.Text)
                .Wait().EqualTo(new[] {"Заявление 1", "Заявление 2", "Заявление 3"});

            // проверка данных по первому столбцу без учёта порядка
            page.ClaimList.Items.Select(x => x.TitleLink.Text)
                .Wait().EquivalentTo(new[] {"Заявление 2", "Заявление 3", "Заявление 1"});
        }

        [Test]
        public void FirstClaim_ShouldDisplayRightDataInColumns()
        {
            // todo  после реализации record расскомментировать код ниже

            // var page = Navigation.OpenEmployeeVacationList(employeeId);
            //
            // // создаем 2 отпуска
            // var claims = new[]
            // {
            //     Claim.CreateDefault() with {Id = "1"},
            //     Claim.CreateDefault() with
            //     {
            //         Id = "2",
            //         StartDate = new DateTime(2020, 8, 13),
            //         EndDate = new DateTime(2020, 8, 20),
            //         Status = ClaimStatus.Accepted
            //     }
            // };
            //
            // ClaimStorage.Add(claims);
            // page.Refresh();
            //
            // // метод Single() находит элемент по признаку, проверяет, что нашелся только один элемент и возвращает его
            // var claim = page.ClaimList.Items.Wait().Single(
            //     x => x.TitleLink.Text, Is.EqualTo("Заявление 2"));
            //
            // // У полученного элемента проверяем разные поля
            // claim.PeriodLabel.Text.Wait().EqualTo("13.08.2020 - 20.08.2020");
            // claim.StatusLabel.Text.Wait().EqualTo(ClaimStatus.Accepted.GetDescription());
            //
            // // можем кликнуть по любому полю или просто по элементу
            // claim.TitleLink.Click();
        }

        [Test]
        public void ClaimsList_ShouldDisplayRightDataInColumns_IgnoringOrder()
        {
            // todo  после реализации record расскомментировать код ниже

            // var page = Navigation.OpenEmployeeVacationList(employeeId);
            //
            // // создаем 3 отпуска
            // var claims = new[]
            // {
            //     Claim.CreateDefault() with
            //     {
            //         Id = "1",
            //         StartDate = new DateTime(2020, 8, 1),
            //         EndDate = new DateTime(2020, 8, 14),
            //         Status = ClaimStatus.NonHandled
            //     },
            //     Claim.CreateDefault() with
            //     {
            //         Id = "2",
            //         StartDate = new DateTime(2020, 8, 13),
            //         EndDate = new DateTime(2020, 8, 20),
            //         Status = ClaimStatus.Accepted
            //     },
            //     Claim.CreateDefault() with
            //     {
            //         Id = "3",
            //         StartDate = new DateTime(2021, 5, 29),
            //         EndDate = new DateTime(2021, 5, 30),
            //         Status = ClaimStatus.Rejected
            //     }
            // };
            // ClaimStorage.Add(claims);
            // page.Refresh();
            //
            // // ожидаемые значения
            // var expected = new[]
            // {
            //     ("Заявление 3", "29.05.2021 - 30.05.2021", ClaimStatus.Rejected.GetDescription()),
            //     ("Заявление 2", "13.08.2020 - 20.08.2020", ClaimStatus.Accepted.GetDescription()),
            //     ("Заявление 1", "01.08.2020 - 14.08.2020", ClaimStatus.NonHandled.GetDescription())
            // };
            //
            // // Проверка значений строк для выбранных столбцов (название заявления, период, статус)
            // // Без учёта порядка элементов списка
            // page.ClaimList.Items
            //     .Select(claim => Props.Create(claim.TitleLink.Text, claim.PeriodLabel.Text, claim.StatusLabel.Text))
            //     .Wait().EquivalentTo(expected);
        }

        [Test]
        public void ClaimsList_ShouldDisplayRightDataInColumns_InRightOrder()
        {
            // todo  после реализации record расскомментировать код ниже

            // var page = Navigation.OpenEmployeeVacationList(employeeId);
            //
            // // создаем 3 отпуска
            // var claims = new[]
            // {
            //     Claim.CreateDefault() with
            //     {
            //         Id = "1",
            //         Status = ClaimStatus.NonHandled
            //     },
            //     Claim.CreateDefault() with
            //     {
            //         Id = "2",
            //         Status = ClaimStatus.Accepted
            //     },
            //     Claim.CreateDefault() with
            //     {
            //         Id = "3",
            //         Status = ClaimStatus.Rejected
            //     }
            // };
            // ClaimStorage.Add(claims);
            // page.Refresh();
            //
            // // ожидаемые значения
            // var expectedColumns = new[]
            // {
            //     ("Заявление 1", true),
            //     ("Заявление 2", true),
            //     ("Заявление 3", true)
            // };
            // // проверка значений строк для выбранных столбцов (название заявления, видимость статуса)
            // // с учётом порядка элементов списка
            // // в качестве Props можно брать любые пропсы, не только Text, но и, например, видимость или Checked
            // page.ClaimList.Items.Select(x => Props.Create(x.TitleLink.Text, x.StatusLabel.Visible))
            //     .Wait()
            //     .EqualTo(expectedColumns);
        }

        [Test]
        public void ClaimsList_EachUserCanSeeOnlyHisClaims()
        {
            const string firstUserId = "employee1";
            const string secondUserId = "employee2";
            var page = Navigation.OpenEmployeeVacationList(firstUserId);

            var claims = new[] {CreateDefaultClaim("1", firstUserId), CreateDefaultClaim("2", secondUserId), CreateDefaultClaim("3", firstUserId)};
            // todo  после реализации record заменить код создания на код ниже
            // var claims = new[]
            // {
            //     Claim.CreateDefault() with {Id = "1", UserId = firstUserId},
            //     Claim.CreateDefault() with {Id = "2", UserId = secondUserId},
            //     Claim.CreateDefault() with {Id = "3", UserId = firstUserId}
            // };
            ClaimStorage.Add(claims);
            page.Refresh();
            page.ClaimList.Items.Count.Wait().EqualTo(2);

            // todo на UI показывается просто порядковый номер заявления, изменить на id
            //page.ClaimList.Items.Select(x => x.TitleLink.Text).Wait().EquivalentTo(new[] { "Заявление 1", "Заявление 3" });

            page = Navigation.OpenEmployeeVacationList(secondUserId);
            page.Refresh();
            page.ClaimList.Items.Count.Wait().EqualTo(1);

            // todo на UI показывается просто порядковый номер заявления, изменить на id
            //page.ClaimList.Items.Select(x => x.TitleLink.Text).Wait().EquivalentTo(new[] { "Заявление 2" });
        }

        // todo  после реализации record удалить код ниже
        private Claim CreateDefaultClaim(string claimId = null, string userId = null)
        {
            var random = new Random();
            var randomClaimId = random.Next(1, 101).ToString();
            var defaultDirector = new Director(14, "Бублик Владимир Кузьмич", "Директор департамента");
            return new Claim(claimId ?? randomClaimId, ClaimType.Study, ClaimStatus.Accepted, defaultDirector,
                DateTime.Now.Date.AddDays(14), DateTime.Now.Date.AddDays(14 + 7), null, userId ?? employeeId, true);
        }
    }
}