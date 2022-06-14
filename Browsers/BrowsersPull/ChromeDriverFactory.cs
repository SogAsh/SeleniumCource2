using System;
using Kontur.Selone.Extensions;
using Kontur.Selone.WebDrivers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace BrowsersPull
{
    class ChromeDriverFactory : IWebDriverFactory
    {
        public IWebDriver Create()
        {
            var chromeDriverService = ChromeDriverService.CreateDefaultService(AppDomain.CurrentDomain.BaseDirectory);
            var options = new ChromeOptions();
            options.AddArguments("--no-sandbox", "--start-maximized", "--disable-extensions");
            // использовать для запуска тестов на ulearn
            // chromeDriverService.SuppressInitialDiagnosticInformation = true;
            // options.AddArguments("--no-sandbox", "--start-maximized", "--disable-extensions", "--disable-dev-shm-usage", "--headless");
            var chromeDriver = new ChromeDriver(chromeDriverService, options, TimeSpan.FromSeconds(180));
            chromeDriver.Manage().Window.SetSize(1280, 960);
            return chromeDriver;
        }
    }
}