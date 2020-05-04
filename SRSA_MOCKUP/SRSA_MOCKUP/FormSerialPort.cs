using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SRSA_MOCKUP
{
    public partial class FormSerialPort : Form
    {
        IContainer comp;
        private String serialPort;
        public FormSerialPort()
        {
            InitializeComponent();
        }

        public String getSerialPort()
        {
            return serialPort;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            serialPort = "COM" + cbbox.SelectedItem.ToString();
            this.Close();

        }

        private void FormSerialPort_Load(object sender, EventArgs e)
        {

        }
    }
}
