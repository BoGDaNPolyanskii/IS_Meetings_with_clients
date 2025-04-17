using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Microsoft.Reporting.WinForms;
using System.Configuration;

namespace IS_Meetings_with_clients
{
    public partial class Main_form_for_SupportWorker : Form
    {
        private readonly SqlConnection connection;
        //private SqlConnection connection;
        private string login;
        private readonly string workerID;
        private readonly Timer scheduleTimer;

        public Main_form_for_SupportWorker(SqlConnection connection, string login, Guid userId)
        {
            InitializeComponent();
            this.connection = connection;
            this.login = login;
            this.workerID = userId.ToString();

            LoadSchedule(); 
            LoadUserData();
            LoadIDRequests();
            // Ініціалізація таймера
            scheduleTimer = new Timer();
            scheduleTimer.Interval = 60000; // Оновлення кожні 60 секунд
            scheduleTimer.Tick += ScheduleTimer_Tick;
            scheduleTimer.Start();
        }

        // Оновлення графіка зустрічей
        private void ScheduleTimer_Tick(object sender, EventArgs e)
        {
            LoadSchedule();
        }

        private void LoadUserData()
        {
            textBox_ID_worker.Text = workerID;
            textBox4.Text = workerID;
            textBox6.Text = workerID;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (connection != null && connection.State == ConnectionState.Open)
                connection.Close();

            Close();
            Application.Exit();
        }

        private void LoadSchedule()
        {
            string query = @" 
            SELECT 
                r.ID_request AS [Код заявки],
                u.Full_name AS [ПІБ клієнта],
                rt.Repair_name AS [Тип ремонту],
                r.Request_date AS [Дата заявки],
                r.Visit_address AS [Адреса візиту],
                r.Problem_description AS [Опис проблеми],
                r.Status AS [Статус]
            FROM Repair_request r
            JOIN Client c ON r.ID_user = c.ID_user
            JOIN [User] u ON c.ID_user = u.ID_user
            JOIN Repair_type rt ON r.ID_repair_type = rt.ID_repair_type
            WHERE r.Status IN('призначено', 'в процесі')";

            DataTable dataTable = new DataTable();

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                dataGridView1.RowHeadersVisible = false;
                adapter.Fill(dataTable);

                comboBox1.Items.Clear();

                // Додати ID заявок до comboBox
                foreach (DataRow row in dataTable.Rows)
                {
                    comboBox1.Items.Add(row["Код заявки"].ToString());
                }

                // Відображення даних у dataGridView1
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.DataSource = dataTable;
                dataGridView1.DefaultCellStyle.Font = new Font("Arial", 7);

                // Встановити порядок стовпців
                dataGridView1.Columns["Код заявки"].DisplayIndex = 0;
                dataGridView1.Columns["Дата заявки"].DisplayIndex = 1;
                dataGridView1.Columns["Статус"].DisplayIndex = 2;
                dataGridView1.Columns["Тип ремонту"].DisplayIndex = 3;
                dataGridView1.Columns["Адреса візиту"].DisplayIndex = 4;
                dataGridView1.Columns["ПІБ клієнта"].DisplayIndex = 5;
                dataGridView1.Columns["Опис проблеми"].DisplayIndex = 6;                

                // Змінити ширину стовпців
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.Width = 90;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Виникла помилка: " + ex.Message);
            }        
        }

