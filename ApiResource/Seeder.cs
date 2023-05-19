using ApiResource.Model;

namespace ApiResource
{
    public class Seeder
    {
        ApplicationDbContext _context;
        public Seeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddCustomer(string firstName, string lastName, int age)
        {
            _context.BankCustomers.Add(new BankCustomer( firstName,lastName,age));
           // _context.BankCustomers.Add(new BankCustomer("Test", "Customer2",
             //   25));
           
            _context.SaveChanges();
        }

        double CheckAccountBalance(string accountNumber)
        {
            var account = _context.AccountBalances.First(x => x.BankCustomerId == accountNumber);//Select(acct => acct.BankCustomerId == accountNumber).FirstOrDefault();
            return account.Balance;
        }

        
        public void Credit(string fromAccountNumber, string toAccountNumber, double amount,DateTime created)
        {
            Console.WriteLine("finding customer 1");
            var fromAccount = _context.BankCustomers.Find(toAccountNumber);
            Console.WriteLine("customer 1 found");
            Console.WriteLine("finding customer 2");
            var toAccount = _context.BankCustomers.Find(toAccountNumber);
            Console.WriteLine("customer 2 found");
            Console.WriteLine("customer 1 debited");
            Console.WriteLine("crediting customer 2");
            _context.Credits.Add(toAccount.CreditAccount(fromAccountNumber, amount,created));
            Console.WriteLine("customer 2 credited");
            _context.SaveChanges();
        }
        public void CreateTransaction(string fromAccountNumber,string toAccountNumber, double amount,string transactionType,DateTime created)
        {

            if (fromAccountNumber == toAccountNumber)
            {
                var fromAccount = _context.BankCustomers.Find(toAccountNumber);
               _context.Add( fromAccount.CreditAccount(fromAccountNumber, amount, created));
                _context.SaveChanges();
                return;
            }

            if (transactionType == "credit")
            {
                Credit(fromAccountNumber, toAccountNumber, amount,created);
            }
            else if(transactionType =="debit")
            {
                Debit(fromAccountNumber, toAccountNumber, amount,created);
            }
            else
            {
                PairedTransaction(fromAccountNumber, toAccountNumber, amount, created   );
            }
        }

        public void Debit(string fromAccountNumber,string toAccountNumber, double amount,DateTime created)
        {
            Console.WriteLine("finding customer 1");
            var fromAccount = _context.BankCustomers.Find(toAccountNumber);
            Console.WriteLine("customer 1 found");
            Console.WriteLine("finding customer 2");
            var toAccount = _context.BankCustomers.Find(toAccountNumber);
            Console.WriteLine("customer 2 found");
            Console.WriteLine("debiting customer 1");
           _context.Debits.Add( fromAccount.DebitAccount(toAccountNumber,amount,created));
            Console.WriteLine("customer 1 debited");
            Console.WriteLine("crediting customer 2");
            _context.SaveChanges();
        }

        public void PairedTransaction(string fromAccountNumber,string toAccountNumber,double amount,DateTime created)
        {
            try
            {
                //Console.WriteLine("finding customer 1");
                var fromAccount = _context.BankCustomers.Find(fromAccountNumber);
                //Console.WriteLine("customer 1 found");
                //Console.WriteLine($"customer 1 Account Bal {fromAccount.CustomerBalance}");
                //Console.WriteLine("finding customer 2");
                var toAccount = _context.BankCustomers.Find(toAccountNumber);
                //Console.WriteLine("customer 2 found");
                //Console.WriteLine("debiting customer 1");
                _context.Debits.Add(fromAccount.DebitAccount(toAccountNumber, amount, created));
                _context.Credits.Add(toAccount.CreditAccount(fromAccountNumber, amount, created));
                //Console.WriteLine("customer 1 debited");
                //Console.WriteLine("crediting customer 2");
                _context.SaveChanges();
            }
            catch(Exception e)
            {

            }
        }

        public DateTime GetMonth(int scaleBackFactor)
        {
            return DateTime.Now.AddMonths(scaleBackFactor);
        }

        public DateTime GetDay(DateTime time,int scaleBackFactor)
        {
            return time.AddDays(scaleBackFactor);
        }

        public DateTime GetHour(DateTime time, int scaleBackFactor)
        {
            return time.AddDays(scaleBackFactor);
        }

        public void GenerateRandomTransactions()
        {
            string[] fromAccounts = { "0000-0000-0000", "113200000014", "113200000021" };
            int scaleBackFactor = -1;
            DateTime startingTime = DateTime.Now.AddDays(-DateTime.Now.Day);
            startingTime = startingTime.AddMonths(scaleBackFactor);
            Console.WriteLine($"Starting time {startingTime}");
            //var randomGenerator = new Random();
            while (scaleBackFactor++ <= 0)
            {  
                int daysBetweenTransaction;
                //var month = startingTime.Month;
                var nextMonth = startingTime.AddMonths(1);
                var randomSecondsGenerator = new Random();
                while (startingTime < nextMonth && startingTime<=DateTime.Now)
                {
                    var randomSeconds = randomSecondsGenerator.NextDouble();
                    DateTime transactionDay = startingTime.Date.AddSeconds(randomSeconds * 86400);
                    Console.WriteLine($"Next transaction day {transactionDay}");
                    var accountSelector = new Random();
                    var transactionAmountSelector = new Random();
                    var debitOrCreditSelector = new Random();
                    var daysBetweenTransactionSelector = new Random();
                    int selectFromAccount = accountSelector.Next(0, 3);
                    var selectToAccount =accountSelector.Next(1, 2);
                    int debitOrCredit =debitOrCreditSelector.Next(0, 1);
                    var fromAccountNumber = fromAccounts[selectFromAccount];
                    var toAccountNumber = fromAccounts[selectToAccount];
                    var transactionAmount = transactionAmountSelector.Next(1000, 100000);
                    
                    double checkedAccountBal = CheckAccountBalance(fromAccountNumber);
                    if (checkedAccountBal < transactionAmount) continue;
                     PairedTransaction(fromAccountNumber, toAccountNumber, transactionAmount,transactionDay);
                    daysBetweenTransaction = 1;//daysBetweenTransactionSelector.Next(1, 5);
                    //transactionDay = transactionDay.AddDays(daysBetweenTransaction);
                    startingTime=startingTime.AddDays(daysBetweenTransaction);
                }
            }
        }

        public void TruncateTables()
        {
            
        }
    }

    
}
