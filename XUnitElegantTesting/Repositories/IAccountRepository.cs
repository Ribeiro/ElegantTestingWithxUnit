using System.Threading.Tasks;
using XUnitElegantTesting.Models;


namespace XUnitElegantTesting.Repositories
{
    public interface IAccountRepository
    {
        Task<Account> GetBy(int number);
    }
}