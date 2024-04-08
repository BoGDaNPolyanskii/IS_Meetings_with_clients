using NUnit.Framework;
using Moq;

namespace LoginTest
{
    public class IntegrationTests
    {
        [Test]
        public void LoginTest_ValidLogin_SupportWorkerRole_ReturnsRole()
        {
            // Arrange
            string login = "testworkerLogin";
            string password = "testworkerPassword";
            string expectedRole = "SupportWorkerRole";

            // Create mock database
            var mockDatabase = new Mock<IDatabase>();
            mockDatabase.Setup(db => db.GetRole(login, password)).Returns("SupportWorkerRole");

            // Create authentication module with dependency on database module
            var authentication = new Authentication(mockDatabase.Object);

            // Act
            string actualRole = authentication.GetRole(login, password);

            // Assert
            Assert.AreEqual(expectedRole, actualRole);
        }

        [Test]
        public void LoginTest_InvalidLogin_ReturnsEmptyRole()
        {
            // Arrange
            string login = "invalidLogin";
            string password = "invalidPassword";
            string expectedRole = string.Empty;

            // Create mock database
            var mockDatabase = new Mock<IDatabase>();
            mockDatabase.Setup(db => db.GetRole(login, password)).Returns(string.Empty);

            // Create authentication module with dependency on database module
            var authentication = new Authentication(mockDatabase.Object);

            // Act
            string actualRole = authentication.GetRole(login, password);

            // Assert
            Assert.AreEqual(expectedRole, actualRole);
        }

        [Test]
        public void LoginTest_ValidLogin_Client_ReturnsRole()
        {
            // Arrange
            string login = "testclientLogin";
            string password = "testclientPassword";
            string expectedRole = "ClientRole";

            // Create mock database
            var mockDatabase = new Mock<IDatabase>();
            mockDatabase.Setup(db => db.GetRole(login, password)).Returns("ClientRole");

            // Create authentication module with dependency on database module
            var authentication = new Authentication(mockDatabase.Object);

            // Act
            string actualRole = authentication.GetRole(login, password);

            // Assert
            Assert.AreEqual(expectedRole, actualRole);
        }
    }

    public interface IDatabase
    {
        string GetRole(string login, string password);
    }

    public class Authentication
    {
        private readonly IDatabase _database;

        public Authentication(IDatabase database)
        {
            _database = database;
        }

        public string GetRole(string login, string password)
        {
            // Interaction with database module to get role
            return _database.GetRole(login, password);
        }
    }
}