// Tests/SecurityTests/InputValidationTests.cs

using System.Net.Http.Json;
using NUnit.Framework;
using WPRRewrite2.DTOs;

namespace WPRRewrite2.Tests.SecurityTests
{
    [TestFixture]
    public class InputValidationTests
    {
        private HttpClient _client;
        private string _baseUrl = "https://localhost:5001/api/";

        [SetUp]
        public void Setup()
        {
            _client = new HttpClient();
        }

        [Test]
        public async Task TestSQLInjection_ZouMoetenFalen()
        {
            // Arrange
            var maliciousEmail = "' OR '1'='1";
            var loginData = new LoginDto(maliciousEmail, "wachtwoord");

            // Act
            var response = await _client.PostAsJsonAsync(_baseUrl + "Accounts/Login", loginData);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Unauthorized));
        }

        [Test]
        public async Task TestXSS_ZouMoetenFalen()
        {
            // Arrange
            var xssPayload = "<script>alert('xss')</script>";
            var accountData = new AccountDto(
                "Particulier",
                xssPayload + "@test.nl",
                "wachtwoord123",
                xssPayload,
                0612345678,
                0,
                1
            );

            // Act
            var response = await _client.PostAsJsonAsync(_baseUrl + "Accounts/Registreer", accountData);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.BadRequest));
        }
        
        [TearDown]
        public void TearDown()
        {
            _client.Dispose(); 
        }
    }
}