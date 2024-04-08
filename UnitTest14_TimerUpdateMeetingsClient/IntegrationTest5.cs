using NUnit.Framework;
using Moq;
using System;
using System.Data;

namespace IntegrationTest5
{
    [TestFixture]
    public class MainFormForClientTests
    {
        public interface IClientDataAccess
        {
            DataTable GetClientData(string login);
            void UpdateClientData(string clientId, string phoneNumber, string email, string address);
        }

        public interface IMeetingDataAccess
        {
            DataTable GetMeetingsForClient(string clientId);
            void AddMeeting(string clientId, DateTime date, DateTime time, string address, string problemDescription);
            void DeleteMeeting(string meetingId);
            void UpdateMeeting(string meetingId, string status, string assignedWorker);
        }

        public class ClientService
        {
            private readonly IClientDataAccess _clientDataAccess;
            private readonly IMeetingDataAccess _meetingDataAccess;

            public ClientService(IClientDataAccess clientDataAccess, IMeetingDataAccess meetingDataAccess)
            {
                _clientDataAccess = clientDataAccess;
                _meetingDataAccess = meetingDataAccess;
            }

            public void DeleteMeeting(string meetingId)
            {
                _meetingDataAccess.DeleteMeeting(meetingId);
            }
        }

        [Test]
        public void DeleteMeeting_Successfully()
        {
            // Arrange
            string clientId = "1";
            string selectedMeetingId = "1";

            var clientDataAccessMock = new Mock<IClientDataAccess>();
            var meetingDataAccessMock = new Mock<IMeetingDataAccess>();

            // Setup mock to return client data
            clientDataAccessMock.Setup(m => m.GetClientData(It.IsAny<string>())).Returns(GetClientData());

            // Setup mock to return meetings for client
            meetingDataAccessMock.Setup(m => m.GetMeetingsForClient(clientId)).Returns(GetMeetingsForClient());

            // Act
            var clientService = new ClientService(clientDataAccessMock.Object, meetingDataAccessMock.Object);
            clientService.DeleteMeeting(selectedMeetingId);

            // Assert
            meetingDataAccessMock.Verify(m => m.DeleteMeeting(selectedMeetingId), Times.Once);
        }

        private DataTable GetClientData()
        {
            var clientData = new DataTable();
            clientData.Columns.Add("ID_client", typeof(string));
            clientData.Columns.Add("Phone_number", typeof(string));
            clientData.Columns.Add("Email", typeof(string));
            clientData.Columns.Add("Address", typeof(string));

            var row = clientData.NewRow();
            row["ID_client"] = "1";
            row["Phone_number"] = "1234567890";
            row["Email"] = "new_email@example.com";
            row["Address"] = "New Address";
            clientData.Rows.Add(row);

            return clientData;
        }

        private DataTable GetMeetingsForClient()
        {
            var meetings = new DataTable();
            meetings.Columns.Add("ID_meeting", typeof(string));
            meetings.Columns.Add("Date", typeof(DateTime));
            meetings.Columns.Add("Time", typeof(TimeSpan));
            meetings.Columns.Add("Status", typeof(string));
            meetings.Columns.Add("Problem_description", typeof(string));
            meetings.Columns.Add("Assigned_Worker", typeof(string));

            var row = meetings.NewRow();
            row["ID_meeting"] = "1";
            row["Date"] = DateTime.Now.Date;
            row["Time"] = DateTime.Now.TimeOfDay;
            row["Status"] = "Scheduled";
            row["Problem_description"] = "Test problem description";
            row["Assigned_Worker"] = "Test Worker";
            meetings.Rows.Add(row);

            return meetings;
        }
    }
}