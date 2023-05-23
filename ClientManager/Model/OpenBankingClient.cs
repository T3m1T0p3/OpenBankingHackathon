using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ClientProject.Model
{
    public class OpenBankingClient : IdentityUser
    {
        public Guid OpenBankingClientId { get; set; }=Guid.NewGuid();
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        // public string ClientPassword { get; set; }
        public DateTime CreatedAt { get; } = DateTime.UtcNow;
        // public string Email { get; set; }
        public Guid RegistrationNumber { get; set; }

        public bool IsLicensed { get; set; } = true;
        public byte[] EncryptedApiKey { get; set; }
        public byte[] IV { get; set; }


    }
}
