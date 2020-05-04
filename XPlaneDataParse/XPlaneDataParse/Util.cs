using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO.Ports;
using System.Threading;

namespace XPlaneDataParse
{

    public class Util
    {
        SerialPort serialPort;
        String messagem = "";

        Double pitch = 0;
        Double roll = 0;
        Double alt = 0;
        Double hdg = 0;
        Double latitude = 0;
        Double longitude = 0;

        public Util()
        {
            serialPort = new SerialPort();
        }
        
        public void StartSerialPort(String commPort)
        {
            try
            {
                serialPort.PortName = commPort;
                serialPort.BaudRate = 9600;
                serialPort.Open();
            }
            catch (Exception ex)
            {

            }
        }

        public void WriteDataToSerial(XplaneDataSet dataSet)
        {
            if (dataSet == null)
            {
                return;
            }
            try
            {
                String sintaxe = dataSet.RawValue.Replace('#', ' ');
                String[] xplaneData = sintaxe.Split(';');
                messagem = "";
                for (int i = 0; i < xplaneData.Length; i++)
                {
                    String[] data = xplaneData[i].Split('|');
                    //
                    if (data[0].ToString().Trim() == "18")
                    {
                        pitch = Double.Parse(data[1].ToString());
                        roll = Double.Parse(data[2].ToString());
                        hdg = Double.Parse(data[3].ToString());
                    }
                    else if (data[0] == "22")
                    {
                        latitude = Double.Parse(data[1]);
                    }
                    else if (data[0] == "23")
                    {
                        longitude = Double.Parse(data[1]);
                    }
                    else if (data[0] == "24")
                    {
                        alt = Double.Parse(data[1]);
                    }
                }

                messagem = "#DEBUG;" + Convert.ToInt32(pitch) + ";" + Convert.ToInt32(roll) + ";" + Convert.ToInt32(alt) + ";" + Convert.ToInt32(hdg) + ";" + 0 + ";" + latitude + ";" + longitude + "*" + "\n";
                messagem = messagem.Replace(',', '.');
                if (!serialPort.IsOpen)
                {
                    this.StartSerialPort("COM6");
                }
                serialPort.Write(messagem);

            }catch(Exception ex ){

            }
        }

    }

}
