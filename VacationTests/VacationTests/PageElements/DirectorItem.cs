using Kontur.Selone.Extensions;
using OpenQA.Selenium;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;

namespace VacationTests.PageElements
{
    public class DirectorItem : ControlBase, IClickable
    {
        public DirectorItem(ISearchContext searchContext, By by) : base(searchContext, by)
        {
            IdLabel = container.Search(x => x.WithTid("IdLabel")).Label();
            FioLabel = container.Search(x => x.WithTid("FioLabel")).Label();
            PositionLabel = container.Search(x => x.WithTid("PositionLabel")).Label();
        }

        public Label IdLabel { get; private set; }
        public Label FioLabel { get; private set; }
        public Label PositionLabel { get; private set; }
    }
}