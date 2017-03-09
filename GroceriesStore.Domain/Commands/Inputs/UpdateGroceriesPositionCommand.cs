using System;
using GroceriesStore.Shared.Commands;

namespace GroceriesStore.Domain.Commands.Inputs
{
    public class UpdateGroceriesPositionCommand : ICommand
    {
        public UpdateGroceriesPositionCommand(Guid id, int position)
        {
            this.Id = id;
            this.Position = position;
        }
        public Guid Id { get; private set; }
        public int Position { get; private set; }
    }
}
