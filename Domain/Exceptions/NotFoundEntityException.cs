using Domain.Entities;

namespace Domain.Exceptions;
public class NotFoundEntityException : Exception 
{
    public NotFoundEntityException(string entityName, int id) : base($"Entity of type {entityName} with id {id} was not found.")
    {
    }
}
