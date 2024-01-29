using System;

namespace ProjectManagementSystem.Core.Entities
{
    public class ProjectTask : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? Deadline { get; set; }
        public int ProjectId { get; set; }

        public int? UserId { get; set; }
    }
}
