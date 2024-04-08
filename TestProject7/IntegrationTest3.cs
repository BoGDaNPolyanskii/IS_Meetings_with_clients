using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;

namespace IntegrationTest1
{
    public interface IClientDataAccess
    {
        DataTable GetClientData(string login);
        void UpdateClientData(string idClient, string phoneNumber, string email, string address);
    }

    public class Client
    {
        public string ID { get; set; }
        public string Phone_number { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public bool IsAuthenticated { get; private set; }

        private readonly string login;
        private readonly string password;
        private readonly IClientDataAccess dataAccess;

        public Client(string login, string password, IClientDataAccess dataAccess)
        {
            this.login = login;
            this.password = password;
            this.dataAccess = dataAccess;
        }

        public void Authenticate()
        {
            // Assuming there's a predefined set of credentials
            string validLogin = "testclientLogin";
            string validPassword = "testclientPassword";

            // Check if the provided login and password match the valid credentials
            if (login == validLogin && password == validPassword)
            {
                IsAuthenticated = true;
            }
            else
            {
                IsAuthenticated = false;
            }
        }

        public void LoadClientData()
        {
            // Load client data from the data access layer
            DataTable dataTable = dataAccess.GetClientData(login);
            if (dataTable.Rows.Count > 0)
            {
                ID = dataTable.Rows[0]["ID"].ToString();
                Phone_number = dataTable.Rows[0]["Phone_number"].ToString();
                Email = dataTable.Rows[0]["Email"].ToString();
                Address = dataTable.Rows[0]["Address"].ToString();
            }
        }

        public void UpdateClientData()
        {
            // Update client data in the data access layer
            dataAccess.UpdateClientData(ID, Phone_number, Email, Address);
        }
    }

    [TestFixture]
    public class IntegrationTests
    {
        private Mock<IClientDataAccess> clientDataAccessMock;
        private Client client;

        [SetUp]
        public void Setup()
        {
            clientDataAccessMock = new Mock<IClientDataAccess>();
            client = new Client("testclientLogin", "testclientPassword", clientDataAccessMock.Object);
        }

        [Test]
        public void Authenticate_Successfully()
        {
            // Arrange
            clientDataAccessMock.Setup(m => m.GetClientData("testclientLogin")).Returns(GetClientData());

            // Act
            client.Authenticate();

            // Assert
            Assert.IsTrue(client.IsAuthenticated);
        }

        [Test]
        public void LoadClientData_Successfully()
        {
            // Arrange
            clientDataAccessMock.Setup(m => m.GetClientData("testclientLogin")).Returns(GetClientData());

            // Act
            client.Authenticate();
            client.LoadClientData();

            // Assert
            Assert.AreEqual("1", client.ID);
            Assert.AreEqual("1234567890", client.Phone_number);
            Assert.AreEqual("new_email@example.com", client.Email);
            Assert.AreEqual("New Address", client.Address);
        }

        [Test]
        public void UpdateClientData_Successfully()
        {
            // Arrange
            clientDataAccessMock.Setup(m => m.GetClientData("testclientLogin")).Returns(GetClientData());

            // Act
            client.Authenticate();
            client.LoadClientData();
            client.Phone_number = "9876543210";
            client.Email = "updated_email@example.com";
            client.Address = "Updated Address";
            client.UpdateClientData();

            // Assert
            clientDataAccessMock.Verify(m => m.UpdateClientData(
                It.Is<string>(id => id == "1"),
                It.Is<string>(phoneNumber => phoneNumber == "9876543210"),
                It.Is<string>(email => email == "updated_email@example.com"),
                It.Is<string>(address => address == "Updated Address")
            ));
        }

        private DataTable GetClientData()
        {
            // Method to model client data
            var dataTable = new DataTable();
            dataTable.Columns.Add("ID", typeof(string));
            dataTable.Columns.Add("Phone_number", typeof(string));
            dataTable.Columns.Add("Email", typeof(string));
            dataTable.Columns.Add("Address", typeof(string));

            dataTable.Rows.Add("1", "1234567890", "new_email@example.com", "New Address");
            return dataTable;
        }
    }
}