using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace XPlaneDataParse
{
    public partial class Form1 : Form
    {
        static byte[] data = new byte[1024];
        static UdpClient newsock;
        static IPEndPoint ipep;

        Util util = new Util();

        ThreadStart ts;
        Thread threadListner;
        static XplaneDataSet xd;
        ThreadStart tsMsg;
        Thread threadMsg;
        IPEndPoint send = new IPEndPoint(IPAddress.Any, 0);

        static String updIn="";
        public Form1()
        {
            InitializeComponent();
            ts = new ThreadStart(TsList);
            threadListner = new Thread(TsList);

            tsMsg = new ThreadStart(TsMsg);
            threadMsg = new Thread(tsMsg);
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            try
            {
                ipep = new IPEndPoint(IPAddress.Any, int.Parse(txtUDP.Text));
                newsock = new UdpClient(ipep);

                threadListner.Start();
                threadMsg.Start();
                Console.WriteLine("Waiting for a client...");

                Console.WriteLine("X-Plane Data Read: \n\n");

            }
            catch (Exception ex)
            {
            }
        }

        private void TsList()
        {
            while (true)
            {
                data = newsock.Receive(ref send);
                xd = new XplaneDataSet();
                xd.Read(data);
                
            }
        }

        private void TsMsg()
        {
            while (true)
            {
                util.WriteDataToSerial(xd);
                Thread.Sleep(100);
            }
        }

        private void UpdateUdpInfo()
        {
            txtUdpIn.Text += updIn;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
