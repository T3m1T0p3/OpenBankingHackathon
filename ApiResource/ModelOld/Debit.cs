/*using System.ComponentModel.DataAnnotations;

namespace ApiResource.Model
{
    public class Debit
    {
        public Debit(string creditedAccountNumber, string debitedAccountNumber, double amount, string description, string transactionId)
        {
            CreditedAccountNumber = creditedAccountNumber;
            DebitedAccountNumber = debitedAccountNumber;
            Amount = amount;
            //Created = created;
            Description = description;
            TransactionId = transactionId;
        }
        public string CreditedAccountNumber { get; set; } //Benefiary
        public string DebitedAccountNumber { get; set; } //Creditor
        public double Amount { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public string Description { get; set; }
        public string TransactionId { get; set; }
        [Key]
        public string DebitId { get; set; } = Guid.NewGuid().ToString();
    }
}
*/