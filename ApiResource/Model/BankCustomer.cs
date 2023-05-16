using System.ComponentModel.DataAnnotations;

namespace ApiResource.Model
{
    public class BankCustomer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }    
        public int Age { get; set; }
        public AccountBalance CustomerBalance  { get; set; } = new AccountBalance();
        [Key]
        public string AccountNumber { get; set; }
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
        public BankCustomer(string firstName,string lastName, int age) 
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public void CreditAccount(string accountNumber, double amount)
        {
            
            throw new NotImplementedException();
        }
        public void DebitAccount(double account)
        {
            throw new NotImplementedException();
        }
        public double GetBalance()
        {
            throw new NotImplementedException();
        }
    }
}