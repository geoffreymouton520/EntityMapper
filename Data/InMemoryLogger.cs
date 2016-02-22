using System;
using System.Collections.Generic;
using Data.Contracts;

namespace Data
{
    public class InMemoryLogger : ILogger
    {
        public InMemoryLogger()
        {
            Exceptions = new List<Exception>();
        }

        public List<Exception> Exceptions { get; }

        public void Write(Exception exception)
        {
            Exceptions.Add(exception);
        }

    }
}