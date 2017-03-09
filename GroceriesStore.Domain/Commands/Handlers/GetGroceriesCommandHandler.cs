using FluentValidator;
using GroceriesStore.Domain.Commands.Inputs;
using GroceriesStore.Domain.Commands.Results;
using GroceriesStore.Domain.Repositories;
using GroceriesStore.Shared.Commands;

namespace GroceriesStore.Domain.Commands.Handlers
{
    public class
        GetGroceriesCommandHandler : Notifiable,
        ICommandHandler<GetGroceriesCommand>
    {
        private readonly IGroceriesRepository _groceriesRepository;

        public GetGroceriesCommandHandler(IGroceriesRepository groceriesRepository)
        {
            _groceriesRepository = groceriesRepository;
        }

        public ICommandResult Handle(GetGroceriesCommand command)
        {
            var groceries = _groceriesRepository.GetById(command.Id);
            
            if (groceries == null)
            {
                AddNotification(new Notification("Groceries", "not found!"));
                return null;
            }
            
            return new GetGroceriesCommandResult(groceries.Id, groceries.Name, groceries.Price, groceries.Unity, groceries.Category);
        }        
    }
}
