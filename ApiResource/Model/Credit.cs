using System.ComponentModel.DataAnnotations;

namespace ApiResource.Model
{
    public class Credit
    {
        public string AccountNumber { get; set; }
        public string Amount { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public string Description { get; set; }
        public string TransactionId { get; set; }
        [Key]
        public string CreditId { get; set; }

        public Credit (string accountNumber, string amount, string description, string transactionId)
        {
            AccountNumber = accountNumber;
            Amount = amount;
            //Created = created;
            Description = description;
            TransactionId = transactionId;
            //CreditId = creditId;
        }
    }
}
