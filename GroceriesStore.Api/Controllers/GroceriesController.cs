using GroceriesStore.Domain.Commands.Handlers;
using GroceriesStore.Domain.Commands.Inputs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GroceriesStore.Api.Controllers
{
    public class GroceriesController : BaseController
    {
        private readonly RegisterGroceriesCommandHandler _registerGroceriesCommandHandler;
        private readonly UpdateGroceriesCommandHandler _updateGroceriesCommandHandler;
        private readonly DeleteGroceriesCommandHandler _deleteGroceriesCommandHandler;
        private readonly SearchGroceriesCommandHandler _searchGroceriesCommandHandler;
        private readonly GetGroceriesCommandHandler _getGroceriesCommandHandler;
        private readonly UpdateGroceriesPositionCommandHandler _updateGroceriesPositionCommandHandler;

        public GroceriesController(RegisterGroceriesCommandHandler registerGroceriesCommandHandler,
            UpdateGroceriesCommandHandler updateGroceriesCommandHandler,
            DeleteGroceriesCommandHandler deleteGroceriesCommandHandler,
            SearchGroceriesCommandHandler searchGroceriesCommandHandler, 
            GetGroceriesCommandHandler getGroceriesCommandHandler,
            UpdateGroceriesPositionCommandHandler updateGroceriesPositionCommandHandler)
        {
            _registerGroceriesCommandHandler = registerGroceriesCommandHandler;
            _updateGroceriesCommandHandler = updateGroceriesCommandHandler;
            _deleteGroceriesCommandHandler = deleteGroceriesCommandHandler;
            _searchGroceriesCommandHandler = searchGroceriesCommandHandler;
            _getGroceriesCommandHandler = getGroceriesCommandHandler;
            _updateGroceriesPositionCommandHandler = updateGroceriesPositionCommandHandler;
        }

        [HttpGet]
        [Route("v1/groceries/")]
        public async Task<IActionResult> Get()
        {
            var result = _searchGroceriesCommandHandler.Handle(new SearchGroceriesCommand(null, 0, 0));
            return await Response(result, _searchGroceriesCommandHandler.Notifications);
        }

        [HttpGet]
        [Route("v1/groceries/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = _getGroceriesCommandHandler.Handle(new GetGroceriesCommand(id));
            if (result != null)
                return await Response(result, _getGroceriesCommandHandler.Notifications);
            else
                return NotFound(_getGroceriesCommandHandler.Notifications);
        }

        [HttpPost]
        [Route("v1/groceries/")]
        public async Task<IActionResult> Post([FromBody] RegisterGroceriesCommand command)
        {
            var result = _registerGroceriesCommandHandler.Handle(command);
            return await Response(result, _registerGroceriesCommandHandler.Notifications);
        }

        [HttpPut]
        [Route("v1/groceries/{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateGroceriesCommand updateGroceriesCommand)
        {
            var result = _updateGroceriesCommandHandler.Handle(updateGroceriesCommand);
            return await Response(result, _updateGroceriesCommandHandler.Notifications);
        }

        [HttpPut]
        [Route("v1/groceries/position/{id}")]
        public async Task<IActionResult> UpdatePosition(Guid id, [FromBody] UpdateGroceriesPositionCommand updateGroceriesPositionCommand)
        {
            var result = _updateGroceriesPositionCommandHandler.Handle(updateGroceriesPositionCommand);
            return await Response(result, _updateGroceriesPositionCommandHandler.Notifications);
        }

        [HttpDelete]
        [Route("v1/groceries/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = _deleteGroceriesCommandHandler.Handle(new DeleteGroceriesCommand(id));
            return await Response(result, _deleteGroceriesCommandHandler.Notifications);
        }
    }
}
