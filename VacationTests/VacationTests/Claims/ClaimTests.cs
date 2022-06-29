using System;
using System.Linq;
using FluentAssertions;
using Kontur.Selone.Pages;
using NUnit.Framework;
using VacationTests.Infrastructure;

namespace VacationTests.Claims
{
    public class ClaimTests : VacationTestBase
    {
        // Это unit тест, проверяющий метод Serialize() у ClaimStorage
        [Test]
        public void Serialize()
        {
            var claim = new Claim("1", ClaimType.Child, ClaimStatus.NonHandled,
                new Director(24939, "Захаров Максим Николаевич", "Руководитель направления тестирования"),
                new DateTime(2021, 08, 1), new DateTime(2021, 08, 5),
                1, "1", true);
            // todo после решения Задания 6 заиспользовать код ниже вместо создания класса напрямую
            /*var claim = Claim.CreateDefault() with
            {
                Id = "1",
                Status = ClaimStatus.NonHandled,
                Type = ClaimType.Child,
                StartDate = new DateTime(2021, 08, 1),
                EndDate = new DateTime(2021, 08, 5),
                PaidNow = true,
                ChildAgeInMonths = 1,
                UserId = "1",
                Director = Director.CreateDefault() with
                {
                    Id = 24939,
                    Name = "Захаров Максим Николаевич",
                    Position = "Руководитель направления тестирования"
                }
            };*/
            var serialized = ClaimStorage.Serialize(claim);
            var expected =
                "{\"id\":\"1\",\"type\":\"По уходу за ребенком\",\"status\":2,\"director\":{\"id\":24939,\"name\":\"Захаров Максим Николаевич\",\"position\":\"Руководитель направления тестирования\"},\"startDate\":\"01.08.2021\",\"endDate\":\"05.08.2021\",\"childAgeInMonths\":1,\"userId\":\"1\",\"paidNow\":true}";
            Assert.That(serialized, Is.EqualTo(expected));
        }

        [Test]
        public void Deserialize()
        {
            const string localStorageData =
                "{\"id\":\"1\",\"type\":\"По уходу за ребенком\",\"status\":2,\"director\":{\"id\":24939,\"name\":\"Захаров Максим Николаевич\",\"position\":\"Руководитель направления тестирования\"},\"startDate\":\"01.08.2021\",\"endDate\":\"05.08.2021\",\"childAgeInMonths\":1,\"userId\":\"1\",\"paidNow\":true}";
            var expectClaim = new Claim("1", ClaimType.Child, ClaimStatus.NonHandled,
                new Director(24939, "Захаров Максим Николаевич", "Руководитель направления тестирования"),
                new DateTime(2021, 08, 1), new DateTime(2021, 08, 5),
                1, "1", true);
            // todo после решения Задания 6 заиспользовать код ниже вместо создания класса напрямую
            /*var expectClaim = Claim.CreateDefault() with
            {
                Id = "1",
                Status = ClaimStatus.NonHandled,
                Type = ClaimType.Child,
                StartDate = new DateTime(2021, 08, 1),
                EndDate = new DateTime(2021, 08, 5),
                PaidNow = true,
                ChildAgeInMonths = 1,
                UserId = "1",
                Director = Director.CreateDefault() with
                {
                    Id = 24939,
                    Name = "Захаров Максим Николаевич",
                    Position = "Руководитель направления тестирования"
                }
            };*/
            var deserialized = ClaimStorage.Deserialize<Claim>(localStorageData);

            deserialized.Should().BeEquivalentTo(expectClaim);
        }

