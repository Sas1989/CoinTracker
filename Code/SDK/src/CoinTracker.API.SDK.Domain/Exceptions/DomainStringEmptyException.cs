using API.SDK.Domain.Entities;

namespace API.SDK.Domain.Exceptions;

public class DomainStringEmptyException : BaseApplicationException 
{
    public DomainStringEmptyException(Error error) : base(error)
    {
    }
}
