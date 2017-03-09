using FluentValidator;
using GroceriesStore.Domain.Enums;
using GroceriesStore.Shared.Entities;
using System;

namespace GroceriesStore.Domain.Entities
{
    public class Groceries : Entity
    {
        public Groceries() { }

        public Groceries(string name, decimal price, Unity unity, Category category)
        {
            SetValues(name, price, unity, category);
        }

        public Groceries(Guid id, string name, decimal price, Unity unity, Category category) : base(id)
        {
            SetValues(name, price, unity, category);
        }

        private void SetValues(string name, decimal price, Unity unity, Category category)
        {
            Name = name;
            Price = price;
            Unity = unity;
            Category = category;

            new ValidationContract<Groceries>(this)
                .IsRequired(x => x.Name, "Name is mandatory")
                .HasMaxLenght(x => x.Name, 60)
                .HasMinLenght(x => x.Name, 3)
                .IsGreaterThan(x => x.Price, 0, "Price should be greater than 0");
        }

        public void Update(string name, decimal price, Unity unity, Category category)
        {
            
            SetValues(name, price, unity, category);
        }

        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public Unity Unity { get; private set; }
        public Category Category { get; private set; }
    }
}
