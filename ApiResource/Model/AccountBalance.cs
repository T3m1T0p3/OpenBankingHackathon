namespace ApiResource.Model
{
    public class AccountBalance
    {

        public AccountBalance(string bankCustomerId, double balance,DateTime time)// string? creditId = null, string? debitId=null)
        {
            BankCustomerId = bankCustomerId;
            Balance = balance;
            Time = time;
        }

        public string BankCustomerId { get; set; }
        public double Balance { get; set; } = 0;
        public DateTime Time { get; set; } 
        //public string? CreditId { get; set; }
        //public string? DebitId { get; set; }
        public string AccountBalanceId { get; set; } = Guid.NewGuid().ToString();
    }
}
