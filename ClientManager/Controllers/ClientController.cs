using ClientManager.Dto;
using ClientManager.Services;
using ClientManager.Services.RepositoryService;
using ClientManager.Services.TokenServices;
using ClientProject.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace OpenBankingCore.Controller
{
    [ApiController]
    [Route("/api")]
    public class ClientController:ControllerBase
    {
        IRepositoryService<OpenBankingClient> _repositoryService;
        UserManager<OpenBankingClient> _userManager;
        ITokenService _tokenService;
        IEncryptionService _encryptionService;

        public ClientController(IEncryptionService encryptionService,IRepositoryService<OpenBankingClient> repositoryService, UserManager<OpenBankingClient> userManager,ITokenService tokenService)
        {
            _repositoryService = repositoryService;
            _userManager = userManager;
            _tokenService = tokenService;
            _encryptionService = encryptionService;
        }
        [AllowAnonymous]
        [EnableCors("Access-Control-Allow-Origin")]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterClient(ClientDto clientDto)
        {
            Console.WriteLine("Hit");
            //1bddc007-c4f6-4656-a585-65980f465ca6
            //3aa3f869-4e3b-4454-9bf3-1c82a0ef2430
            HttpClient httpClient = new HttpClient();
            IdsClientDto idsClientDto=new IdsClientDto { ClientId=clientDto.ClientId,ClientName=clientDto.ClientName };
            StringContent httpContent = new StringContent(JsonConvert.SerializeObject(idsClientDto),Encoding.UTF8,"application/json");
            var req = await httpClient.PostAsync("http://localhost:5036/api/create/client",httpContent);
            try
            {
                if (req.IsSuccessStatusCode)
                {
                    var dbClient = new OpenBankingClient
                    {
                        Email = clientDto.Email,
                        ClientName = clientDto.ClientName,
                        UserName = clientDto.ClientId,
                        ClientId=clientDto.ClientId
                    };
                    var createClient = await _userManager.CreateAsync(dbClient, clientDto.Password);
                    if (!createClient.Succeeded) throw new Exception(createClient.Errors.FirstOrDefault().Description);
                    
                }
                return Ok(await req.Content.ReadAsStringAsync());
            }
            catch(Exception e)
            {
                Console.WriteLine(req.StatusCode);
                Console.WriteLine(e.Message);
                return StatusCode(500, "Action could not be completed");
            }
            
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginReqDto reqDto)
        {
            OpenBankingClient user;
            string token;
            try
            {
                user =await _userManager.FindByEmailAsync(reqDto.Email);
                if (user is null) throw new Exception("Invalid login");
                Console.WriteLine($"User found {user.Email}");
                if (! (await _userManager.CheckPasswordAsync(user,reqDto.Password))) return Unauthorized("Invalid login");
                Console.WriteLine("Password valiadated");
                token=_tokenService.GetToken("SuperSecretSigningKey", "http://localhost:5187", user);
                //Console.WriteLine(token);
                HttpContext.Response.Headers.Add("Authorization", "Bearer " + token);
                return Ok(token);
            }
            catch(Exception ex)
            {
                return Unauthorized("Invalid Login");
            }
        }

        [HttpPost("logout")]
        public Task<IActionResult> Logout()
        {
            throw new Exception("Not Implemented");
        }

        [HttpPost("encryptkey")]
        public async Task<IActionResult> EncryptApiKey([FromBody]EncryptionDto encryptionDto)
        {
            OpenBankingClient user;
            try
            {
                Console.WriteLine("Retrieving user");

                user =  _repositoryService.Get(encryptionDto.Email);
                Console.Write($"Found {user.ClientName}");
                if (user.EncryptedApiKey is not null)
                {
                    //string decrypt = _encryptionService.Decrypt(user.EncryptedApiKey, Encoding.ASCII.GetBytes(encryptionDto.EncryptionKey.PadLeft(16, 'p')), Encoding.ASCII.GetBytes(encryptionDto.EncryptionKey.PadLeft(16, 'p')));
                    //Console.WriteLine($"Decrypted key {decrypt}");
                    return BadRequest();
                }

                Console.WriteLine($"byte length:{Encoding.ASCII.GetBytes(encryptionDto.EncryptionKey.PadLeft(15,'p')).Length}");
                byte[] encryptedKey = _encryptionService.Encrypt(encryptionDto.ApiKey, Encoding.ASCII.GetBytes(encryptionDto.EncryptionKey.PadLeft(16,'p')), Encoding.ASCII.GetBytes(encryptionDto.EncryptionKey.PadLeft(16,'p')));
                //Console.WriteLine($"Encrypted key {encryptedKey.ToString()}");
                user.EncryptedApiKey = encryptedKey;
                _repositoryService.Update();
                string decrypt = _encryptionService.Decrypt(encryptedKey, Encoding.ASCII.GetBytes(encryptionDto.EncryptionKey.PadLeft(16, 'p')), Encoding.ASCII.GetBytes(encryptionDto.EncryptionKey.PadLeft(16, 'p')));
                Console.WriteLine($"Decrypted key {decrypt}");
                return Ok();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500);
            }
        }
        [HttpPost("decryptkey")]
        public Task<IActionResult> DecryptApiKey()
        {
            throw new Exception("Not Implemented");
        }
        [AllowAnonymous]
        [Authorize]
        [HttpPost("cwscore")]
        public async Task<IActionResult> GetCreditScore(GetCreditScoreReqDto reqDto)
        {
            CreditScoreRes creditScoreRes;
            string token=HttpContext.Request.Headers["Authorization"];
            token = token.Split(" ")[1].Trim();
            Console.WriteLine($"Got token from http header:{token}");

            try
            {
                OpenBankingClient client = await _userManager.FindByEmailAsync(reqDto.Email);
                string apiKey = _encryptionService.Decrypt(client.EncryptedApiKey, Encoding.ASCII.GetBytes(reqDto.EncryptionKey.PadLeft(16,'p')), Encoding.ASCII.GetBytes(reqDto.EncryptionKey.PadLeft(16, 'p')));

                if (_tokenService.ValidateToken("SuperSecretSigningKey", "http://localhost:5187", "http://localhost:5187", token))
                {
  
                    HttpClient httpClient = new HttpClient();
                    CreditScoreReqDto creditScoreReq = new CreditScoreReqDto { AccountNumber = reqDto.AccountNumber, Amount = reqDto.Amount };
                    StringContent httpContent = new StringContent(JsonConvert.SerializeObject(creditScoreReq), Encoding.UTF8, "application/json");
                    StringContent tokenReq = new StringContent(JsonConvert.SerializeObject(new TokenRequestDto { ClientSecret = apiKey, ClientId = client.ClientId }), Encoding.UTF8, "application/json");
                    var req = await httpClient.PostAsync("http://localhost:5036/api/get/token", tokenReq);
                    if (req.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"retirved token {await req.Content.ReadAsStringAsync()}");
                        httpClient = new HttpClient();
                        httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + await req.Content.ReadAsStringAsync());
                        httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                        Console.WriteLine($"header {httpClient.DefaultRequestHeaders.Authorization}");
                        var ScoreReq = await httpClient.PostAsync(@"http://localhost:5215/api/credit_score/score", httpContent);
                        if (ScoreReq.IsSuccessStatusCode)
                        {
                            creditScoreRes = JsonConvert.DeserializeObject<CreditScoreRes>(await ScoreReq.Content.ReadAsStringAsync());
                            return Ok(creditScoreRes);
                        }
                    }
                    throw new Exception(req.ReasonPhrase);
                }
                return Unauthorized("Token validation error");
            }
            catch(Exception e)
            {
                Console.WriteLine($"Something went wrong {e.Message}");
                return BadRequest("Something went wrong");
            }
        }

    }
}
