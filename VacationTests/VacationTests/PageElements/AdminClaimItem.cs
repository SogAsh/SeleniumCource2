using Kontur.Selone.Selectors.Context;
using VacationTests.Infrastructure.PageElements;

namespace VacationTests.PageElements
{
    public class AdminClaimItem : ControlBase
    {
        public AdminClaimItem(IContextBy contextBy) : base(contextBy.SearchContext, contextBy.By)
        {
            // TitleLink = container.Search(x => x.WithTid("TitleLink")).Link();
            // PeriodLabel = container.Search(x => x.WithTid("PeriodLabel")).Label();
            // StatusLabel = container.Search(x => x.WithTid("StatusLabel")).Label();
        }

        // при обращении из теста к любому элементу списка отпусков будут доступны три свойства
        public Link TitleLink { get; private set; }
        public Link  UserFioLabel { get; private set; }
        public Label PeriodLabel { get; private set; }
        public Label StatusLabel { get; private set; }
        public Button AcceptButton { get; private set; }
        public Button RejectButton { get; private set; }
    }
}