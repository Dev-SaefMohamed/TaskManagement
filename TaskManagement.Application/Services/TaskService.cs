using TaskManagement.Application.Interfaces;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Interfaces;
using static TaskManagement.Application.DTOs.TaskDTOs;

namespace TaskManagement.Application.Services
{
    public class TaskService : ITaskService
    {

        private readonly ITaskRepository _repo;
        public TaskService(ITaskRepository repo) => _repo = repo;


        public async Task<TaskReadDto> CreateAsync(TaskCreateDto dto)
        {
            if (dto.DueDate.HasValue && dto.DueDate.Value <= DateTime.UtcNow)
                throw new ArgumentException("DueDate must be in the future.");

            var entity = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate
            };

            await _repo.AddAsync(entity);

            return new TaskReadDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                CreatedAt = entity.CreatedAt,
                DueDate = entity.DueDate,
                IsCompleted = entity.IsCompleted
            };
        }

        // implementation of filtering and sorting in services
        public async Task<IEnumerable<TaskReadDto>> GetFilteredAsync(bool? isCompleted = null, string sortBy = null)
        {
              
             var list = await _repo.GetFilterdAsync(isCompleted, sortBy);
            return list.Select(e => new TaskReadDto
                {
                    Id = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    CreatedAt = e.CreatedAt,
                    DueDate = e.DueDate,
                    IsCompleted = e.IsCompleted
                });

        }

        public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);

        public async Task<IEnumerable<TaskReadDto>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return list.Select(e => new TaskReadDto
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                CreatedAt = e.CreatedAt,
                DueDate = e.DueDate,
                IsCompleted = e.IsCompleted
            });
        }

        public async Task<TaskReadDto> GetByIdAsync(int id)
        {
            var e = await _repo.GetByIdAsync(id);
            if (e == null) return null;
            return new TaskReadDto
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                CreatedAt = e.CreatedAt,
                DueDate = e.DueDate,
                IsCompleted = e.IsCompleted
            };
        }

        public async Task UpdateAsync(int id, TaskUpdateDto dto)
        {
            var e = await _repo.GetByIdAsync(id);
            if (e == null) throw new ArgumentException("Not found");

            if (dto.DueDate.HasValue && dto.DueDate.Value <= DateTime.UtcNow)
                throw new ArgumentException("DueDate must be in the future.");

            e.Title = dto.Title;
            e.Description = dto.Description;
            e.DueDate = dto.DueDate;
            e.IsCompleted = dto.IsCompleted;

            await _repo.UpdateAsync(e);
        }

    }
}
