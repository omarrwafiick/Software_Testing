
using BankApplicationApi.Data;
using BankApplicationApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using UnitTestingProj.Bank.Entities; 
namespace BankApplicationApi.Repositories
{
    public class Repo: IRepo
    {
        private readonly ApplicationDbContext _context;
        public Repo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Account> Get(Guid id)
        {
            var result = await _context.Accounts.FindAsync(id);
            return result is null ? null : result;
        }

        public async Task<List<Account>> Get()
        {
            var result = await _context.Accounts.ToListAsync();
            return !result.Any() ? null : result;
        }

        public async Task<bool> Save(Account account)
        { 
            var newAccount = await _context.Accounts.AddAsync(account);
            var result = await _context.SaveChangesAsync();
            return result > 0 ;
        }

        public bool Update(Account account, decimal balance)
        { 
            account.SetBalance(balance);
            _context.Accounts.Update(account);
            var result = _context.SaveChanges();
            return result > 0;
        }

        public bool Delete(Account account)
        { 
            _context.Accounts.Remove(account);
            var result = _context.SaveChanges();
            return result > 0;
        }

    }
}
