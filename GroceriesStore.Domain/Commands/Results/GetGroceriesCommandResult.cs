using System;
using GroceriesStore.Shared.Commands;
using GroceriesStore.Domain.Enums;

namespace GroceriesStore.Domain.Commands.Results
{
    public class GetGroceriesCommandResult : ICommandResult
    {
        public GetGroceriesCommandResult(Guid id, string name, decimal price, Unity unity, Category category)
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
    }
}
