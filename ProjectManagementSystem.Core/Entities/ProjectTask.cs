using System;

namespace ProjectManagementSystem.Core.Entities
{
    public class ProjectTask : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Deadline { get; set; }

        public Project? Project { get; set; }

        public UserEntity? AssignedUser { get; set; }
    }
}
