using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.DTOs
{
    public class TaskDTOs
    {
        public class TaskCreateDto
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime? DueDate { get; set; }
        }

        public class TaskReadDto
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime? DueDate { get; set; }
            public bool IsCompleted { get; set; }
        }

        public class TaskUpdateDto
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime? DueDate { get; set; }
            public bool IsCompleted { get; set; }
        }
    }
}
