using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;

namespace IntegrationTest1
{
    // Інтерфейс для доступу до даних
    public interface IDataAccess
    {
        DataTable ExecuteDataTable(string query, Dictionary<string, object> parameters);
        void ExecuteNonQuery(string query, Dictionary<string, object> parameters);
    }

    // Клас авторизації
    public class Authentication
    {
        // Метод авторизації клієнта
        public string Authenticate(string login, string password)
        {
            // Приклад реалізації авторизації
            if (login == "testclientLogin" && password == "testclientPassword")
            {
                return "ClientRole";
            }
            else
            {
                return string.Empty;
            }
        }
    }

    // Клас клієнта
    public class Client
    {
        public string ID { get; set; }
        public string Phone_number { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        // Метод оновлення даних клієнта
        public void UpdateClientData(IDataAccess dataAccess)
        {
            // Перевірка на null для кожного поля, та виклик відповідного запиту до бази даних
            if (Phone_number != null)
                dataAccess.ExecuteNonQuery("UPDATE Client SET Phone_number = @PhoneNumber WHERE ID_client = @ID",
                    new Dictionary<string, object> { { "@PhoneNumber", Phone_number }, { "@ID", ID } });

            if (Email != null)
                dataAccess.ExecuteNonQuery("UPDATE Client SET Email = @Email WHERE ID_client = @ID",
                    new Dictionary<string, object> { { "@Email", Email }, { "@ID", ID } });

            if (Address != null)
                dataAccess.ExecuteNonQuery("UPDATE Client SET Address = @Address WHERE ID_client = @ID",
                    new Dictionary<string, object> { { "@Address", Address }, { "@ID", ID } });
        }
    }

    // Інтеграційний тест
    [TestFixture]
    public class IntegrationTests
    {
        [Test]
        public void AuthenticateAndUpdateClientData_Successfully()
        {
            // Arrange
            var authentication = new Authentication();
            var client = new Client
            {
                ID = "1",
                Phone_number = "1234567890",
                Email = "new_email@example.com",
                Address = "New Address"
            };

            var dataAccessMock = new Mock<IDataAccess>();
            dataAccessMock.Setup(m => m.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<Dictionary<string, object>>()));

            // Act
            string role = authentication.Authenticate("testclientLogin", "testclientPassword");
            if (role == "ClientRole")
            {
                client.UpdateClientData(dataAccessMock.Object);
            }

            // Assert
            dataAccessMock.Verify(da => da.ExecuteNonQuery(
                It.Is<string>(s => s.Contains("UPDATE Client SET Phone_number")),
                It.Is<Dictionary<string, object>>(dict => dict["@PhoneNumber"].Equals("1234567890") && dict["@ID"].Equals("1"))
            ));

            dataAccessMock.Verify(da => da.ExecuteNonQuery(
                It.Is<string>(s => s.Contains("UPDATE Client SET Email")),
                It.Is<Dictionary<string, object>>(dict => dict["@Email"].Equals("new_email@example.com") && dict["@ID"].Equals("1"))
            ));

            dataAccessMock.Verify(da => da.ExecuteNonQuery(
                It.Is<string>(s => s.Contains("UPDATE Client SET Address")),
                It.Is<Dictionary<string, object>>(dict => dict["@Address"].Equals("New Address") && dict["@ID"].Equals("1"))
            ));
        }
    }
}