using FluentAssertions;
using Moq;
using System;
using Xunit;
using XUnitElegantTesting.Èxceptions;
using XUnitElegantTesting.Models;
using XUnitElegantTesting.Repositories;
using XUnitElegantTesting.Services;
using XunitElegantTestingTests.Services.Fixtures;

namespace XunitElegantTestingTests.Services
{
    public class AccountManagerServiceTest
    {
        private readonly AccountManagerService _sut;
        private readonly Mock<IAccountRepository> _accountRepositoryMock = new Mock<IAccountRepository>();

        public AccountManagerServiceTest()
        {
            _sut = new AccountManagerService(_accountRepositoryMock.Object);
        }

        [Fact]
        public void ShouldSuccessfullyTransferAmountBetweenAccounts()
        {
            _accountRepositoryMock.Setup(p => p.GetBy(1234)).Returns(AccountFactory.GetValidAccountWithNumberAndBalanceOf(1234, 1000));
            _accountRepositoryMock.Setup(p => p.GetBy(5678)).Returns(AccountFactory.GetValidAccountWithNumberAndBalanceOf(5678, 2000));

            Account originAccount = new Account(1234);
            decimal originExpectedBalance = 500;

            Account destinationAccount = new Account(5678);
            decimal destinationExpectedBalance = 2500;

            Tuple <Account, Account> transferResult = _sut.Transfer(500, originAccount, destinationAccount);

            transferResult.Item1.Balance.Should().Be(originExpectedBalance);
            transferResult.Item2.Balance.Should().Be(destinationExpectedBalance);
        }

        [Fact]
        public void ShouldFailTransferAmountBetweenAccountsDueToNotEnoughFundsForDebit()
        {
            _accountRepositoryMock.Setup(p => p.GetBy(1234)).Returns(AccountFactory.GetValidAccountWithNumberAndBalanceOf(1234, 0));
            _accountRepositoryMock.Setup(p => p.GetBy(5678)).Returns(AccountFactory.GetValidAccountWithNumberAndBalanceOf(5678, 0));

            Account originAccount = new Account(1234);
            Account destinationAccount = new Account(5678);

            Action action = () => _sut.Transfer(1500, originAccount, destinationAccount);

            action.Should()
                  .Throw<NotEnoughFundsException>()
                  .WithMessage("Debit account has not enough funds to proceed.");
        }

    }
}