using System;
using XUnitElegantTesting.Exceptions;
using XUnitElegantTesting.Models;
using XUnitElegantTesting.Repositories;

namespace XUnitElegantTesting.Services
{
    public class AccountManagerService
    {
        private const string AccountNotFoundMessage = "Account not found.";
        private readonly IAccountRepository _accountRepository;

        public AccountManagerService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public Tuple<Account, Account> Transfer(decimal amount, Account originAccount, Account destinationAccount)
        {
            Account originAccountFromDb = _accountRepository.GetBy(originAccount.Number);
            Account destinationAccountFromDb = _accountRepository.GetBy(destinationAccount.Number);

            AssertExisting(originAccountFromDb);
            AssertExisting(destinationAccountFromDb);

            return new Tuple<Account, Account>(originAccountFromDb.Debit(amount),
                                               destinationAccountFromDb.Credit(amount)
                                              );
        }

        private void AssertExisting(Account account)
        {
            if(null == account)
            {
                throw new BaseAppException(AccountNotFoundMessage);
            }
        }

    }
}