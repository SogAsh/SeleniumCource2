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

            // всё также получаем все элементы страницы и выбираем те, которые наследуются от ControlBase
            var props = GetType().GetProperties()
                .Where(p => typeof(ControlBase).IsAssignableFrom(p.PropertyType)).ToList();

            foreach (var prop in props)
            {
                // для каждого элемента проверяем, есть ли наш атрибут ByTidAttribute
                var tidName = prop.GetCustomAttributes<ByTidAttribute>()
                                  .Select(x => x.Tid) // если есть, запоминаем значение Tid
                                  .FirstOrDefault()
                              ?? prop.Name; // если нет атрибута, то берём название элемента (свойства класса) 
            
                // запоминаем селектор
                var contextBy = webDriver.Search(x => x.WithTid(tidName));
                // пробуем достать конструктор
                var constructor = prop.PropertyType.GetConstructor(new[] { typeof(ISearchContext), typeof(By) });
                // var constructor = prop.PropertyType.GetConstructor(new[] {typeof(IContextBy)});
            
                // если есть конструктор в методе инициализации страницы, то используем его
                if (constructor != null)
                {
                    var value = constructor.Invoke(new object[] { contextBy.SearchContext, contextBy.By });
                    prop.SetValue(this, value);
                }
                // если нет конструктора
                else 
                {
                    // можно попробовать получить другой тип конструктора и проинициализировать по нему
                }
                // дальше код попробует проинициализировать элемент из метода инициализации класса
            }
        }

        public IWebDriver WrappedDriver { get; }
    }
}