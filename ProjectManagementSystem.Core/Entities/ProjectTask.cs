using System;

namespace ProjectManagementSystem.Core.Entities
{
    public class ProjectTask : BaseEntity
    {
        public int TaskId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Deadline { get; set; }

        // Navigation property for the project the task belongs to (assuming a many-to-one relationship)
        public Project Project { get; set; }

        // Navigation property for the assigned user (assuming a many-to-one relationship)
        public User AssignedUser { get; set; }
    }
}
