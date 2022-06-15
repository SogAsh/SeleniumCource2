using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

[assembly: Parallelizable(ParallelScope.All)]
[assembly: LevelOfParallelism(2)]

namespace BrowsersParallelTestFixture
{
    [FixtureLifeCycleAttribute(LifeCycle.InstancePerTestCase)]
    public class TestBase
    {
        public IWebDriver Driver;

        // [OneTimeSetUp]
        // public void SetUpGlobal()
        // {
        //     var options = new ChromeOptions();
        //     // options.AddArgument("--start-maximized"); //на весь экран делаем браузер
        //     options.AddArgument("--window-size=1024,768"); //устанавливем ширину экрана
        //     Driver = new ChromeDriver(options);
        // }

        [SetUp]
        public void SetUp()
        {
            // Navigate("https://www.google.com/");
            
            var options = new ChromeOptions();
            // options.AddArgument("--start-maximized"); //на весь экран делаем браузер
            options.AddArgument("--window-size=1024,768"); //устанавливем ширину экрана
            Driver = new ChromeDriver(options);
        }
        
        [TearDown]
        public void TearDownGlobal()
        {
            Driver.Quit();
            Driver = null;
        }

        // [OneTimeTearDown]
        // public virtual void TearDownGlobal()
        // {
        //     Driver.Quit();
        //     Driver = null;
        // }
        public void Navigate(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }
    }
}