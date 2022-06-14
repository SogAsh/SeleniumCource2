using Kontur.Selone.Extensions;
using Kontur.Selone.WebDrivers;
using NUnit.Framework;
using VacationTests.Infrastructure;

[assembly: Parallelizable(ParallelScope.All)] // Запуск в параллель
[assembly: LevelOfParallelism(3)] // Уровень потоков

namespace VacationTests
{
    [SetUpFixture]
    public class AssemblySetUpFixture
    {
        public static WebDriverPool WebDriverPool;

        // метод запускается один раз перед всеми тестами, это обеспечивает тег SetUpFixture
        [OneTimeSetUp]
        public void SetUp()
        {
            // инициализация пула
            var factory = new ChromeDriverFactory();
            var cleaner = new DelegateWebDriverCleaner(x => x.ResetWindows());
            WebDriverPool = new WebDriverPool(factory, cleaner);
        }

        // метод запускается один раз после всех тестов, это обеспечивает тег SetUpFixture
        [OneTimeTearDown]
        public void TearDown()
        {
            // закрытие пула
            WebDriverPool.Clear();
        }
    }
}