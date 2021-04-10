using System;
using System.Threading;

namespace Demo
{
    class BankCard
    {
        public DateTime ValidBefore { get; set; }
        public decimal Balance { get; set; }

        public decimal GetAvailableAmount(decimal desired)
        {
            return HasExpired
                ? 0
                : Math.Min(Balance, desired);
        }

        private bool HasExpired => DateTime.Now.CompareTo(ValidBefore) >= 0;
    }

    class ImmutableBankCard
    {
        public DateTime ValidBefore { get; }
        public decimal Balance { get; }

        public ImmutableBankCard(DateTime validBefore, decimal balance)
        {
            ValidBefore = validBefore;

            Balance = balance;
        }

        public decimal GetAvailableAmount(decimal desired, DateTime at)
        {
            return HasExpired(at)
                ? 0
                : Math.Min(Balance, desired);
        }

        private bool HasExpired(DateTime at) => at.CompareTo(ValidBefore) >= 0;
    }

    class Program
    {
        static void Main(string[] args)
        {
            var card = new BankCard()
            {
               ValidBefore = DateTime.Now.AddSeconds(2),
               Balance = 100
            };

            Console.WriteLine(card.GetAvailableAmount(20)); // returns 20

            card.Balance = 15;

            Console.WriteLine(card.GetAvailableAmount(20)); // returns 15

            Thread.Sleep(3000); // three seconds later...

            Console.WriteLine(card.GetAvailableAmount(20)); // returns 0

            var immutableCard = new ImmutableBankCard(DateTime.Now.AddSeconds(2), 100);

            var queryTime = DateTime.Now;

            Console.WriteLine(immutableCard.GetAvailableAmount(20, queryTime)); // returns 20

            Console.WriteLine(immutableCard.GetAvailableAmount(20, queryTime.AddSeconds(3))); // returns 0

            Console.WriteLine(immutableCard.GetAvailableAmount(20, queryTime)); // returns 20
        }
    }
}
