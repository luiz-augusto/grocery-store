using System;
using System.Collections.Generic;
using GroceriesStore.Domain.Entities;

namespace GroceriesStore.Domain.Repositories
{
    public interface IGroceriesRepository
    {
        List<Groceries> GetAll();
        Groceries GetById(Guid id);
        void Insert(Groceries entity);
        void Update(Groceries entity);
        void Update(List<Groceries> entity);
        void Delete(Guid id);
    }
}
