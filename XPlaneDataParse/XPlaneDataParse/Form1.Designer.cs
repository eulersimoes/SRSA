namespace XPlaneDataParse
{
    partial class Form1
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
            this.btStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btStop = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUDP = new System.Windows.Forms.TextBox();
            this.txtSerial = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUdpIn = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btStart
            // 
            this.btStart.Location = new System.Drawing.Point(15, 57);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(75, 23);
            this.btStart.TabIndex = 0;
            this.btStart.Text = "Iniciar";
            this.btStart.UseVisualStyleBackColor = true;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "PORTA UDP:";
            // 
            // btStop
            // 
            this.btStop.Location = new System.Drawing.Point(96, 57);
            this.btStop.Name = "btStop";
            this.btStop.Size = new System.Drawing.Size(75, 23);
            this.btStop.TabIndex = 3;
            this.btStop.Text = "Parar";
            this.btStop.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "PORTA SERIAL:";
            // 
            // txtUDP
            // 
            this.txtUDP.Location = new System.Drawing.Point(106, 3);
            this.txtUDP.Name = "txtUDP";
            this.txtUDP.Size = new System.Drawing.Size(65, 20);
            this.txtUDP.TabIndex = 6;
            this.txtUDP.Text = "49003";
            // 
            // txtSerial
            // 
            this.txtSerial.Location = new System.Drawing.Point(106, 31);
            this.txtSerial.Name = "txtSerial";
            this.txtSerial.Size = new System.Drawing.Size(65, 20);
            this.txtSerial.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "UDP IN:";
            // 
            // txtUdpIn
            // 
            this.txtUdpIn.Location = new System.Drawing.Point(15, 110);
            this.txtUdpIn.Name = "txtUdpIn";
            this.txtUdpIn.Size = new System.Drawing.Size(151, 138);
            this.txtUdpIn.TabIndex = 9;
            this.txtUdpIn.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(178, 260);
            this.Controls.Add(this.txtUdpIn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSerial);
            this.Controls.Add(this.txtUDP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btStop);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btStart);
            this.Name = "Form1";
            this.Text = "XPlane Listener UDP";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btStop;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUDP;
        private System.Windows.Forms.TextBox txtSerial;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox txtUdpIn;
    }
}

