using Kontur.Selone.Extensions;
using Kontur.Selone.Pages;
using NUnit.Framework;
using VacationTests.Infrastructure;
using VacationTests.PageObjects;

namespace VacationTests.Tests.Storage
{
    [NonParallelizable]
    public class LocalStorageTests : VacationTestBase
    {
        // название ключа, который будем добавлять
        private const string ClaimsKeyName = "Vacation_App_Claims";

        // значение, которое будем добавлять для ключа ClaimsKeyName
        private const string FirstVacation =
            "[{\"endDate\":\"14.08.2020\",\"id\":\"1\",\"paidNow\":true,\"startDate\":\"01.08.2020\",\"status\":2,\"type\":\"Основной\",\"userId\":\"1\",\"director\":{\"id\":24939,\"name\":\"Захаров Максим Николаевич\",\"position\":\"Руководитель направления тестирования\"}}]";

        private EmployeeVacationListPage employeePage;

        [SetUp]
        public void SetUp()
        {
            // открываем список отпусков в каждом тесте
            employeePage = Navigation.OpenEmployeeVacationList();

            // перед каждого теста чистим LocalStorage, поскольку в данном случае данные с прошлого теста могут мешать новому тесту
            LocalStorage.Clear();
            employeePage.Refresh();

            // проверяем, что список пустой
            employeePage.ClaimList.Items.Count.Wait().EqualTo(0);
        }

        [Test]
        public void AddClaims_WithSeloneInterface()
        {
            // получаем веб-драйвер и с помощью Selonе и метода JavaScriptExecutor() добавляем отпуск в localStorage
            GetWebDriver().JavaScriptExecutor()
                .ExecuteScript($"localStorage.setItem(\"{ClaimsKeyName}\", '{FirstVacation}');");

            // чтобы отпуск появился в интерфейсе, обновляем страницу
            employeePage.Refresh();

            // проверяем, что запись отобразилась
            employeePage.ClaimList.Items.Count.Wait().EqualTo(1);
        }

        [Test]
        public void AddClaims_WithLocalStorageClassInterface()
        {
            // добавляем в хранилище отпуск
            LocalStorage.SetItem(ClaimsKeyName, FirstVacation);

            employeePage.Refresh();
            employeePage.ClaimList.Items.Count.Wait().EqualTo(1);
        }

        [Test]
        public void AddClaims_DeleteClaimsWithRemoveItemMethod()
        {
            // добавляем 1 отпуск
            LocalStorage.SetItem(ClaimsKeyName, FirstVacation);
            employeePage.Refresh();
            employeePage.ClaimList.Items.Count.Wait().EqualTo(1);
            // добавляем ещё один ключ, который не должен удалиться
            LocalStorage.SetItem("TestKey", "TestName");

            // проверяем, что в LocalStorage два ключа
            Assert.That(LocalStorage.Length, Is.EqualTo(2)); 

            // удаляем отпуск
            LocalStorage.RemoveItem(ClaimsKeyName);
            employeePage.Refresh();
            // проверяем, что отпуск исчез из интерфейса
            employeePage.ClaimList.Items.Count.Wait().EqualTo(0);

            // проверяем, что в LocalStorage остался 1 ключ
            Assert.That(LocalStorage.Length, Is.EqualTo(1));
        }

        [Test]
        public void AddClaims_DeleteClaimsWithClearMethod()
        {
            // добавляем 1 отпуск
            LocalStorage.SetItem(ClaimsKeyName, FirstVacation);
            employeePage.Refresh();
            employeePage.ClaimList.Items.Count.Wait().EqualTo(1);

            // добавляем ещё один ключ, который тоже удалим
            LocalStorage.SetItem("TestKey", "TestName");

            // очищаем всё хранилище
            LocalStorage.Clear();

            employeePage.Refresh();
            // проверяем, что отпуск исчез из интерфейса
            employeePage.ClaimList.Items.Count.Wait().EqualTo(0);

            // проверяем, что в LocalStorage нет ключей
            Assert.That(LocalStorage.Length, Is.EqualTo(0));
        }

        [Test]
        public void AddClaims_GetItem()
        {
            // добавляем 1 отпуск
            LocalStorage.SetItem(ClaimsKeyName, FirstVacation);

            // запрашиваем значение для ключа ClaimsKeyName
            var result = LocalStorage.GetItem(ClaimsKeyName);

            // проверяем, что значение равно тому, что мы задавали
            Assert.That(result, Is.EqualTo(FirstVacation));
        }

        [Test]
        public void GetNonExistentItem()
        {
            var nonExistentKey = "nonExistentKey";
            // запрашиваем значение для ключа ClaimsKeyName
            var result = LocalStorage.GetItem(nonExistentKey);

            // проверяем, что значение равно тому, что мы задавали
            Assert.That(result, Is.Null);
        }

        [Test]
        public void AddClaims_CheckLength()
        {
            // добавляем 1 отпуск
            LocalStorage.SetItem(ClaimsKeyName, FirstVacation);

            // проверяем, что теперь в хранилище есть всего 1 ключ
            Assert.That(LocalStorage.Length, Is.EqualTo(1));
        }

        [Test]
        public void AddClaims_GetKeyNameByIndex()
        {
            // добавляем 1 отпуск, первый ключ будет иметь индекс 0
            LocalStorage.SetItem(ClaimsKeyName, FirstVacation);
            // добавляем ещё один ключ, второй ключ будет иметь индекс 1
            LocalStorage.SetItem("TestKey", "TestName");

            // запрашиваем ключ с индексом 0, проверяем, что это ClaimsKeyName
            Assert.That(LocalStorage.Key(0).Equals(ClaimsKeyName));
        }
    }
}