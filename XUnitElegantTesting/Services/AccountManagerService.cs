using System;
using XUnitElegantTesting.Models;
using XUnitElegantTesting.Repositories;

namespace XUnitElegantTesting.Services
{
    public class AccountManagerService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountManagerService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public Tuple<Account, Account> Transfer(decimal amount, Account originAccount, Account destinationAccount)
        {
            return new Tuple<Account, Account>(_accountRepository.GetBy(originAccount.Number).Debit(amount), 
                                               _accountRepository.GetBy(destinationAccount.Number).Credit(amount)
                                              );
        }

    }
}