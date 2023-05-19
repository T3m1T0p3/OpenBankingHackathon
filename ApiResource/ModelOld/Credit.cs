/*using System.ComponentModel.DataAnnotations;

namespace ApiResource.Model
{
    public class Credit
    {
        public string CreditedAccountNumber { get; set; }
        public string DebitedAccountNumber { get; set; }
        public double Amount { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public string Description { get; set; }
        public string TransactionId { get; set; }
        [Key]
        public string CreditId { get; set; } = Guid.NewGuid().ToString();

        public Credit(string creditedAccountNumber, string debitedAccountNumber, double amount, string description, string transactionId)
        {
            CreditedAccountNumber = creditedAccountNumber;
            DebitedAccountNumber = debitedAccountNumber;
            Amount = amount;
            //Created = created;
            Description = description;
            TransactionId = transactionId;
        }
    }
}
*/