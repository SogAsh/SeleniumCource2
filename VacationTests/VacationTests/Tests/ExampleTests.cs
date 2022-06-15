using System;
using Kontur.Selone.Pages;
using NUnit.Framework;
using VacationTests.Claims;
using VacationTests.Infrastructure;
using VacationTests.PageObjects;

namespace VacationTests.Tests
{
    // [Parallelizable ] // тут можно указать, что тесты класса не должны / должны идти в параллель
    public class
        ExampleTests : VacationTestBase // класс с тестами наследуется от базового класса, в котором могут быть SetUp и TearDown
    {
        private readonly string employeeId = "newEmployeeId"; // тут можно хранить переменные, доступные для всех тестов
        private EmployeeVacationListPage employeePage;

        [SetUp] // действия, выполняемые перед каждым тестом
        public void SetUp()
        {
            //base.SetUp(); // если SetUp был бы ещё в базовом классе TestBase
            employeePage =
                Navigation.OpenEmployeeVacationList(
                    employeeId); // можно сделать какое-то общее действие для всех тестов
        }

        [TearDown] // действия, выполняемые после каждого теста
        public override void TearDown()
        {
            //ClaimStorage.ClearClaims(); // можно сделать какое-то общее действие для всех тестов
            base.TearDown(); // вызов TearDown базового класса VacationTestBase
        }

        [Test] // тест
        public void EnterWorker()
        {
            var defaultDirector = new Director(14, "Бублик Владимир Кузьмич", "Директор департамента");
            var claim = new Claim("567", ClaimType.Study, ClaimStatus.Accepted, defaultDirector,
                DateTime.Now.Date.AddDays(14), DateTime.Now.Date.AddDays(14 + 7), null, employeeId, true);
            // todo: после решение задания 6 заменить создание на код ниже
            // var claim = Claim.CreateDefault() with
            // {
            //     UserId = employeeId,
            // };

            ClaimStorage.Add(new[] {claim});
            employeePage.Refresh();
            employeePage.SalaryCalculatorTab.Present.Wait().EqualTo(true);
            employeePage.ClaimList.Items.Count.Wait().That(Is.AtLeast(1));
        }
    }
}