using System;


namespace XUnitElegantTesting.Èxceptions
{
    public class NotEnoughFundsException : Exception
    {
        public NotEnoughFundsException() { }

        public NotEnoughFundsException(string message) : base(message) { }

        public NotEnoughFundsException(string message, Exception innerException) : base(message, innerException) { }
    }
}
