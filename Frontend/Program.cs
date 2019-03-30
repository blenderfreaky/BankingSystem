using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banking;
using Banking.BankAccountTypes;

namespace Frontend
{
    class Program
    {
        static void Main(string[] args)
        {
            Company bank = new Company("Deutsche Bank", new List<Person>(), new List<BankAccount>());

            bank.bankAccounts.Add(new CheckAccount(bank, null, 1500200));
            bank.bankAccounts.Add(new CheckAccount(bank, null, 3170300));
            bank.bankAccounts.Add(new CheckAccount(bank, null,  520090));

            Person person = new Person("Max Mustermann", new DateTime(1996, 4, 17));

            CheckAccount checkAccount = new CheckAccount(person, bank);
            person.checkAccount = checkAccount;
            person.creditAccount = new CreditAccount(checkAccount);

            Console.WriteLine($"{person.name}.Total = {checkAccount.GetTotal(DateTime.Now)}");
            Console.WriteLine($"{person.name}.CreditAccount.Total = {person.creditAccount.GetTotal(DateTime.Now)}");
            Console.WriteLine();


            //BankAccount bankAccount = checkAccount;
            //bankAccount.GetCredit();
            //=> Crashes, GetCredit is specific to CheckAccount

            checkAccount.TakeCredit(5000, 3, DateTime.Now);

            Console.WriteLine($"{person.name}.CheckAccount.Total = {checkAccount.GetTotal(DateTime.Now)}");
            Console.WriteLine($"{person.name}.CreditAccount.Total = {person.creditAccount.GetTotal(DateTime.Now)}");
            Console.WriteLine();


            Person person2 = new Person("Erika Mustermann", new DateTime(1994, 6, 23));

            CheckAccount checkAccount2 = new CheckAccount(person2, bank);
            person.checkAccount = checkAccount2;

            person.Transfer(checkAccount2, 1000, DateTime.Now);

            Console.WriteLine($"{person.name}.CheckAccount.Total = {checkAccount.GetTotal(DateTime.Now)}");
            Console.WriteLine($"{person.name}.CreditAccount.Total = {person.creditAccount.GetTotal(DateTime.Now)}");
            Console.WriteLine($"{person2.name}.CheckAccount.Total = {checkAccount2.GetTotal(DateTime.Now)}");
            Console.WriteLine();


            //More than a month later
            person.Transfer(checkAccount2, 5, DateTime.Now + new TimeSpan(35, 0, 0, 0));

            Console.WriteLine($"{person.name}.CheckAccount.Total = {checkAccount.GetTotal(DateTime.Now)}");
            Console.WriteLine($"{person.name}.CreditAccount.Total = {person.creditAccount.GetTotal(DateTime.Now)}");
            Console.WriteLine($"{person2.name}.CheckAccount.Total = {checkAccount2.GetTotal(DateTime.Now)}");

            Console.ReadLine();
        }
    }
}
