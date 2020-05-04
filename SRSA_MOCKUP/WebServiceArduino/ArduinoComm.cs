using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO.Ports;

namespace WebServiceArduino
{

    public class ArduinoComm
    {
        static System.IO.Ports.SerialPort serialPort1;
        System.ComponentModel.IContainer components = new System.ComponentModel.Container();
        private Boolean isConnected = false;

        public Boolean IsConnected
        {
            get { return isConnected; }
            set { isConnected = value; }
        }
        static String retorno;

        public void Connect()
        {
            serialPort1 = new System.IO.Ports.SerialPort(components);
            serialPort1.PortName = "COM3";
            serialPort1.BaudRate = 9600;
            serialPort1.Open();
            isConnected = true;

            // this turns on !
            serialPort1.DtrEnable = true;

            // callback for text coming back from the arduino
            serialPort1.DataReceived += OnReceived;

            // give it 5 secs to start up the sketch
            System.Threading.Thread.Sleep(5000);
        }


        public string ReturnStatus()
        {
            char[] buffer = new char[100];
            String command;

            if (!serialPort1.IsOpen)
            {
                try
                {
                    Connect();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Oops");
                    return null;
                }
            }

            using (serialPort1)
            {
                command = "STATUS";
                for (int i = 0; i < 100; i++)
                {
                    char[] charAux = command.ToCharArray();
                    try
                    {
                        buffer[i] = charAux[i];
                    }
                    catch
                    {
                        buffer[i] = ' ';
                    }
                }

                serialPort1.Write(buffer, 0, 99);
                System.Threading.Thread.Sleep(10000);

                return retorno;

            }
        }
        private static void OnReceived(object sender, SerialDataReceivedEventArgs c)
        {
            try
            {
                retorno =serialPort1.ReadExisting().ToString();
            }
            catch (Exception exc) 
            {

            }
        }
    }
}