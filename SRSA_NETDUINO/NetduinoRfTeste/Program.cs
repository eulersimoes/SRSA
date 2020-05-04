using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using System.IO.Ports;
using System.Text;

namespace NetduinoRfTeste
{
    public class Program
    {
        private static bool pin13Value;
        private static OutputPort pin13 = new OutputPort(Pins.GPIO_PIN_D13, false);
        
        public static void Main()
        {
            byte[] buffer = new byte[32];
            OutputPort ledPort = new OutputPort(Pins.ONBOARD_LED, false);
            SerialPort port = new SerialPort("COM1", 9600);
            port.ReadTimeout = 0;
            port.Open();

            ledPort.Write(true);
            Thread.Sleep(5000);
            ledPort.Write(false);
            while (true)
            {
                int count = port.Read(buffer, 0, buffer.Length);
                if (count > 0)
                {
                    char[] chars = Encoding.UTF8.GetChars(buffer);
                    if (chars[0] == 'h')
                        ledPort.Write(true);
                    if (chars[0] == 'l')
                        ledPort.Write(false);
                    Debug.Print(buffer.ToString());
                    pin13.Write(pin13Value);
                    port.Write(buffer, 0, count);
                }
            }
        }
    }
}
