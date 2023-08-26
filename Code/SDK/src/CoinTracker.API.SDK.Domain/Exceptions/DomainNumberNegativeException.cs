using API.SDK.Domain.Entities;

namespace API.SDK.Domain.Exceptions;

public class DomainNumberNegativeException : BaseApplicationException
{
    public DomainNumberNegativeException(Error error) : base(error)
    {
    }
}
