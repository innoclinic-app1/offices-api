namespace Infrastructure.Exceptions;

public class AlreadyExistsException : Exception
{
    public AlreadyExistsException(string message) : base(message)
    {
    }
    
    public AlreadyExistsException(int entityId) : base ($"Entity with id {entityId} already exists.")
    {
    }
}
