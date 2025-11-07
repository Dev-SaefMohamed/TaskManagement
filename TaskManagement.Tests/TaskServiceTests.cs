using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Services;
using TaskManagement.Infrastructure.Data;
using TaskManagement.Infrastructure.Repositories;
using Xunit;
using static TaskManagement.Application.DTOs.TaskDTOs;

namespace TaskManagement.Tests
{
    public class TaskServiceTests
    {
    
        private readonly TaskService _service;
        private readonly ApplicationDbContext _db;


        public TaskServiceTests()
        {

            var opt = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _db = new ApplicationDbContext(opt);
            var repo = new TaskRepository(_db);
            _service = new TaskService(repo);

        }

        [Fact]
        public async Task CreateAsync_Should_Add_Task()
        {

            // arrange 
            var dto = new TaskCreateDto
            {

                Title = "Test Task",
                Description = "Testing",
                DueDate = DateTime.UtcNow.AddDays(2)

            };

            // act
            var result = await  _service.CreateAsync(dto);
            Assert.NotNull(result);
            Assert.Equal("Test Task", result.Title);
            Assert.Single(_db.Tasks);

        }

        [Fact]
        public async Task CreateAsync_Should_Throw_Exception_For_Past_DueDate()
        {
            // arrange 
            var dto = new TaskCreateDto
            {
                Title = "Bad Test Task",
                Description = "Invalid Date",
                DueDate = DateTime.UtcNow.AddDays(-1)
            };
            // act & assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _service.CreateAsync(dto);
            });
        }
    }
}
