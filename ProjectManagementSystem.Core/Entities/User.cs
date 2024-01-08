using System;
using System.Collections.Generic;

namespace ProjectManagementSystem.Core.Entities
{
    public class User
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        // Navigation property for projects assigned to the user
        public ICollection<Project> AssignedProjects { get; set; }
    }
}
