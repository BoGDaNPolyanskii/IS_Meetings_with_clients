using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IS_Meetings_with_clients
{
    public class MeetingDataAccess : IMeetingDataAccess
    {
        private readonly SqlConnection _connection;

        public MeetingDataAccess(SqlConnection connection)
        {
            _connection = connection;
        }

        public DataTable GetMeetingsForClient(string clientId)
        {
            DataTable dataTable = new DataTable();
            try
            {
                if (_connection.State != ConnectionState.Open)
                    _connection.Open();

                string query = @"SELECT 
                                    Meetings.ID_meeting, 
                                    Meetings.Date, 
                                    Meetings.Time, 
                                    Meetings.Status, 
                                    Meetings.Problem_description, 
                                    Support_worker.Worker_name AS Assigned_Worker 
                                FROM 
                                    Meetings 
                                LEFT JOIN 
                                    Support_worker ON Meetings.ID_worker = Support_worker.ID_worker 
                                WHERE 
                                    Meetings.ID_client = @ID_client";

                using (SqlCommand cmd = new SqlCommand(query, _connection))
                {
                    cmd.Parameters.AddWithValue("@ID_client", clientId);

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    dataAdapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }
            return dataTable;
        }

        public DataTable GetMeetingsForSupportWorker()
        {
            DataTable dataTable = new DataTable();
            try
            {
                if (_connection.State != ConnectionState.Open)
                    _connection.Open();

                string query = @"SELECT 
                                Meetings.ID_meeting, 
                                Meetings.Date, 
                                Meetings.Time, 
                                Client.Address, 
                                Client.Full_name, 
                                Client.Phone_number, 
                                Meetings.Problem_description, 
                                Meetings.Status, 
                                Support_worker.Worker_name AS Assigned_Worker 
                            FROM 
                                Meetings 
                            INNER JOIN 
                                Client ON Meetings.ID_client = Client.ID_client 
                            LEFT JOIN 
                                Support_worker ON Meetings.ID_worker = Support_worker.ID_worker 
                            ORDER BY 
                                Meetings.Date, Meetings.Time";

                using (SqlCommand cmd = new SqlCommand(query, _connection))
                {
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    dataAdapter.Fill(dataTable);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dataTable;
        }

        public void AddMeeting(string clientId, DateTime date, DateTime time, string address, string problemDescription)
        {
            try
            {
                if (_connection.State != ConnectionState.Open)
                    _connection.Open();

                // Перевірка, чи вже існує зустріч для цього користувача на ту саму дату та годину
                string checkQuery = @"SELECT COUNT(*) FROM Meetings WHERE ID_client = @ID_client AND CONVERT(DATE, Date) = @Date AND DATEPART(HOUR, Time) = DATEPART(HOUR, @Time)";

                using (SqlCommand checkCmd = new SqlCommand(checkQuery, _connection))
                {
                    checkCmd.Parameters.AddWithValue("@ID_client", clientId);
                    checkCmd.Parameters.AddWithValue("@Date", date.Date);
                    checkCmd.Parameters.AddWithValue("@Time", time);

                    int existingMeetingsCount = (int)checkCmd.ExecuteScalar();

                    if (existingMeetingsCount > 0)
                    {
                        MessageBox.Show("На цю дату та годину у вас вже існує зустріч.", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                string insertQuery = @"INSERT INTO Meetings (ID_client, Date, Time, Address, Problem_description) 
                                    VALUES (@ID_client, @Date, @Time, @Address, @Problem_description)";

                using (SqlCommand insertCmd = new SqlCommand(insertQuery, _connection))
                {
                    insertCmd.Parameters.AddWithValue("@ID_client", clientId);
                    insertCmd.Parameters.AddWithValue("@Date", date.Date);
                    insertCmd.Parameters.AddWithValue("@Time", time.TimeOfDay);
                    insertCmd.Parameters.AddWithValue("@Address", address);
                    insertCmd.Parameters.AddWithValue("@Problem_description", problemDescription);

                    insertCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }
        }

        public void DeleteMeeting(string meetingId)
        {
            try
            {
                if (_connection.State != ConnectionState.Open)
                    _connection.Open();

                string deleteQuery = "DELETE FROM Meetings WHERE ID_meeting = @ID_meeting";

                using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, _connection))
                {
                    deleteCmd.Parameters.AddWithValue("@ID_meeting", meetingId);

                    deleteCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }
        }
        public void UpdateMeetingStatus(int meetingID, int workerID, string status)
        {
            try
            {
                if (_connection.State != ConnectionState.Open)
                    _connection.Open();
                string updateQuery = @"UPDATE Meetings 
                                    SET Status = @Status, ID_worker = @ID_worker 
                                    WHERE ID_meeting = @ID_meeting";

                using (SqlCommand updateCmd = new SqlCommand(updateQuery, _connection))
                {
                    updateCmd.Parameters.AddWithValue("@Status", status);
                    updateCmd.Parameters.AddWithValue("@ID_worker", workerID);
                    updateCmd.Parameters.AddWithValue("@ID_meeting", meetingID);

                    int rowsAffected = updateCmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Meeting data updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to update meeting data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
