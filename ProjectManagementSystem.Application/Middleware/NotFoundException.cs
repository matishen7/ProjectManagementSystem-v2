using System.Runtime.Serialization;

namespace ProjectManagementSystem.Application.Middleware
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entityName, int entityId)
       : base($"{entityName} with ID {entityId} not found.")
        {
        }
    }
}