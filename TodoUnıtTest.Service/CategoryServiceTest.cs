using AutoMapper;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ToDoProject.Model.Categories.Dtos.Create;
using ToDoProject.Model.Categories.Dtos.GlobalDto;
using ToDoProject.Model.Categories.Dtos.Update;
using ToDoProject.Model.Categories.Entity;
using ToDoProject.Repository.Categories.Abstracts;
using ToDoProject.Repository.UnitOfWorks.Abstracts;
using ToDoProject.Service.Categories.Concretes;
using ToDoProject.Service.Categories.Rules;
using Core.Entities.ReturnModels;

namespace TodoUnitTest.Service
{
    public class CategoryServiceTest
    {
        private CategoryService _categoryService;
        private Mock<ICategoryRepository> _mockRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<IMapper> _mockMapper;
        private Mock<CategoryBusinessRules> _mockBusinessRules;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<ICategoryRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _mockBusinessRules = new Mock<CategoryBusinessRules>(_mockRepository.Object);

            _categoryService = new CategoryService(_mockRepository.Object, _mockUnitOfWork.Object, _mockMapper.Object, _mockBusinessRules.Object);
        }

        [Test]
        public async Task CategoryService_WhenCategoryCreated_ReturnSuccess()
        {
            // Arrange
            var createRequest = new CreateCategoryRequestDto { Name = "Test Category" };
            var category = new Category { Name = createRequest.Name, Id = 1 };

            _mockRepository.Setup(x => x.Where(It.IsAny<Func<Category, bool>>())).ReturnsAsync(false);
            _mockMapper.Setup(x => x.Map<Category>(createRequest)).Returns(category);
            _mockRepository.Setup(x => x.AddAsync(category)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _categoryService.CreateAsync(createRequest);

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
            Assert.IsTrue(result.Success);
            Assert.AreEqual(category.Id, result.Data);
        }

        [Test]
        public async Task CategoryService_WhenCategoryDeleted_ReturnSuccess()
        {
            // Arrange
            int categoryId = 1;
            var category = new Category { Id = categoryId, Name = "Test Category" };

            _mockRepository.Setup(x => x.GetByIdAsync(categoryId)).ReturnsAsync(category);
            _mockRepository.Setup(x => x.Delete(category));
            _mockUnitOfWork.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _categoryService.DeleteAsync(categoryId);

            // Assert
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
            Assert.IsTrue(result.Success);
        }

        [Test]
        public async Task CategoryService_WhenCategoryUpdated_ReturnSuccess()
        {
            // Arrange
            int categoryId = 1;
            var updateRequest = new UpdateCategoryRequestDto { Name = "Updated Category" };
            var category = new Category { Id = categoryId, Name = "Old Category" };

            _mockRepository.Setup(x => x.GetByIdAsync(categoryId)).ReturnsAsync(category);
            _mockRepository.Setup(x => x.Update(category));
            _mockUnitOfWork.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask);
            _mockMapper.Setup(x => x.Map(updateRequest, category)).Returns(category);

            // Act
            var result = await _categoryService.UpdateAsync(categoryId, updateRequest);

            // Assert
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
            Assert.IsTrue(result.Success);
        }

        [Test]
        public async Task CategoryService_WhenGetById_ReturnsCategoryDto()
        {
            // Arrange
            int categoryId = 1;
            var category = new Category { Id = categoryId, Name = "Test Category" };
            var categoryDto = new CategoryDto { Id = categoryId, Name = category.Name };

            _mockRepository.Setup(x => x.GetByIdAsync(categoryId)).ReturnsAsync(category);
            _mockMapper.Setup(x => x.Map<CategoryDto>(category)).Returns(categoryDto);

            // Act
            var result = await _categoryService.GetByIdAsync(categoryId);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(categoryDto, result.Data);
        }

        [Test]
        public async Task CategoryService_WhenGetAll_ReturnsListOfCategories()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Category 1" },
                new Category { Id = 2, Name = "Category 2" }
            };

            var categoryDtos = new List<CategoryDto>
            {
                new CategoryDto { Id = 1, Name = "Category 1" },
                new CategoryDto { Id = 2, Name = "Category 2" }
            };

            _mockRepository.Setup(x => x.GetAll()).Returns(categories.AsQueryable());
            _mockMapper.Setup(x => x.Map<List<CategoryDto>>(categories)).Returns(categoryDtos);

            // Act
            var result = await _categoryService.GetAllAsync();

            // Assert
            Assert.AreEqual(categoryDtos, result.Data);
            Assert.IsTrue(result.Success);
        }
    }
}
