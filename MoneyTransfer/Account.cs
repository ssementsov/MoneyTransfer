using MoneyTransfer.Common;

namespace MoneyTransfer
{
    public class Account
    {
        public decimal Balance { get; private set; }
        public string Name { get; }
        public decimal LimitPerTransaction { get; private set; } = 400;

        public Account(decimal balance, string name)
        {
            Name = name;
            Balance = balance;
        }

        public void Withdraw(decimal amount)
        {
            if (Balance < amount)
            {
                throw new NotEnoughtMoneyException("You are a beggar. Sorry.");
            }

            if (amount > LimitPerTransaction)
            {
                 throw new LimitException("Exceeded the limit for one transaction.");
            }

            DecreaseBalance(amount);
        }

        public void Deposit(decimal amount)
        {
            try
            {
                IncreaseBalance(amount);
            }
            catch
            {
                // Log
            }
        }

        public void ChangeLimit(decimal newLimit)
        {
            LimitPerTransaction = newLimit;
        }

        private void IncreaseBalance(decimal transferAmount) => Balance += transferAmount;

        private void DecreaseBalance(decimal transferAmount) => Balance -= transferAmount;
    }
}