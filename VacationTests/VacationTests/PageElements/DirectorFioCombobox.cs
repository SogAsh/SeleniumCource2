using Kontur.Selone.Elements;
using Kontur.Selone.Extensions;
using Kontur.Selone.Selectors.Context;
using Kontur.Selone.Selectors.Css;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;

namespace VacationTests.PageElements
{
    public class DirectorFioCombobox : Combobox
    {
        public DirectorFioCombobox(IContextBy contextBy) : base(contextBy.SearchContext, contextBy.By)
        {
            MenuItems = new ElementsCollection<DirectorItem>(container.Root(),
                x => x.WithTid("ComboBoxMenu__item").FixedByIndex(),
                (s, b, e) => new DirectorItem(s, b));
        }

        public new ElementsCollection<DirectorItem> MenuItems { get; private set; }
    }
}