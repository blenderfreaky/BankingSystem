using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.BankAccountTypes
{
    public class CheckAccount : BankAccount
    {
        public override IBankAccountOwner Owner { get; }
        protected override decimal Total { get; set; }
        public override DateTime LastTransaction { get; protected set; }
        public IBankAccountOwner Bank;
        public readonly List<Credit> credits;

        //decimal startingBalance = 0
        //Default-Parameter; new CheckAccount(owner, startingBalance)
        //                   new CheckAccount(owner) = new CheckAccount(owner, 0)
        //Zwei valide Aufrufsweisen
        public CheckAccount(IBankAccountOwner owner, IBankAccountOwner bank, decimal startingBalance = 0)
        {
            Owner = owner;
            Bank = bank;
            Total = startingBalance;
            credits = new List<Credit>();
        }

        public override decimal GetTotal(DateTime date)
        {
            HandleInterest(date);
            return base.GetTotal(date);
        }

        public Credit TakeCredit(decimal amount, int years, DateTime date)
        {
            if (Bank == null) return null;
            Credit credit = new Credit
            {
                yearlyInterest = 0.06m * amount / years,
                yearlyRate = amount / years,
                remainingTotal = amount,
                total = amount,
                start = date
            };
            credits.Add(credit);
            Bank.Transfer(this, amount, date);
            return credit;
        }

        //Zinsen und Kredite
        protected void HandleInterest(DateTime date)
        {
            if (Bank == null) return;
            decimal interestRate = (decimal)Math.Pow(1.01, date.Month - LastTransaction.Month) - 1;
            Bank.Transfer(this, Total * interestRate, date);

            for (int i = 0; i < credits.Count; i++)
            {
                Credit credit = credits[i];
                if (date.Year > LastTransaction.Year)
                {
                    Bank.TransferTo(this, credit.yearlyRate + credit.yearlyInterest, date);
                    credit.remainingTotal -= credit.yearlyRate;

                    if (credit.remainingTotal <= 0)
                    {
                        Bank.Transfer(this, -credit.remainingTotal, date);
                        credits.RemoveAt(i);
                        i--;
                    }
                }
            }
        }
    }
}
