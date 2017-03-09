using GroceriesStore.Shared.Commands;

namespace GroceriesStore.Domain.Commands.Inputs
{
    public class SearchGroceriesCommand : ICommand
    {
        public SearchGroceriesCommand()
        {
        }

        public SearchGroceriesCommand(string term, int page, int pageSize)
        {
            Term = term;
            Page = page;
            PageSize = pageSize;
        }
        public string Term { get; private set; }
        public int Page { get; private set; }
        public int PageSize { get; private set; }
    }
}
