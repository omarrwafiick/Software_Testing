using UnitTestingProj.Bank.Repositories;
using UnitTestingProj.Bank.Entities;

public class AccountService
{
    private readonly IRepo _repo;
    public int SaveAttempts = 0;
    public Account LastSaved {  get; set; }

    public AccountService(IRepo repo)
    {
        _repo = repo;
    }

    public bool SaveAccount(Account account)
    {
        if(account is null || account.IsFrozen) return false;
        if (account.Balance < 30) throw new ArgumentException();
        SaveAttempts++;
        LastSaved = account;
        return _repo.Save(account);
    }

}