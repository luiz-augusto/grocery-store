using FluentValidator;
using GroceriesStore.Domain.Commands.Inputs;
using GroceriesStore.Domain.Commands.Results;
using GroceriesStore.Domain.Repositories;
using GroceriesStore.Shared.Commands;
using System.Linq;

namespace GroceriesStore.Domain.Commands.Handlers
{
    public class
        UpdateGroceriesPositionCommandHandler : Notifiable,
        ICommandHandler<UpdateGroceriesPositionCommand>
    {
        private readonly IGroceriesRepository _groceriesRepository;

        public UpdateGroceriesPositionCommandHandler(IGroceriesRepository groceriesRepository)
        {
            _groceriesRepository = groceriesRepository;
        }

        public ICommandResult Handle(UpdateGroceriesPositionCommand command)
        {
            var groceries = _groceriesRepository.GetAll();
            var grocery = groceries.FirstOrDefault(x => x.Id == command.Id);
            if (grocery == null)
            {
                AddNotification(new Notification("Id", "not found!"));
                return null;
            }

            var oldIndex = groceries.IndexOf(grocery);
            var newIndex = command.Position;

            groceries.RemoveAt(oldIndex);

            if (newIndex > oldIndex) newIndex--;
            // the actual index could have shifted due to the removal
            groceries.Insert(newIndex, grocery);

            AddNotifications(grocery.Notifications);   

            if (IsValid())
                _groceriesRepository.Update(groceries);
            
            return new UpdateGroceriesPositionCommandResult(grocery.Id);
        }        
    }
}
