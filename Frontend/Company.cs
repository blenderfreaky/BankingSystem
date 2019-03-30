using Banking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend
{
    public class Company : IBankAccountOwner
    {
        public string name;
        public List<Person> employees;

        public List<BankAccount> bankAccounts;

        public Company(string name, List<Person> employees, List<BankAccount> bankAccounts)
        {
            this.name = name;
            this.employees = employees;
            this.bankAccounts = bankAccounts;
        }

        private BankAccount GetFullestAccount(DateTime date)
        {
            if (bankAccounts.Count == 0) return null;

            BankAccount fullestBankAccount = bankAccounts[0];
            foreach (BankAccount otherBankAccount in bankAccounts)
            {
                if (otherBankAccount.GetTotal(date) > fullestBankAccount.GetTotal(date))
                {
                    fullestBankAccount = otherBankAccount;
                }
            }

            return fullestBankAccount;
        }

        public bool CanPay(decimal amount, DateTime date) => GetFullestAccount(date).CanPay(amount, date);

        public void Transfer(BankAccount target, decimal amount, DateTime date) => GetFullestAccount(date).Transfer(target, amount, date);

        public void TransferTo(BankAccount target, decimal amount, DateTime date) => target.Transfer(GetFullestAccount(date), amount, date);
    }
}
