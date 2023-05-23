namespace ClientManager.Dto
{
    public class GetCreditScoreReqDto
    {
        public string Email { get; set; }
        public double Amount { get; set; }
        public string AccountNumber { get; set; }
        public string EncryptionKey {get; set;}
    }
}
