using System.Collections.Concurrent;
using NUnit.Framework;
using OpenQA.Selenium;

namespace BrowsersPull
{
    public class TestBase
    {
        // создаём словарь для хранения браузеров пула
        private static readonly ConcurrentDictionary<string, IWebDriver> AcquiredWebDrivers =
            new ConcurrentDictionary<string, IWebDriver>();


        // свойство, которое с помощью NUnit отдаёт текущий WorkerId теста
        private static string TestWorkerId => TestContext.CurrentContext.WorkerId ?? "debug";
        protected IWebDriver Driver => GetWebDriver();

        [SetUp]
        public void SetUp()
        {
            Navigate("https://www.google.com/");
        }

        [TearDown]
        public virtual void TearDown()
        {
            if (AcquiredWebDrivers.TryRemove(TestWorkerId, out var webDriver))
                AssemblySetUpFixture.WebDriverPool.Release(webDriver);
        }
        public void Navigate(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }

        protected static IWebDriver GetWebDriver()
        {
            return AcquiredWebDrivers.GetOrAdd(TestWorkerId ?? "debug",
                x => AssemblySetUpFixture.WebDriverPool.Acquire());
        }
    }
}