using Kontur.Selone.Pages;
using OpenQA.Selenium;
using VacationTests.Infrastructure.PageElements;

// О наследовании https://ulearn.me/course/basicprogramming/Nasledovanie_ac2b8cb6-8d63-4b81-9083-eaa77ab0c89c
// Об интерфейсах https://ulearn.me/course/basicprogramming/Interfeysy_3df89dfb-7f0f-4123-82ac-364c3a426396
namespace VacationTests.PageObjects
{
    public class PageBase : IPage
    {
        protected PageBase(IWebDriver webDriver)
        {
            WrappedDriver = webDriver;
        }

        public IWebDriver WrappedDriver { get; }
    }
}