
using ToDoProject.Model.ToDos.Enum;
namespace ToDoProject.Model.ToDos.Dtos.Update;
public record UpdateToDoRequestDto(

    string Title,
    string Description,
<<<<<<< HEAD
    Priority Priority,
=======
    Precedence Priority,
>>>>>>> 08dcd5530dbef0ae6c680364eebd803bf00a3088
    bool Completed,
    int CategoryId
);

