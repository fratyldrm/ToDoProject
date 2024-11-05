using ToDoProject.Model.ToDos.Enum;

namespace ToDoProject.Model.ToDos.Dtos.Create.Request;

public  record CreateToDoRequestDto(
      string Title,
    string Description,
<<<<<<< HEAD
    Priority Priority,
=======
    Precedence Priority,
>>>>>>> 08dcd5530dbef0ae6c680364eebd803bf00a3088
    int CategoryId,
    Guid UserId,
    DateTime StartDate,
    DateTime EndDate,
    bool Completed

    );
