using System;
using Microsoft.SPOT;
using SecretLabs.NETMF.Hardware.Netduino;
using System.Threading;
using Microsoft.SPOT.Hardware;
using System.IO.Ports;
using System.Text;
using Model;
namespace MFConsoleGps
{
    public class Program
    {
        public static void Main()
        {
            try
            {
                Thread.Sleep(5000);
                AppModel model = new AppModel("COM1", "COM2");
                model.Executar();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message.ToString());
            }
          
        }
    }
}
