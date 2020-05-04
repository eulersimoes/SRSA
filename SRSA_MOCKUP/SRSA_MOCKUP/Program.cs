using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel;

namespace AvionicsInstrumentsControls
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            IContainer components = new System.ComponentModel.Container();
            try
            {
                SerialPortHolder.Start(components);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Erro!");
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (SerialPortHolder.SerialPort1)
            {
                Application.Run(new Form1());
            }
        }
    }
}
