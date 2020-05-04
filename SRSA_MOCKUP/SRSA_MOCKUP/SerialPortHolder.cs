using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.ComponentModel;

namespace AvionicsInstrumentsControls
{
    public static class SerialPortHolder
    {
        static SerialPort serialPort1;

        public static SerialPort SerialPort1
        {
            get { return SerialPortHolder.serialPort1; }
            set { SerialPortHolder.serialPort1 = value; }
        }
        
 
        public static void Start(IContainer components)
        {
            try
            {
                serialPort1 = new SerialPort(components);
                serialPort1.PortName = "COM3";
                serialPort1.BaudRate = 9600;
                serialPort1.Open();
                // this turns on !
                serialPort1.DtrEnable = true;

                // give it 5 secs to start up the sketch
                System.Threading.Thread.Sleep(5000);
            }
            catch (Exception ex)
            {
                throw new Exception("Arduino não iniciou, impossível iniciar modulo de comandos/telemetria ! " + ex.Message); 
            }
        }
    }
}
