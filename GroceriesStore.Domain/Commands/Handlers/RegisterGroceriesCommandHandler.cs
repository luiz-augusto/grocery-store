using FluentValidator;
using GroceriesStore.Domain.Commands.Inputs;
using GroceriesStore.Domain.Commands.Results;
using GroceriesStore.Domain.Entities;
using GroceriesStore.Domain.Repositories;
using GroceriesStore.Shared.Commands;

namespace GroceriesStore.Domain.Commands.Handlers
{
    public class 
        RegisterGroceriesCommandHandler : Notifiable,
        ICommandHandler<RegisterGroceriesCommand>
    {
        private readonly IGroceriesRepository _groceriesRepository;

        public RegisterGroceriesCommandHandler(IGroceriesRepository groceriesRepository)
        {
            _groceriesRepository = groceriesRepository;
        }

        public ICommandResult Handle(RegisterGroceriesCommand command)
        {
            var groceries = new Groceries(command.Name, command.Price, command.Unity, command.Category);

            AddNotifications(groceries.Notifications);   

            if (IsValid())
                _groceriesRepository.Insert(groceries);
            
            return new RegisterGroceriesCommandResult(groceries.Id, groceries.Name, 
                groceries.Price, groceries.Unity, groceries.Category);
        }        
    }
}
