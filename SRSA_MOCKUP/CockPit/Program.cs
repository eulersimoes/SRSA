using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ComponentModel;
using AvionicsInstrumentControlDemo;
using COCKPIT;
using System.Threading;
using System.Globalization;

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
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            String commPort;
            IContainer components = new System.ComponentModel.Container();
            FormCommPortSelect frmCommSetup = new FormCommPortSelect();
            frmCommSetup.ShowDialog();
            commPort = frmCommSetup.commPort;
            try
            {
                //Service.planeInfoService.OpenSerialPort("");
                SerialPortHolder.Start(components, commPort);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro no Servidor: " + ex.ToString());
            }

            /*
            try
            {
                Application.Run(new FormCockPit());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro na aplicação: " + ex.ToString());
            }
            finally
            {
                Service.planeInfoService.CloseSerialPort("");
            }
             * */

            using (SerialPortHolder.SerialPort1)
            {
                Application.Run(new FormFpv());
            }
        }
    }
}
