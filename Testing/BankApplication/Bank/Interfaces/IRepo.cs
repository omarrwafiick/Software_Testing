 
using UnitTestingProj.Bank.Entities;

namespace BankApplicationApi.Interfaces
{
    public interface IRepo
    {
        Task<Account> Get(Guid id);
        Task<List<Account>> Get();
        Task<bool> Save(Account account);
        Task<bool> Update(Guid id, decimal balance);
        Task<bool> Delete(Guid id);
    }
}
