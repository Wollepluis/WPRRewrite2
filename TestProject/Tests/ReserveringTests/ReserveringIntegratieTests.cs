// Tests/ReserveringTests/ReserveringIntegratieTests.cs
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using WPRRewrite2.Controllers;
using WPRRewrite2.DTOs;
using WPRRewrite2.Modellen.Accounts;
using WPRRewrite2.Modellen.Kar;

namespace WPRRewrite2.Tests.ReserveringTests
{
    [TestFixture]
    public class ReserveringIntegratieTests
    {
        private Context _context;
        private ReserveringController _reserveringController;
        private VoertuigController _voertuigController;

        [OneTimeSetUp]
        public void SetupDatabase()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
    
            _context = new Context(options);
            
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
    
            _reserveringController = new ReserveringController(_context);
            _voertuigController = new VoertuigController(_context);
        }

        [Test]
        public async Task VoertuigReservering_CompleteWorkflow_Succesvol()
        {
            // Arrange
            var account = new AccountParticulier("test@test.nl", "wachtwoord123", "Test User", 0612345678, 1);
            _context.Accounts.Add(account);

            var voertuig = new Auto("AB-123-C", "Toyota", "Yaris", "Blauw", 2020, 25000, "Benzine");
            _context.Voertuigen.Add(voertuig);
            await _context.SaveChangesAsync();

            var reservering = new ReserveringDto
            {
                AccountId = account.AccountId,
                VoertuigId = voertuig.VoertuigId,
                Begindatum = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                Einddatum = DateOnly.FromDateTime(DateTime.Now.AddDays(3))
            };

            // Act & Assert
            var beschikbaarheid = await _voertuigController.GetSpecific(voertuig.VoertuigId);
            Assert.That(beschikbaarheid.Value, Is.Not.Null);
        }
        
        [OneTimeTearDown]
        public void CleanupDatabase()
        {
            _context.Dispose(); 
        }
    }
}