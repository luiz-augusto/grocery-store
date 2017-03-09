using GroceriesStore.Domain.Entities;

namespace GroceriesStore.Domain.Repositories
{
    public interface IOrderRepository
    {
        void Save(Order order);
    }
}
