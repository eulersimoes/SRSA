using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;

namespace AvionicsInstrumentsControls
{
    public partial class Form1 : Form
    {
        ModelControle model = new ModelControle();
        System.Threading.Thread t;
        System.Threading.ThreadStart ts;

        public Form1()
        {
            InitializeComponent();
            bdsControle.DataSource = model;
            model.Capturar(fpv);


        }

        private void button1_Click(object sender, EventArgs e)
        {
            CallThread();
        }

        public void CallThread()
        {
            ts = new System.Threading.ThreadStart(ExecuteThread);
            t = new System.Threading.Thread(ts);
            t.IsBackground = true;
            t.Start();
        }

        private void ExecuteThread()
        {
            String valor;
            MethodInvoker updateDelegate = new MethodInvoker(UpdateStatus);
            while (t.IsAlive)
            {
                txtTeste.Text = model.Xteste();
                //Invoke(new MethodInvoker(UpdateStatus));
                Thread.Sleep(1000);
            }

        }

        private void UpdateStatus()
        {
          
        }
    }
}
