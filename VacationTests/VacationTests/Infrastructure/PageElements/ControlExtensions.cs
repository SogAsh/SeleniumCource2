using System;
using System.Linq;
using System.Reflection;
using Kontur.Selone.Extensions;
using Kontur.Selone.Selectors.Context;
using OpenQA.Selenium;

namespace VacationTests.Infrastructure.PageElements
{
    // О методах расширения
    // https://ulearn.me/course/basicprogramming/Metody_rasshireniya_01a1f9a5-c475-4af3-bef3-060f92e69a92
    public static class ControlExtensions
    {
        public static T Control<T>(this IContextBy contextBy) where T : ControlBase
        {
            return (T) Activator.CreateInstance(typeof(T), contextBy.SearchContext, contextBy.By);
        }

        public static void InitializeControls(object control, ISearchContext searchContext)
        {
            // todo: добавить код автосоздания контролов
        }

        public static Button Button(this IContextBy contextBy)
        {
            return new Button(contextBy.SearchContext, contextBy.By);
        }

        public static Link Link(this IContextBy contextBy)
        {
            return new Link(contextBy.SearchContext, contextBy.By);
        }

        public static Input Input(this IContextBy contextBy)
        {
            return new Input(contextBy.SearchContext, contextBy.By);
        }

        public static Checkbox Checkbox(this IContextBy contextBy)
        {
            return new Checkbox(contextBy.SearchContext, contextBy.By);
        }

        public static Label Label(this IContextBy contextBy)
        {
            return new Label(contextBy.SearchContext, contextBy.By);
        }

        public static CurrencyLabel CurrencyLabel(this IContextBy contextBy)
        {
            return new CurrencyLabel(contextBy.SearchContext, contextBy.By);
        }

        public static CurrencyInput CurrencyInput(this IContextBy contextBy)
        {
            return new CurrencyInput(contextBy.SearchContext, contextBy.By);
        }

        public static Select Select(this IContextBy contextBy)
        {
            return new Select(contextBy.SearchContext, contextBy.By);
        }

        public static Combobox Combobox(this IContextBy contextBy)
        {
            return new Combobox(contextBy.SearchContext, contextBy.By);
        }

        public static DatePicker DateInput(this IContextBy contextBy)
        {
            return new DatePicker(contextBy.SearchContext, contextBy.By);
        }

        public static void Click(this IClickable control)
        {
            control.Container.Click();
        }

        /// <typeparam name="TPageObject">Должен содержать конструктор, принимающий IWebDriver</typeparam>
        public static TPageObject ClickAndOpen<TPageObject>(this IClickable control)
        {
            Click(control);
            return control.Container.WebDriver().CreatePage<TPageObject>();
        }

        public static void WaitPresence(this ControlBase control)
        {
            control.Present.Wait().EqualTo(true);
        }

        public static void WaitAbsence(this ControlBase control)
        {
            control.Present.Wait().EqualTo(false);
        }

        public static void WaitDisabled(this ControlBase control)
        {
            control.Disabled.Wait().EqualTo(true);
        }

        public static void WaitEnabled(this ControlBase control)
        {
            control.Disabled.Wait().EqualTo(false);
        }
    }
}