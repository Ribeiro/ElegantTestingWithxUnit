using XUnitElegantTesting.Models;


namespace XUnitElegantTesting.Repositories
{
    public interface IAccountRepository
    {
        Account GetBy(int number);
    }
}