
using Moq;
using UnitTestingProj.Bank.Entities;
using UnitTestingProj.Bank.Repositories;
using UnitTestingProj.Bank.Services;

namespace UnitTestingProj
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
            //arrange //act  
            var account = new Account(400);
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
            Assert.Equal(204, account.Balance, 2);
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

        [Fact]
        public void SaveAccountDatabaseFailTest_ShouldReturnFalse()
        {
            // Arrange
            var mockRepo = new Mock<IRepo>();
            var testAccount = new Account(232);
            mockRepo.Setup(r => r.Save(It.IsAny<Account>())).Returns(false);
            var service = new AccountService(mockRepo.Object);
            // Act
            var result = service.SaveAccount(testAccount);
            // Assert
            Assert.False(result);
            Assert.Equal(1, service.SaveAttempts);
            Assert.Equal(testAccount, service.LastSaved);
        }

        [Fact]
        public void SaveAccountDatabaseSuccessTest_ShouldReturnTrue()
        {
            // Arrange
            var testAccount = new Account(232);
            var mockRepo = new Mock<IRepo>(); 
            var service = new AccountService(mockRepo.Object);
            // Act
            var result = service.SaveAccount(testAccount);
            // Assert
            mockRepo.Verify(x => x.Save(It.IsAny<Account>()), Times.Once);
        }

        [Fact]
        public void ValidateAccountBeforeDatabaseSuccessTest_ShouldReturnFalse()
        {
            // Arrange
            var mockRepo = new Mock<IRepo>();
            var service = new AccountService(mockRepo.Object);
            // Act
            var result = service.SaveAccount(null);
            // Assert
            Assert.False(result);
            mockRepo.Verify(r => r.Save(It.IsAny<Account>()), Times.Never); 
        }

        [Fact]
        public void SaveAccountSpecificAccount_CalledWithExactAccountInstance()
        {
            // Arrange
            var mockRepo = new Mock<IRepo>();
            var account = new Account(500);
            var service = new AccountService(mockRepo.Object);

            // Act
            service.SaveAccount(account);

            // Assert
            mockRepo.Verify(r => r.Save(account), Times.Once); 
        }

        [Fact]
        public void SaveAccountBalanceCheck_ShouldThrowArgumentException()
        {
            // Arrange
            var mockRepo = new Mock<IRepo>();
            var account = new Account(25); // balance < 30
            var service = new AccountService(mockRepo.Object);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => service.SaveAccount(account));
        }

        [Fact]
        public void SaveAccountOnce_ShouldFailIdSaveSameAccountTwice()
        {
            // Arrange
            var mockRepo = new Mock<IRepo>();
            var account = new Account(100);
            var service = new AccountService(mockRepo.Object);

            // Act
            service.SaveAccount(account);

            // Assert
            mockRepo.Verify(r => r.Save(account), Times.Once); 
        }

        [Fact]
        public void Save3AccountsAndTraceThem_ShouldTriggered3Times()
        {
            // Arrange
            var mockRepo = new Mock<IRepo>();
            var account1 = new Account(100);
            var account2 = new Account(200);
            var account3 = new Account(300);
            var service = new AccountService(mockRepo.Object);

            // Act
            service.SaveAccount(account1);
            service.SaveAccount(account2);
            service.SaveAccount(account3);

            // Assert
            Assert.Equal(3, service.SaveAttempts);
        }

        [Fact]
        public void SaveAccountCallback_ShouldSetWasSavedFlag()
        {
            // Arrange
            var mockRepo = new Mock<IRepo>();
            var service = new AccountService(mockRepo.Object);
            var wasSaved = false;

            mockRepo.Setup(r => r.Save(It.IsAny<Account>()))
                    .Callback(() => {
                        Thread.Sleep(100);
                        wasSaved = true;
                    } )
                    .Returns(true);

            // Act
            var result = service.SaveAccount(new Account(100));

            // Assert
            Assert.True(wasSaved);
            Assert.True(result);
        }

        [Fact]
        public void SaveAccountDenayedIfIsFrozen_ShouldTriggered3Times()
        {
            // Arrange
            var mockRepo = new Mock<IRepo>();
            var account = new Account(100);
            account.IsFrozen = true;
            var service = new AccountService(mockRepo.Object);

            // Act
            var result = service.SaveAccount(account);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void SaveAccountAndDepositeIsTriggered_ShouldAccountHaveOneTransaction()
        {
            // Arrange
            var mockRepo = new Mock<IRepo>();
            var account = new Account(100);
            var service = new AccountService(mockRepo.Object); 
            // Act
            var result = service.SaveAccount(account);

            // Assert
            Assert.Single(account.Transactions);
        }
    }
}
