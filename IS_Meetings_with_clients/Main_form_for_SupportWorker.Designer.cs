namespace IS_Meetings_with_clients
{
    partial class Main_form_for_SupportWorker
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main_form_for_SupportWorker));
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource4 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.Worker = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.change_meeting_data = new System.Windows.Forms.Button();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.Support = new System.Windows.Forms.TabPage();
            this.Clients = new System.Windows.Forms.TabPage();
            this.label25 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.button13 = new System.Windows.Forms.Button();
            this.textBox_ID_worker = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.reportViewer2 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.dataSet1 = new IS_Meetings_with_clients.DataSet1();
            this.dataSet1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.clientBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.clientTableAdapter = new IS_Meetings_with_clients.DataSet1TableAdapters.ClientTableAdapter();
            this.Support_workerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.supportworkerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.support_workerTableAdapter = new IS_Meetings_with_clients.DataSet1TableAdapters.Support_workerTableAdapter();
            this.Worker.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.Support.SuspendLayout();
            this.Clients.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Support_workerBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.supportworkerBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // Worker
            // 
            this.Worker.Controls.Add(this.tabPage4);
            this.Worker.Controls.Add(this.tabPage1);
            this.Worker.Controls.Add(this.tabPage5);
            this.Worker.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Worker.Location = new System.Drawing.Point(30, 115);
            this.Worker.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Worker.Name = "Worker";
            this.Worker.SelectedIndex = 0;
            this.Worker.Size = new System.Drawing.Size(1019, 577);
            this.Worker.TabIndex = 27;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox8);
            this.tabPage4.Location = new System.Drawing.Point(4, 34);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage4.Size = new System.Drawing.Size(1011, 539);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Перегляд зустрічей";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.groupBox9);
            this.groupBox8.Controls.Add(this.comboBox4);
            this.groupBox8.Controls.Add(this.label1);
            this.groupBox8.Location = new System.Drawing.Point(15, 4);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox8.Size = new System.Drawing.Size(1000, 548);
            this.groupBox8.TabIndex = 2;
            this.groupBox8.TabStop = false;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.dataGridView1);
            this.groupBox9.Controls.Add(this.richTextBox1);
            this.groupBox9.Location = new System.Drawing.Point(-9, 0);
            this.groupBox9.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox9.Size = new System.Drawing.Size(1009, 539);
            this.groupBox9.TabIndex = 6;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Перегляд інформації про зустрічі";
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(9, 29);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(990, 485);
            this.dataGridView1.TabIndex = 29;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(9, 28);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(993, 486);
            this.richTextBox1.TabIndex = 28;
            this.richTextBox1.Text = "";
            // 
            // comboBox4
            // 
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Items.AddRange(new object[] {
            "Журнал оцінок відмінників",
            "Інформація про випускників",
            "Журнал оцінок двієчників"});
            this.comboBox4.Location = new System.Drawing.Point(250, 21);
            this.comboBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(287, 33);
            this.comboBox4.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(237, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "Виберіть звіт для перегляду";
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Gainsboro;
            this.tabPage1.Controls.Add(this.groupBox11);
            this.tabPage1.Location = new System.Drawing.Point(4, 34);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1011, 539);
            this.tabPage1.TabIndex = 4;
            this.tabPage1.Text = "Налаштування параметрів зустрічі";
            // 
            // groupBox11
            // 
            this.groupBox11.BackColor = System.Drawing.Color.White;
            this.groupBox11.Controls.Add(this.textBox2);
            this.groupBox11.Controls.Add(this.textBox1);
            this.groupBox11.Controls.Add(this.label2);
            this.groupBox11.Controls.Add(this.comboBox1);
            this.groupBox11.Controls.Add(this.label20);
            this.groupBox11.Controls.Add(this.label21);
            this.groupBox11.Controls.Add(this.change_meeting_data);
            this.groupBox11.Location = new System.Drawing.Point(228, 139);
            this.groupBox11.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox11.Size = new System.Drawing.Size(554, 275);
            this.groupBox11.TabIndex = 6;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Встановлення статусу зустрічі та назначення працівника";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(276, 41);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(233, 31);
            this.textBox2.TabIndex = 32;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(276, 95);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(233, 31);
            this.textBox1.TabIndex = 31;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(18, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 30);
            this.label2.TabIndex = 29;
            this.label2.Text = "ID зустрічі";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Scheduled",
            "Canceled",
            "In process",
            "Completed"});
            this.comboBox1.Location = new System.Drawing.Point(276, 147);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(233, 33);
            this.comboBox1.TabIndex = 28;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.label20.Location = new System.Drawing.Point(18, 146);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(171, 30);
            this.label20.TabIndex = 24;
            this.label20.Text = "Статус зустрічі";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.label21.Location = new System.Drawing.Point(18, 94);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(165, 30);
            this.label21.TabIndex = 23;
            this.label21.Text = "ID працівника";
            // 
            // change_meeting_data
            // 
            this.change_meeting_data.Font = new System.Drawing.Font("Sitka Banner", 10F, System.Drawing.FontStyle.Bold);
            this.change_meeting_data.Location = new System.Drawing.Point(198, 207);
            this.change_meeting_data.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.change_meeting_data.Name = "change_meeting_data";
            this.change_meeting_data.Size = new System.Drawing.Size(148, 42);
            this.change_meeting_data.TabIndex = 8;
            this.change_meeting_data.Text = "Зберегти";
            this.change_meeting_data.UseVisualStyleBackColor = true;
            this.change_meeting_data.Click += new System.EventHandler(this.change_meeting_data_Click);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.tabControl2);
            this.tabPage5.Location = new System.Drawing.Point(4, 34);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(1011, 539);
            this.tabPage5.TabIndex = 5;
            this.tabPage5.Text = "Переглянути звіт";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.Support);
            this.tabControl2.Controls.Add(this.Clients);
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(1008, 539);
            this.tabControl2.TabIndex = 0;
            // 
            // Support
            // 
            this.Support.Controls.Add(this.reportViewer1);
            this.Support.Location = new System.Drawing.Point(4, 34);
            this.Support.Name = "Support";
            this.Support.Padding = new System.Windows.Forms.Padding(3);
            this.Support.Size = new System.Drawing.Size(1000, 501);
            this.Support.TabIndex = 0;
            this.Support.Text = "Support";
            this.Support.UseVisualStyleBackColor = true;
            // 
            // Clients
            // 
            this.Clients.Controls.Add(this.reportViewer2);
            this.Clients.Location = new System.Drawing.Point(4, 34);
            this.Clients.Name = "Clients";
            this.Clients.Padding = new System.Windows.Forms.Padding(3);
            this.Clients.Size = new System.Drawing.Size(1000, 501);
            this.Clients.TabIndex = 1;
            this.Clients.Text = "Clients";
            this.Clients.UseVisualStyleBackColor = true;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.BackColor = System.Drawing.Color.Transparent;
            this.label25.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label25.Location = new System.Drawing.Point(287, 67);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(497, 31);
            this.label25.TabIndex = 32;
            this.label25.Text = "Розробники: Полянський Б., Костенко А.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(69, 702);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(625, 27);
            this.label7.TabIndex = 30;
            this.label7.Text = "Служба підтримки: www.help.ua ; телефон: 0961234567";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Bold);
            this.label14.Location = new System.Drawing.Point(265, 19);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(551, 37);
            this.label14.TabIndex = 29;
            this.label14.Text = "ІС проведених зустрічей з клієнтами";
            // 
            // button13
            // 
            this.button13.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.button13.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button13.BackgroundImage")));
            this.button13.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button13.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button13.FlatAppearance.BorderSize = 5;
            this.button13.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.button13.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.button13.Location = new System.Drawing.Point(1035, 10);
            this.button13.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(39, 37);
            this.button13.TabIndex = 28;
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // textBox_ID_worker
            // 
            this.textBox_ID_worker.BackColor = System.Drawing.Color.Silver;
            this.textBox_ID_worker.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_ID_worker.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_ID_worker.ForeColor = System.Drawing.Color.Black;
            this.textBox_ID_worker.Location = new System.Drawing.Point(947, 703);
            this.textBox_ID_worker.Name = "textBox_ID_worker";
            this.textBox_ID_worker.ReadOnly = true;
            this.textBox_ID_worker.Size = new System.Drawing.Size(100, 26);
            this.textBox_ID_worker.TabIndex = 34;
            this.textBox_ID_worker.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.BackColor = System.Drawing.Color.Transparent;
            this.label24.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(832, 703);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(120, 27);
            this.label24.TabIndex = 33;
            this.label24.Text = "ID worker:";
            // 
            // reportViewer1
            // 
            reportDataSource3.Name = "DataSet1";
            reportDataSource3.Value = this.supportworkerBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "IS_Meetings_with_clients.Report2.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(-4, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1005, 499);
            this.reportViewer1.TabIndex = 0;
            // 
            // reportViewer2
            // 
            reportDataSource4.Name = "DataSet1";
            reportDataSource4.Value = this.clientBindingSource;
            this.reportViewer2.LocalReport.DataSources.Add(reportDataSource4);
            this.reportViewer2.LocalReport.ReportEmbeddedResource = "IS_Meetings_with_clients.Report1.rdlc";
            this.reportViewer2.Location = new System.Drawing.Point(-4, 0);
            this.reportViewer2.Name = "reportViewer2";
            this.reportViewer2.ServerReport.BearerToken = null;
            this.reportViewer2.Size = new System.Drawing.Size(1005, 502);
            this.reportViewer2.TabIndex = 0;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "DataSet1";
            this.dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dataSet1BindingSource
            // 
            this.dataSet1BindingSource.DataSource = this.dataSet1;
            this.dataSet1BindingSource.Position = 0;
            // 
            // clientBindingSource
            // 
            this.clientBindingSource.DataMember = "Client";
            this.clientBindingSource.DataSource = this.dataSet1BindingSource;
            // 
            // clientTableAdapter
            // 
            this.clientTableAdapter.ClearBeforeFill = true;
            // 
            // Support_workerBindingSource
            // 
            this.Support_workerBindingSource.DataMember = "Support_worker";
            this.Support_workerBindingSource.DataSource = this.dataSet1;
            // 
            // supportworkerBindingSource
            // 
            this.supportworkerBindingSource.DataMember = "Support_worker";
            this.supportworkerBindingSource.DataSource = this.dataSet1;
            // 
            // support_workerTableAdapter
            // 
            this.support_workerTableAdapter.ClearBeforeFill = true;
            // 
            // Main_form_for_SupportWorker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1085, 754);
            this.Controls.Add(this.textBox_ID_worker);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.Worker);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.button13);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Main_form_for_SupportWorker";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Main_form_for_SupportWorker_Load);
            this.Worker.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.Support.ResumeLayout(false);
            this.Clients.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Support_workerBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.supportworkerBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl Worker;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button change_meeting_data;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_ID_worker;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage Support;
        private System.Windows.Forms.TabPage Clients;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer2;
        private DataSet1 dataSet1;
        private System.Windows.Forms.BindingSource dataSet1BindingSource;
        private System.Windows.Forms.BindingSource clientBindingSource;
        private DataSet1TableAdapters.ClientTableAdapter clientTableAdapter;
        private System.Windows.Forms.BindingSource Support_workerBindingSource;
        private System.Windows.Forms.BindingSource supportworkerBindingSource;
        private DataSet1TableAdapters.Support_workerTableAdapter support_workerTableAdapter;
    }
}