using Kontur.Selone.Extensions;
using Kontur.Selone.Selectors.Context;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;

namespace VacationTests.PageElements
{
    public class PageFooter : ControlBase
    {
        public PageFooter(IContextBy contextBy) : base(contextBy.SearchContext, contextBy.By)
        {
            KnowEnvironmentLink = container.Search(x => x.WithTid("KnowEnvironmentLink")).Link();
            OurFooterLink = container.Search(x => x.WithTid("OurFooterLink")).Link();
        }

        public Link KnowEnvironmentLink { get; private set; }
        public Link OurFooterLink { get; private set; }
    }
}