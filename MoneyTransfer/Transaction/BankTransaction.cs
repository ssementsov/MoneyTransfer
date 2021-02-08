using System;

namespace MoneyTransfer.Transaction
{
    public class BankTransaction : ITransaction
    {
        private readonly Account _from;
        private readonly Account _to;
        private readonly decimal _amount;

        public int TransactionId { get; }
        public DateTime StartTime { get; }

        public TransactionState State { get; set; }

        public BankTransaction(int id, Account from, Account to, decimal amount)
        {
            _from = from;
            _to = to;

            _amount = amount;
            TransactionId = id;

            StartTime = DateTime.UtcNow;
        }

        public void Execute()
        {
            try
            {
                _from.Withdraw(_amount);
                _to.Deposit(_amount);

                State = TransactionState.TransferSucceeded;
            }
            catch
            {
                // Log
                State = TransactionState.TransactionInvalid;
            }
        }

        public void RollBack()
        {
            if (_to.Balance >= _amount)
            {
                _to.Withdraw(_amount);
                _from.Deposit(_amount);

                State = TransactionState.RollbackSucceeded;
            }
            else
            {
                State = TransactionState.RollbackInvalid;
            }
        }
    }
}