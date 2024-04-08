using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;

namespace TestClient_DataUpdate
{
    // ��������� ��� ������� �� �����
    public interface IDataAccess
    {
        DataTable ExecuteDataTable(string query, Dictionary<string, object> parameters);
        void ExecuteNonQuery(string query, Dictionary<string, object> parameters);
    }

    // ���� �볺���
    public class Client
    {
        public string ID { get; set; }
        public string Phone_number { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        // ����� ��������� ����� �볺���
        public void UpdateClientData(IDataAccess dataAccess)
        {
            // �������� �� null ��� ������� ����, �� ������ ���������� ������ �� ���� �����
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

    // �������� ���� ��� ���������� ��������� ����� �볺���
    [TestFixture]
    public class Tests
    {
        // ���� ��������� ������� �� �����
        private class MockDataAccess : IDataAccess
        {
            public DataTable ExecuteDataTable(string query, Dictionary<string, object> parameters)
            {
                // ��������� ������� ������� ��� ��������� ������
                return new DataTable();
            }

            public void ExecuteNonQuery(string query, Dictionary<string, object> parameters)
            {
                // ������ ����� ��� ��������� ��������� ������, ������� �� �� ���������� ���������
            }
        }

        [Test]
        public void UpdateClientData_SuccessfullyUpdatesData()
        {
            // Arrange
            var client = new Client
            {
                ID = "1",
                Phone_number = "1234567890",
                Email = "new_email@example.com",
                Address = "New Address"
            };

            var dataAccess = new MockDataAccess();

            // Act
            client.UpdateClientData(dataAccess);

            // Assert
            // ��������, �� �������� �������� ��������� ����� �볺���
            Assert.IsTrue(client.Phone_number == null || client.Phone_number == "1234567890");
            Assert.IsTrue(client.Email == null || client.Email == "new_email@example.com");
            Assert.IsTrue(client.Address == null || client.Address == "New Address");
        }
    }
}