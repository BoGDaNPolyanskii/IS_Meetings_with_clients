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
    // Клас для роботи з базою даних
    public class WorkerDataAccess : IWorkerDataAccess
    {
        private readonly SqlConnection _connection;

        public WorkerDataAccess(SqlConnection connection)
        {
            _connection = connection;
        }

        public object GetWorkerData(string login)
        {
            try
            {
                if (_connection.State != ConnectionState.Open)
                    _connection.Open();

                string query = "SELECT ID_worker FROM Support_worker WHERE Login = @Login";

                using (SqlCommand cmd = new SqlCommand(query, _connection))
                {
                    cmd.Parameters.AddWithValue("@Login", login);

                    return cmd.ExecuteScalar();
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

            return null;
        }
    }

}
