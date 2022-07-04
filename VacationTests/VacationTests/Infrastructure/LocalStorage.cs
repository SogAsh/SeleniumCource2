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
        public long Length => (long) webDriver.JavaScriptExecutor().ExecuteScript("return localStorage.length;");

        // очистка всего хранилища
        public void Clear()
        {
            webDriver.JavaScriptExecutor().ExecuteScript("localStorage.clear();");
        }

        // получение данных по ключу keyName
        public string GetItem(string keyName)
        {
            var result = webDriver.JavaScriptExecutor().ExecuteScript($"return localStorage.getItem(\"{keyName}\");");
            return result as string;
        }

        // получение ключа на заданной позиции
        public string Key(int keyNumber)
        {
            var result = webDriver.JavaScriptExecutor().ExecuteScript($"return localStorage.key(\"{keyNumber}\");");
            return result.ToString();
        }

        // удаление данных с ключом keyName
        public void RemoveItem(string keyName)
        {
            webDriver.JavaScriptExecutor().ExecuteScript($"localStorage.removeItem(\"{keyName}\");");
        }

        // сохранение пары ключ/значение
        public void SetItem(string keyName, string value)
        {
            webDriver.JavaScriptExecutor().ExecuteScript($"localStorage.setItem(\"{keyName}\", '{value}');");
        }
    }
}