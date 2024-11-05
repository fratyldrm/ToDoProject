﻿

using Core.Repository.Abstracts;
using ToDoProject.Model.ToDos.Entity;

namespace ToDoProject.Repository.ToDos.Abstracts;

public interface IToDoRepository : IRepository<ToDo,Guid>
{
}