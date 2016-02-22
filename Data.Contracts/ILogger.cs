using System;

namespace Data.Contracts
{
    public interface ILogger
    {
        void Write(Exception exception);
    }
}