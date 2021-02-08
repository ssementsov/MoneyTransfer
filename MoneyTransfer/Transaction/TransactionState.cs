namespace MoneyTransfer.Transaction
{
    public enum TransactionState
    {
        New = 0,

        TransferSucceeded = 1,
        TransactionInvalid = 2,

        RollbackSucceeded = 3,
        RollbackInvalid = 4
    }
}