
using ToDoProject.Model.ToDos.Dtos.Global;
namespace ToDoProject.Model.Categories.Dtos.GlobalDto;

public record    CategoryTodosDto(int Id,string Name,List<ToDoDto> ToDos);
