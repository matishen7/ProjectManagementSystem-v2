using System;
using System.Collections.Generic;

namespace ProjectManagementSystem.Core.Entities
{
    public class UserEntity : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

    }
}
