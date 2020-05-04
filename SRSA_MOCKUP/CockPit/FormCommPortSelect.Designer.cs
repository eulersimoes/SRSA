namespace COCKPIT
{
    partial class FormCommPortSelect
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbPorta = new System.Windows.Forms.ComboBox();
            this.btIniciar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Porta:";
            // 
            // cmbPorta
            // 
            this.cmbPorta.FormattingEnabled = true;
            this.cmbPorta.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10",
            "COM11",
            "COM12",
            "COM13",
            "COM14",
            "COM15"});
            this.cmbPorta.Location = new System.Drawing.Point(53, 6);
            this.cmbPorta.Name = "cmbPorta";
            this.cmbPorta.Size = new System.Drawing.Size(121, 21);
            this.cmbPorta.TabIndex = 1;
            // 
            // btIniciar
            // 
            this.btIniciar.Location = new System.Drawing.Point(180, 6);
            this.btIniciar.Name = "btIniciar";
            this.btIniciar.Size = new System.Drawing.Size(75, 23);
            this.btIniciar.TabIndex = 2;
            this.btIniciar.Text = "Iniciar";
            this.btIniciar.UseVisualStyleBackColor = true;
            this.btIniciar.Click += new System.EventHandler(this.btIniciar_Click);
            // 
            // FormCommPortSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 35);
            this.Controls.Add(this.btIniciar);
            this.Controls.Add(this.cmbPorta);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormCommPortSelect";
            this.Text = "Comm Setup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbPorta;
        private System.Windows.Forms.Button btIniciar;
    }
}