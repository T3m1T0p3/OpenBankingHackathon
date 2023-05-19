/*using System.Linq.Expressions;

namespace ApiResource.Model
{
    public class Transaction
    {
        //double _amount = 0;

        public Transaction(double amount, string accountNumber,double accountBalanceBeforeTransaction, string transactionType, string? debitedAccountNumber = null, string? creditedAccountNumber = null)
        {
            Amount = amount;
            AccountNumber = accountNumber;
            CreditedAccountNumber = creditedAccountNumber;
            DebitedAccountNumber = debitedAccountNumber;
            AccountBalanceBeforeTransaction = accountBalanceBeforeTransaction;
            TransactionId=Guid.NewGuid().ToString();
            TransactionType = transactionType;
        }
        public string AccountNumber { get; set; }
        public string? DebitedAccountNumber { get; set; }//
        public double Amount { get; set; }
        public double AccountBalanceBeforeTransaction { get; set; }
        public string TransactionId { get; set; }
        //public string? FromAccountNumber { get; set; }
        public string? CreditedAccountNumber { get; set; }
        public List<Debit> DebitTransactions { get; set; } = new();
        public List<Credit> CreditTransactions { get; set; } = new();
        public string TransactionType { get; set; }
        public List<AccountBalance> AccountBalances { get; set; } = new();
        public AccountBalance Credit(string toAccount, double initialBal)
        {
            var credit = new Credit(CreditedAccountNumber,DebitedAccountNumber, Amount, $"Credit transaction from {DebitedAccountNumber} to {CreditedAccountNumber}", TransactionId);
            CreditTransactions.Add(credit);
            var bal=new AccountBalance(AccountNumber,initialBal+Amount, TransactionId,credit.CreditId);
            AccountBalances.Add(bal);
            return bal;
        }

        public AccountBalance Debit(string toAccount, double initialBal)
        {
            var debit = new Debit(CreditedAccountNumber, DebitedAccountNumber, Amount, $"Credit transaction from {DebitedAccountNumber} to {CreditedAccountNumber}", TransactionId);
            DebitTransactions.Add(debit);
            var bal=new AccountBalance(AccountNumber, initialBal-Amount,TransactionId,null,debit.DebitId);
            AccountBalances.Add(bal);
            return bal;
        }
    }
}
*/