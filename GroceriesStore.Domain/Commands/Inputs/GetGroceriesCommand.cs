using GroceriesStore.Shared.Commands;
using System;

namespace GroceriesStore.Domain.Commands.Inputs
{
    public class GetGroceriesCommand : ICommand
    {
        public GetGroceriesCommand(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; private set; }
    }
}
