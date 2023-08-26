using API.SDK.Domain.Entities;
using API.SDK.Domain.Exceptions;
using System.Numerics;

namespace API.SDK.Domain.ValuesObjects
{
    public abstract class MajorThenZero<T> : BaseValueObject<T> where T : INumberBase<T>
    {
        protected MajorThenZero(T value) : base(value)
        {
        }

        protected override bool FailConditon(T value)
        {
            return T.IsNegative(value);
        }

        protected override BaseApplicationException SetException(Error error)
        {
            return new DomainNumberNegativeException(error);
        }
    }
}