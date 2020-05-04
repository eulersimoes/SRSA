namespace COCKPIT
{
    partial class FormFpv
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
            this.cmbVideoDevices = new System.Windows.Forms.ComboBox();
            this.btnStartStopRecording = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.fpv = new System.Windows.Forms.PictureBox();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.attitudeIndicatorInstrumentControl1 = new AvionicsInstrumentControlDemo.AttitudeIndicatorInstrumentControl();
            this.airSpeedIndicatorInstrumentControl1 = new AvionicsInstrumentControlDemo.AirSpeedIndicatorInstrumentControl();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpv)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbVideoDevices
            // 
            this.cmbVideoDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVideoDevices.FormattingEnabled = true;
            this.cmbVideoDevices.Location = new System.Drawing.Point(66, 3);
            this.cmbVideoDevices.Name = "cmbVideoDevices";
            this.cmbVideoDevices.Size = new System.Drawing.Size(165, 21);
            this.cmbVideoDevices.TabIndex = 8;
            // 
            // btnStartStopRecording
            // 
            this.btnStartStopRecording.Location = new System.Drawing.Point(237, 3);
            this.btnStartStopRecording.Name = "btnStartStopRecording";
            this.btnStartStopRecording.Size = new System.Drawing.Size(75, 23);
            this.btnStartStopRecording.TabIndex = 7;
            this.btnStartStopRecording.Text = "Broad/REC";
            this.btnStartStopRecording.UseVisualStyleBackColor = true;
            this.btnStartStopRecording.Click += new System.EventHandler(this.btnStartStopRecording_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Video IN:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.airSpeedIndicatorInstrumentControl1);
            this.groupBox1.Controls.Add(this.attitudeIndicatorInstrumentControl1);
            this.groupBox1.Controls.Add(this.webBrowser);
            this.groupBox1.Controls.Add(this.fpv);
            this.groupBox1.Location = new System.Drawing.Point(8, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(863, 450);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // fpv
            // 
            this.fpv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fpv.Location = new System.Drawing.Point(6, 9);
            this.fpv.Name = "fpv";
            this.fpv.Size = new System.Drawing.Size(851, 435);
            this.fpv.TabIndex = 0;
            this.fpv.TabStop = false;
            // 
            // webBrowser
            // 
            this.webBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser.Location = new System.Drawing.Point(566, 9);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(291, 120);
            this.webBrowser.TabIndex = 1;
            // 
            // attitudeIndicatorInstrumentControl1
            // 
            this.attitudeIndicatorInstrumentControl1.Location = new System.Drawing.Point(133, 9);
            this.attitudeIndicatorInstrumentControl1.Name = "attitudeIndicatorInstrumentControl1";
            this.attitudeIndicatorInstrumentControl1.PitchAngle = 0D;
            this.attitudeIndicatorInstrumentControl1.RollAngle = 0D;
            this.attitudeIndicatorInstrumentControl1.Size = new System.Drawing.Size(120, 120);
            this.attitudeIndicatorInstrumentControl1.TabIndex = 12;
            this.attitudeIndicatorInstrumentControl1.Text = "attitudeIndicatorInstrumentControl1";
            // 
            // airSpeedIndicatorInstrumentControl1
            // 
            this.airSpeedIndicatorInstrumentControl1.AirSpeed = 0;
            this.airSpeedIndicatorInstrumentControl1.Location = new System.Drawing.Point(7, 9);
            this.airSpeedIndicatorInstrumentControl1.Name = "airSpeedIndicatorInstrumentControl1";
            this.airSpeedIndicatorInstrumentControl1.Size = new System.Drawing.Size(120, 120);
            this.airSpeedIndicatorInstrumentControl1.TabIndex = 13;
            this.airSpeedIndicatorInstrumentControl1.Text = "airSpeedIndicatorInstrumentControl1";
            // 
            // FormFpv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 492);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbVideoDevices);
            this.Controls.Add(this.btnStartStopRecording);
            this.Name = "FormFpv";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormFpv";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormFpv_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbVideoDevices;
        private System.Windows.Forms.Button btnStartStopRecording;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox fpv;
        private System.Windows.Forms.WebBrowser webBrowser;
        private AvionicsInstrumentControlDemo.AttitudeIndicatorInstrumentControl attitudeIndicatorInstrumentControl1;
        private AvionicsInstrumentControlDemo.AirSpeedIndicatorInstrumentControl airSpeedIndicatorInstrumentControl1;
    }
}