using System;


namespace XUnitElegantTesting.Models
{
    public sealed class Account
    {
        public int? Number { get; private set; }
        public decimal Balance { get; private set; }

        public Account(int? number, decimal balance)
        {
            this.Number = number;
            this.Balance = balance;
        }

        private Account With(Action<Account> update)
        {
            var clone = (Account) MemberwiseClone();
            update(clone);
            return clone;
        }

        public Account SetNumber(int nextNumber)
        {
            return With(a => a.Number = nextNumber);
        }

        public Account SetBalance(decimal nextBalance)
        {
            return With(a => a.Balance = nextBalance);
        }

        internal Account Debit(decimal amount)
        {
            if(amount > Balance)
            {
                throw new ApplicationException("Not enough funds.");
            }
            return With(a => a.Balance = a.Balance - amount);
        }

        internal Account Credit(decimal amount)
        {
            return With(a => a.Balance = a.Balance + amount);
        }

    }

}