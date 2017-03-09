using System;
using GroceriesStore.Shared.Commands;
using GroceriesStore.Domain.Entities;
using System.Collections.Generic;
using GroceriesStore.Domain.Enums;

namespace GroceriesStore.Domain.Commands.Results
{
    public class SearchGroceriesCommandResult : ICommandResult
    {
        public SearchGroceriesCommandResult(List<Groceries> groceries, int totalItems)
        {
            this.TotalItems = totalItems;
            GroceriesList = groceries.ConvertAll(x => new GroceriesResult(x.Id, x.Name, x.Price, x.Unity, x.Category));
        }

        public IEnumerable<GroceriesResult> GroceriesList { get; private set; }
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public int TotalItems { get; private set; }
    }

    public class GroceriesResult
    {
        public GroceriesResult(Guid id, string name, decimal price, Unity unity, Category category)
        {
            Id = id;
            Name = name;
            Price = price;
            Unity = Enum.GetName(typeof(Unity), unity);
            Category = Enum.GetName(typeof(Category), category);
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public string Unity { get; private set; }
        public string Category { get; private set; }
    }
}