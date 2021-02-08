using System;
using System.Collections.Generic;
using System.Linq;

namespace MoneyTransfer.Transaction
{
    public class TransactionService
    {
        private readonly List<ITransaction> _transactions = new();

        public bool HasTransactionsForExecuting =>
            _transactions.Any(x =>
                x.State == TransactionState.New ||
                x.State == TransactionState.TransactionInvalid);

        public void AddTransaction(ITransaction transaction)
        {
            _transactions.Add(transaction);
        }

        public void ExecuteTransactions()
        {
            var transactions = _transactions.Where(t => t.State == TransactionState.New);

            foreach (var transaction in transactions)
            {
                transaction.Execute();
            }
        }

        public void RollbackTransaction(int transactionId)
        {
            var transaction = _transactions.FirstOrDefault(t => t.TransactionId == transactionId);
            if (transaction == null)
            {
                throw new ArgumentException($"Transaction Id={transactionId} not found.");
            }

            transaction.RollBack();

            if (transaction.State == TransactionState.RollbackSucceeded)
            {
                _transactions.Remove(transaction);
            }
        }
    }
}