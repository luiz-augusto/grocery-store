using GroceriesStore.Shared.Commands;
using GroceriesStore.Domain.Enums;

namespace GroceriesStore.Domain.Commands.Inputs
{
    public class RegisterGroceriesCommand : ICommand
    {
        public RegisterGroceriesCommand()
        {
        }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Unity Unity { get; set; }
        public Category Category { get; set; }
    }
}
