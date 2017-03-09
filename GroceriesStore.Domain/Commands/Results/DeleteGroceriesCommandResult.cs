using System;
using GroceriesStore.Shared.Commands;

namespace GroceriesStore.Domain.Commands.Results
{
    public class DeleteGroceriesCommandResult : ICommandResult
    {
        public DeleteGroceriesCommandResult(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}