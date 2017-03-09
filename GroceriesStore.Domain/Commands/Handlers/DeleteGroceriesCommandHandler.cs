using FluentValidator;
using GroceriesStore.Domain.Commands.Inputs;
using GroceriesStore.Domain.Commands.Results;
using GroceriesStore.Domain.Repositories;
using GroceriesStore.Shared.Commands;

namespace GroceriesStore.Domain.Commands.Handlers
{
    public class 
        DeleteGroceriesCommandHandler : Notifiable,
        ICommandHandler<DeleteGroceriesCommand>
    {
        private readonly IGroceriesRepository _groceriesRepository;

        public DeleteGroceriesCommandHandler(IGroceriesRepository groceriesRepository)
        {
            _groceriesRepository = groceriesRepository;
        }

        public ICommandResult Handle(DeleteGroceriesCommand command)
        {
            var groceries = _groceriesRepository.GetById(command.Id);
            if (groceries == null)
            {
                AddNotification(new Notification("Id", "not found!"));
                return null;
            } 

            if (IsValid())
                _groceriesRepository.Delete(command.Id);
            
            return new DeleteGroceriesCommandResult(groceries.Id, groceries.Name);
        }        
    }
}
