using Kontur.Selone.Extensions;
using OpenQA.Selenium;

namespace VacationTests.Infrastructure
{
    // Задание 5: для студентов оставляем только название методов и свойств, реализацию пишут сами
    public class LocalStorage
    {
        private readonly IWebDriver webDriver;

        public LocalStorage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        // получение количества элементов в хранилище
        // todo: public long Length => (long) написать код
        public long Length;

        // очистка всего хранилища
        public void Clear()
        {
            // todo: написать код
        }

        // получение данных по ключу keyName
        public string GetItem(string keyName)
        {
            // todo: написать код
            return null;
        }

        // получение ключа на заданной позиции
        public string Key(int keyNumber)
        {
            // todo: написать код
            return null;
        }

        // удаление данных с ключом keyName
        public void RemoveItem(string keyName)
        {
            // todo: написать код
        }

        // сохранение пары ключ/значение
        public void SetItem(string keyName, string value)
        {
            webDriver.JavaScriptExecutor().ExecuteScript($"localStorage.setItem(\"{keyName}\", '{value}');");
        }
    }
}