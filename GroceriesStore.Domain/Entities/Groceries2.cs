using FluentValidator;
using GroceriesStore.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GroceriesStore.Domain.Entities
{
    public class GroceriesList : Entity
    {
        protected GroceriesList() { }

        private readonly IList<Groceries> _items;

        public ICollection<GroceriesItem> Items => _items.ToArray();
    }
}
