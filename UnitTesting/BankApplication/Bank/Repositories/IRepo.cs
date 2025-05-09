

using UnitTestingProj.Bank.Entities;

namespace UnitTestingProj.Bank.Repositories
{
    public interface IRepo
    {
        bool Save(Account account);
    }
}
