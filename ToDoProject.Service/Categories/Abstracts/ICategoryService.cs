

using Core.Entities.ReturnModels;
using ToDoProject.Model.Categories.Dtos.Create;
using ToDoProject.Model.Categories.Dtos.GlobalDto;
using ToDoProject.Model.Categories.Dtos.Update;

namespace ToDoProject.Service.Categories.Abstracts;

public interface ICategoryService
{
    Task<ReturnModel<List<CategoryDto>>> GetAllAsync();
    Task<ReturnModel<CategoryTodosDto>> GetCategoryWithToDosAsync(int categoryId);
    Task<ReturnModel<List<CategoryTodosDto>>> GetCategoryWithToDosAsync();
    Task<ReturnModel<CategoryDto>> GetByIdAsync(int id);
    Task<ReturnModel<int>> CreateAsync(CreateCategoryRequestDto request);
    Task<ReturnModel> UpdateAsync(int id, UpdateCategoryRequestDto request);
    Task<ReturnModel> DeleteAsync(int id);
    Task UpdateAsync(int id, UpdateCategoryRequestDto request, string? userId);
}
