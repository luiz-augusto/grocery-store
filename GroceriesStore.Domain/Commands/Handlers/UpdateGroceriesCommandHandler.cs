using FluentValidator;
using GroceriesStore.Domain.Commands.Inputs;
using GroceriesStore.Domain.Commands.Results;
using GroceriesStore.Domain.Repositories;
using GroceriesStore.Shared.Commands;

namespace GroceriesStore.Domain.Commands.Handlers
{
    public class 
        UpdateGroceriesCommandHandler : Notifiable,
        ICommandHandler<UpdateGroceriesCommand>
    {
        private readonly IGroceriesRepository _groceriesRepository;

        public UpdateGroceriesCommandHandler(IGroceriesRepository groceriesRepository)
        {
            _groceriesRepository = groceriesRepository;
        }

        public ICommandResult Handle(UpdateGroceriesCommand command)
        {
            var groceries = _groceriesRepository.GetById(command.Id);
            if (groceries == null)
            {
                AddNotification(new Notification("Id", "not found!"));
                return null;
            }

            groceries.Update(command.Name, command.Price, command.Unity, command.Category);

            AddNotifications(groceries.Notifications);   

            if (IsValid())
                _groceriesRepository.Update(groceries);
            
            return new UpdateGroceriesCommandResult(groceries.Id, groceries.Name,
                groceries.Price, groceries.Unity, groceries.Category);
        }        
    }
}