        private void LoadIDRequests()
        {
            string query = @"
            SELECT ID_request
            FROM Repair_request
            WHERE Status IN ('в процесі')
            ORDER BY Request_date DESC";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    comboBox3.Items.Clear();
                    comboBox4.Items.Clear();

                    while (reader.Read())
                    {
                        string requestId = reader["ID_request"].ToString();
                        comboBox3.Items.Add(requestId);
                        comboBox4.Items.Add(requestId);
                    }
                }
            }
        }

        private void change_meeting_data_Click(object sender, EventArgs e)
        {
            // Перевірка, чи обрані значення в обох ComboBox
            if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Будь ласка, оберіть заявку та новий статус.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Отримання ID заявки з comboBox1
            Guid requestId;
            if (!Guid.TryParse(comboBox1.SelectedItem.ToString(), out requestId))
            {
                MessageBox.Show("Невірний формат ID заявки.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Отримання нового статусу з comboBox2
            string newStatus = comboBox2.SelectedItem.ToString();

            // Оновлення статусу заявки в базі даних
            try
            {
                string query = "UPDATE Repair_request SET [Status] = @Status WHERE ID_request = @RequestId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Status", newStatus);
                    command.Parameters.AddWithValue("@RequestId", requestId);

                    // Перевірка стану з'єднання
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Статус заявки успішно оновлено.", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadSchedule();
                    }
                    else
                    {
                        MessageBox.Show("Заявку не знайдено.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при оновленні статусу: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddRepairVisit_Click(object sender, EventArgs e)
        {
            // Перевірка вибору заявки
            if (comboBox3.SelectedItem == null)
            {
                MessageBox.Show("Будь ласка, оберіть заявку для візиту.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Отримання ID заявки
            if (!Guid.TryParse(comboBox3.SelectedItem.ToString(), out Guid requestId))
            {
                MessageBox.Show("Невірний формат ID заявки.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Отримання ID працівника
            if (!Guid.TryParse(textBox4.Text.Trim(), out Guid workerId))
            {
                MessageBox.Show("Невірний формат ID працівника.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Об'єднання дати та часу
            DateTime selectedDate = dateTimePicker2.Value.Date;
            TimeSpan selectedTime = dateTimePicker1.Value.TimeOfDay;
            DateTime visitDateTime = selectedDate + selectedTime;

            // Отримання опису роботи
            string workDescription = richTextBox3.Text.Trim();

            // Перевірка на порожній опис роботи
            if (string.IsNullOrWhiteSpace(workDescription))
            {
                MessageBox.Show("Будь ласка, введіть опис роботи.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Перевірка наявності дублюючого запису
            string checkQuery = @"
        SELECT COUNT(*) FROM Repair_visit 
        WHERE ID_request = @RequestId AND ID_user = @WorkerId AND Visit_date = @VisitDate";

            using (SqlCommand checkCmd = new SqlCommand(checkQuery, connection))
            {
                checkCmd.Parameters.AddWithValue("@RequestId", requestId);
                checkCmd.Parameters.AddWithValue("@WorkerId", workerId);
                checkCmd.Parameters.AddWithValue("@VisitDate", visitDateTime);

                int existingRecords = (int)checkCmd.ExecuteScalar();
                if (existingRecords > 0)
                {
                    MessageBox.Show("Такий візит вже існує.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // SQL-запит для вставки нового візиту
            string query = @"
            INSERT INTO Repair_visit (ID_user, Visit_date, Work_description, ID_request)
            VALUES (@WorkerId, @VisitDate, @WorkDescription, @RequestId)";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@WorkerId", workerId);
                    cmd.Parameters.AddWithValue("@VisitDate", visitDateTime);
                    cmd.Parameters.AddWithValue("@WorkDescription", workDescription);
                    cmd.Parameters.AddWithValue("@RequestId", requestId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Візит успішно додано!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Не вдалося додати візит.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Сталася помилка: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddCompletedWork_Click(object sender, EventArgs e)
        {
            // Перевірка вибору заявки
            if (comboBox4.SelectedItem == null)
            {
                MessageBox.Show("Будь ласка, оберіть заявку для завершення.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Отримання та перевірка ID заявки
            if (!Guid.TryParse(comboBox4.SelectedItem.ToString(), out Guid requestId))
            {
                MessageBox.Show("Невірний формат ID заявки.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Отримання та перевірка ID працівника
            if (!Guid.TryParse(textBox6.Text.Trim(), out Guid workerId))
            {
                MessageBox.Show("Невірний формат ID працівника.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Отримання та перевірка дати завершення
            DateTime completionDate = dateTimePicker4.Value.Date;

            // Отримання та перевірка опису виконаних робіт
            string workDescription = richTextBox2.Text.Trim();
            if (string.IsNullOrWhiteSpace(workDescription))
            {
                MessageBox.Show("Будь ласка, введіть опис виконаних робіт.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Отримання та перевірка загальної вартості
            if (!decimal.TryParse(textBox7.Text.Trim(), out decimal totalCost))
            {
                MessageBox.Show("Невірний формат загальної вартості.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Перевірка наявності запису з таким ID заявки
            string checkQuery = "SELECT COUNT(*) FROM Completed_work WHERE ID_request = @RequestId";
            using (SqlCommand checkCmd = new SqlCommand(checkQuery, connection))
            {
                checkCmd.Parameters.AddWithValue("@RequestId", requestId);
                int existingRecords = (int)checkCmd.ExecuteScalar();
                if (existingRecords > 0)
                {
                    MessageBox.Show("Акт виконаних робіт для цієї заявки вже існує.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Додавання запису до таблиці Completed_work
            string insertQuery = @"
            INSERT INTO Completed_work (ID_request, Completion_date, Work_description, Total_cost, ID_user)
            VALUES (@RequestId, @CompletionDate, @WorkDescription, @TotalCost, @WorkerId)";
            using (SqlCommand insertCmd = new SqlCommand(insertQuery, connection))
            {
                insertCmd.Parameters.AddWithValue("@RequestId", requestId);
                insertCmd.Parameters.AddWithValue("@CompletionDate", completionDate);
                insertCmd.Parameters.AddWithValue("@WorkDescription", workDescription);
                insertCmd.Parameters.AddWithValue("@TotalCost", totalCost);
                insertCmd.Parameters.AddWithValue("@WorkerId", workerId);

                int rowsAffected = insertCmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Акт виконаних робіт успішно додано.", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Не вдалося додати акт виконаних робіт.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Main_form_for_SupportWorker_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }

        //private void Main_form_for_SupportWorker_Load(object sender, EventArgs e)
        //{
        //    // Завантаження даних у відповідні таблиці
        //    //this.support_workerTableAdapter.Fill(this.RepairServiceDBDataSet.Support_worker);
        //    this.clientTableAdapter.Fill(this.RepairServiceDBDataSet.Client);

        //    // Завантаження даних для звіту
        //    DataTable reportData = LoadClientRepairData();

        //    // Налаштування ReportViewer для відображення звіту
        //    reportViewer1.LocalReport.ReportPath = "ClientRepairReport.rdlc";
        //    reportViewer1.LocalReport.DataSources.Clear();

        //    ReportDataSource rds = new ReportDataSource("ClientRepairDataSet", reportData);
        //    reportViewer1.LocalReport.DataSources.Add(rds);

        //    reportViewer1.RefreshReport();
        //}

        //this.support_workerTableAdapter.Fill(this.dataSet1.Support_worker);
        //this.clientTableAdapter.Fill(this.dataSet1.Client);
        //LoadSchedule();
        //this.reportViewer1.RefreshReport();
        //this.reportViewer2.RefreshReport();
        //private DataTable LoadClientRepairData()
        //{
        //    string query = @"
        //SELECT 
        //    r.ID_request AS [Код заявки],
        //    u.Full_name AS [ПІБ клієнта],
        //    rt.Repair_name AS [Тип ремонту],
        //    r.Request_date AS [Дата заявки],
        //    r.Visit_address AS [Адреса візиту],
        //    r.Problem_description AS [Опис проблеми],
        //    r.Status AS [Статус]
        //FROM Repair_request r
        //JOIN Client c ON r.ID_user = c.ID_user
        //JOIN [User] u ON c.ID_user = u.ID_user
        //JOIN Repair_type rt ON r.ID_repair_type = rt.ID_repair_type
        //WHERE r.Status IN('призначено', 'в процесі')";

        //    DataTable dataTable = new DataTable();

        //    try
        //    {
        //        using (SqlCommand cmd = new SqlCommand(query, connection))
        //        {
        //            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //            adapter.Fill(dataTable);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Помилка при завантаженні даних для звіту: " + ex.Message);
        //    }

        //    return dataTable;
        //}

        //private void Main_form_for_SupportWorker_Load_1(object sender, EventArgs e)
        //{
        //    // TODO: This line of code loads data into the 'repairServiceDBDataSet.GetClientRepairReport' table. You can move, or remove it, as needed.
        //    this.getClientRepairReportTableAdapter1.Fill(this.repairServiceDBDataSet.GetClientRepairReport);
        //    // TODO: This line of code loads data into the 'repairServiceDBDataSet1.GetClientRepairReport' table. You can move, or remove it, as needed.
        //    this.getClientRepairReportTableAdapter.Fill(this.repairServiceDBDataSet1.GetClientRepairReport);

        //    this.reportViewer2.RefreshReport();
        //}

        private void Form1_Load(object sender, EventArgs e)
        {
            // Використання вже переданого підключення
            SqlCommand cmd = new SqlCommand("GetWorkerRepairReport", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            reportViewer1.LocalReport.DataSources.Clear();

            ReportDataSource rds = new ReportDataSource("DataSet_1", dt);
            reportViewer1.LocalReport.DataSources.Add(rds);

            reportViewer1.RefreshReport();
        }
    }
}