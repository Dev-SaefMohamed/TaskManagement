using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagement.Application.DTOs;
using static TaskManagement.Application.DTOs.TaskDTOs;

namespace TaskManagement.Application.Interfaces
{
    public interface ITaskService
    {
        Task<TaskReadDto> CreateAsync(TaskCreateDto dto);

        // new methods for filtering and sorting
        Task<IEnumerable<TaskReadDto>> GetFilteredAsync(bool? isCompleted = null, string sortBy = null);

        Task<IEnumerable<TaskReadDto>> GetAllAsync();
        Task<TaskReadDto> GetByIdAsync(int id);
        Task UpdateAsync(int id, TaskUpdateDto dto);
        Task DeleteAsync(int id);
    }
}