        [Test]
        public void DeserializeArray()
        {
            const string localStorageArray =
                "[{\"endDate\":\"29.03.2022\",\"id\":\"1\",\"paidNow\":false,\"startDate\":\"22.03.2022\",\"status\":1,\"type\":\"Основной\",\"userId\":\"3\",\"director\":{\"id\":24939,\"name\":\"Захаров Максим Николаевич\",\"position\":\"Руководитель направления тестирования\"}},{\"endDate\":\"09.04.2022\",\"id\":\"2\",\"paidNow\":false,\"startDate\":\"29.03.2022\",\"status\":2,\"type\":\"Основной\",\"userId\":\"1\",\"director\":{\"id\":24939,\"name\":\"Захаров Максим Николаевич\",\"position\":\"Руководитель направления тестирования\"}}]";

            var claim1 = new Claim("1", ClaimType.Paid, ClaimStatus.Rejected,
                new Director(24939, "Захаров Максим Николаевич", "Руководитель направления тестирования"),
                new DateTime(2022, 03, 22), new DateTime(2022, 03, 29),
                null, "3", false);
            var claim2 = new Claim("2", ClaimType.Paid, ClaimStatus.NonHandled,
                new Director(24939, "Захаров Максим Николаевич", "Руководитель направления тестирования"),
                new DateTime(2022, 03, 29), new DateTime(2022, 04, 09),
                null, "1", false);
            var expectClaim = new[] { claim1, claim2 };

            // todo после решения Задания 6 заиспользовать код ниже вместо создания класса напрямую
            /*var expectClaim = new[]
            {
                Claim.CreateDefault() with
                {
                    Id = "1",
                    Type = ClaimType.Paid,
                    Status = ClaimStatus.Rejected,
                    Director = Director.CreateDefault() with
                    {
                        Id = 24939,
                        Name = "Захаров Максим Николаевич",
                        Position = "Руководитель направления тестирования"
                    },
                    StartDate = new DateTime(2022, 03, 22),
                    EndDate = new DateTime(2022, 03, 29),
                    UserId = "3",
                    PaidNow = false
                },
                Claim.CreateDefault() with
                {
                    Id = "2",
                    Type = ClaimType.Paid,
                    Status = ClaimStatus.NonHandled,
                    Director = Director.CreateDefault() with
                    {
                        Id = 24939,
                        Name = "Захаров Максим Николаевич",
                        Position = "Руководитель направления тестирования"
                    },
                    StartDate = new DateTime(2022, 03, 29),
                    EndDate = new DateTime(2022, 04, 09),
                    UserId = "1",
                    PaidNow = false
                }
            };*/
            var deserialized = ClaimStorage.Deserialize<Claim[]>(localStorageArray);
            deserialized.Should().BeEquivalentTo(expectClaim, options => options.WithoutStrictOrdering());
        }

        [Test]
        public void ClaimTest()
        {
            var director = new Director(24939, "Захаров Максим Николаевич", "Руководитель направления тестирования");
            var startDate = DateTime.Now.Date.AddDays(7);
            var endDate = DateTime.Now.Date.AddDays(12);
            
            var page = Navigation.OpenEmployeeVacationList(employeeId1);

            // создаем 3 заявления на отпуск
            var claim1 = new Claim("1", ClaimType.Paid, ClaimStatus.Accepted, director, startDate, endDate, null, employeeId1, false);
            var claim2 = new Claim("2", ClaimType.Paid, ClaimStatus.Accepted, director, startDate, endDate, null, employeeId1, false);
            var claim3 = new Claim("3", ClaimType.Paid, ClaimStatus.Accepted, director, startDate, endDate, null, employeeId2, false);

            // записываем заявления в базу
            ClaimStorage.Add(new[] { claim1, claim2, claim3 });

            page.Refresh();

            // проверяем, что на списке сотрудника с id=1 два заявления с номерами 1 и 2
            page.ClaimList.Items.Select(x => x.TitleLink.Text)
                .Wait().EqualTo(new[] { "Заявление 1", "Заявление 2" });
        }

