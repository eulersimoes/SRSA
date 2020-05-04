using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;

namespace SRSA_MOCKUP
{
    class ArduinoComm
    {
        System.ComponentModel.IContainer components = new System.ComponentModel.Container();
        private Boolean isConnected = false;
        Thread readThread;
        static bool _continue;
        public Boolean IsConnected
        {
            get { return isConnected; }
            set { isConnected = value; }
        }
        static String retorno = String.Empty;

        public ArduinoComm()
        {
            this.Connect();
        }

        public void Connect()
        {

        }

        public string ReturnStatus()
        {
            char[] buffer = new char[100];
            String command;
            readThread = new Thread(Read);


            //using (SerialPortHolder.SerialPort1)
            //{
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

                _continue = true;
                readThread.Start();
                SerialPortHolder.SerialPort1.Write(buffer, 0, 99);
                
                
                readThread.Join();
                return retorno;

            //}
        }
        private static void OnReceived(object sender, SerialDataReceivedEventArgs c)
        {
            try
            {
                retorno = SerialPortHolder.SerialPort1.ReadExisting().ToString();
            }
            catch (Exception ex)
            {
                retorno += "<xml>";
                retorno += "<erro>";
                retorno += ex.Message;
                retorno += "</erro>";
                retorno += "</xml>";
            }
        }

        public static void Read()
        {
            while (_continue)
            {
                try
                {
                    string message = SerialPortHolder.SerialPort1.ReadLine();
                    if (message.Contains("</XML>"))
                    {
                        retorno = message;
                        _continue = false;
                    }
                }
                catch (Exception ex) 
                {
                    throw ex;
                }
            }
        }
    }
}
