namespace ClientManager.Dto
{
    public class CreditScoreRes
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double CreditScore { get; set; }
        public string Bank { get; set; }
        public bool Defaulter { get; set; } = false;
        public string AccountType { get; set; }
    }
}
