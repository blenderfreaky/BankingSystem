using System;

namespace Banking.BankAccountTypes
{
    public class CreditAccount : BankAccount
    {
        public override IBankAccountOwner Owner { get; }
        protected override decimal Total { get; set; }
        public override DateTime LastTransaction { get; protected set; }

        public CheckAccount MainAccount { get; internal set; }

        public CreditAccount(CheckAccount Account)
        {
            MainAccount = Account;
        }

        public override decimal GetTotal(DateTime date)
        {
            HandleEndOfMonthPayments(date);
            return base.GetTotal(date);
        }

        public override bool CanPay(decimal amount, DateTime date) => true;

        public override void Transfer(BankAccount target, decimal amount, DateTime date)
        {
            HandleEndOfMonthPayments(date);
            base.Transfer(target, amount, date);
        }

        public void HandleEndOfMonthPayments(DateTime date)
        {
            if (date.Month > LastTransaction.Month) MainAccount.Transfer(this, -Total, date);
        }
    }
}
