using BankApplicationApi.Interfaces; 
using Microsoft.AspNetCore.Mvc;
using UnitTestingProj.Bank.Entities;

namespace BankApplicationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly IRepo _repo;
        public BankController(IRepo repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var accounts = await _repo.Get();
            return !accounts.Any() ? NotFound() : Ok(accounts.Select( x => x.AccountNumber));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var account = await _repo.Get(id);

            return account is null ? NotFound() : Ok(account.AccountNumber);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Account account)
        {
            var result = await _repo.Save(account); 
            return !result ? BadRequest("Failed to save account.") : Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Account account, decimal balance)
        {  
            var result = _repo.Update(account, balance); 
            return !result ? NotFound() : Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Account account)
        { 
            var result = _repo.Delete(account); 
            return !result ? NotFound() : Ok();
        }

    }
}
