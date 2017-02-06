using System;

namespace Ornament.Uow
{
    public class UowExcepton : Exception
    {
        public UowExcepton(string message) : base(message)
        {
        }
    }
}