using System;
using XUnitElegantTesting.Models;

namespace XUnitElegantTesting.Services
{
    public class AccountManagerService
    {
        public Tuple<Account, Account> Transfer(decimal amount, Account originAccount, Account destinationAccount)
        {
            return new Tuple<Account, Account>(originAccount.Debit(amount), destinationAccount.Credit(amount));
        }

    }
}