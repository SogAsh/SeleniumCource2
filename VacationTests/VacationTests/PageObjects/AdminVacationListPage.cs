using Kontur.Selone.Extensions;
using OpenQA.Selenium;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageElements;

namespace VacationTests.PageObjects
{
    public class AdminVacationListPage : PageBase
    {
        public AdminVacationListPage(IWebDriver webDriver) : base(webDriver)
        {
            TitleLabel = webDriver.Search(x => x.WithTid("TitleLabel")).Label();
            ClaimsTab = webDriver.Search(x => x.WithTid("ClaimsTab")).Link();
            DownloadButton = webDriver.Search(x => x.WithTid("DownloadButton")).Button();
            Footer = new PageFooter(webDriver.Search(x => x.WithTid("Footer")));
        }

        public Label TitleLabel { get; private set; }
        public Link ClaimsTab { get; private set; }
        public Button DownloadButton { get; private set; }
        public PageFooter Footer { get; private set; }
    }
}