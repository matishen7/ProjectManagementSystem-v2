using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Core.Entities
{
    public abstract class BaseEntity
    {
        public DateTime? DateModified { get; set; }
        public int Id { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