        [Test]
        public void ClaimTest1()
        {
            var page = Navigation.OpenEmployeeVacationList(employeeId1);

            // создаем 3 заявления на отпуск
            // AClaim() статический метод класса, поэтому его можно вызвать так - ClaimBuilder.AClaim()
            var claim1 = ClaimBuilder.AClaim().WithId("1").WithUserId(employeeId1).Build();
            var claim2 = ClaimBuilder.AClaim().WithId("2").WithUserId(employeeId1).Build();
            var claim3 = ClaimBuilder.AClaim().WithId("3").WithUserId(employeeId2).Build();

            // записываем заявления в базу
            ClaimStorage.Add(new[] { claim1, claim2, claim3 });

            page.Refresh();

            // проверяем, что на списке сотрудника с id=1 два заявления с номерами 1 и 2
            page.ClaimList.Items.Select(x => x.TitleLink.Text)
                .Wait().EqualTo(new[] { "Заявление 1", "Заявление 2" });
        }

        [Test]
        public void ClaimTest3()
        {
            var page = Navigation.OpenEmployeeVacationList(employeeId1);
            var startDate = DateTime.Now.AddDays(7).Date;
            var endDate = DateTime.Now.AddDays(12).Date;

            // создаем 3 заявления на отпуск
            // AClaim() статический метод класса, поэтому его можно вызвать так - ClaimBuilder.AClaim()
            var claim1 = ClaimBuilder.AClaim().WithUserId(employeeId1).Build();
            var claim2 = ClaimBuilder.AClaim().WithUserId(employeeId1).Build();
            var claim3 = ClaimBuilder.AClaim().WithUserId(employeeId2).Build();
            var claim4 = ClaimBuilder.AClaim().WithPeriod(startDate, endDate).Build();

            // записываем заявления в базу
            ClaimStorage.Add(new[] { claim1, claim2, claim3 });

            page.Refresh();

            // проверяем, что на списке сотрудника с id=1 два заявления с номерами 1 и 2
            page.ClaimList.Items.Select(x => x.TitleLink.Text)
                .Wait().EqualTo(new[] {$"Заявление {claim1.Id}", $"Заявление {claim2.Id}"});
        }

        [Test]
        public void ClaimTest4()
        {
            Navigation.OpenEmployeeVacationList(employeeId1);
            
            // создаем 3 заявления на отпуск
            var claims = new[]
            {
                ClaimBuilder.ADefaultClaim(), ClaimBuilder.ADefaultClaim(), ClaimBuilder.AChildClaim()
            };

            // записываем заявления в базу
            ClaimStorage.Add(claims);
        }
        
        // [Test]
        // public void ClaimTest5Record()
        // {
        //     var page = Navigation.OpenEmployeeVacationList(employeeId1);
        //     
        //     // создаем 3 заявления на отпуск
        //     // CreateDefault() статический метод класса, поэтому его можно вызвать так - Claim.CreateDefault()
        //     // для указания нужных свойств используется ключевое слово `with`
        //     // в фигурных скобках перечисляются необходимые для теста свойства и их значения
        //     var claim1 = Claim.CreateDefault() with {Id = "1", UserId = employeeId1};
        //     var claim2 = Claim.CreateDefault() with {Id = "2", UserId = employeeId1};
        //     var claim3 = Claim.CreateDefault() with {Id = "3", UserId = employeeId2};
        //
        //     // записываем заявления в базу
        //     ClaimStorage.Add(new[] {claim1, claim2, claim3});
        //
        //     page.Refresh();
        //
        //     // проверяем, что на списке сотрудника с id=1 два заявления с номерами 1 и 2
        //     page.ClaimList.Items.Select(x => x.TitleLink.Text)
        //         .Wait().EqualTo(new[] {"Заявление 1", "Заявление 2"});
        // }
        //
        // [Test]
        // public void ClaimTest6Record()
        // {
        //     // создаем 3 заявления на отпуск
        //     // записываем заявления в базу
        //     ClaimStorage.Add(new[] {Claim.CreateDefault(), Claim.CreateDefault(), Claim.CreateChildType()});
        // }
        
        private string employeeId1 = "1";
        private string employeeId2 = "2";
    }
}