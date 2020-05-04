using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.ComponentModel;

namespace COCKPIT
{
    public static class SerialPortHolder
    {
        static SerialPort serialPort1;
        private static Boolean isBusy = false;

        public static Boolean IsBusy
        {
            get { return SerialPortHolder.isBusy; }
            set { SerialPortHolder.isBusy = value; }
        }

        public static SerialPort SerialPort1
        {
            get { return SerialPortHolder.serialPort1; }
            set { SerialPortHolder.serialPort1 = value; }
        }

        public static void Write(byte[] buffer, int offset, int count)
        {
            SerialPort1.Write(buffer, offset, count);
        }
 
        public static void Start(IContainer components,String serialPort)
        {
            try
            {
                serialPort1 = new SerialPort(components);
                serialPort1.PortName = serialPort;
                serialPort1.BaudRate = 9600;
                serialPort1.Open();
                serialPort1.DtrEnable = true;
                System.Threading.Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                throw new Exception("Dispositivo não iniciou, impossível iniciar modulo de comandos/telemetria ! " + ex.Message); 
            }
        }
    }
}
