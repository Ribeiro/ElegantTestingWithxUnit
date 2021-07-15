using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using XUnitElegantTesting.Exceptions;
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
            IList<Account> relatedAccounts = AccountFactory.GetAccountsFor(new Dictionary<int, decimal>() { [1234] = 1000, [5678] = 2000 });

            _accountRepositoryMock.Setup(p => p.GetBy(1234)).Returns(relatedAccounts[0]);
            _accountRepositoryMock.Setup(p => p.GetBy(5678)).Returns(relatedAccounts[1]);
            
            decimal originExpectedBalance = 500;
            decimal destinationExpectedBalance = 2500;

            Tuple <Account, Account> transferResult = _sut.Transfer(500, relatedAccounts[0], relatedAccounts[1]);

            transferResult.Item1.Balance.Should().Be(originExpectedBalance);
            transferResult.Item2.Balance.Should().Be(destinationExpectedBalance);
        }

        [Fact]
        public void ShouldFailTransferAmountBetweenAccountsDueToNotEnoughFundsForDebit()
        {
            IList<Account> relatedAccounts = AccountFactory.GetAccountsFor(new Dictionary<int, decimal>() { [1234] = 0, [5678] = 0 });

            _accountRepositoryMock.Setup(p => p.GetBy(1234)).Returns(relatedAccounts[0]);
            _accountRepositoryMock.Setup(p => p.GetBy(5678)).Returns(relatedAccounts[1]);

            Action action = () => _sut.Transfer(1500, relatedAccounts[0], relatedAccounts[1]);

            action.Should()
                  .Throw<BaseAppException>()
                  .WithMessage(string.Format("Debit account {0} has not enough funds to proceed.", relatedAccounts[0].Number));
        }

        [Fact]
        public void ShouldFailTransferAmountBetweenAccountsDueAccountNotFound()
        {
            IList<Account> relatedAccounts = AccountFactory.GetAccountsFor(new Dictionary<int, decimal>() { [1234] = 0, [5678] = 0 });

            _accountRepositoryMock.Setup(p => p.GetBy(1234)).Returns(relatedAccounts[0]);
            _accountRepositoryMock.Setup(p => p.GetBy(5678)).Returns(AccountFactory.GetAccountNotFound());

            Action action = () => _sut.Transfer(1500, relatedAccounts[0], relatedAccounts[1]);

            action.Should()
                  .Throw<BaseAppException>()
                  .WithMessage(string.Format("Account not found: {0}", relatedAccounts[1].Number));
        }

    }
}