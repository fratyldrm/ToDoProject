using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoProject.Model.ToDos.Dtos.Create.Request;
using ToDoProject.Model.ToDos.Dtos.Update;
using ToDoProject.Service.ToDoS.Abstracts;
using Core.Entities.ReturnModels;

namespace ToDoProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ToDosController(IToDoService toDoService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await toDoService.GetAllListAsync();
            if (result == null)
            {
                return NotFound(ReturnModel<string>.Failure("No To-Do items found."));
            }
            return Ok(ReturnModel.Success(result));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await toDoService.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound(ReturnModel<string>.Failure("To-Do item not found."));
            }
            return Ok(ReturnModel.Success(result));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateToDoRequestDto request)
        {
            var result = await toDoService.CreateAsync(request);
            if (!result.IsSuccess)
            {
                return BadRequest(ReturnModel<string>.Failure("Failed to create To-Do item."));
            }
            return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, ReturnModel.Success(result.Data));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateToDoRequestDto request)
        {
            var result = await toDoService.UpdateAsync(id, request);
            if (!result.IsSuccess)
            {
                return BadRequest(ReturnModel<string>.Failure("Failed to update To-Do item."));
            }
            return Ok(ReturnModel.Success("To-Do item updated successfully."));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await toDoService.DeleteAsync(id);
            if (!result.IsSuccess)
            {
                return BadRequest(ReturnModel<string>.Failure("Failed to delete To-Do item."));
            }
            return Ok(ReturnModel.Success("To-Do item deleted successfully."));
        }
    }
}
