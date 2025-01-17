// Tests/ReserveringTests/ReserveringIntegratieTests.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WPRRewrite2.Controllers;
using WPRRewrite2.DTOs;
using WPRRewrite2.Modellen;
using WPRRewrite2.Modellen.Kar;

namespace WPRRewrite2.Tests.ReserveringTests
{
    [TestFixture]
    public class ReserveringIntegrationTests
    {
        private Context _context;
        private AccountController _accountController;
        private VoertuigController _voertuigController;
        private ReserveringController _reserveringController;
        private int _testAccountId;
        private int _testVoertuigId;

        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            // Setup in-memory database
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "ReserveringIntegrationTest")
                .Options;

            _context = new Context(options);
            
            // Setup controllers
            _accountController = new AccountController(_context);
            _voertuigController = new VoertuigController(_context);
            _reserveringController = new ReserveringController(_context);

            // Create test account
            var accountDto = new AccountDto(
                accountType: "Particulier",
                email: "test@example.com",
                wachtwoord: "Test123!",
                naam: "Test User",
                telefoonnummer: 0612345678,
                bedrijfId: 0,
                adresId: 1
            );

            var createAccountResult = await _accountController.Create(accountDto);
            var okResult = createAccountResult.Result as OkObjectResult;
            var accountResponse = okResult?.Value.GetType().GetProperty("AccountId").GetValue(okResult.Value);
            _testAccountId = (int)accountResponse;

            // Create test vehicle
            var testAuto = new Auto(
                kenteken: "TEST123",
                merk: "TestMerk",
                model: "TestModel",
                kleur: "Zwart",
                aanschafjaar: 2023,
                prijs: 25000,
                brandstofType: "Elektrisch"
            );

            _context.Voertuigen.Add(testAuto);
            await _context.SaveChangesAsync();
            _testVoertuigId = testAuto.VoertuigId;
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task CompleteReservationFlow_Success()
        {
            // Arrange
            var reserveringDto = new ReserveringDto
            {
                AccountId = _testAccountId,
                VoertuigId = _testVoertuigId,
                Begindatum = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                Einddatum = DateOnly.FromDateTime(DateTime.Now.AddDays(3)),
                VoertuigStatus = "Gereserveerd"
            };

            // Act & Assert - Check if vehicle is available
            var availableVehiclesResult = await _voertuigController.GetAll(
                startDatum: reserveringDto.Begindatum.ToDateTime(TimeOnly.MinValue),
                eindDatum: reserveringDto.Einddatum.ToDateTime(TimeOnly.MinValue)
            );
            Assert.That(availableVehiclesResult.Result, Is.TypeOf<OkObjectResult>());

            var okResult = availableVehiclesResult.Result as OkObjectResult;
            var vehicles = okResult?.Value.GetType().GetProperty("Voertuigen").GetValue(okResult.Value) as List<Voertuig>;
            Assert.That(vehicles, Is.Not.Empty);
            Assert.That(vehicles[0].VoertuigId, Is.EqualTo(_testVoertuigId));

            // Create reservation
            var reservering = new Reservering
            {
                AccountId = reserveringDto.AccountId,
                VoertuigId = reserveringDto.VoertuigId,
                Begindatum = reserveringDto.Begindatum,
                Einddatum = reserveringDto.Einddatum,
                IsGoedgekeurd = false,
                TotaalPrijs = 150.0,
                IsBetaald = false
            };

            _context.Reserveringen.Add(reservering);
            await _context.SaveChangesAsync();

            // Verify reservation is created
            var accountReserveringenResult = await _reserveringController.GetAccountReserveringen(_testAccountId);
            Assert.That(accountReserveringenResult.Result, Is.TypeOf<OkObjectResult>());

            var reserveringen = (accountReserveringenResult.Result as OkObjectResult)
                ?.Value.GetType().GetProperty("reserveringen").GetValue((accountReserveringenResult.Result as OkObjectResult).Value) 
                as List<Reservering>;

            Assert.Multiple(() =>
            {
                Assert.That(reserveringen, Is.Not.Empty);
                Assert.That(reserveringen[0].AccountId, Is.EqualTo(_testAccountId));
                Assert.That(reserveringen[0].VoertuigId, Is.EqualTo(_testVoertuigId));
                Assert.That(reserveringen[0].Begindatum, Is.EqualTo(reserveringDto.Begindatum));
                Assert.That(reserveringen[0].Einddatum, Is.EqualTo(reserveringDto.Einddatum));
            });

            // Verify vehicle is no longer available for the same period
            var unavailableVehiclesResult = await _voertuigController.GetAll(
                startDatum: reserveringDto.Begindatum.ToDateTime(TimeOnly.MinValue),
                eindDatum: reserveringDto.Einddatum.ToDateTime(TimeOnly.MinValue)
            );
            var unavailableVehicles = (unavailableVehiclesResult.Result as OkObjectResult)
                ?.Value.GetType().GetProperty("Voertuigen").GetValue((unavailableVehiclesResult.Result as OkObjectResult).Value) 
                as List<Voertuig>;

            Assert.That(unavailableVehicles, Is.Empty);
        }
    }
}