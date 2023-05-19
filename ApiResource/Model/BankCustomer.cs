
using System.ComponentModel.DataAnnotations;

namespace ApiResource.Model
{
    public class BankCustomer
    {
        public BankCustomer(string firstName, string lastName, int age)
        {
            BankCustomerId = NubanGenerator.GenerateAccountNumberAsync2().Result;
            //BankCustomerId = "0000-0000-0000";
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            //CustomerBalance = 100000000;
            CustomerBalance = 0;
        }
        [Key]
        public string BankCustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public List<AccountBalance> AccountBalanceHistory {get; set;}=new List<AccountBalance>();
        public double CustomerBalance { get; set; }

        public Credit CreditAccount(string fromAccount, double amont,DateTime created)
        {
            var credit = new Credit(BankCustomerId, BankCustomerId, fromAccount, CustomerBalance,amont, created);
            AccountBalanceHistory.Add(credit.CreditAccount());
            CustomerBalance= credit.Balance.Balance;
            Console.WriteLine(credit.CreditId);
            return credit;

        }
        public Debit DebitAccount(string toAccount,double amount, DateTime created)
        {
            var debit = new Debit(BankCustomerId,toAccount,BankCustomerId,CustomerBalance,amount,created);
           AccountBalanceHistory.Add(debit.DebitAccount());
            CustomerBalance = debit.Balance.Balance;
            return debit;
        }
    }
}
