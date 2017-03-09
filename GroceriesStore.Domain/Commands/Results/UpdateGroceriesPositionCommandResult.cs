using System;
using GroceriesStore.Shared.Commands;

namespace GroceriesStore.Domain.Commands.Results
{
    public class UpdateGroceriesPositionCommandResult : ICommandResult
    {
        public UpdateGroceriesPositionCommandResult(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
