
using Core.Entities.ReturnModels;
using Core.Exceptions;
using ToDoProject.Model.ToDos.Entity;
using ToDoProject.Repository.ToDos.Abstracts;
using ToDoProject.Service.Constants;

namespace ToDoProject.Service.ToDoS.Rules;

public class ToDoBusinessRules(IToDoRepository toDoRepository)
{
    public  void ToDoExists(ToDo? toDo)
    {
        if (toDo is null)
        {
            throw new NotFoundException(Messages.ToDoNotFoundMessage);
           
        }
    
    }

    public  void ToDoTitleDoesNotExist(bool titleExists)
    {
        if (titleExists)
        {
            throw new BusinessException(Messages.ToDoTitleExistMessage);
        
        }
     
    }
}
