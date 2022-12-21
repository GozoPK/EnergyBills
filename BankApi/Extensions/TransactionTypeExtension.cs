using BankApi.Entities;

namespace BankApi.Extensions
{
    public static class TransactionTypeExtension
    {
        public static string GetTransactionType(this TransactionTypes transactionType)
        {
            if (transactionType == TransactionTypes.Credit) return "Credit";

            return "Debit";
        }
    }
}