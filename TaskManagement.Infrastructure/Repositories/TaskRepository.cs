using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Interfaces;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _db;
        public TaskRepository(ApplicationDbContext db) => _db = db;

        // filterin and sorting implementation
        public async Task<IEnumerable<TaskItem>> GetFilterdAsync(bool? isCompleted = null, string sortBy = null)
        {
            IQueryable<TaskItem> query = _db.Tasks.AsQueryable();

            // check if it has value 
            if (isCompleted.HasValue)
            {
                query = query.Where(t => t.IsCompleted == isCompleted.Value);
            }

            // sorting logic

            query = sortBy switch
            {

                "created" => query.OrderBy(t => t.CreatedAt),
                "due" => query.OrderBy(t => t.DueDate),
                "title" => query.OrderBy(t => t.Title),
                _ => query.OrderBy(t => t.Id)
            };

            return await query.AsNoTracking().ToListAsync();
        }

        //GET ALL
        public async Task<IEnumerable<TaskItem>> GetAllAsync() =>
            await _db.Tasks.AsNoTracking().ToListAsync();

        //GET BY ID
        public async Task<TaskItem> GetByIdAsync(int id) =>
           await _db.Tasks.FindAsync(id);

        //ADD
        public async Task AddAsync(TaskItem task)
        {
            await _db.Tasks.AddAsync(task);
            await _db.SaveChangesAsync();
        }


        //UPDATE
        public async Task UpdateAsync(TaskItem task)
        {
            _db.Tasks.Update(task);
            await _db.SaveChangesAsync();
        }

        //DELETE
        public async Task DeleteAsync(int id)
        {
            var t = await _db.Tasks.FindAsync(id);
            if (t != null) { _db.Tasks.Remove(t); await _db.SaveChangesAsync(); }
        }

    }
}
