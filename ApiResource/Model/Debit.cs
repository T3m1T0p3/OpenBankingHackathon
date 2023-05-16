using System.ComponentModel.DataAnnotations;

namespace ApiResource.Model
{
    public class Debit
    {
        public Debit(string accountNumber, string amount, string transactionId)
        {
            AccountNumber = accountNumber;
            Amount = amount;
            TransactionId = transactionId;
        }

        public string AccountNumber { get; set; }
        public string Amount { get; set; }
        public DateTime Created { get; set; }= DateTime.Now;
        public string TransactionId { get; set; }
        [Key]
        public string DebitId { get; set; }
    }
}
