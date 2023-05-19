using ApiResource.Model;
using System.Linq;

namespace ApiResource.Services
{
    public class CreditScoreService
    {
        ApplicationDbContext _context;
        //ICreditScore _creditScore;
        List<AccountBalance> _accountBalances;
        public string _accountNumber;
        private const int TurnOverTime = 6;
        List<double> _timeFactors = new();
        int _maxNumberOfDaysInAMonth = 31;
        public CreditScoreService(ApplicationDbContext context,  string accountNumber, double amount)
        {
            _context = context;
            _accountNumber = accountNumber;
            _accountBalances = GetAccountBalanceOverSpecifiedPeriod(TurnOverTime);
           // _creditScore = new CreditScore(amount,_accountBalances);

        }

        public double GetCreditScore()
        {
            return 0;//_creditScore.GetCreditScore();
        }
        List<AccountBalance> GetAccountBalanceOverSpecifiedPeriod(int specifiedPeriodInMonths)
        {
            return _context.AccountBalances.Where(accountBalance => accountBalance.BankCustomerId == _accountNumber && accountBalance.Time.Month >= DateTime.Now.AddMonths(-specifiedPeriodInMonths).Month).ToList();
            
        }
    }
}
