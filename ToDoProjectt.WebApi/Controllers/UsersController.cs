using Core.Entities.ReturnModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoProject.Model.Users.Dtos.Request;
using ToDoProject.Service.Users.Abstracts;

namespace ToDoProject.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpGet("getbyemail")]
    public async Task<IActionResult> GetByEmail([FromQuery] string email)
    {
        var result = await userService.GetByEmailAsync(email);
        if (result == null)
        {
            return NotFound(ReturnModel<string>.Failure("User not found."));
        }
        return Ok(ReturnModel<string>.Success(result));
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromQuery] string id)
    {
        var result = await userService.DeleteAsync(id);
        if (!result.IsSuccess)
        {
            return BadRequest(ReturnModel<string>.Failure("Failed to delete user."));
        }
        return Ok(ReturnModel<string>.Success("User deleted successfully."));
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update([FromQuery] string id, [FromBody] UpdateRequestDto request)
    {
        var result = await userService.UpdateAsync(id, request);
        if (!result.IsSuccess)
        {
            return BadRequest(ReturnModel<string>.Failure("Failed to update user."));
        }
        return Ok(ReturnModel<string>.Success("User updated successfully."));
    }

    [HttpPut("changepassword")]
    public async Task<IActionResult> ChangePassword([FromQuery] string id, [FromBody] ChangePasswordRequestDto request)
    {
        var result = await userService.ChangePasswordAsync(id, request);
        if (!result.IsSuccess)
        {
            return BadRequest(ReturnModel<string>.Failure("Failed to change password."));
        }
        return Ok(ReturnModel<string>.Success("Password changed successfully."));
    }
}
