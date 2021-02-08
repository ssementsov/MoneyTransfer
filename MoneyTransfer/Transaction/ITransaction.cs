using System;

namespace MoneyTransfer.Transaction
{
    public interface ITransaction
    {
        int TransactionId { get; }
        DateTime StartTime { get; }
        TransactionState State { get; set; }
        void Execute();
        void RollBack();
    }
}