using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS_Meetings_with_clients
{
    public interface IClientDataAccess
    {
        DataTable GetClientData(string login);
        void UpdateClientData(string idClient, string phoneNumber, string email, string address);
    }

    public interface IMeetingDataAccess
    {
        DataTable GetMeetingsForClient(string clientId);
        DataTable GetMeetingsForSupportWorker();

        void AddMeeting(string clientId, DateTime date, DateTime time, string address, string problemDescription);
        void DeleteMeeting(string meetingId);
        void UpdateMeetingStatus(int meetingID, int workerID, string status);
    }

    public interface IWorkerDataAccess
    {
        object GetWorkerData(string login);
    }
}
