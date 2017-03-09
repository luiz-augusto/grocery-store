using GroceriesStore.Domain.Enums;
using System;
using GroceriesStore.Domain.Entities;

namespace GroceriesStore.Infra.Mappings
{
    public class GroceriesMap
    {
        public GroceriesMap() { }
        public GroceriesMap(Guid id, string name, decimal price, Unity unity, Category category)
        {
            Id = id;
            Name = name;
            Price = price;
            Unity = unity;
            Category = category;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Unity Unity { get; set; }
        public Category Category { get; set; }

        public static implicit operator GroceriesMap(Groceries groceries)
        {
            return new GroceriesMap(groceries.Id, groceries.Name, groceries.Price, groceries.Unity, groceries.Category);
        }
    }
}
