using Kontur.Selone.Extensions;
using Kontur.Selone.Selectors.Context;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;

namespace VacationTests.PageElements
{
    public class ClaimLightboxFooter : ControlBase
    {
        public ClaimLightboxFooter(IContextBy contextBy) : base(contextBy.SearchContext, contextBy.By)
        {
            AcceptButton = container.Search(x => x.WithTid("AcceptButton")).Button();
            RejectButton = container.Search(x => x.WithTid("RejectButton")).Button();
        }

        public Button AcceptButton { get; private set; }
        public Button RejectButton { get; private set; }
    }
}