using System;
using XUnitElegantTesting.Models;


namespace XunitElegantTestingTests.Services.Fixtures
{
    internal static class AccountFactory
    {
        public static Account GetInvalidAccountDueToMissingNumber()
        {
            return new Account(number: -1 , balance: 1234);
        }

        public static Account GetValidAccountWithNoBalance()
        {
            return new Account(number: new Random().Next(int.MinValue, int.MaxValue), balance: 0);
        }

        public static Account GetValidAccountWithNumberAndBalanceOf(int number, decimal amount)
        {
            return new Account(number: number, balance: amount);
        }

        public static Account GetAccountNotFound()
        {
            return null;
        }

    }
}