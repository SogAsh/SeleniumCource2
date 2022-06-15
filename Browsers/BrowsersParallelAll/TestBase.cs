using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

[assembly: Parallelizable(ParallelScope.All)]
// [assembly: LevelOfParallelism(4)]

namespace BrowsersParallelAll
{
    [FixtureLifeCycleAttribute(LifeCycle.InstancePerTestCase)]
    public class TestBase
    {
        public IWebDriver Driver;

        [SetUp]
        public void SetUp()
        {
            var options = new ChromeOptions();
            //options.AddArgument("--start-maximized"); //на весь экран делаем браузер
            options.AddArgument("--window-size=1024,768"); //устанавливем ширину экрана
            Driver = new ChromeDriver(options);
            
            // Navigate("https://www.google.com/");
        }

        [TearDown]
        public void TearDownGlobal()
        {
            Driver.Quit();
            Driver = null;
        }
        public void Navigate(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }
    }
}