using ApiResource.Model;
using System.Linq;

namespace ApiResource.Services
{
    public class CreditScoreService
    {
        ApplicationDbContext _context;
        ICreditScore _creditScore;
        List<AccountBalance> _accountBalances;
        public string _accountNumber;
        private const int TurnOverTime = 6;
        ITimeFactor _timeFactor;
        public CreditScoreService(ApplicationDbContext context,ICreditScore creditScore,ITimeFactor timeFactor) 
        {
            _context = context;
            _creditScore = creditScore;
            _timeFactor = timeFactor;
        }

        public double GetCreditScore()
        {
            return _creditScore.GetCreditScore();
        }
        List<AccountBalance> GetAccountBalanceOverSpecifiedPeriod(int specifiedPeriodInMonths)
        {
            _accountBalances = _context.AccountBalances.Where(accountBalance => accountBalance.AccountNumber == _accountNumber && accountBalance.Time.Month>=DateTime.Now.AddMonths(-specifiedPeriodInMonths).Month).ToList();
            return _accountBalances;
        }

        List<AccountBalance> GetClosingAccountBalance(List<AccountBalance> accountBalances)
        {
            var balancesByMonth = accountBalances.GroupBy(accountBal => accountBal.Time.Month).ToList();
            balancesByMonth.ForEach(accountBals => accountBals.OrderBy(x => x.Time));
            var closingAccountBals = new List<AccountBalance>();
            balancesByMonth.ForEach(accountBalances => closingAccountBals.Add(accountBalances.Last()));
            return closingAccountBals;
        }


    }
}
