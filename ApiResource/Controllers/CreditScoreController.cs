using ApiResource.Dto;
using ApiResource.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiResource.Controllers
{
    [ApiController]
    [Route("api/credit_score")]
    public class CreditScoreController:ControllerBase
    {
        ApplicationDbContext _context;
        public CreditScoreController(ApplicationDbContext context)
        {
        _context = context;
        }
        //[Authorize]
        [HttpPost("score")]
        public IActionResult GetCreditScore([FromBody] CreditScoreReqDto reqData)
        {
            BankCustomer customer;
            try
            {
                var query = from cust in _context.BankCustomers  where cust.BankCustomerId == reqData.AccountNumber select cust;
                customer = query.FirstOrDefault();
                CreditScore creditScore = new CreditScore(reqData.Amount, _context.AccountBalances.Where(acct => acct.BankCustomerId == reqData.AccountNumber).ToList());
                double score = creditScore.GetCreditScore();
                return Ok(new CreditScoreRes
                {
                    FirstName=customer.FirstName,
                    LastName=customer.LastName,
                    CreditScore=score,
                    Bank="FirstBank Nigeria",
                    Defaulter=false,
                    AccountType="savings"
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return BadRequest("Something went wrong. Please try again");
            }
        }
    }
}
