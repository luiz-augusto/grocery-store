using System;
using GroceriesStore.Domain.Commands.Results;
using GroceriesStore.Domain.Entities;

namespace GroceriesStore.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Customer Get(Guid id);
        Customer GetByUsername(string username);
        GetCustomerCommandResult Get(string username);
        void Save(Customer customer);
        void Update(Customer customer);
        bool DocumentExists(string document);        
    }
}
