 
using UnitTestingProj.Bank.Entities;

namespace BankApplicationApi.Interfaces
{
    public interface IRepo
    {
        Task<Account> Get(Guid id);
        Task<List<Account>> Get();
        Task<bool> Save(Account account);
        bool Update(Account account, decimal balance);
        bool Delete(Account account);
    }
}
