using System;

namespace Demo
{
    public class ImmutableBankCard
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
}
