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
    public class ClientDataAccess : IClientDataAccess
    {
        private readonly SqlConnection _connection;

        public ClientDataAccess(SqlConnection connection)
        {
            _connection = connection;
        }

        public DataTable GetClientData(string login)
        {
            DataTable dataTable = new DataTable();
            try
            {
                if (_connection.State != ConnectionState.Open)
                    _connection.Open();

                string query = @"SELECT ID_client, Phone_number, Email, Address FROM Client WHERE Login = @Login";

                using (SqlCommand cmd = new SqlCommand(query, _connection))
                {
                    cmd.Parameters.AddWithValue("@Login", login);

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

        public void UpdateClientData(string idClient, string phoneNumber, string email, string address)
        {
            try
            {
                if (_connection.State != ConnectionState.Open)
                    _connection.Open();

                string updateQuery = "UPDATE Client SET Phone_number = @PhoneNumber, Email = @Email, Address = @Address WHERE ID_client = @ID";

                using (SqlCommand updateCmd = new SqlCommand(updateQuery, _connection))
                {
                    updateCmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    updateCmd.Parameters.AddWithValue("@Email", email);
                    updateCmd.Parameters.AddWithValue("@Address", address);
                    updateCmd.Parameters.AddWithValue("@ID", idClient);

                    int rowsAffected = updateCmd.ExecuteNonQuery();


                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Дані успішно оновлено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Не вдалося оновити дані.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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
    }
}
