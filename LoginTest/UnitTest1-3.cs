using NUnit.Framework;

namespace LoginTest
{
    public class Tests
    {
        [Test]
        public void LoginTest_ValidLogin_SupportWorkerRole_ReturnsRole()
        {
            // Arrange
            string login = "testworkerLogin";
            string password = "testworkerPassword";
            string expectedRole = "SupportWorkerRole";

            // Act
            string actualRole = GetRole(login, password);

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

            // Act
            string actualRole = GetRole(login, password);

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

            // Act
            string actualRole = GetRole(login, password);

            // Assert
            Assert.AreEqual(expectedRole, actualRole);
        }

        // Метод для тестування
        private string GetRole(string login, string password)
        {
            // Приклад реалізації, де роль визначається за введеним логіном та паролем
            if (login == "testworkerLogin" && password == "testworkerPassword")
            {
                return "SupportWorkerRole";
            }
            else if (login == "testclientLogin" && password == "testclientPassword")
            {
                return "ClientRole";
            }
            else
            {
                return string.Empty;
            }
        }
    }
}