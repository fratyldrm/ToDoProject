using ToDoProject.Model.ToDos.Dtos.Create.Request;
using ToDoProject.Model.ToDos.Enum;

namespace ToDoProject.Model.ToDos.Dtos.Create.Response;

public record CreateToDoResponseDto(
    Guid Id,
    string Title,
    string Description,
<<<<<<< HEAD
    Priority Priority,
=======
    Precedence Priority,
>>>>>>> 08dcd5530dbef0ae6c680364eebd803bf00a3088
    DateTime StartDate,
    DateTime EndDate,
    bool Completed,
    string CategoryName,
    string UserFirstName
);
