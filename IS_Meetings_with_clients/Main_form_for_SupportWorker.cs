using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace IS_Meetings_with_clients
{
    public partial class Main_form_for_SupportWorker : Form
    {
        // private readonly SqlConnection connection;
        private readonly string workerID;
        private readonly Timer scheduleTimer;
        private readonly IWorkerDataAccess _workerDataAccess;
        private readonly IMeetingDataAccess _meetingDataAccess;

        public Main_form_for_SupportWorker(IWorkerDataAccess workerDataAccess, IMeetingDataAccess meetingDataAccess, string workerID)
        {
            InitializeComponent();
            // connection = conn;
            this._workerDataAccess = workerDataAccess;
            this._meetingDataAccess = meetingDataAccess;
            this.workerID = workerID;
            LoadUserData();
            LoadSchedule();

            // Ініціалізація таймера
            scheduleTimer = new Timer();
            scheduleTimer.Interval = 60000; // Оновлення кожні 60 секунд (60000 мілісекунд)
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
            object result = _workerDataAccess.GetWorkerData(this.workerID);

            if (result != null)
            {
                // Встановлюємо результат в textBox
                textBox_ID_worker.Text = result.ToString();
                textBox1.Text = result.ToString();
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Close();
            Application.Exit();
        }

        private void LoadSchedule()
        {
            DataTable dataTable = _meetingDataAccess.GetMeetingsForSupportWorker();


            // Видаляємо першу пусту колонку
            dataGridView1.RowHeadersVisible = false;

            // Встановлюємо порядок стовпців
            dataGridView1.DataSource = dataTable;
            dataGridView1.Columns["ID_meeting"].DisplayIndex = 0;
            dataGridView1.Columns["Date"].DisplayIndex = 1;
            dataGridView1.Columns["Time"].DisplayIndex = 2;
            dataGridView1.Columns["Address"].DisplayIndex = 3;

            // Змінити розмір шрифту для тексту
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 8);

            // Змінити ширину стовпців для підбору до ширини сторінки
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.Width = 70;
            }
        }

        private void change_meeting_data_Click(object sender, EventArgs e)
        {
            // Перевірка, чи всі поля заповнені
            if (string.IsNullOrEmpty(comboBox1.Text) || string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Будь ласка, заповніть усі поля.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Отримання даних з полів форми
            string status = comboBox1.Text; // Статус зустрічі
            string idWorker = textBox1.Text; // Отримання рядка з текстового поля
            int idWorkerInt = Convert.ToInt32(idWorker); // Перетворення рядка на ціле число ID працівника
            string idMeeting = textBox2.Text; // Отримання рядка з текстового поля
            int idMeetingInt = Convert.ToInt32(idMeeting); // Перетворення рядка на ціле число

            _meetingDataAccess.UpdateMeetingStatus(idMeetingInt, idWorkerInt, status);
        }

        private void Main_form_for_SupportWorker_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.Support_worker' table. You can move, or remove it, as needed.
            this.support_workerTableAdapter.Fill(this.dataSet1.Support_worker);
            // TODO: This line of code loads data into the 'dataSet1.Client' table. You can move, or remove it, as needed.
            this.clientTableAdapter.Fill(this.dataSet1.Client);
            LoadSchedule();
            this.reportViewer1.RefreshReport();
            this.reportViewer2.RefreshReport();
        }
    }
}