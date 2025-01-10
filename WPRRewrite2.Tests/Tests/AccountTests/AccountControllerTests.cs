// Tests/AccountTests/AccountControllerTests.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using WPRRewrite2.Controllers;
using WPRRewrite2.DTOs;
using WPRRewrite2.Modellen.Accounts;

namespace WPRRewrite2.Tests.AccountTests
{
    [TestFixture]
    public class AccountControllerTests
    {
        private Mock<Context> _mockContext;
        private AccountController _controller;
        private Mock<DbSet<Account>> _mockAccountSet;

        [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<Context>();
            _mockAccountSet = new Mock<DbSet<Account>>();
            _mockContext.Setup(c => c.Accounts).Returns(_mockAccountSet.Object);
            _controller = new AccountController(_mockContext.Object);
        }

        [Test]
        public async Task GetAll_GeenAccounts_RetourneertNotFound()
        {
            // Arrange
            var accounts = new List<Account>().AsQueryable();
            _mockAccountSet.Setup(m => m.ToListAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Account>());

            // Act
            var resultaat = await _controller.GetAll(null, null);

            // Assert
            Assert.That(resultaat.Result, Is.InstanceOf<NotFoundObjectResult>());
            var notFoundResultaat = resultaat.Result as NotFoundObjectResult;
            Assert.That(notFoundResultaat.Value.ToString(), Contains.Substring("Er staan geen accounts in de database"));
        }

        [Test]
        public async Task Login_CorrectWachtwoord_RetourneertOk()
        {
            // Arrange
            var loginGegevens = new LoginDto("test@test.nl", "wachtwoord123");
            var account = new AccountParticulier("test@test.nl", "wachtwoord123", "Test Gebruiker", 0612345678, 1);
    
            var mockDbSet = new Mock<DbSet<Account>>();
            var accounts = new List<Account> { account }.AsQueryable();
    
            mockDbSet.As<IQueryable<Account>>().Setup(m => m.Provider).Returns(accounts.Provider);
            mockDbSet.As<IQueryable<Account>>().Setup(m => m.Expression).Returns(accounts.Expression);
            mockDbSet.As<IQueryable<Account>>().Setup(m => m.ElementType).Returns(accounts.ElementType);
            mockDbSet.As<IQueryable<Account>>().Setup(m => m.GetEnumerator()).Returns(accounts.GetEnumerator());
    
            _mockContext.Setup(c => c.Accounts).Returns(mockDbSet.Object);
    
            // Act
            var resultaat = await _controller.Login(loginGegevens);
    
            // Assert
            Assert.That(resultaat, Is.InstanceOf<OkObjectResult>());
        }
    }
}









