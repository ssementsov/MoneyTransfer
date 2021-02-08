using System;

namespace MoneyTransfer.Common
{
    public class NotEnoughtMoneyException : Exception
    {
        public NotEnoughtMoneyException(string errorMessage)
            : base(errorMessage)
        {
            // You can puy additional logic here
        }
    }
}