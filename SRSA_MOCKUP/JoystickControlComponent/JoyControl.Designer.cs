namespace JoystickControlComponent
{
    partial class JoyControl
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblCross = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblCross
            // 
            this.lblCross.AutoSize = true;
            this.lblCross.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCross.Location = new System.Drawing.Point(12, 19);
            this.lblCross.Name = "lblCross";
            this.lblCross.Size = new System.Drawing.Size(18, 20);
            this.lblCross.TabIndex = 0;
            this.lblCross.Text = "+";
            // 
            // JoyControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblCross);
            this.Name = "JoyControl";
            this.Size = new System.Drawing.Size(148, 148);
            this.Load += new System.EventHandler(this.JoyControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCross;
    }
}
