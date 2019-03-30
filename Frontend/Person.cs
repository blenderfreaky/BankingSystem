using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banking;
using Banking.BankAccountTypes;

namespace Frontend
{
    public class Person : IBankAccountOwner
    {
        public string name;
        public DateTime birthday;
        public int Age => (DateTime.Now - birthday).Days / 356;
        public CreditAccount creditAccount;
        public CheckAccount checkAccount;

        public Person(string name, DateTime birthday, CreditAccount creditAccount = null, CheckAccount checkAccount = null)
        {
            this.name = name;
            this.birthday = birthday;
            this.creditAccount = creditAccount;
            this.checkAccount = checkAccount;
        }

        public bool CanPay(decimal amount, DateTime date) => creditAccount.CanPay(amount, date);

        public void Transfer(BankAccount target, decimal amount, DateTime date) => creditAccount.Transfer(target, amount, date);

        public void TransferTo(BankAccount target, decimal amount, DateTime date) => target.Transfer(checkAccount, amount, date);
    }
}
