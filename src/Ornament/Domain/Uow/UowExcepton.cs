using System;

namespace Ornament.Domain.Uow
{
    public class UowExcepton : Exception
    {
        public UowExcepton(string message) : base(message)
        {
        }
    }
}