using System;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using IS_Meetings_with_clients;

namespace IS_Meetings_with_clients
{
    public partial class Login_form : Form
    {
        private SqlConnection connection;

        public Login_form()
        {
            InitializeComponent();
            connection = new SqlConnection(GetConnectionString());
        }

        // Клас авторизації
        public static class AuthorizationService
        {
            // Отримати роль користувача за номером телефону
            public static string GetRole(SqlConnection connection, string phoneNumber)
            {
                string querySupportWorker = @"
                    SELECT 'SupportWorkerRole'
                    FROM Support_worker SW
                    JOIN [User] U ON U.ID_user = SW.ID_user
                    WHERE U.Phone_number = @PhoneNumber";

                string queryClient = @"
                    SELECT 'ClientRole'
                    FROM Client C
                    JOIN [User] U ON U.ID_user = C.ID_user
                    WHERE U.Phone_number = @PhoneNumber";

                using (SqlCommand cmd = new SqlCommand(querySupportWorker, connection))
                {
                    cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                        return result.ToString();
                }

                using (SqlCommand cmd = new SqlCommand(queryClient, connection))
                {
                    cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                        return result.ToString();
                }

                return string.Empty;
            }

            // Отримати ID_user за номером телефону
            public static Guid? GetUserId(SqlConnection connection, string phoneNumber)
            {
                string query = "SELECT ID_user FROM [User] WHERE Phone_number = @PhoneNumber";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                        return (Guid)result;
                }
                return null;
            }
        }

        // Авторизація
        private void Join_Click(object sender, EventArgs e)
        {
            string phoneNumber = loginBox.Text.Trim(); // Використовуємо номер телефону

            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                string role = AuthorizationService.GetRole(connection, phoneNumber);
                Guid? userId = AuthorizationService.GetUserId(connection, phoneNumber);

                if (string.IsNullOrEmpty(role) || userId == null)
                {
                    MessageBox.Show("Невірний номер телефону або користувача не знайдено.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    OpenMainForm(role, userId.Value);
                    Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        // Відкриття відповідної форми
        private void OpenMainForm(string role, Guid userId)
        {
            Form mainForm = null;

            if (role == "SupportWorkerRole")
            {
                mainForm = new Main_form_for_SupportWorker(userId);
            }
            else if (role == "ClientRole")
            {
                mainForm = new Main_form_for_Client(userId);
            }

            mainForm?.Show();
        }

        // Отримання рядка підключення
        private string GetConnectionString()
        {
            string connectionFile = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "DBConnection");
            return File.ReadAllText(connectionFile);
        }

        private void LeaveButton_Click(object sender, EventArgs e)
        {
            Close();
            Application.Exit();
        }

        private void ExitButton2_Click(object sender, EventArgs e)
        {
            Close();
            Application.Exit();
        }
    }
}