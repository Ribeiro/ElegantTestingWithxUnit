using System;
using XUnitElegantTesting.Models;


namespace XunitElegantTestingTests.Services.Fixtures
{
    internal static class AccountFactory
    {
        public static Account GetAccountWithNumberAndBalanceOf(int number, decimal amount)
        {
            return new Account(number: number, balance: amount);
        }

        public static Account GetAccountWithNumber(int number)
        {
            return new Account(number: number);
        }

        public static Account GetAccountNotFound()
        {
            return null;
        }

    }
}