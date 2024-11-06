using AutoMapper;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ToDoProject.Model.ToDos.Dtos.Create.Request;
using ToDoProject.Model.ToDos.Dtos.Create.Response;
using ToDoProject.Model.ToDos.Dtos.Global;
using ToDoProject.Model.ToDos.Dtos.Update;
using ToDoProject.Model.ToDos.Entity;
using ToDoProject.Repository.ToDos.Abstracts;
using ToDoProject.Repository.UnitOfWorks.Abstracts;
using ToDoProject.Service.ToDoS.Concretes;
using ToDoProject.Service.ToDoS.Rules;
using Core.Entities.ReturnModels;

namespace TodoTest
{
    public class ToDoServiceTest
    {
        private ToDoService _toDoService;
        private Mock<IToDoRepository> _mockRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<IMapper> _mockMapper;
        private Mock<ToDoBusinessRules> _mockBusinessRules;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IToDoRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _mockBusinessRules = new Mock<ToDoBusinessRules>(_mockRepository.Object);

            _toDoService = new ToDoService(_mockRepository.Object, _mockUnitOfWork.Object, _mockMapper.Object, _mockBusinessRules.Object);
        }

        [Test]
        public async Task ToDoService_WhenToDoCreated_ReturnSuccess()
        {
            // Arrange
            var createRequest = new CreateToDoRequestDto
            {
                Title = "Test Task",
                Description = "Description"
            };

            var toDo = new ToDo
            {
                Title = createRequest.Title,
                Description = createRequest.Description
            };

            var createResponse = new CreateToDoResponseDto
            {
                Id = Guid.NewGuid(),
                Title = createRequest.Title,
                Description = createRequest.Description
            };

            _mockRepository.Setup(x => x.Where(It.IsAny<Func<ToDo, bool>>())).Returns(Task.FromResult(false));
            _mockMapper.Setup(x => x.Map<ToDo>(createRequest)).Returns(toDo);
            _mockRepository.Setup(x => x.AddAsync(toDo)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask);
            _mockMapper.Setup(x => x.Map<CreateToDoResponseDto>(toDo)).Returns(createResponse);

            // Act
            var result = await _toDoService.CreateAsync(createRequest);

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
            Assert.IsTrue(result.Success);
            Assert.AreEqual(createResponse, result.Data);
        }

        [Test]
        public async Task ToDoService_WhenToDoDeleted_ReturnSuccess()
        {
            // Arrange
            var toDoId = Guid.NewGuid();
            var toDo = new ToDo
            {
                Id = toDoId,
                Title = "Test Task"
            };

            _mockRepository.Setup(x => x.GetByIdAsync(toDoId)).ReturnsAsync(toDo);
            _mockRepository.Setup(x => x.Delete(toDo));
            _mockUnitOfWork.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _toDoService.DeleteAsync(toDoId);

            // Assert
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
            Assert.IsTrue(result.Success);
        }

        [Test]
        public async Task ToDoService_WhenToDoUpdated_ReturnSuccess()
        {
            // Arrange
            var toDoId = Guid.NewGuid();
            var updateRequest = new UpdateToDoRequestDto
            {
                Title = "Updated Title"
            };

            var toDo = new ToDo
            {
                Id = toDoId,
                Title = "Old Title",
                Description = "Description"
            };

            _mockRepository.Setup(x => x.GetByIdAsync(toDoId)).ReturnsAsync(toDo);
            _mockMapper.Setup(x => x.Map(updateRequest, toDo)).Returns(toDo);
            _mockRepository.Setup(x => x.Update(toDo));
            _mockUnitOfWork.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _toDoService.UpdateAsync(toDoId, updateRequest);

            // Assert
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
            Assert.IsTrue(result.Success);
        }

        [Test]
        public async Task ToDoService_WhenToDoRetrieved_ReturnToDoDto()
        {
            // Arrange
            var toDoId = Guid.NewGuid();
            var toDo = new ToDo
            {
                Id = toDoId,
                Title = "Test Task"
            };

            var toDoDto = new ToDoDto
            {
                Id = toDoId,
                Title = toDo.Title
            };

            _mockRepository.Setup(x => x.GetByIdAsync(toDoId)).ReturnsAsync(toDo);
            _mockMapper.Setup(x => x.Map<ToDoDto>(toDo)).Returns(toDoDto);

            // Act
            var result = await _toDoService.GetByIdAsync(toDoId);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(toDoDto, result.Data);
        }
    }
}
