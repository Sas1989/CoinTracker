using API.SDK.Domain.Entities;

namespace API.SDK.Domain.Exceptions;

public abstract class BaseApplicationException : Exception
{
    public string Code { get; }
    public string Description { get; }
    public Error Error { get; }
    protected BaseApplicationException (Error error) : base($"{error.Code}: {error.Description}")
    {
        Code = error.Code;
        Description = error.Description;
        Error = error; 
    }
}
