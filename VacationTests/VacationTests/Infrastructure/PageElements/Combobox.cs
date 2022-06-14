using Kontur.Selone.Elements;
using Kontur.Selone.Extensions;
using Kontur.Selone.Properties;
using Kontur.Selone.Selectors.Css;
using NUnit.Framework;
using OpenQA.Selenium;
using VacationTests.Infrastructure.Properties;

namespace VacationTests.Infrastructure.PageElements
{
    public class Combobox : ControlBase
    {
        private readonly Input input;

        public Combobox(ISearchContext searchContext, By by) : base(searchContext, by)
        {
            input = new Input(container, By.XPath(".//*[contains(@data-comp-name,'CommonWrapper Input')]"));
            MenuItems = new ElementsCollection<Button>(container.Root(),
                x => x.WithTid("ComboBoxMenu__item").FixedByIndex(),
                (s, b, e) => new Button(s, b));
        }

        public IProp<string> Text => container.Text();
        public IProp<bool> HasError => container.HasError();
        public ElementsCollection<Button> MenuItems { get; }

        public void SelectValue(string value)
        {
            Open();
            MenuItems.Wait().Single(x => x.Text, Contains.Substring(value))
                .Click();
        }

        public void InputValue(string text)
        {
            container.Click();
            input.ClearAndInputText(text);
        }

        public void WaitValue(string text)
        {
            input.Value.Wait().EqualTo(text);
        }

        public void Open()
        {
            container.Click();
        }
    }
}