using BankApplicationApi.Controllers;
using BankApplicationApi.Data;
using BankApplicationApi.Repositories; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore;
using UnitTestingProj.Bank.Entities;

namespace IntegrationTesting
{
    public class BankingSystemTest
    {
        private const string ConnectionString = "Server=DESKTOP-BAUHCE7\\SQLEXPRESS;Database=BankingDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True";

        private async Task<Account> CreateGlobalAsync()
        {
            var account = new Account(Guid.NewGuid(), 1200);
            var (controller, context) = MakeSetUp();
            await context.Accounts.AddAsync(account);
            await context.SaveChangesAsync();
            return account;
        }

        private (BankController controller, ApplicationDbContext context) MakeSetUp()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(ConnectionString);

            var context = new ApplicationDbContext(optionsBuilder.Options);
            var repo = new Repo(context);
            var controller = new BankController(repo);

            return (controller, context);
        }

        [Fact]
        public async Task AccountControllerCreateAccountsAllFromDatabase_ShouldReturnOk()
        {
            // Arrange
            var (controller, context) = MakeSetUp();
            // Act
            var result = await controller.Create(new Account(1400));
            var okResult = Assert.IsType<OkResult>(result);
            // Assert
            Assert.IsType<OkResult>(okResult);
            // Clean up
            context.Dispose();
        }

        [Fact]
        public async Task AccountControllerGetAccountsAllFromDatabase_ShouldReturnOk()
        {
            // Arrange
            var (controller, context) = MakeSetUp();
            await CreateGlobalAsync();
            // Act
            var result = await controller.GetAll();
            var okResult = Assert.IsType<OkObjectResult>(result); 
            // Assert
            Assert.IsType<OkObjectResult>(okResult);  
            // Clean up
            context.Dispose();
        }

        [Fact]
        public async Task AccountControllerGetAccountByIdFromDatabase_ShouldReturnTrueAndOk()
        {
            // Arrange
            var (controller, context) = MakeSetUp();
            var newAccount = await CreateGlobalAsync();
            // Act
            var result = await controller.GetById(newAccount.AccountNumber);
            var okResult = Assert.IsType<OkObjectResult>(result); 
            // Assert  
            Assert.IsType<OkObjectResult>(okResult);
            // Clean up
            context.Dispose();
        }

        [Fact]
        public async Task AccountControllerUpdateAccountByIdFromDatabase_ShouldReturnOk()
        {
            // Arrange
            var (controller, context) = MakeSetUp();
            var newAccount = await CreateGlobalAsync();
            // Act
            var result = await controller.Update(newAccount, 1300);
            var okResult = Assert.IsType<OkResult>(result);
            // Assert 
            Assert.IsType<OkResult>(okResult);

            // Clean up
            context.Dispose();
        }
        [Fact]  
        public async Task AccountControllerDeleteAccountByIdFromDatabase_ShouldReturnOk()
        {
            // Arrange 
            var (controller, context) = MakeSetUp();
            var newAccount = await CreateGlobalAsync();
            // Act 
            var deleteResult = await controller.Delete(newAccount);
            // Assert
            Assert.IsType<OkResult>(deleteResult);
            // Clean up
            context.Dispose();
        }  
    }
}
