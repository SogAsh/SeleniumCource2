using NUnit.Framework;
using OpenQA.Selenium;

namespace BrowsersParallelTestFixture.Tests
{
    [TestFixture]
    class TestClass3 : TestBase
    {
        // [SetUp]
        // public new void SetUp()
        // {
        //     Navigate("https://yandex.ru/");
        // }

        [Test]
        public void WikipediaTest()
        {
            Navigate("https://ru.wikipedia.org/");
            var search = Driver.FindElement(By.Name("search"));
            var searchButton = Driver.FindElement(By.Name("go"));

            search.SendKeys("Selenium");
            searchButton.Click();

            Assert.That(Driver.Title, Contains.Substring("Selenium — Википедия"), "Перешли на неверную страницу");
        }

        [Test]
        public void LabirintTest()
        {
            Navigate("https://labirint.ru/");
            Assert.That(Driver.Title, Contains.Substring("Лабиринт"), "Перешли на неверную страницу");
        }
    }
}