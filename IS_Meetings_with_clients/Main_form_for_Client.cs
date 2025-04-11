using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace IS_Meetings_with_clients
{
 
    public partial class Main_form_for_Client : Form
    {
        private SqlConnection connection;
        private string login;
        private readonly string _login;
        private Timer scheduleTimer;

        public Main_form_for_Client(SqlConnection connection, string login)
        {
            InitializeComponent();
            this.connection = connection;
            this.login = login;
            _login = login;

            //// Load user data on form load
            //LoadUserData();
            //LoadMeetingsForClient();

            //// Ініціалізація таймера
            //scheduleTimer = new Timer();
            //scheduleTimer.Interval = 60000; // Оновлення кожні 60 секунд (60000 мілісекунд)
            //scheduleTimer.Tick += ScheduleTimer_Tick;
            //scheduleTimer.Start();
        }

        private void Save_changed_client_data_Click(object sender, EventArgs e)
        {

        }

        //// Метод, який викликається під час спрацьовування таймера для оновлення графіка зустрічей
        //private void ScheduleTimer_Tick(object sender, EventArgs e)
        //{
        //    LoadMeetingsForClient();
        //}
        //private void LoadUserData()
        //{
        //    DataTable dataTable = _clientDataAccess.GetClientData(_login);
        //    if (dataTable.Rows.Count > 0)
        //    {
        //        ID_client_textBox.Text = dataTable.Rows[0]["ID_client"].ToString();
        //        textBox2.Text = dataTable.Rows[0]["Phone_number"].ToString();
        //        textBox3.Text = dataTable.Rows[0]["Email"].ToString();
        //        richTextBox3.Text = dataTable.Rows[0]["Address"].ToString();
        //    }
        //}

        //private void Add_meeting_Click(object sender, EventArgs e)
        //{
        //    DateTime date = dateTimePicker2.Value.Date;
        //    DateTime time = dateTimePicker1.Value;
        //    string address = richTextBox3.Text;
        //    string problemDescription = richTextBox2.Text;
        //    string clientId = ID_client_textBox.Text;

        //    _meetingDataAccess.AddMeeting(clientId, date, time, address, problemDescription);
        //    LoadMeetingsForClient();
        //}

        //private void Save_changed_client_data_Click(object sender, EventArgs e)
        //{
        //    string phoneNumber = textBox2.Text;
        //    string email = textBox3.Text;
        //    string address = richTextBox3.Text;
        //    string clientId = ID_client_textBox.Text;

        //    _clientDataAccess.UpdateClientData(clientId, phoneNumber, email, address);
        //}
        //private void Main_form_for_Client_Load(object sender, EventArgs e)
        //{
        //    LoadMeetingsForClient();
        //    this.reportViewer1.RefreshReport();
        //}

        //private void LoadMeetingsForClient()
        //{
        //    DataTable dataTable = _meetingDataAccess.GetMeetingsForClient(ID_client_textBox.Text);

        //    // Очищення вмісту comboBox14 перед додаванням нових значень
        //    comboBox14.Items.Clear();

        //    // Додавання кодів зустрічей до comboBox14
        //    foreach (DataRow row in dataTable.Rows)
        //    {
        //        comboBox14.Items.Add(row["ID_meeting"].ToString());
        //    }

        //    // Видалення першої пустої колонки
        //    dataGridView1.RowHeadersVisible = false;

        //    // Відображення даних в dataGridView1
        //    dataGridView1.DataSource = dataTable;

        //    // Зміна розміру шрифту для тексту
        //    dataGridView1.DefaultCellStyle.Font = new Font("Arial", 8);

        //    // Зміна ширини стовпців для підбору до ширини сторінки
        //    foreach (DataGridViewColumn column in dataGridView1.Columns)
        //    {
        //        column.Width = 110;
        //    }
        //}

        // private void delete_meeting_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (comboBox14.SelectedIndex == -1)
        //        {
        //            MessageBox.Show("Будь ласка, виберіть зустріч для видалення!", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return;
        //        }

        //        DialogResult result = MessageBox.Show("Ви впевнені що хочете видалити обрану зустріч?", "Підтвердження видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        //        if (result == DialogResult.Yes)
        //        {
        //            int meetingId = Convert.ToInt32(comboBox14.SelectedItem.ToString());//в комбобокс 14 пропав код для загрузки ід зустрічей тут
        //            _meetingDataAccess.DeleteMeeting(meetingId.ToString());
        //            LoadMeetingsForClient();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        //private void button13_Click(object sender, EventArgs e)
        //{
        //    Close();
        //    Application.Exit();
        //}

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

        //private void Client_control_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}


        //private void comboBox14_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}

        //private void Save_changed_client_data_Click_1(object sender, EventArgs e)
        //{

        //}
    }
}
