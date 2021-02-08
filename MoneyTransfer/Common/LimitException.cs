using System;

namespace MoneyTransfer.Common
{
    public class LimitException : Exception
    {
        public LimitException(string errorMessage)
            : base(errorMessage)
        {
            // You can puy additional logic here
        }
    }
}