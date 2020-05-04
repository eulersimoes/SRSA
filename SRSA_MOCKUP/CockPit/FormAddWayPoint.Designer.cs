namespace COCKPIT
{
    partial class FormAddWayPoint
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.btIncluir = new System.Windows.Forms.Button();
            this.txtAltitude = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtAltitude);
            this.groupBox1.Controls.Add(this.webBrowser);
            this.groupBox1.Controls.Add(this.btIncluir);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1146, 430);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // webBrowser
            // 
            this.webBrowser.Location = new System.Drawing.Point(9, 65);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(1131, 330);
            this.webBrowser.TabIndex = 12;
            
            // 
            // btIncluir
            // 
            this.btIncluir.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btIncluir.Location = new System.Drawing.Point(1035, 24);
            this.btIncluir.Name = "btIncluir";
            this.btIncluir.Size = new System.Drawing.Size(105, 23);
            this.btIncluir.TabIndex = 9;
            this.btIncluir.Text = "Incluir";
            this.btIncluir.UseVisualStyleBackColor = true;
            this.btIncluir.Click += new System.EventHandler(this.btIncluir_Click);
            // 
            // txtAltitude
            // 
            this.txtAltitude.Location = new System.Drawing.Point(58, 19);
            this.txtAltitude.Name = "txtAltitude";
            this.txtAltitude.Size = new System.Drawing.Size(100, 20);
            this.txtAltitude.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Altitude:";
            // 
            // FormAddWayPoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1170, 454);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormAddWayPoint";
            this.Text = "Add WayPoint";
            this.Load += new System.EventHandler(this.FormAddWayPoint_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.Button btIncluir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox txtAltitude;
    }
}