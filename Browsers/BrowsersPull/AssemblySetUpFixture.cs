using Kontur.Selone.Extensions;
using Kontur.Selone.WebDrivers;
using NUnit.Framework;

[assembly: Parallelizable(ParallelScope.All)]
// [assembly: LevelOfParallelism(4)]

namespace BrowsersPull
{
    [SetUpFixture]
    public class AssemblySetUpFixture
    {
        public static WebDriverPool WebDriverPool;

        [OneTimeSetUp]
        public void SetUpGlobal()
        {
            var factory = new ChromeDriverFactory();
            var cleaner = new DelegateWebDriverCleaner(x => x.ResetWindows());
            WebDriverPool = new WebDriverPool(factory, cleaner);
        }

        [OneTimeTearDown]
        public void TearDownGlobal()
        {
            WebDriverPool.Clear();
        }
    }
}