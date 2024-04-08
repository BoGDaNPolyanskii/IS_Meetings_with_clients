using NUnit.Framework;
using Moq;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MeetingDataAccessTest
{
    public class MeetingDataAccessTests
    {
        [Test]
        public void GetMeetingsForClient_ValidClientId_ReturnsMeetings()
        {
            // Arrange
            string clientId = "testClientId";
            DataTable expectedDataTable = new DataTable();

            var mockConnection = new Mock<IDbConnection>();
            mockConnection.SetupGet(c => c.State).Returns(ConnectionState.Open);

            var meetingDataAccess = new Mock<IMeetingDataAccess>();
            meetingDataAccess.Setup(mda => mda.GetMeetingsForClient(clientId)).Returns(expectedDataTable);

            // Act
            var actualDataTable = meetingDataAccess.Object.GetMeetingsForClient(clientId);

            // Assert
            Assert.AreEqual(expectedDataTable, actualDataTable);
        }

        [Test]
        public void GetMeetingsForSupportWorker_ReturnsMeetings()
        {
            // Arrange
            DataTable expectedDataTable = new DataTable();

            var mockConnection = new Mock<IDbConnection>();
            mockConnection.SetupGet(c => c.State).Returns(ConnectionState.Open);

            var meetingDataAccess = new Mock<IMeetingDataAccess>();
            meetingDataAccess.Setup(mda => mda.GetMeetingsForSupportWorker()).Returns(expectedDataTable);

            // Act
            var actualDataTable = meetingDataAccess.Object.GetMeetingsForSupportWorker();

            // Assert
            Assert.AreEqual(expectedDataTable, actualDataTable);
        }

        [Test]
        public void AddMeeting_ValidParameters_AddsMeeting()
        {
            // Arrange
            string clientId = "testClientId";
            DateTime date = DateTime.Now.Date;
            DateTime time = DateTime.Now;
            string address = "testAddress";
            string problemDescription = "testDescription";

            var mockConnection = new Mock<IDbConnection>();
            mockConnection.SetupGet(c => c.State).Returns(ConnectionState.Open);

            var meetingDataAccess = new Mock<IMeetingDataAccess>();
            meetingDataAccess.Setup(mda => mda.AddMeeting(clientId, date, time, address, problemDescription));

            // Act
            meetingDataAccess.Object.AddMeeting(clientId, date, time, address, problemDescription);

            // Assert
            meetingDataAccess.Verify(mda => mda.AddMeeting(clientId, date, time, address, problemDescription), Times.Once);
        }

        [Test]
        public void DeleteMeeting_ValidMeetingId_DeletesMeeting()
        {
            // Arrange
            string meetingId = "testMeetingId";

            var mockConnection = new Mock<IDbConnection>();
            mockConnection.SetupGet(c => c.State).Returns(ConnectionState.Open);

            var meetingDataAccess = new Mock<IMeetingDataAccess>();
            meetingDataAccess.Setup(mda => mda.DeleteMeeting(meetingId));

            // Act
            meetingDataAccess.Object.DeleteMeeting(meetingId);

            // Assert
            meetingDataAccess.Verify(mda => mda.DeleteMeeting(meetingId), Times.Once);
        }

        [Test]
        public void UpdateMeetingStatus_ValidParameters_UpdatesStatus()
        {
            // Arrange
            int meetingId = 1;
            int workerId = 1;
            string status = "InProgress";

            var mockConnection = new Mock<IDbConnection>();
            mockConnection.SetupGet(c => c.State).Returns(ConnectionState.Open);

            var meetingDataAccess = new Mock<IMeetingDataAccess>();
            meetingDataAccess.Setup(mda => mda.UpdateMeetingStatus(meetingId, workerId, status));

            // Act
            meetingDataAccess.Object.UpdateMeetingStatus(meetingId, workerId, status);

            // Assert
            meetingDataAccess.Verify(mda => mda.UpdateMeetingStatus(meetingId, workerId, status), Times.Once);
        }
    }

    public interface IMeetingDataAccess
    {
        DataTable GetMeetingsForClient(string clientId);
        DataTable GetMeetingsForSupportWorker();
        void AddMeeting(string clientId, DateTime date, DateTime time, string address, string problemDescription);
        void DeleteMeeting(string meetingId);
        void UpdateMeetingStatus(int meetingId, int workerId, string status);
    }
}