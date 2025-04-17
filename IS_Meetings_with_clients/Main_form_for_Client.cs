using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace IS_Meetings_with_clients
{ 
    public partial class Main_form_for_Client : Form
    {
        private SqlConnection connection;
        private readonly string login;
        private readonly string clientID;
        private Timer scheduleTimer;

        public Main_form_for_Client(SqlConnection connection, string login, Guid userId)
        {
            InitializeComponent();
            this.connection = connection;
            this.login = login;
            this.clientID = userId.ToString();

            LoadUserData();
            LoadMeetingsForClient();
            LoadScheduledMeetings();
            LoadRepairTypes();

            // Ініціалізація таймера
            scheduleTimer = new Timer();
            scheduleTimer.Interval = 60000; // Оновлення кожні 60 секунд (60000 мілісекунд)
            scheduleTimer.Tick += ScheduleTimer_Tick;
            scheduleTimer.Start();
        }

        private void Save_changed_client_data_Click(object sender, EventArgs e)
        {
            string email = textBox3.Text;
            string address = richTextBox3.Text;

            // Оновлення email в таблиці [User]
            using (SqlCommand updateEmailCmd = new SqlCommand("UPDATE [User] SET Email = @Email WHERE ID_user = @id", connection))
            {
                updateEmailCmd.Parameters.AddWithValue("@Email", email);
                updateEmailCmd.Parameters.AddWithValue("@id", clientID);
                updateEmailCmd.ExecuteNonQuery();
            }

            // Оновлення адреси в таблиці Client
            using (SqlCommand updateAddressCmd = new SqlCommand("UPDATE Client SET Address = @Address WHERE ID_user = @id", connection))
            {
                updateAddressCmd.Parameters.AddWithValue("@Address", address);
                updateAddressCmd.Parameters.AddWithValue("@id", clientID);
                updateAddressCmd.ExecuteNonQuery();
            }

            MessageBox.Show("Дані успішно оновлено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Метод, який викликається під час спрацьовування таймера для оновлення даних, наприклад графіка візитів
        private void ScheduleTimer_Tick(object sender, EventArgs e)
        {
            LoadMeetingsForClient();
            LoadUserData();
            LoadScheduledMeetings();
        }
        private void LoadUserData()
        {
            ID_client_textBox.Text = clientID;

            // Завантаження email з таблиці [User]
            using (SqlCommand emailCmd = new SqlCommand("SELECT Email FROM [User] WHERE ID_user = @id", connection))
            {
                emailCmd.Parameters.AddWithValue("@id", clientID);
                var emailResult = emailCmd.ExecuteScalar();
                if (emailResult != null)
                    textBox3.Text = emailResult.ToString();
            }

            // Завантаження адреси з таблиці Client
            using (SqlCommand addressCmd = new SqlCommand("SELECT Address FROM Client WHERE ID_user = @id", connection))
            {
                addressCmd.Parameters.AddWithValue("@id", clientID);
                var addressResult = addressCmd.ExecuteScalar();
                if (addressResult != null)
                    richTextBox3.Text = addressResult.ToString();
                    richTextBox4.Text = addressResult.ToString();
            }
        }

        private void LoadRepairTypes()
        {
            string query = "SELECT ID_repair_type, Repair_name, Description_of_types FROM Repair_type";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    DataTable repairTypes = new DataTable();
                    repairTypes.Load(reader);

                    comboBox2.DataSource = repairTypes;
                    comboBox2.DisplayMember = "Repair_name";
                    comboBox2.ValueMember = "ID_repair_type";
                }
            }
        }

        private void Add_meeting_Click(object sender, EventArgs e)
        {
            // Об'єднання дати та часу
            DateTime selectedDate = dateTimePicker4.Value.Date;
            TimeSpan selectedTime = dateTimePicker3.Value.TimeOfDay;
            DateTime requestDateTime = selectedDate + selectedTime;

            string address = richTextBox4.Text.Trim();
            string problemDescription = richTextBox5.Text.Trim();

            if (comboBox2.SelectedValue == null)
            {
                MessageBox.Show("Будь ласка, оберіть тип ремонту.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Guid repairTypeId = (Guid)comboBox2.SelectedValue;

            // Перевірка на дублювання заявки на той самий час
            string duplicateCheckQuery = @"
            SELECT COUNT(*) FROM Repair_request
            WHERE ID_user = @UserId AND Request_date = @RequestDate";

            using (SqlCommand duplicateCheckCmd = new SqlCommand(duplicateCheckQuery, connection))
            {
                duplicateCheckCmd.Parameters.AddWithValue("@UserId", clientID);
                duplicateCheckCmd.Parameters.AddWithValue("@RequestDate", requestDateTime);

                int existingCount = (int)duplicateCheckCmd.ExecuteScalar();
                if (existingCount > 0)
                {
                    MessageBox.Show("У вас вже є заявка на цей час. Будь ласка, оберіть інший час.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Перевірка на інтервал не менше 2 годин між заявками
            string intervalCheckQuery = @"
            SELECT COUNT(*) FROM Repair_request
            WHERE ID_user = @UserId AND ABS(DATEDIFF(MINUTE, Request_date, @RequestDate)) < 120";

            using (SqlCommand intervalCheckCmd = new SqlCommand(intervalCheckQuery, connection))
            {
                intervalCheckCmd.Parameters.AddWithValue("@UserId", clientID);
                intervalCheckCmd.Parameters.AddWithValue("@RequestDate", requestDateTime);

                int intervalCount = (int)intervalCheckCmd.ExecuteScalar();
                if (intervalCount > 0)
                {
                    MessageBox.Show("Між заявками має бути інтервал не менше 2 годин. Будь ласка, оберіть інший час.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Додавання нової заявки
            string insertQuery = @"
            INSERT INTO Repair_request (Request_date, Visit_address, ID_user, ID_repair_type, Problem_description, Status)
            VALUES (@RequestDate, @VisitAddress, @UserId, @RepairTypeId, @ProblemDescription, 'призначено')";

            using (SqlCommand insertCmd = new SqlCommand(insertQuery, connection))
            {
                insertCmd.Parameters.AddWithValue("@RequestDate", requestDateTime);
                insertCmd.Parameters.AddWithValue("@VisitAddress", address);
                insertCmd.Parameters.AddWithValue("@UserId", clientID);
                insertCmd.Parameters.AddWithValue("@RepairTypeId", repairTypeId);
                insertCmd.Parameters.AddWithValue("@ProblemDescription", problemDescription);

                int rowsAffected = insertCmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Заявку успішно додано!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadMeetingsForClient();
                    LoadScheduledMeetings();
                }
                else
                {
                    MessageBox.Show("Не вдалося додати заявку.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //private void Main_form_for_Client_Load(object sender, EventArgs e)
        //{
        //    LoadMeetingsForClient();
        //    this.reportViewer1.RefreshReport();
        //}

        private void LoadMeetingsForClient()
        {
            string query = @"
            SELECT 
                r.ID_request AS [ID заявки],
                r.Request_date AS [Дата та час],
                r.Visit_address AS [Адреса],
                rt.Repair_name AS [Тип ремонту],
                r.Problem_description AS [Опис проблеми],
                r.Status AS [Статус]
            FROM Repair_request r
            INNER JOIN Repair_type rt ON r.ID_repair_type = rt.ID_repair_type
            WHERE r.ID_user = @id
            ORDER BY r.Request_date DESC";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@id", clientID);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView2.DataSource = table;

                dataGridView2.RowHeadersVisible = false;
                dataGridView2.DefaultCellStyle.Font = new Font("Arial", 8);

                foreach (DataGridViewColumn column in dataGridView2.Columns)
                {
                    column.Width = 110;
                }
            }
        }

        private void Form_close(object sender, EventArgs e)
        {
            if (connection != null && connection.State == ConnectionState.Open)
                connection.Close();

            Close();
            Application.Exit();
        }

        private void LoadScheduledMeetings()
        {
            string query = @"
            SELECT ID_request
            FROM Repair_request
            WHERE ID_user = @id AND Status = 'призначено'
            ORDER BY Request_date DESC";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@id", clientID);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    comboBox3.Items.Clear();
                    while (reader.Read())
                    {
                        comboBox3.Items.Add(reader["ID_request"].ToString());
                    }
                }
            }
        }

        private void cancellation_of_visit_request_Click(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem == null)
            {
                MessageBox.Show("Будь ласка, оберіть зустріч для скасування.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Ви впевнені, що хочете скасувати обрану заявку на візит?", "Підтвердження скасування", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            if (!Guid.TryParse(comboBox3.SelectedItem.ToString(), out Guid requestId))
            {
                MessageBox.Show("Невірний формат ID зустрічі.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = "UPDATE Repair_request SET Status = 'скасовано' WHERE ID_request = @RequestId AND ID_user = @id";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@RequestId", requestId);
                cmd.Parameters.AddWithValue("@id", clientID);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Заявку на візит успішно скасовано.", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadScheduledMeetings(); // Оновлення списку після скасування
                    LoadMeetingsForClient(); // Оновлення таблиці заявок
                }
                else
                {
                    MessageBox.Show("Не вдалося скасувати зустріч.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            LoadScheduledMeetings();
            LoadMeetingsForClient();
        }

        //private void reportViewer1_Load(object sender, EventArgs e)
        //{
        //    // Отримати ID_client з ID_client_textBox
        //    string ID_client = ID_client_textBox.Text;

        //    // Оновити звіт
        //    string projectPath = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\"));
        //    string reportPath = Path.Combine(projectPath, "Report3.rdlc");
        //    reportViewer1.LocalReport.ReportPath = reportPath; // Шлях до файлу звіту

        //    // Завантаження даних до датасету
        //    LoadReportData(ID_client);

        //    // Оновлення даних в звіті
        //    reportViewer1.LocalReport.DataSources.Clear();
        //    reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", dataSet2.Tables["DataTable1"]));

        //    reportViewer1.RefreshReport();
        //}

        //private void LoadReportData(string ID_client)
        //{
        //    //// Очищення даних в датасеті
        //    //dataSet2.Clear();

        //    //try
        //    //{
        //    //    // Виклик методу Fill для заповнення даних в DataTable1DataTable
        //    //    int rowsAffected = dataTable1TableAdapter.Fill(dataSet2.DataTable1, int.Parse(ID_client));

        //    //    if (rowsAffected > 0)
        //    //    {
        //    //        // Дані успішно завантажені, оновіть звіт
        //    //        reportViewer1.RefreshReport();
        //    //    }
        //    //    else
        //    //    {
        //    //        // Відобразити повідомлення, що дані не знайдені
        //    //        MessageBox.Show("Дані для вказаного клієнта не знайдено.", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    // Обробити помилку, якщо її виникає
        //    //    MessageBox.Show($"Помилка під час завантаження даних: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    //}
        //}

    }
}
