using System;


namespace XUnitElegantTesting.Exceptions
{
    public class BaseAppException : Exception
    {
        public BaseAppException() { }

        public BaseAppException(string message) : base(message) { }

        public BaseAppException(string message, Exception innerException) : base(message, innerException) { }
    }
}
