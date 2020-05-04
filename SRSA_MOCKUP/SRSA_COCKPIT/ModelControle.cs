using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO.Ports;
using CaptureCam;
using System.Windows.Forms;
namespace SRSA_MOCKUP
{
    public class ModelControle : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        #region Declaração de Variáveis

        private int quantFuel;
        private int hdg;
        private Boolean autopilot;
        private String teste;
        ArduinoComm arduinoComm;
        static System.IO.Ports.SerialPort serialPort1;
        System.ComponentModel.IContainer components = new System.ComponentModel.Container();
        CaptureCam.CaptureCam device = new CaptureCam.CaptureCam();

        public String Teste
        {
            get { return teste; }
            set
            {
                teste = value;
                OnPropertyChanged("Teste");
            }
        }

        public void Capturar(PictureBox pic)
        {
            device.Capturar(ref pic);
        }
        #endregion
        #region Metódos Publicos
        public ModelControle()
        {
            arduinoComm = new ArduinoComm();

        }


        public int QuantFuel
        {
            get { return quantFuel; }
            set { quantFuel = value; }
        }

        public int Hdg
        {
            get { return hdg; }
            set { hdg = value; }
        }

        public Boolean Autopilot
        {
            get { return autopilot; }
            set { autopilot = value; }
        }


        public String Xteste()
        {
            teste = @"<?xml version=""1.0""?>";

            return  arduinoComm.ReturnStatus();

        }

        public void fTeste()
        {
            teste =@"<?xml version=""1.0""?>";

            Teste += arduinoComm.ReturnStatus();
            
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.LoadXml(Teste);
            System.Xml.XmlNode element = doc.SelectSingleNode("/XML/STATUS/AUTOPILOT");
           
        }

        #endregion

        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

    }
}
