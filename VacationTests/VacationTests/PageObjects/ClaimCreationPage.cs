using Kontur.Selone.Extensions;
using OpenQA.Selenium;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageElements;

namespace VacationTests.PageObjects
{
    public class ClaimCreationPage : PageBase
    {
        public ClaimCreationPage(IWebDriver webDriver) : base(webDriver)
        {
            UserFioLabel = webDriver.Search(x => x.WithTid("UserFioLabel")).Label();
            ClaimTypeSelect = webDriver.Search(x => x.WithTid("ClaimTypeSelect")).Select();
            ChildAgeInput = webDriver.Search(x => x.WithTid("ChildAgeInput")).Input();
            ClaimStartDatePicker = webDriver.Search(x => x.WithTid("ClaimStartDatePicker")).DateInput();
            ClaimEndDatePicker = webDriver.Search(x => x.WithTid("ClaimEndDatePicker")).DateInput();
            AvailableDaysMessage = webDriver.Search(x => x.WithTid("AvailableDaysMessage")).Label();
            AvailableDaysLabel = webDriver.Search(x => x.WithTid("AvailableDaysLabel")).Label();
            PayNowCheckbox = webDriver.Search(x => x.WithTid("PayNowCheckbox")).Checkbox();
            DirectorFioCombobox = new DirectorFioCombobox(webDriver.Search(x => x.WithTid("DirectorFioCombobox")));
            SendButton = webDriver.Search(x => x.WithTid("SendButton")).Button();
            
            Footer = new PageFooter(webDriver.Search(x => x.WithTid("Footer")));
        }

        public Label UserFioLabel { get; private set; }
        public Select ClaimTypeSelect { get; private set; }
        public Input ChildAgeInput { get; private set; }
        public DatePicker ClaimStartDatePicker { get; private set; }
        public DatePicker ClaimEndDatePicker { get; private set; }
        public Label AvailableDaysMessage { get; private set; }
        public Label AvailableDaysLabel { get; private set; }
        public Checkbox PayNowCheckbox { get; private set; }
        public DirectorFioCombobox DirectorFioCombobox { get; private set; }
        public Button SendButton { get; private set; }
        
        public PageFooter Footer { get; private set; }
    }
}