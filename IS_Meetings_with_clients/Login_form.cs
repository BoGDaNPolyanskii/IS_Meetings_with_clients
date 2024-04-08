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

        public static class AuthorizationService
        {
            // отримати роль
            public static string GetRole(SqlConnection connection, string login, string password)
            {
                string querySupportWorker = "SELECT 'SupportWorkerRole' AS Role FROM Support_worker WHERE Login = @Login AND Password = @Password";
                string queryClient = "SELECT 'ClientRole' AS Role FROM Client WHERE Login = @Login AND Password = @Password";

                using (SqlCommand cmd = new SqlCommand(querySupportWorker, connection))
                {
                    cmd.Parameters.AddWithValue("@Login", login);
                    cmd.Parameters.AddWithValue("@Password", password);
                    var role = cmd.ExecuteScalar();
                    if (role != null)
                        return role.ToString();  // конвертувати роль в рядок
                }

                using (SqlCommand cmd = new SqlCommand(queryClient, connection))
                {
                    cmd.Parameters.AddWithValue("@Login", login);
                    cmd.Parameters.AddWithValue("@Password", password);
                    var role = cmd.ExecuteScalar();
                    if (role != null)
                        return role.ToString();  // конвертувати роль в рядок
                }

                return string.Empty;
            }
        }

        // закриття програми
        private void LeaveButton_Click(object sender, EventArgs e)
        {
            Close();
            Application.Exit();
        }

        // авторизація
        private void button1_Click(object sender, EventArgs e)
        {
            // string connectionString = GetConnectionString();

            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                string login = loginBox.Text;
                string password = passBox.Text;

                string role = AuthorizationService.GetRole(connection, login, password);

                if (string.IsNullOrEmpty(role))
                {
                    MessageBox.Show("Invalid login or password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    OpenMainForm(role, connection, login);
                    Hide();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        // відкриття відповідної головної форми в залежності від ролі
        private void OpenMainForm(string role, SqlConnection connection, string login)
        {
            Form mainForm = null;

            if (role.Equals("SupportWorkerRole"))
            {
                mainForm = new Main_form_for_SupportWorker(new WorkerDataAccess(connection), new MeetingDataAccess(connection), login);
            }
            else if (role.Equals("ClientRole"))
            {
                mainForm = new Main_form_for_Client(new ClientDataAccess(connection), new MeetingDataAccess(connection), login);
            }

            if (mainForm != null)
            {
                mainForm.Show();
            }
        }

        // отримання рядка з'єднання з файлу
        private string GetConnectionString()
        {
            string connectionFile = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\DBConnection";
            return File.ReadAllText(connectionFile);
        }

        // закриття програми
        private void ExitButton2_Click(object sender, EventArgs e)
        {
            Close();
            Application.Exit();
        }
    }
}