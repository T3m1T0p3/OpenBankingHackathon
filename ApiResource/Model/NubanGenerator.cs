using Microsoft.Extensions.Primitives;
using System.Text;

namespace ApiResource.Model
{
    public class NubanGenerator
    {
        private static readonly int _firstBankCode = 11;
        //private static readonly int _serialNumberUpperLimit = 999999999;
        private static int _serialNumberLowerLimit = 320000001;

        public static async Task<string> GenerateAccountNumberAsync2()
        {
            var serialNumber = _serialNumberLowerLimit++;//new Random().Next(_serialNumberLowerLimit,_serialNumberUpperLimit);
            int checkDigit = await GenerateCheckDigitAsync((Int64)Math.Pow(10, 9) * _firstBankCode + serialNumber);
            StringBuilder res = new StringBuilder();
            res=res.Append(_firstBankCode);
            res = res.Append(serialNumber).Append(checkDigit);
            Console.WriteLine($"Serial Number:{serialNumber} CheckDigit:{checkDigit}");
            return res.ToString();
        }
        private static async Task<int> GenerateCheckDigitAsync(Int64 serialNumber)
        {
            Int64 temp=serialNumber;
            Int64 lastDigit;
            Int64 checkDigit=0;
            int ternarySwitch=1;
            
            var task= Task.Run(() =>
            {
                //Console.WriteLine($"Starting {temp}");
                while (0 < temp)
                {
                    temp = temp / 10;
                    //Console.WriteLine($"Temp {temp}");
                    lastDigit = serialNumber - (temp * 10);
                    //Console.WriteLine($"Last digit {lastDigit}");
                    checkDigit = checkDigit + (ternarySwitch == 2 ? lastDigit * 7 : lastDigit * 3);
                    ternarySwitch++;
                    ternarySwitch = ternarySwitch > 2 ? ternarySwitch % 3 : ternarySwitch;
                    serialNumber /= 10;
                    temp = serialNumber;
                }
            }
            );
            task.Wait();
            //Console.WriteLine($"checkdigit {checkDigit}");
            var mod = checkDigit % 10;
            checkDigit = 10-mod;
            return checkDigit == 10 ? 0 : (int)checkDigit;
        }
    }
}
