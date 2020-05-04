using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using SHAREDCLASSES;

namespace COCKPIT
{
    public static class NetduinoComm
    {
        static Thread threadSendMsg;
        static ThreadStart tsSendMsg;

        private static List<NetduinoMessage> listaMensagemEnviar = new List<NetduinoMessage>();

        private static List<NetduinoMessage> listaMensagemReceber = new List<NetduinoMessage>();

        public static List<NetduinoMessage> ListaMensagemReceber
        {
            get { return NetduinoComm.listaMensagemReceber; }
        }

        public static List<NetduinoMessage> ListaMensagemEnviar
        {
            get { return NetduinoComm.listaMensagemEnviar; }
        }

        public static void RemoveItem(int item)
        {
            ListaMensagemReceber.RemoveAt(item);
        }

        private static void SendMensagem()
        {
            if (listaMensagemEnviar.Count > 0)
            {
                String uncryptcomm = listaMensagemEnviar[0].MensageId + ";" + listaMensagemEnviar[0].Msg;
                String command = uncryptcomm;
                //String command = cryEngine.Encrypt(uncryptcomm);
                command = "*" + command + "#";
                for (int i = 0; i < 100; i++)
                {
                    SerialPortHolder.SerialPort1.WriteLine("stop");
                    Thread.Sleep(10);
                }

                SerialPortHolder.SerialPort1.WriteLine(" ");

                Thread.Sleep(500);

                SerialPortHolder.SerialPort1.WriteLine(command);

                if (listaMensagemEnviar.Count > 0)
                {
                    listaMensagemEnviar.RemoveAt(0);
                }
            }
        }

        private static void SendMsgWork()
        {
            while (true)
            {
                SendMensagem();
                Thread.Sleep(50);
            }
        }

        private static void OnReceived(object sender, SerialDataReceivedEventArgs c)
        {
            try
            {
                String resposta = SerialPortHolder.SerialPort1.ReadLine().ToString();
                resposta = resposta.Replace('#',' ');
                resposta = resposta.Replace('*', ' ');
                NetduinoMessage msgResp = new NetduinoMessage(listaMensagemReceber.Count, resposta, false);
                listaMensagemReceber.Add(msgResp);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                //Received(sender, new EventArgs());
            }
        }

        public static void Start()
        {
            SerialPortHolder.SerialPort1.DataReceived += OnReceived;
            tsSendMsg = new ThreadStart(SendMsgWork);
            threadSendMsg = new Thread(tsSendMsg);
            threadSendMsg.Start();
        }

        public static void AddMensage(String comando, Boolean isPriority)
        {

            NetduinoMessage msg = new NetduinoMessage(Util.GerarIdRandom(), comando, isPriority);
            if (isPriority)
            {
                listaMensagemEnviar.Insert(0, msg);
            }
            else
            {
                listaMensagemEnviar.Add(msg);
            }
        }
    }
}
