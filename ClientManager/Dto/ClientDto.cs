namespace ClientManager.Dto
{
    public class ClientDto
    {
        public string ClientName { get; set; }
        // public string ClientPassword { get; set; }
        public DateTime CreatedAt { get; } = DateTime.UtcNow;
        public string Email { get; set; }
        //public Guid RegistrationNumber { get; set; }

        public bool IsLicensed { get; set; } = true;
        public string Password { get; set; }

    }
}
