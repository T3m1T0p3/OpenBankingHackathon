using ClientManager.Dto;
using ClientProject.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OpenBankingCore.RepositoryService;
using System.Text;

namespace OpenBankingCore.Controller
{
    [ApiController]
    [Route("/api")]
    public class ClientController:ControllerBase
    {
        IRepositoryService<OpenBankingClient> _repositoryService;
        UserManager<OpenBankingClient> _userManager;

        public ClientController(IRepositoryService<OpenBankingClient> repositoryService, UserManager<OpenBankingClient> userManager)
        {
            _repositoryService = repositoryService;
            _userManager = userManager;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterClient(ClientDto clientDto)
        {
            //register client with Ids
            //if reg seccessful return apikey
            HttpClient httpClient = new HttpClient();
            IdsClientDto idsClientDto=new IdsClientDto { ClientId=clientDto.ClientName,ClientName=clientDto.ClientName };
            StringContent httpContent = new StringContent(JsonConvert.SerializeObject(idsClientDto),Encoding.UTF8,"application/json");
            var req = await httpClient.PostAsync("http://localhost:5036/api/create/client",httpContent);
            if (req.IsSuccessStatusCode)
            {
                var dbClient = new OpenBankingClient
                {
                    Email = clientDto.Email,
                    ClientName = clientDto.ClientName,
                    UserName=clientDto.ClientName
                };
                var createClient =await _userManager.CreateAsync(dbClient);
                if (createClient.Succeeded)
                {
                    await _userManager.AddPasswordAsync(dbClient, clientDto.Password);
                }
                else
                {
                    createClient.Errors.ToList().ForEach(x => Console.WriteLine(x));
                }
                return Ok(await req.Content.ReadAsStringAsync());
            }
            Console.WriteLine(req.StatusCode);
            return StatusCode(500,"Action could not be completed");
        }
        [HttpPost("login")]
        public Task<IActionResult> Login()
        {
            throw new Exception("Not implemented");
        }

        [HttpPost("logout")]
        public Task<IActionResult> Logout()
        {
            throw new Exception("Not Implemented");
        }

        [HttpPost("cwscore")]
        public Task<IActionResult> GetCreditScore()
        {
            throw new Exception("Not implemented");
        }
    }
}
