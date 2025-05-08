 
namespace UnitTestingProj.Bank
{
    public class AccountTests
    { 

        [Fact]
        public void TransferFundsTest_ShouldNotThrowArgumentException()
        {
            //arrange
            var account1 = new Account(100);
            var account2 = new Account(400);
            //act 
            //assert 
            Assert.Throws<ArgumentException>(() => BankingService.TransferFunds(account1, account2, -50)); 
        }

        [Fact]
        public void TransferFundsTest_ShouldNotThrowInvalidOperationException()
        {
            //arrange
            var account1 = new Account(40);
            var account2 = new Account(400);
            //act 
            //assert 
            Assert.Throws<InvalidOperationException>(() => BankingService.TransferFunds(account1, account2, 50)); 
        }

        [Fact]
        public void TransactionSavedTest_ShouldHaveSingleValue()
        {
            //arrange 
            var account = new Account(400);
            //act 
            account.Deposit(300);
            //assert 
            Assert.Single(account.Transactions);
        }

        [Fact]
        public void AccountIsFrozenTest_ShouldNotThrowInvalidOperationException()
        {
            //arrange
            var account = new Account(20); 
            //act 
            account.IsFrozen = true;
            //assert 
            Assert.True(account.IsFrozen);
        }

        [Fact]
        public void AddMontlyInterestToSavingAccountTest_ShouldBeExactAmount()
        {
            //arrange
            var account = new SavingAccount(0.02m, 200);
            //act 
            account.ApplyMonthlyInterest();
            //assert 
            Assert.Equal(204, account.Balance);
        }

        [Fact]
        public void SetMontlyInterestToSavingAccountTest_ShouldBeExactAmount()
        {
            //arrange
            var account = new SavingAccount(0.02m, 200);
            //act 
            account.SetInterest(0.04m);
            //assert 
            Assert.Equal(0.04m, account.GetInterest());
        }
    }
}
