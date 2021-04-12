using System;

namespace Demo
{
    public class BankCard
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
}
