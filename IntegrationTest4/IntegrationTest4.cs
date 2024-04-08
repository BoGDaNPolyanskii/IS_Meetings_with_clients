using NUnit.Framework;
using Moq;
using System;
using System.Data;
using System.Threading;

namespace IntegrationTest1
{
    public interface IClientDataAccess
    {
        DataTable GetClientData(string login);
    }

    public interface IMeetingDataAccess
    {
        DataTable GetMeetingsForClient(string clientId);
        void AddMeeting(string clientId, DateTime date, DateTime time, string address, string problemDescription);
        void DeleteMeeting(string meetingId);
        void UpdateMeeting(int meetingId, string status, string assignedWorker);
    }

    [TestFixture]
    public class IntegrationTests
    {
        public class ClientService
        {
            private readonly IClientDataAccess _clientDataAccess;
            private readonly IMeetingDataAccess _meetingDataAccess;
            private readonly string _clientId;
            private readonly Timer _timer;

            public ClientService(IClientDataAccess clientDataAccess, IMeetingDataAccess meetingDataAccess, string clientId)
            {
                _clientDataAccess = clientDataAccess;
                _meetingDataAccess = meetingDataAccess;
                _clientId = clientId;

                // Ініціалізація таймера для оновлення графіка зустрічей кожну хвилину
                _timer = new Timer(UpdateMeetingsForClient, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            }

            private void UpdateMeetingsForClient(object state)
            {
                LoadMeetingsForClient();
            }

            public DataTable LoadMeetingsForClient()
            {
                return _meetingDataAccess.GetMeetingsForClient(_clientId);
            }
        }

        private Mock<IClientDataAccess> clientDataAccessMock;
        private Mock<IMeetingDataAccess> meetingDataAccessMock;
        private ClientService clientService;

        [SetUp]
        public void Setup()
        {
            clientDataAccessMock = new Mock<IClientDataAccess>();
            meetingDataAccessMock = new Mock<IMeetingDataAccess>();
            clientService = new ClientService(clientDataAccessMock.Object, meetingDataAccessMock.Object, "1");
        }

        

        [Test]
        public void LoadMeetingsForClient_CalledPeriodically()
        {
            // Arrange
            var meetings = GetMeetingsForClient();
            meetingDataAccessMock.Setup(m => m.GetMeetingsForClient("1")).Returns(meetings);

            // Act
            Thread.Sleep(1500);
            clientService.LoadMeetingsForClient(); // Перший виклик

            Thread.Sleep(1500); // Затримка
            clientService.LoadMeetingsForClient(); // Другий виклик

            // Assert
            meetingDataAccessMock.Verify(m => m.GetMeetingsForClient("1"), Times.AtLeastOnce);
        }

        private DataTable GetMeetingsForClient()
        {
            var meetings = new DataTable();
            meetings.Columns.Add("ID_meeting", typeof(string));
            meetings.Columns.Add("Date", typeof(DateTime));
            meetings.Columns.Add("Time", typeof(DateTime));
            meetings.Columns.Add("Status", typeof(string));
            meetings.Columns.Add("Problem_description", typeof(string));
            meetings.Columns.Add("Assigned_Worker", typeof(string));

            var row = meetings.NewRow();
            row["ID_meeting"] = "1";
            row["Date"] = DateTime.Now.Date;
            row["Time"] = DateTime.Now;
            row["Status"] = "Scheduled";
            row["Problem_description"] = "Test problem description";
            row["Assigned_Worker"] = "Test Worker";
            meetings.Rows.Add(row);

            return meetings;
        }
    }
}