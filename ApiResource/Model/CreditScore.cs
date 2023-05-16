using ApiResource.Repository;

namespace ApiResource.Model
{
    public class CreditScore:ICreditScore
    {
        private const int TurnOverTime= 6;
        private const double TurnOverPercent = 0.33;
        public string _accountNumber;
        public double _amount;
        List<AccountBalance> _balances;
        IRepositoryService _repository;
        ITimeFactor _timeFactor;
        double _turnover = 0;
        private double _creditScore= 0;
        public CreditScore(string accountNumber, double amount, ITimeFactor timeFactor,List<AccountBalance> balances)
        {
            _accountNumber = accountNumber;
            _amount = amount;
            _balances = balances;
            //_timeFactor = new TimeFactor(_balances,_amount);
        }

        public double GetTurnover(List<AccountBalance> closingBalanceOverSpecifiedPeriod)
        {
            closingBalanceOverSpecifiedPeriod.ForEach(bal => _turnover+=bal.Balance );
            //Get account bal for this month
            //get the timefactor for the specified amount

            return _turnover*TurnOverPercent;
        }

        public double GetCreditScore(List<AccountBalance> closingAccountBals,double timeFactors, double amount)
        {
            /*if (closingAccountBals.Count is 0 || timeFactors.Count is 0) return _creditScore;
            _creditScore += closingAccountBals.First().Balance * timeFactors.First();
            GetCreditScore(closingAccountBals[1..], timeFactors.RemoveAt[1..], amount);
            return _creditScore/amount;*/
        }
    }
}
