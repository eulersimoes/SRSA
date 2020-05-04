using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace COCKPIT
{
    public partial class FormCommPortSelect : Form
    {
        public String commPort = String.Empty;
        public FormCommPortSelect()
        {
            InitializeComponent();
        }

        private void btIniciar_Click(object sender, EventArgs e)
        {
            commPort = cmbPorta.Text;
            DialogResult = DialogResult.OK;
        }
    }
}
