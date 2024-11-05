

using Core.Repository.Concretes;
using ToDoProject.Model.ToDos.Entity;
using ToDoProject.Repository.Contexts;
using ToDoProject.Repository.ToDos.Abstracts;

namespace ToDoProject.Repository.ToDos.Concretes;

public class ToDoRepository(BaseDbContext context) : EfRepository<BaseDbContext,ToDo,Guid>(context) , IToDoRepository
{
}
