using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking
{
    public interface IBankAccountOwner
    {
        /// <summary>
        /// Returns whether the IBankAccountOwner can afford a certain amount
        /// </summary>
        bool CanPay(decimal amount, DateTime date);

        /// <summary>
        /// Transfer the IBankAccountOwner's Money
        /// </summary>
        void Transfer(BankAccount target, decimal amount, DateTime date);

        /// <summary>
        /// Pay money to IBankAccountOwner
        /// </summary>
        void TransferTo(BankAccount target, decimal amount, DateTime date);
    }
}
