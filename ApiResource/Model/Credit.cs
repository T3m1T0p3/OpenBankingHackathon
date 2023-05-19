using ApiResource.Model;

namespace ApiResource.Model
{
    public class Credit
    {
        public Credit(string bankCustomerId, string creditedAccountNumber, string debitedAccountNumber, double balanceBeforeCredit, double amount,DateTime created)
        {
            CreditId = Guid.NewGuid().ToString();
            BankCustomerId = bankCustomerId;
            CreditedAccountNumber = creditedAccountNumber;
            DebitedAccountNumber = debitedAccountNumber;
            BalanceBeforeCredit = balanceBeforeCredit;
            Amount = amount;
            Created = created;
        }
        public string BankCustomerId { get; set; }
        public string CreditId { get; set; } = Guid.NewGuid().ToString();
        public string CreditedAccountNumber { get; set; } //Benefiary
        public string DebitedAccountNumber { get; set; } //Creditor
        public double Amount { get; set; }
        public DateTime Created { get; set; }
        public string Description { get; set; } = String.Empty;
        public double BalanceBeforeCredit { get; set; }
        public AccountBalance Balance { get; set; }
        public AccountBalance CreditAccount()
        {
            Balance = new(BankCustomerId, BalanceBeforeCredit + Amount,Created);//, CreditId,null);
                return Balance;
        }
    }
}
