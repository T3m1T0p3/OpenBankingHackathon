using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiResource.Model
{
    /*public class AccountBalance
    {
        public AccountBalance(string accountNumber, double balance, string? transactionId,string creditId=null,string debitId=null)
        {
            AccountNumber = accountNumber;
            Balance = balance;
            TransactionId = transactionId;
        }
        

        [ForeignKey("BankCustomer")]
        public string AccountNumber { get; set; }
        public double Balance { get; set; } = 0;
        public DateTime Time { get; set; } = DateTime.Now;
        //[Key]
        public string? TransactionId { get; set; }
        public string? CreditId { get; set; }
        public string? DebitId { get; set; }
        public string AccountBalanceId { get; set; } = Guid.NewGuid().ToString();
    }*/
}
