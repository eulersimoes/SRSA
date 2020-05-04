namespace AvionicsInstrumentControlDemo
{
    partial class FormCockPit
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btAtualizar = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.cmbVideoDevices = new System.Windows.Forms.ComboBox();
            this.btnStartStopRecording = new System.Windows.Forms.Button();
            this.fpv = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.altimeterInstrumentControl1 = new AvionicsInstrumentControlDemo.AltimeterInstrumentControl();
            this.headingIndicatorInstrumentControl1 = new AvionicsInstrumentControlDemo.HeadingIndicatorInstrumentControl();
            this.airSpeedIndicatorInstrumentControl1 = new AvionicsInstrumentControlDemo.AirSpeedIndicatorInstrumentControl();
            this.bdsModel = new System.Windows.Forms.BindingSource(this.components);
            this.attitudeIndicatorInstrumentControl1 = new AvionicsInstrumentControlDemo.AttitudeIndicatorInstrumentControl();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btFarol = new System.Windows.Forms.Button();
            this.btNavLight = new System.Windows.Forms.Button();
            this.btStrobe = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btEmergencia = new System.Windows.Forms.Button();
            this.btSync = new System.Windows.Forms.Button();
            this.rdApNextWayPoint = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.QntaBat = new System.Windows.Forms.Label();
            this.QntFuel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.apNav = new System.Windows.Forms.ComboBox();
            this.bdsWayPoints = new System.Windows.Forms.BindingSource(this.components);
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.apHdg = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.rdApAlt = new System.Windows.Forms.RadioButton();
            this.apAlt = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btAdd = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvWay = new System.Windows.Forms.DataGridView();
            this.Del = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.latitudeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.longitudeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.altitudeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpv)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdsModel)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdsWayPoints)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWay)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btAtualizar);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.webBrowser);
            this.groupBox1.Controls.Add(this.cmbVideoDevices);
            this.groupBox1.Controls.Add(this.btnStartStopRecording);
            this.groupBox1.Controls.Add(this.fpv);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(947, 285);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "FPV";
            // 
            // btAtualizar
            // 
            this.btAtualizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btAtualizar.Location = new System.Drawing.Point(812, 262);
            this.btAtualizar.Name = "btAtualizar";
            this.btAtualizar.Size = new System.Drawing.Size(75, 23);
            this.btAtualizar.TabIndex = 8;
            this.btAtualizar.Text = "AVIAO";
            this.btAtualizar.UseVisualStyleBackColor = true;
            this.btAtualizar.Click += new System.EventHandler(this.btAtualizar_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(893, 262);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(48, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "CASA";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Right;
            this.webBrowser.Location = new System.Drawing.Point(449, 16);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(495, 266);
            this.webBrowser.TabIndex = 7;
            // 
            // cmbVideoDevices
            // 
            this.cmbVideoDevices.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbVideoDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVideoDevices.FormattingEnabled = true;
            this.cmbVideoDevices.Location = new System.Drawing.Point(87, 258);
            this.cmbVideoDevices.Name = "cmbVideoDevices";
            this.cmbVideoDevices.Size = new System.Drawing.Size(165, 21);
            this.cmbVideoDevices.TabIndex = 6;
            this.cmbVideoDevices.SelectedIndexChanged += new System.EventHandler(this.cmbVideoDevices_SelectedIndexChanged);
            // 
            // btnStartStopRecording
            // 
            this.btnStartStopRecording.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStartStopRecording.Location = new System.Drawing.Point(3, 258);
            this.btnStartStopRecording.Name = "btnStartStopRecording";
            this.btnStartStopRecording.Size = new System.Drawing.Size(75, 23);
            this.btnStartStopRecording.TabIndex = 5;
            this.btnStartStopRecording.Text = "Broad/REC";
            this.btnStartStopRecording.UseVisualStyleBackColor = true;
            this.btnStartStopRecording.Click += new System.EventHandler(this.btnStartStopRecording_Click);
            // 
            // fpv
            // 
            this.fpv.Dock = System.Windows.Forms.DockStyle.Left;
            this.fpv.Location = new System.Drawing.Point(3, 16);
            this.fpv.Name = "fpv";
            this.fpv.Size = new System.Drawing.Size(435, 266);
            this.fpv.TabIndex = 0;
            this.fpv.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.altimeterInstrumentControl1);
            this.groupBox2.Controls.Add(this.headingIndicatorInstrumentControl1);
            this.groupBox2.Controls.Add(this.airSpeedIndicatorInstrumentControl1);
            this.groupBox2.Controls.Add(this.attitudeIndicatorInstrumentControl1);
            this.groupBox2.Location = new System.Drawing.Point(12, 303);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(266, 283);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "INSTRUMENTOS";
            // 
            // altimeterInstrumentControl1
            // 
            this.altimeterInstrumentControl1.Altitude = 0;
            this.altimeterInstrumentControl1.Location = new System.Drawing.Point(132, 145);
            this.altimeterInstrumentControl1.Name = "altimeterInstrumentControl1";
            this.altimeterInstrumentControl1.Size = new System.Drawing.Size(120, 120);
            this.altimeterInstrumentControl1.TabIndex = 3;
            this.altimeterInstrumentControl1.Text = "altimeterInstrumentControl1";
            // 
            // headingIndicatorInstrumentControl1
            // 
            this.headingIndicatorInstrumentControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.headingIndicatorInstrumentControl1.Heading = 0;
            this.headingIndicatorInstrumentControl1.Location = new System.Drawing.Point(6, 145);
            this.headingIndicatorInstrumentControl1.Name = "headingIndicatorInstrumentControl1";
            this.headingIndicatorInstrumentControl1.Size = new System.Drawing.Size(120, 120);
            this.headingIndicatorInstrumentControl1.TabIndex = 2;
            this.headingIndicatorInstrumentControl1.Text = "headingIndicatorInstrumentControl1";
            // 
            // airSpeedIndicatorInstrumentControl1
            // 
            this.airSpeedIndicatorInstrumentControl1.AirSpeed = 0;
            this.airSpeedIndicatorInstrumentControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.airSpeedIndicatorInstrumentControl1.DataBindings.Add(new System.Windows.Forms.Binding("AirSpeed", this.bdsModel, "AirSpeed", true));
            this.airSpeedIndicatorInstrumentControl1.Location = new System.Drawing.Point(132, 19);
            this.airSpeedIndicatorInstrumentControl1.Name = "airSpeedIndicatorInstrumentControl1";
            this.airSpeedIndicatorInstrumentControl1.Size = new System.Drawing.Size(120, 120);
            this.airSpeedIndicatorInstrumentControl1.TabIndex = 1;
            this.airSpeedIndicatorInstrumentControl1.Text = "airSpeedIndicatorInstrumentControl1";
            // 
            // bdsModel
            // 
            this.bdsModel.DataSource = typeof(AvionicsInstrumentsControls.ModelControle);
            // 
            // attitudeIndicatorInstrumentControl1
            // 
            this.attitudeIndicatorInstrumentControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.attitudeIndicatorInstrumentControl1.Location = new System.Drawing.Point(6, 19);
            this.attitudeIndicatorInstrumentControl1.Name = "attitudeIndicatorInstrumentControl1";
            this.attitudeIndicatorInstrumentControl1.PitchAngle = 0D;
            this.attitudeIndicatorInstrumentControl1.RollAngle = 0D;
            this.attitudeIndicatorInstrumentControl1.Size = new System.Drawing.Size(120, 120);
            this.attitudeIndicatorInstrumentControl1.TabIndex = 0;
            this.attitudeIndicatorInstrumentControl1.Text = "attitudeIndicatorInstrumentControl1";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.groupBox6);
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.btSync);
            this.groupBox3.Controls.Add(this.rdApNextWayPoint);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.apNav);
            this.groupBox3.Controls.Add(this.radioButton2);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.radioButton1);
            this.groupBox3.Controls.Add(this.apHdg);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.rdApAlt);
            this.groupBox3.Controls.Add(this.apAlt);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.btAdd);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.dgvWay);
            this.groupBox3.Location = new System.Drawing.Point(284, 303);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(675, 283);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "DADOS";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btFarol);
            this.groupBox6.Controls.Add(this.btNavLight);
            this.groupBox6.Controls.Add(this.btStrobe);
            this.groupBox6.Location = new System.Drawing.Point(176, 157);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(101, 120);
            this.groupBox6.TabIndex = 24;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Luzes";
            // 
            // btFarol
            // 
            this.btFarol.Location = new System.Drawing.Point(6, 82);
            this.btFarol.Name = "btFarol";
            this.btFarol.Size = new System.Drawing.Size(75, 23);
            this.btFarol.TabIndex = 2;
            this.btFarol.Text = "Farol";
            this.btFarol.UseVisualStyleBackColor = true;
            this.btFarol.Click += new System.EventHandler(this.btFarol_Click);
            // 
            // btNavLight
            // 
            this.btNavLight.Location = new System.Drawing.Point(6, 53);
            this.btNavLight.Name = "btNavLight";
            this.btNavLight.Size = new System.Drawing.Size(75, 23);
            this.btNavLight.TabIndex = 1;
            this.btNavLight.Text = "Nav";
            this.btNavLight.UseVisualStyleBackColor = true;
            this.btNavLight.Click += new System.EventHandler(this.btNavLight_Click);
            // 
            // btStrobe
            // 
            this.btStrobe.Location = new System.Drawing.Point(6, 24);
            this.btStrobe.Name = "btStrobe";
            this.btStrobe.Size = new System.Drawing.Size(75, 23);
            this.btStrobe.TabIndex = 0;
            this.btStrobe.Text = "Strobe";
            this.btStrobe.UseVisualStyleBackColor = true;
            this.btStrobe.Click += new System.EventHandler(this.btStrobe_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btEmergencia);
            this.groupBox5.Location = new System.Drawing.Point(6, 220);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(153, 57);
            this.groupBox5.TabIndex = 23;
            this.groupBox5.TabStop = false;
            // 
            // btEmergencia
            // 
            this.btEmergencia.Location = new System.Drawing.Point(6, 19);
            this.btEmergencia.Name = "btEmergencia";
            this.btEmergencia.Size = new System.Drawing.Size(141, 23);
            this.btEmergencia.TabIndex = 3;
            this.btEmergencia.Text = "RETORNO CASA";
            this.btEmergencia.UseVisualStyleBackColor = true;
            // 
            // btSync
            // 
            this.btSync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSync.Location = new System.Drawing.Point(507, 12);
            this.btSync.Name = "btSync";
            this.btSync.Size = new System.Drawing.Size(75, 23);
            this.btSync.TabIndex = 24;
            this.btSync.Text = "Sync";
            this.btSync.UseVisualStyleBackColor = true;
            this.btSync.Click += new System.EventHandler(this.btSync_Click);
            // 
            // rdApNextWayPoint
            // 
            this.rdApNextWayPoint.AutoSize = true;
            this.rdApNextWayPoint.Location = new System.Drawing.Point(176, 103);
            this.rdApNextWayPoint.Name = "rdApNextWayPoint";
            this.rdApNextWayPoint.Size = new System.Drawing.Size(76, 17);
            this.rdApNextWayPoint.TabIndex = 22;
            this.rdApNextWayPoint.TabStop = true;
            this.rdApNextWayPoint.Text = "Proximo W";
            this.rdApNextWayPoint.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.QntaBat);
            this.groupBox4.Controls.Add(this.QntFuel);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Location = new System.Drawing.Point(6, 145);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(153, 69);
            this.groupBox4.TabIndex = 21;
            this.groupBox4.TabStop = false;
            // 
            // QntaBat
            // 
            this.QntaBat.AutoSize = true;
            this.QntaBat.Location = new System.Drawing.Point(53, 41);
            this.QntaBat.Name = "QntaBat";
            this.QntaBat.Size = new System.Drawing.Size(16, 13);
            this.QntaBat.TabIndex = 22;
            this.QntaBat.Text = "0 ";
            // 
            // QntFuel
            // 
            this.QntFuel.AutoSize = true;
            this.QntFuel.Location = new System.Drawing.Point(53, 16);
            this.QntFuel.Name = "QntFuel";
            this.QntFuel.Size = new System.Drawing.Size(24, 13);
            this.QntFuel.TabIndex = 21;
            this.QntFuel.Text = "0 %";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "FUEL";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(28, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "BAT";
            // 
            // apNav
            // 
            this.apNav.DataSource = this.bdsWayPoints;
            this.apNav.DisplayMember = "Id";
            this.apNav.FormattingEnabled = true;
            this.apNav.Location = new System.Drawing.Point(87, 99);
            this.apNav.Name = "apNav";
            this.apNav.Size = new System.Drawing.Size(83, 21);
            this.apNav.TabIndex = 15;
            // 
            // bdsWayPoints
            // 
            this.bdsWayPoints.DataMember = "ListaWayPoints";
            this.bdsWayPoints.DataSource = this.bdsModel;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(67, 105);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(14, 13);
            this.radioButton2.TabIndex = 14;
            this.radioButton2.TabStop = true;
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Nav:";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(67, 75);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(14, 13);
            this.radioButton1.TabIndex = 11;
            this.radioButton1.TabStop = true;
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // apHdg
            // 
            this.apHdg.Location = new System.Drawing.Point(87, 70);
            this.apHdg.Name = "apHdg";
            this.apHdg.Size = new System.Drawing.Size(82, 20);
            this.apHdg.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Hdg:";
            // 
            // rdApAlt
            // 
            this.rdApAlt.AutoSize = true;
            this.rdApAlt.Location = new System.Drawing.Point(67, 44);
            this.rdApAlt.Name = "rdApAlt";
            this.rdApAlt.Size = new System.Drawing.Size(14, 13);
            this.rdApAlt.TabIndex = 8;
            this.rdApAlt.TabStop = true;
            this.rdApAlt.UseVisualStyleBackColor = true;
            // 
            // apAlt
            // 
            this.apAlt.Location = new System.Drawing.Point(87, 39);
            this.apAlt.Name = "apAlt";
            this.apAlt.Size = new System.Drawing.Size(82, 20);
            this.apAlt.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Altitude:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "AP MODO:";
            // 
            // btAdd
            // 
            this.btAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btAdd.Location = new System.Drawing.Point(594, 12);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(75, 23);
            this.btAdd.TabIndex = 2;
            this.btAdd.Text = "Add";
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(426, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "WAYPOINTS:";
            // 
            // dgvWay
            // 
            this.dgvWay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvWay.AutoGenerateColumns = false;
            this.dgvWay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWay.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Del,
            this.Id,
            this.latitudeDataGridViewTextBoxColumn,
            this.longitudeDataGridViewTextBoxColumn,
            this.altitudeDataGridViewTextBoxColumn});
            this.dgvWay.DataSource = this.bdsWayPoints;
            this.dgvWay.Location = new System.Drawing.Point(283, 41);
            this.dgvWay.Name = "dgvWay";
            this.dgvWay.RowHeadersVisible = false;
            this.dgvWay.Size = new System.Drawing.Size(386, 236);
            this.dgvWay.TabIndex = 0;
            this.dgvWay.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWay_CellContentClick);
            // 
            // Del
            // 
            this.Del.HeaderText = "Del";
            this.Del.Name = "Del";
            this.Del.Text = "Del";
            this.Del.ToolTipText = "Delete WayPoint";
            this.Del.Width = 30;
            // 
            // Id
            // 
            this.Id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Width = 30;
            // 
            // latitudeDataGridViewTextBoxColumn
            // 
            this.latitudeDataGridViewTextBoxColumn.DataPropertyName = "Latitude";
            this.latitudeDataGridViewTextBoxColumn.HeaderText = "Latitude";
            this.latitudeDataGridViewTextBoxColumn.Name = "latitudeDataGridViewTextBoxColumn";
            // 
            // longitudeDataGridViewTextBoxColumn
            // 
            this.longitudeDataGridViewTextBoxColumn.DataPropertyName = "Longitude";
            this.longitudeDataGridViewTextBoxColumn.HeaderText = "Longitude";
            this.longitudeDataGridViewTextBoxColumn.Name = "longitudeDataGridViewTextBoxColumn";
            // 
            // altitudeDataGridViewTextBoxColumn
            // 
            this.altitudeDataGridViewTextBoxColumn.DataPropertyName = "Altitude";
            this.altitudeDataGridViewTextBoxColumn.HeaderText = "Altitude";
            this.altitudeDataGridViewTextBoxColumn.Name = "altitudeDataGridViewTextBoxColumn";
            // 
            // FormCockPit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 598);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "FormCockPit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormCockPit";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormCockPit_FormClosing);
            this.Load += new System.EventHandler(this.FormCockPit_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpv)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bdsModel)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdsWayPoints)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox fpv;
        private System.Windows.Forms.GroupBox groupBox2;
        private AltimeterInstrumentControl altimeterInstrumentControl1;
        private HeadingIndicatorInstrumentControl headingIndicatorInstrumentControl1;
        private AirSpeedIndicatorInstrumentControl airSpeedIndicatorInstrumentControl1;
        private AttitudeIndicatorInstrumentControl attitudeIndicatorInstrumentControl1;
        //private FC.GEPluginCtrls.GEWebBrowser webBrowser;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvWay;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.BindingSource bdsModel;
        private System.Windows.Forms.BindingSource bdsWayPoints;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox apNav;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.MaskedTextBox apHdg;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rdApAlt;
        private System.Windows.Forms.MaskedTextBox apAlt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RadioButton rdApNextWayPoint;
        private System.Windows.Forms.Button btSync;
        private System.Windows.Forms.Button btnStartStopRecording;
        private System.Windows.Forms.Label QntaBat;
        private System.Windows.Forms.Label QntFuel;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btFarol;
        private System.Windows.Forms.Button btNavLight;
        private System.Windows.Forms.Button btStrobe;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btEmergencia;
        private System.Windows.Forms.ComboBox cmbVideoDevices;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.DataGridViewButtonColumn Del;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn latitudeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn longitudeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn altitudeDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btAtualizar;

    }
}