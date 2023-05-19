using ApiResource.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ApiResource.Model
{
    public class CreditScore : ICreditScore
    {
        private const int TurnOverTime = 6;
        private const double TurnOverPercent = 0.33;
        private double _amount;
        List<AccountBalance> _accountBalances;
        IRepositoryService _repository;
        ITimeFactor _timeFactor;

        private double _creditScore = 0;
        List<double> _timeFactors = new();
        List<double> _turnOvers = new();
        int _maxNumberOfDaysInAMonth = 31;
        List<AccountBalance> _closingAcountBalances;
        List<List<AccountBalance>> _balancesByMonth;
        List<AccountBalance> _closingDailyBalance;
        public CreditScore(double amount, List<AccountBalance> balances)
        {
            //_accountNumber = accountNumber;
            _amount = amount;
            _accountBalances = balances;
            _balancesByMonth = GroupAccountBalancesByMonth(_accountBalances);
            //_balancesByDay = GroupAccountBalancesByDay(_balancesByMonth);
            _closingAcountBalances = ComputeClosingAccountBalance(_balancesByMonth);
            _turnOvers = ComputeTurnovers(_balancesByMonth);
            _timeFactors = ComputeTimeFactors(_balancesByMonth, _amount);
            _creditScore = ComputeCreditScore(_turnOvers, _timeFactors);
        }

        double ComputeTurnover(AccountBalance closingBalance)
        {

            return closingBalance.Balance * TurnOverPercent;
        }
        public List<double> ComputeTurnovers(List<List<AccountBalance>> balances)
        {
            List<double> turnOvers = new();
            foreach (List<AccountBalance> accountBalances in balances)
            {
                turnOvers.Add(ComputeTurnover(accountBalances.First()));
            }
            turnOvers.ForEach(x => Console.WriteLine($"Turnover {x}"));
            return turnOvers;
        }

        public List<AccountBalance> ComputeClosingAccountBalance(List<List<AccountBalance>> balancesByMonth)//List<AccountBalance> accountBalances)
        {
            //List<List<AccountBalance>> _balancesByMonth= (List<List<AccountBalance>>)accountBalances.GroupBy(x => x.Time.Month);
            //Console.WriteLine("Enumerating acount balance grouped by month");
            /*foreach(var v in balancesByMonth)
            {
                foreach(var vv in v)
                {
                    Console.WriteLine($"Account Number: {vv.BankCustomerId } Time: {vv.Time} Account bal;{vv.Balance}");
                }
            }*/
            var closingAccountBals = new List<AccountBalance>();
            _balancesByMonth.ForEach(accountBalances => closingAccountBals.Add(accountBalances.First()));
            //Console.WriteLine("Enumerating closing balance for each month");
            /*foreach(var v in closingAccountBals)
            {
                Console.WriteLine($"BalanceId: {v.BankCustomerId} month {v.Time} Closing bal:{v.Balance}");
            }*/
            return closingAccountBals;
        }

        public double ComputeTimeFactor(List<List<AccountBalance>> closingDailyBalances, double balanceLimit)
        {
            double numberOfDays = 0;
            AccountBalance closingBal;
            foreach (List<AccountBalance> bals in closingDailyBalances)
            {
                closingBal = bals.First();
                if (closingBal.Balance >= balanceLimit) {
                    Console.WriteLine($"Account Number{closingBal.BankCustomerId} Day: {closingBal.Time} closing bal {closingBal.Balance}");
                    numberOfDays++;
                }
            }
            var factor= ComputeExponenetialFactor(numberOfDays);
            //Console.WriteLine($"Time factor {factor}");
            return factor;
        }

        public List<double> ComputeTimeFactors(List<List<AccountBalance>> balancesByMonth, double balanceLimit)
        {
            Console.WriteLine($"balancesby motnh length {balancesByMonth.Count}");
            var tempList = new List<double>();
            double maxDailyBalance = Int64.MinValue;
            foreach (List<AccountBalance> monthlyBals in balancesByMonth)
            {
                Console.WriteLine($"Computing tf for month {monthlyBals.First().Time.Month}");
                List<List<AccountBalance>> accountBalGroupedByDays = new();
                var accountBalances = monthlyBals.GroupBy(accountBal => accountBal.Time.Day).AsEnumerable();
                foreach (var accountGroup in accountBalances)
                {
                    
                    accountBalGroupedByDays.Add(accountGroup.OrderByDescending(acctBal => acctBal.Time).ToList());
                    
                }
                tempList.Add(ComputeTimeFactor(accountBalGroupedByDays, balanceLimit));
            }
            tempList.ForEach(tf => Console.WriteLine($"time factor {tf}"));
            Console.WriteLine($"tempList length {tempList.Count}");
            return tempList;
        }

        public List<List<AccountBalance>> GroupAccountBalancesByMonth(List<AccountBalance> balances)
        {
            var groupByMonth = balances.GroupBy(accountBal => accountBal.Time.Month).AsEnumerable();
            var tempList = new List<List<AccountBalance>>();
            foreach (var iGrouping in groupByMonth)
            {
                tempList.Add(iGrouping.OrderByDescending(accontBal => accontBal.Time).ToList());
            }
            return tempList;
        }

        /*public List<AccountBalance> GetClosingDailyBalance(List<List<AccountBalance>> balancesByMonth)
          {
            List<AccountBalance> dailyClosingBalance = new();
            
            foreach(List<AccountBalance> v in balancesByMonth)
            {
                var temp = v.OrderByDescending(acctBal => acctBal.Time);
                var first = temp.First();
                Console.WriteLine($"Closing daily balance {first.Balance} Time: {first.Time}");
                dailyClosingBalance.Add(first);
            }
            return dailyClosingBalance;
         }*/

            public double ComputeExponenetialFactor(double time)
        {
            if (time <= 0) return 0;
            return Math.Pow(Math.E, (time - _maxNumberOfDaysInAMonth) / (time * _maxNumberOfDaysInAMonth));
        }

       public double ComputeCreditScore(List<double> turnOvers, List<double> timeFactors)
        {
            if (turnOvers.Count - timeFactors.Count == 0)
            {
                var turnoverTimefactorZip = turnOvers.Zip(timeFactors, (to, tf) => new { timeFactor = to, turnoverFactor = tf });
                double result = 0;
                foreach (var turnoverTimefactor in turnoverTimefactorZip)
                {
                    //Console.WriteLine($"");
                    result += turnoverTimefactor.timeFactor * turnoverTimefactor.turnoverFactor;
                }
                Console.WriteLine($"Credit Score {result}");
                return result;
            }
            throw new Exception("Cannot compute credit Score. Inconsistent nuber of elements in turnOvers and timeFactors");
        }

        public double GetCreditScore()
        {
            return _creditScore<_amount?_creditScore/_amount:1;
        }
    }
}
