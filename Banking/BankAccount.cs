using System;

namespace Banking
{
    public abstract class BankAccount
    {
        public abstract IBankAccountOwner Owner { get; }
        protected abstract decimal Total { get; set; }
        public abstract DateTime LastTransaction { get; protected set; }

        public virtual decimal GetTotal(DateTime date) => Total;

        public virtual bool CanPay(decimal amount, DateTime date) => amount <= GetTotal(date);
        public virtual void Transfer(BankAccount target, decimal amount, DateTime date)
        {
            if (!CanPay(amount, date)) throw new ArgumentOutOfRangeException($"Can't pay {amount.ToString("0.00")}$, as only {Total.ToString("0.00")}$ are available");

            Total -= amount;
            LastTransaction = date;
            
            target.Total += amount;
            target.LastTransaction = date;
        }
    }
}