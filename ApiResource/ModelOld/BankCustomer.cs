/*using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApiResource.Model
{
    public class BankCustomer
    {
        public BankCustomer(string firstName, string lastName, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            AccountNumber = GenerateAccountNumberAsync().Result;
            CustomerBalance = 0;//new(AccountNumber, 1000000);
        }

        [Key]
        public string AccountNumber { get; set; } = "";
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public double CustomerBalance { get; set; }
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
        //public List<AccountBalance> AcountBalanceHistory { get; set; } =new List<AccountBalance>();
        private string TransactionType { get; set; }
        

        //Rceive from thirdParty
        public void CreditAccount(double amount, string fromAccount)
        {
            TransactionType = "credit";
            var trans = new Transaction(amount, AccountNumber, CustomerBalance, TransactionType,fromAccount, AccountNumber);
            CustomerBalance = trans.Credit(fromAccount, CustomerBalance).Balance;
            Transactions.Add(trans);
            
        }
        public void DebitAccount(double amount, string beneficiaryAccount)
        {
            //create a new transaction of type debit
            //if success: create new AccountNumber
            TransactionType = "debit";
            var trans = new Transaction(amount, AccountNumber,  CustomerBalance, TransactionType, AccountNumber, beneficiaryAccount);
            
            CustomerBalance = trans.Debit(beneficiaryAccount, CustomerBalance).Balance;
            Transactions.Add(trans);
        }
        public double GetBalance()
        {
            return CustomerBalance;
        }
        async Task<string> GenerateAccountNumberAsync()
        {
            return await NubanGenerator.GenerateAccountNumberAsync();
        }
    }
}*/