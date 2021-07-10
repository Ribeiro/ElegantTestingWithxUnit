using FluentAssertions;
using System;
using Xunit;
using XUnitElegantTesting.Models;
using XUnitElegantTesting.Services;
using XunitElegantTestingTests.Services.Fixtures;

namespace XunitElegantTestingTests.Services
{
    public class AccountManagerServiceTest
    {
        private readonly AccountManagerService _sut;

        public AccountManagerServiceTest()
        {
            _sut = new AccountManagerService();
        }

        [Fact]
        public void ShouldSuccessfullyTransferAmountBetweenAccounts()
        {
            Account originAccount = AccountFactory.GetValidAccountWithNumberAndBalanceOf(1234, 1000);
            decimal originExpectedBalance = 500;

            Account destinationAccount = AccountFactory.GetValidAccountWithNumberAndBalanceOf(5678, 2000);
            decimal destinationExpectedBalance = 2500;

            Tuple <Account, Account> transferResult = _sut.Transfer(500, originAccount, destinationAccount);

            originExpectedBalance.Should().Be(transferResult.Item1.Balance);
            destinationExpectedBalance.Should().Be(transferResult.Item2.Balance);
        }

        [Fact]
        public void ShouldFailTransferAmountBetweenAccountsDueToNotEnoughFundsForDebit()
        {
            Account originAccount = AccountFactory.GetValidAccountWithNoBalance();
            Account destinationAccount = AccountFactory.GetValidAccountWithNoBalance();

            Action action = () => _sut.Transfer(1500, originAccount, destinationAccount);

            action.Should()
                  .Throw<ApplicationException>()
                  .WithMessage("Not enough funds.");
        }

    }
}