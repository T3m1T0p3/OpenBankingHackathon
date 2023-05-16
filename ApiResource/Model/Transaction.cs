namespace ApiResource.Model
{
    public class Transaction
    {
        public List<Credit> Credits { get; set; } = new List<Credit>();
        public string TransactionId { get; set; }
        public string AccountNumber { get; set; }
        public List<Debit> Debits { get; set; } = new List<Debit>();
    }
}
