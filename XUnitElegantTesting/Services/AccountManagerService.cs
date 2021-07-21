using System;
using System.Threading.Tasks;
using XUnitElegantTesting.Exceptions;
using XUnitElegantTesting.Models;
using XUnitElegantTesting.Repositories;

namespace XUnitElegantTesting.Services
{
    public class AccountManagerService
    {
        private const string AccountNotFoundMessage = "Account not found: {0}";
        private readonly IAccountRepository _accountRepository;

        public AccountManagerService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Tuple<Account, Account>> Transfer(decimal amount, Account originAccount, Account destinationAccount)
        {
            Account originAccountFromDb = await _accountRepository.GetBy(originAccount.Number);
            Account destinationAccountFromDb = await _accountRepository.GetBy(destinationAccount.Number);

            AssertExisting(originAccountFromDb, originAccount);
            AssertExisting(destinationAccountFromDb, destinationAccount);

            return new Tuple<Account, Account>(originAccountFromDb.Debit(amount),
                                               destinationAccountFromDb.Credit(amount)
                                              );
        }

        private void AssertExisting(Account accountFromDb, Account inputedAccount)
        {
            if(null == accountFromDb)
            {
                throw new BaseAppException(string.Format(AccountNotFoundMessage, inputedAccount.Number));
            }
        }

    }
}