namespace ApiResource.Model
{
    public class AccountBalance
    {
        //double _amountAddedOrDeducted;
        public string AccountNumber { get; set; }
        public double Balance { get; set; } = 0;
        public DateTime Time { get; set; } = DateTime.Now;
    }
}
