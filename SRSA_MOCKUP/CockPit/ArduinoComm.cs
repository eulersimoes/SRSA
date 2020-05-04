using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Threading;
using COCKPIT;

namespace AvionicsInstrumentsControls
{
    class ArduinoComm:IDisposable
    {
        System.ComponentModel.IContainer components = new System.ComponentModel.Container();
        private Boolean isConnected = false;
        private static Boolean resultadoRecebido = false;
        static bool _continue = true;
        private static Criptografia cryEngine = new Criptografia();

        private static String serialTxt = String.Empty;

        ThreadStart job;
        Thread thread;

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

        public string EnviarComando(String comando,Boolean isRetornable,Boolean isPriority)
        {
            resultadoRecebido = false;
            int count = 0;
            if (isRetornable)
            {
                SerialPortHolder.SerialPort1.DataReceived += OnReceived;
            }
            _continue = true;
            retorno = String.Empty;
            serialTxt = String.Empty;
            ExecutarComando(comando, isRetornable,isPriority);
            if (isRetornable)
            {
                SerialPortHolder.SerialPort1.DataReceived -= OnReceived;
            }

            if (isPriority)
            {
                while (resultadoRecebido == false && count < 700)
                {
                    Thread.Sleep(5);
                    count++;
                }
            }
            return retorno;
        }
  
        private static void OnReceived(object sender, SerialDataReceivedEventArgs c)
        {
            try
            {
                serialTxt = SerialPortHolder.SerialPort1.ReadLine().ToString();
                if (serialTxt.Contains("info"))
                {
                    retorno = serialTxt.ToString();
                    _continue = false;
                }
            }
            catch (Exception ex)
            {
                retorno += ex.Message;
            }
            finally
            {
                SerialPortHolder.IsBusy = false;
                resultadoRecebido = true;
            }

        }

        public static void ExecutarComando(String comm, Boolean possuiRetorno, Boolean isPriority)
        {
            int countExecute = 0;
            String command;
            
            command = cryEngine.Encrypt(comm);

            char[] buffer = new char[command.Length];

            for (int i = 0; i < command.Length; i++)
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

            if (!SerialPortHolder.IsBusy && !isPriority && !possuiRetorno)
            {
                SerialPortHolder.IsBusy = true;
                SerialPortHolder.SerialPort1.Write(buffer, 0, buffer.Length);
                SerialPortHolder.IsBusy = false;
            }
            else if (isPriority || possuiRetorno)
            {
                while (SerialPortHolder.IsBusy && countExecute <= 100)
                {
                    Thread.Sleep(1);
                    countExecute++;
                }
                if (!SerialPortHolder.IsBusy)
                {
                    SerialPortHolder.IsBusy = true;

                    SerialPortHolder.SerialPort1.Write(buffer, 0, buffer.Length);
                    while (SerialPortHolder.IsBusy && countExecute <= 100)
                    {
                        countExecute++;
                        Thread.Sleep(10);
                    }
                    SerialPortHolder.IsBusy = false;
                }
            }
            else
            {
                while (SerialPortHolder.IsBusy && countExecute <= 20)
                {
                    Thread.Sleep(1);
                    countExecute++;
                }
                if (!SerialPortHolder.IsBusy)
                {
                    SerialPortHolder.IsBusy = true;

                    SerialPortHolder.SerialPort1.Write(buffer, 0, buffer.Length);
                    while (SerialPortHolder.IsBusy && countExecute <= 100)
                    {
                        countExecute++;
                        Thread.Sleep(10);
                    }
                    SerialPortHolder.IsBusy = false;
                }
            }
        }

        public void Dispose()
        {
           
        }
    }
}
