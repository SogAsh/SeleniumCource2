using System.Collections.Concurrent;
using NUnit.Framework;
using OpenQA.Selenium;
using VacationTests.Claims;
using VacationTests.Infrastructure;
using VacationTests.PageNavigation;

namespace VacationTests
{
    [FixtureLifeCycleAttribute(LifeCycle.InstancePerTestCase)]
    public abstract class VacationTestBase
    {
        // создаём словарь для хранения браузеров пула
        private static readonly ConcurrentDictionary<string, IWebDriver> AcquiredWebDrivers = new();

        // свойство, которое с помощью NUnit отдаёт текущий WorkerId теста
        private static string TestWorkerId => TestContext.CurrentContext.WorkerId ?? "debug";
        protected IWebDriver WebDriver => GetWebDriver();
        protected Navigation Navigation => new(WebDriver);
        protected ClaimStorage ClaimStorage => new(LocalStorage);
        protected LocalStorage LocalStorage => new(WebDriver);

        // после каждого теста отдаём браузер обратно в пул
        [TearDown]
        public virtual void TearDown()
        {
            if (AcquiredWebDrivers.TryRemove(TestWorkerId, out var webDriver))
                AssemblySetUpFixture.WebDriverPool.Release(webDriver);
        }

        // метод получения текущего вебдрайвера
        protected static IWebDriver GetWebDriver()
        {
            return AcquiredWebDrivers.GetOrAdd(TestWorkerId, x => AssemblySetUpFixture.WebDriverPool.Acquire());
        }
    }
}