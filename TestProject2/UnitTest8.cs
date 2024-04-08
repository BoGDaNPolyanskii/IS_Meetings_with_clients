using NUnit.Framework;
using Moq;
using System.Data;

namespace ClientDataAccessTest
{
    public class ClientDataAccessTests
    {
        [Test]
        public void GetClientData_ValidLogin_ReturnsClientData()
        {
            // Arrange
            string login = "testLogin";
            DataTable expectedDataTable = new DataTable();

            // ��������� ������ (mock) ��� IClientDataAccess
            var clientDataAccessMock = new Mock<IClientDataAccess>();

            // ����������� �������� ������
            clientDataAccessMock.Setup(cda => cda.GetClientData(login)).Returns(expectedDataTable);

            // ��������� ��'���� ���������� � ������������� ������
            var clientLogic = new ClientDataLogic(clientDataAccessMock.Object);

            // Act
            var actualDataTable = clientLogic.GetClientData(login);

            // Assert
            Assert.AreEqual(expectedDataTable, actualDataTable);
        }
    }

    // ��� �����, ���� ���� �����������
    public class ClientDataLogic
    {
        private readonly IClientDataAccess _clientDataAccess;

        public ClientDataLogic(IClientDataAccess clientDataAccess)
        {
            _clientDataAccess = clientDataAccess;
        }

        public DataTable GetClientData(string login)
        {
            return _clientDataAccess.GetClientData(login);
        }
    }
        
    public interface IClientDataAccess
    {
        DataTable GetClientData(string login);
    }
}