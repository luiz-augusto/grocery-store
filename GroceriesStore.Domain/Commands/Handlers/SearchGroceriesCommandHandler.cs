using FluentValidator;
using GroceriesStore.Domain.Commands.Inputs;
using GroceriesStore.Domain.Commands.Results;
using GroceriesStore.Domain.Repositories;
using GroceriesStore.Shared.Commands;

namespace GroceriesStore.Domain.Commands.Handlers
{
    public class 
        SearchGroceriesCommandHandler : Notifiable,
        ICommandHandler<SearchGroceriesCommand>
    {
        private readonly IGroceriesRepository _groceriesRepository;

        public SearchGroceriesCommandHandler(IGroceriesRepository groceriesRepository)
        {
            _groceriesRepository = groceriesRepository;
        }

        public ICommandResult Handle(SearchGroceriesCommand command)
        {
            var groceries = _groceriesRepository.GetAll();

            //pagination removed
            //if (!string.IsNullOrEmpty(command.Term))
            //    groceries = groceries.Where(x => x.Name.ToUpper().Contains(command.Term.ToUpper())).ToList();
            //groceries = groceries.Skip((command.Page - 1) * command.PageSize).Take(command.PageSize).ToList();

            if (groceries == null)
            {
                AddNotification(new Notification("Groceries", "not found!"));
                return null;
            }

            int totalItems = groceries.Count;

            return new SearchGroceriesCommandResult(groceries, totalItems);
        }        
    }
}
