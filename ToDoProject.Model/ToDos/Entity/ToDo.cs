

using Core.Entities;
using ToDoProject.Model.Categories.Entity;
using ToDoProject.Model.ToDos.Enum;
using ToDoProject.Model.Users.Entity;

namespace ToDoProject.Model.ToDos.Entity;

public sealed class ToDo : BaseEntity<Guid>, IInspectionEntity
{

    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
<<<<<<< HEAD
    public Priority Priority { get; set; }
=======
    public Precedence Priority { get; set; }
>>>>>>> 08dcd5530dbef0ae6c680364eebd803bf00a3088
    public bool Completed { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }

    public string UserId { get; set; }
    public User User { get; set; }
}