﻿using Kontur.Selone.Elements;
using Kontur.Selone.Selectors.Context;
using Kontur.Selone.Selectors.Css;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;

namespace VacationTests.PageElements
{
    public class AdminClaimList : ControlBase
    {
        public AdminClaimList(IContextBy contextBy) : base(contextBy.SearchContext, contextBy.By)
        {
            // EmployeeClaimItem - тип каждого элемента, который также создаем ниже
            // WithTid("claimItem") - одинаковый селектор для каждого элемента 
            // FixedByAttribute - используется для указания уникального атрибута, чтобы элемент сделать уникальным
            // это позволяет, например, проверить невидимость этого элемента или его порядок в списке
            // FixedByIndex - используется, когда нет уникального признака, индексируются элементы по-порядку
            Items = new ElementsCollection<AdminClaimItem>(container,
                x => x.WithTid("ClaimItem").FixedByIndex(),
                (s, b, e) => new AdminClaimItem(new ContextBy(s, b)));
        }

        // сам список, к которому из тестов надо обращаться
        // ElementsCollection<T> специальный класс Selone для коллекции элементов
        // имеет интерфейсы : IElementsCollection<T>, IEnumerable<T>, IEnumerable
        // имеет свойство Count для получения количества элементов в коллекции
        public ElementsCollection<AdminClaimItem> Items { get; private set; }
    }
}