using System.Linq;
using System.Reflection;
using Kontur.Selone.Extensions;
using Kontur.Selone.Properties;
using OpenQA.Selenium;
using VacationTests.Infrastructure.Properties;

namespace VacationTests.Infrastructure.PageElements
{
    // Об интерфейсах https://ulearn.me/course/basicprogramming/Interfeysy_3df89dfb-7f0f-4123-82ac-364c3a426396
    public class ControlBase : IHaveContainer
    {
        protected IWebElement container;

        protected ControlBase(ISearchContext searchContext, By by)
        {
            container = searchContext.SearchElement(by);
            
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
                var contextBy = container.Search(x => x.WithTid(tidName));
                // пробуем достать конструктор
                var constructor = prop.PropertyType.GetConstructor(new[] { typeof(ISearchContext), typeof(By) });
            
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

        public IProp<bool> Present => container.Present(); // Typo IsPreset. Expression reflection
        public IProp<bool> Visible => container.Visible();
        public IProp<bool> Disabled => container.Disabled();
        IWebElement IHaveContainer.Container => container;

        public override string ToString()
        {
            try
            {
                return $"{container.TagName} {container.Text}";
            }
            catch (StaleElementReferenceException)
            {
                return "StaleElement (not found in DOM)";
            }
        }
    }
}