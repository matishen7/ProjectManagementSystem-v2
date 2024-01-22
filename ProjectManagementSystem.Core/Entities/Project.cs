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

            public ICollection<ProjectTask>? ProjectTasks { get; set; }
            public UserEntity? ProjectManager { get; set; }
            public int ProjectManagerId { get; set; }


    }
}
