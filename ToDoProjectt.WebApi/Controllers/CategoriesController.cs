using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDoProject.Model.Categories.Dtos.Create;
using ToDoProject.Model.Categories.Dtos.Update;
using ToDoProject.Service.Categories.Abstracts;

namespace ToDoProject.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController(ICategoryService categoryService) : ControllerBase
{
    [HttpGet("all")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetCategories()
    {
        var result = await categoryService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<IActionResult> GetCategory([FromRoute] int id)
    {
        var result = await categoryService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpGet("{id:int}/todos")]
    [Authorize]
    public async Task<IActionResult> GetCategoryWithProducts([FromRoute] int id)
    {
        var result = await categoryService.GetCategoryWithToDosAsync(id);
        return Ok(result);
    }

    [HttpGet("todos")]
    [Authorize]
    public async Task<IActionResult> GetCategoryWithProducts()
    {
        var result = await categoryService.GetCategoryWithToDosAsync();
        return Ok(result);
    }

    [HttpPost("add")]
    [Authorize]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequestDto request)
    {
        string userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        var result = await categoryService.CreateAsync(request, userId);
        return Ok(result);
    }

    [HttpPut("update/{id:int}")]
    [Authorize]
    public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] UpdateCategoryRequestDto request)
    {
        string userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        var result = await categoryService.UpdateAsync(id, request, userId);
        return Ok(result);
    }

    [HttpDelete("delete/{id:int}")]
    [Authorize]
    public async Task<IActionResult> DeleteCategory([FromRoute] int id)
    {
        var result = await categoryService.DeleteAsync(id);
        return Ok(result);
    }
}
