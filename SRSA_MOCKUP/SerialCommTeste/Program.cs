using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace SerialCommTeste
{
    class Program
    {
        private static System.IO.Ports.SerialPort serialPort1;
        static void Main(string[] args)
        {

            char[] buffer = new char[100];
            String command;
            System.ComponentModel.IContainer components = new System.ComponentModel.Container();
            serialPort1 = new System.IO.Ports.SerialPort(components);
            serialPort1.PortName = "COM7";
            serialPort1.BaudRate = 9600;

            serialPort1.Open();
            if (!serialPort1.IsOpen)
            {
                Console.WriteLine("Oops");
                return;
            }

            // this turns on !
            serialPort1.DtrEnable = true;

            // callback for text coming back from the arduino
            serialPort1.DataReceived += OnReceived;

            // give it 5 secs to start up the sketch
            System.Threading.Thread.Sleep(5000);
            
 
                using (serialPort1)
                {
                    while (1 == 1)
                    {
                    /*Console.WriteLine("Entre o valor");
                    command = Console.ReadLine().ToUpper();
                    buffer = command.ToCharArray();

                    serialPort1.Write(buffer, 0, buffer.Length);
                    Console.WriteLine("Comando enviado !");
                    System.Threading.Thread.Sleep(10);
                     * */

                        string cmd = "#cHdg:124 *";
                        buffer = cmd.ToCharArray();

                        serialPort1.Write(buffer, 0, buffer.Length);
                        Console.WriteLine("Comando enviado !");
                        System.Threading.Thread.Sleep(1000
                            );
                    }
                }
        }
        private static void OnReceived(object sender, SerialDataReceivedEventArgs c)
        {
            try
            {
                Console.Write(serialPort1.ReadExisting());
            }
            catch (Exception ex) { }
        }
    }
}
