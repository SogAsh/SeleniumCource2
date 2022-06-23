using Kontur.Selone.Extensions;
using Kontur.Selone.Selectors.Context;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;

namespace VacationTests.PageElements
{
    // класс элемента списка отпусков EmployeeClaimList
    // наследуем от ControlBase, поскольку это тоже контрол и могут понадобиться базовые методы и пропсы
    public class EmployeeClaimItem : ControlBase
    {
        public EmployeeClaimItem(IContextBy contextBy) : base(contextBy.SearchContext, contextBy.By)
        {
            // TitleLink = container.Search(x => x.WithTid("TitleLink")).Link();
            // PeriodLabel = container.Search(x => x.WithTid("PeriodLabel")).Label();
            // StatusLabel = container.Search(x => x.WithTid("StatusLabel")).Label();
        }

        // при обращении из теста к любому элементу списка отпусков будут доступны три свойства
        public Link TitleLink { get; private set; }
        public Label PeriodLabel { get; private set; }
        public Label StatusLabel { get; private set; }

        // можно вот так реализовать метод для наведения мыши на конкретный элемент
        // такой метод может понадобиться, если только при наведении на элемент списка показывается какой-то контрол
        public void MouseOver()
        {
            container.Mouseover();
        }
    }
}