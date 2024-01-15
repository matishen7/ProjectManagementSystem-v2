using System;
using System.Collections.Generic;

namespace ProjectManagementSystem.Core.Entities
{
    public class Project : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        // Navigation properties
        public ICollection<ProjectTask>? ProjectTasks { get; set; }

        // Navigation property for team members (assuming a many-to-many relationship)
        public ICollection<User>? TeamMembers { get; set; }
    }
}
