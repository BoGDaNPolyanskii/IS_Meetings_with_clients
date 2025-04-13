using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

                // Додати ID заявок до comboBox14
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
        //DataTable dataTable = _meetingDataAccess.GetMeetingsForSupportWorker();


        //// Видаляємо першу пусту колонку
        //dataGridView1.RowHeadersVisible = false;

        //// Встановлюємо порядок стовпців
        //dataGridView1.DataSource = dataTable;
        //dataGridView1.Columns["ID_meeting"].DisplayIndex = 0;
        //dataGridView1.Columns["Date"].DisplayIndex = 1;
        //dataGridView1.Columns["Time"].DisplayIndex = 2;
        //dataGridView1.Columns["Address"].DisplayIndex = 3;

        //// Змінити розмір шрифту для тексту
        //dataGridView1.DefaultCellStyle.Font = new Font("Arial", 8);

        //// Змінити ширину стовпців для підбору до ширини сторінки
        //foreach (DataGridViewColumn column in dataGridView1.Columns)
        //{
        //    column.Width = 70;
        //}
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

        //    private void Main_form_for_SupportWorker_Load(object sender, EventArgs e)
        //    {
        //        // TODO: This line of code loads data into the 'dataSet1.Support_worker' table. You can move, or remove it, as needed.
        //        this.support_workerTableAdapter.Fill(this.dataSet1.Support_worker);
        //        // TODO: This line of code loads data into the 'dataSet1.Client' table. You can move, or remove it, as needed.
        //        this.clientTableAdapter.Fill(this.dataSet1.Client);
        //        LoadSchedule();
        //        this.reportViewer1.RefreshReport();
        //        this.reportViewer2.RefreshReport();
        //    }

        //    private void groupBox9_Enter(object sender, EventArgs e)
        //    {

        //    }            


    }
}