using System;
using System.Collections.Generic;
using System.Linq;
using XUnitElegantTesting.Models;


namespace XunitElegantTestingTests.Services.Fixtures
{
    internal static class AccountFactory
    {
        public static Account GetAccountNotFound()
        {
            return null;
        }

        public static IList<Account> GetAccountsFor(IDictionary<int, decimal> parameters)
        {
            return parameters.Select(x => new Account(x.Key, x.Value)).ToList();
        }

    }

}