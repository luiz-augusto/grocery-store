using GroceriesStore.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceriesStore.Shared.Domain.Entities
{
    public class Category: Entity
    {
        public Category() { }
        public Category(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
