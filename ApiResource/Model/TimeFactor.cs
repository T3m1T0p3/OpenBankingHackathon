namespace ApiResource.Model
{
    public class TimeFactor:ITimeFactor
    {
        List<AccountBalance> _balances;
        double _accountBalanceLimit;
        double _timeFactor;
        public TimeFactor(List<AccountBalance> balancesForSpecifiedMonth,double accountBalanceLimit)
        {
            _balances = balancesForSpecifiedMonth;
            _accountBalanceLimit= accountBalanceLimit;
            _timeFactor=GetTimeFactorInDays();
        }
    
        public double GetTimeFactorInDays()
        {
            return 0;
        }

        public double GetTimeFactorInSeconds()
        {
            return 0;
        }

        public double GetTimeFactorInHours()
        {
            return 0;
        }
    }
}
