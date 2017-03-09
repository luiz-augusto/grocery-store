using System;
using GroceriesStore.Shared.Commands;

namespace GroceriesStore.Domain.Commands.Inputs
{
    public class DeleteGroceriesCommand : ICommand
    {
        public DeleteGroceriesCommand()
        {
        }

        public DeleteGroceriesCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
