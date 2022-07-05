using System;
using System.Linq;
using System.Reflection;
using Kontur.Selone.Extensions;
using Kontur.Selone.Pages;
using Kontur.Selone.Selectors.Context;
using OpenQA.Selenium;
using VacationTests.Infrastructure;
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
            ControlExtensions.InitializeControls(this, webDriver);
        }

        public IWebDriver WrappedDriver { get; }
        
        public TPage ChangePageType<TPage>() where TPage : PageBase, new()
        {
            TPage page = new TPage();
            return page;
        }
    }
}