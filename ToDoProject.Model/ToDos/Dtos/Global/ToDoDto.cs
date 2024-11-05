
using ToDoProject.Model.ToDos.Enum;
namespace ToDoProject.Model.ToDos.Dtos.Global;

public record ToDoDto(
    Guid Id,
    string Title,
    string Description,
    DateTime StartDate,
    DateTime EndDate,
    DateTime CreatedDate,
<<<<<<< HEAD
    Priority Priority,
=======
    Precedence Priority,
>>>>>>> 08dcd5530dbef0ae6c680364eebd803bf00a3088
    bool Completed,
    int CategoryId
);
