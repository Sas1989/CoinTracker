using API.SDK.Domain.Entities;
using API.SDK.Domain.Exceptions;

namespace API.SDK.Domain.ValuesObjects;

public abstract class StringRequired : BaseValueObject<string>
{
    protected StringRequired(string value) : base(value)
    {
    }
    protected override bool FailConditon(string value)
    {
        return string.IsNullOrEmpty(value);
    }

    protected override BaseApplicationException  SetException(Error error)
    {
        return new DomainStringEmptyException(error);
    }
}
