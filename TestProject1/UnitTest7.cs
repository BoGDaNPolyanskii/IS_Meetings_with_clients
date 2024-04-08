using NUnit.Framework;
using Moq;
using System.Data.SqlClient;
using System.Data;

namespace WorkerDataAccessTest
{
    public class WorkerDataAccessTests
    {
        [Test]
        public void GetWorkerData_ValidLogin_ReturnsWorkerId()
        {
            // Arrange
            string expectedWorkerId = "1";
            string login = "testLogin";

            // ��������� ������ (mock) ��� IWorkerDataAccess
            var workerDataAccessMock = new Mock<IWorkerDataAccess>();

            // ����������� �������� ������
            workerDataAccessMock.Setup(wda => wda.GetWorkerData(login)).Returns(expectedWorkerId);

            // ��������� ��'���� ���������� � ������������� ������
            var workerLogic = new WorkerDataLogic(workerDataAccessMock.Object);

            // Act
            var actualWorkerId = workerLogic.GetWorkerId(login);

            // Assert
            Assert.AreEqual(expectedWorkerId, actualWorkerId);
        }
    }

    // ��� �����, ���� ���� �����������
    public class WorkerDataLogic
    {
        private readonly IWorkerDataAccess _workerDataAccess;

        public WorkerDataLogic(IWorkerDataAccess workerDataAccess)
        {
            _workerDataAccess = workerDataAccess;
        }

        public string GetWorkerId(string login)
        {
            return _workerDataAccess.GetWorkerData(login).ToString();
        }
    }

    // ���������, ���� ������� ���� ����������
    public interface IWorkerDataAccess
    {
        object GetWorkerData(string login);
    }
}