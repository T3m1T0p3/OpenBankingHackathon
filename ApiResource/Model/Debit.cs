namespace ApiResource.Model
{
    public class Debit
    {

        public Debit(string bankCustomerId,string creditedAccountNumber,string debitedAccountNumber,double balanceBeforeDebit, double amount,DateTime created)
        {
            DebitId = Guid.NewGuid().ToString();
            BankCustomerId = bankCustomerId;
            CreditedAccountNumber = creditedAccountNumber;
            DebitedAccountNumber = debitedAccountNumber;
            BalanceBeforeDebit = balanceBeforeDebit;
            Amount=amount;
            Created = created;
        }
        public string CreditedAccountNumber { get; set; }
        public string DebitedAccountNumber { get; set; }
        public double Amount { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public string Description { get; set; } = String.Empty;
        public string BankCustomerId { get; set; }
        public AccountBalance Balance { get; set; }
        public double BalanceBeforeDebit { get; set; }
        public string DebitId { get; set; } = Guid.NewGuid().ToString();

        public AccountBalance DebitAccount()
        {
            if (BalanceBeforeDebit >= Amount)
            {
                Balance = new(BankCustomerId, BalanceBeforeDebit - Amount,Created);
                Console.WriteLine($"Debit created on {BankCustomerId}");
                return Balance;
            }

            throw new Exception("Invalid transaction. Insufficient funds");
        } 
    }
}
