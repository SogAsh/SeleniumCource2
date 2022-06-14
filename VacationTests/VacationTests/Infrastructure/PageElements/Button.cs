using Kontur.Selone.Extensions;
using Kontur.Selone.Properties;
using OpenQA.Selenium;

namespace VacationTests.Infrastructure.PageElements
{
    // Об интерфейсах https://ulearn.me/course/basicprogramming/Interfeysy_3df89dfb-7f0f-4123-82ac-364c3a426396
    // О наследовании https://ulearn.me/course/basicprogramming/Nasledovanie_ac2b8cb6-8d63-4b81-9083-eaa77ab0c89c
    public class Button : ControlBase, IClickable
    {
        public Button(ISearchContext searchContext, By by) : base(searchContext, by)
        {
        }

        public IProp<string> Text => container.Text();
    }
}