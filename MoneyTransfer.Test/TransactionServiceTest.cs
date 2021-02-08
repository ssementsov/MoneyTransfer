using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoneyTransfer.Transaction;

namespace MoneyTransfer.Test
{
    [TestClass]
    public class TransactionServiceTest
    {
        [TestMethod]
        public void SuccessTest()
        {
            var service = new TransactionService();

            var from = new Account(new decimal(1000.50), "Jonas Dixon");
            var to = new Account(500, "Owen Moss");

            var transaction_1 = new BankTransaction(1, from, to, 250);
            var transaction_2 = new BankTransaction(2, from, to, 250);
            var transaction_3 = new BankTransaction(3, from, to, 250);
            var transaction_4 = new BankTransaction(4, from, to, 250);

            service.AddTransaction(transaction_1);
            service.AddTransaction(transaction_2);
            service.AddTransaction(transaction_3);
            service.AddTransaction(transaction_4);

            if (service.HasTransactionsForExecuting)
            {
                service.ExecuteTransactions();
            }

            Assert.AreEqual(new decimal(0.50), from.Balance);
            Assert.AreEqual(1500, to.Balance);
        }

        [TestMethod]
        public void NotEnoughtMoneyTest()
        {
            var service = new TransactionService();

            var from = new Account(new decimal(1000.50), "Jonas Dixon");
            var to = new Account(500, "Owen Moss");

            var transaction_1 = new BankTransaction(1, from, to, 250);
            var transaction_2 = new BankTransaction(2, from, to, 1250);

            service.AddTransaction(transaction_1);
            service.AddTransaction(transaction_2);

            if (service.HasTransactionsForExecuting)
            {
                service.ExecuteTransactions();
            }

            Assert.AreEqual(new decimal(750.50), from.Balance);
            Assert.AreEqual(750, to.Balance);

            Assert.IsTrue(service.HasTransactionsForExecuting);

            if (service.HasTransactionsForExecuting)
            {
                service.ExecuteTransactions();
            }

            Assert.AreEqual(new decimal(750.50), from.Balance);
            Assert.AreEqual(750, to.Balance);
        }

        [TestMethod]
        public void RollbackTest()
        {
            var service = new TransactionService();

            var from = new Account(new decimal(1000.50), "Jonas Dixon");
            var to = new Account(500, "Owen Moss");

            var transaction_1 = new BankTransaction(1, from, to, 200);
            var transaction_2 = new BankTransaction(2, from, to, 300);
            var transaction_3 = new BankTransaction(2, from, to, 400);

            service.AddTransaction(transaction_1);
            service.AddTransaction(transaction_2);
            service.AddTransaction(transaction_3);

            if (service.HasTransactionsForExecuting)
            {
                service.ExecuteTransactions();
            }

            Assert.AreEqual(new decimal(100.50), from.Balance);
            Assert.AreEqual(1400, to.Balance);
            Assert.IsFalse(service.HasTransactionsForExecuting);

            service.RollbackTransaction(2);

            Assert.AreEqual(new decimal(400.50), from.Balance);
            Assert.AreEqual(1100, to.Balance);
        }
    }
}