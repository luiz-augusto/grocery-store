using FluentValidator;
using GroceriesStore.Shared.Entities;

namespace GroceriesStore.Domain.Entities
{
    public class OrderItem : Entity
    {
        protected OrderItem() { }

        public OrderItem(Groceries product, int quantity)
        {
            Product = product;
            Quantity = quantity;
            Price = Product.Price;
        }

        public Groceries Product { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }

        public decimal Total() => Price * Quantity;
    }
}
