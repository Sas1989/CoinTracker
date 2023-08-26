using API.SDK.Domain.Entities;
using API.SDK.Domain.Exceptions;

namespace API.SDK.Domain.ValuesObjects;

public abstract class BaseValueObject<T> : IEquatable<BaseValueObject<T>?> 
{

    protected BaseValueObject(T value)
    {
        CheckCondition(value);

        Value = value;
    }

    private void CheckCondition(T value)
    {
        if (FailConditon(value))
        {
            var error = GenerateError();
            throw SetException(error);
        }
    }

    protected abstract bool FailConditon(T value);

    protected abstract BaseApplicationException  SetException(Error error);

    protected abstract Error GenerateError();

    public T Value { get; protected set; }

    public override bool Equals(object? obj)
    {
        return Equals(obj as BaseValueObject<T>);
    }

    public bool Equals(BaseValueObject<T>? other)
    {
        return other != null &&
               EqualityComparer<T>.Default.Equals(Value, other.Value);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Value);
    }

    public static bool operator ==(BaseValueObject<T>? left, BaseValueObject<T>? right)
    {
        return EqualityComparer<BaseValueObject<T>>.Default.Equals(left, right);
    }

    public static bool operator !=(BaseValueObject<T>? left, BaseValueObject<T>? right)
    {
        return !(left == right);
    }
}
