// Tests/ReserveringTests/ReserveringControllerTests.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using WPRRewrite2.Controllers;
using WPRRewrite2.Modellen;

namespace WPRRewrite2.Tests.ReserveringTests
{
    [TestFixture]
    public class ReserveringControllerTests
    {
        private Mock<Context> _mockContext;
        private ReserveringController _controller;

        [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<Context>();
            _controller = new ReserveringController(_mockContext.Object);
        }

        [Test]
        public async Task GetAll_GeenReserveringen_RetourneertNotFound()
        {
            // Arrange
            var mockReserveringen = new Mock<DbSet<Reservering>>();
            _mockContext.Setup(c => c.Reserveringen).Returns(mockReserveringen.Object);
            mockReserveringen.Setup(m => m.ToListAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Reservering>());

            // Act
            var resultaat = await _controller.GetAll();

            // Assert
            Assert.That(resultaat.Result, Is.InstanceOf<NotFoundObjectResult>());
        }
    }
}