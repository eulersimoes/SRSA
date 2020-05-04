using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using System.IO.Ports;
using System.Threading;

namespace Gps
{
    public class InterfaceGps
    {
        SerialPort serialPort;

        public InterfaceGps(String ComPort)
        {
            //COM1 = PIN0
            //COM2 = PIN2

            serialPort = new SerialPort(ComPort, 9600, Parity.None, 8, StopBits.One);
            Thread.Sleep(5000);
            this.AbrirPorta(ComPort);
        }

        public void DesligarPorta()
        {
            serialPort.Close();
        }

        public void AbrirPorta(String ComPort)
        {
            if (!serialPort.IsOpen)
            {
                serialPort.Open();
            }
            else
            {
                throw new Exception("Porta Serial do Gps já esta aberta !");
            }
        }

        public String RetornarPosicaoAtual(string Setenca)
        {
            return RetornarSentenca(Setenca);
        }

        private String RetornarSentenca(String setenca)
        {

            int bytesToRead = serialPort.BytesToRead;
            String retorno = String.Empty;

            try
            {
                int count = 0;
                while (count <= 10)
                {
                    //10 tentativas
                    count++;
                    //start reading the stream
                    if (bytesToRead >= 512)
                    {
                        // get the waiting data
                        byte[] buffer = new byte[bytesToRead];
                        serialPort.Read(buffer, 0, buffer.Length);

                        //only accept a statement starting with $
                        if (buffer[0] != 36 && buffer.Length <= 1)
                            continue;

                        //copy the byte array to a readable string
                        String str = String.Empty;
                        str = new String(System.Text.Encoding.UTF8.GetChars(buffer));
                        //break up string into statements
                        String delimStr = "\r";
                        char[] delim = delimStr.ToCharArray();
                        string[] statements = null;
                        statements = str.Split(new Char[] { '\r', '\n' });

                        for (int i = 0; i < statements.Length; i++)
                        {
                            //only accept statements with a checksum
                            String currStatement = statements[i];
                            if (currStatement.Length > 0 && currStatement.IndexOf("*") >= 0)
                            {
                                //parseNMEA(statements[i]);
                                String statementType = statements[i].Substring(0, 6);
                                if (statementType.ToUpper() == setenca.ToUpper())
                                {
                                    retorno = statements[i];
                                    break;
                                }
                            }

                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Print("Erro Modulo Gps: " +ex.ToString());
            }
            return retorno;
        }

        private static void parseNMEA(String statement)
        {
            String statementType = statement.Substring(0, 6);

            switch (statementType)
            {
                case "$GPGGA":
                    ProcessGPGGA(statement);
                    break;
                case "$GPGSA":
                    ProcessGPGSA(statement);
                    break;
                case "$GPGSV":
                    ProcessGPGSV(statement);
                    break;
                case "$GPRMC":
                    ProcessGPRMC(statement);
                    break;
                default:
                    Debug.Print("Unrecognized NMEA String: " + statement);
                    break;
            }
        }

        private static void ProcessGPGGA(String nmeaStatement)
        {
            Debug.Print(nmeaStatement);
            /*
            $GPGGA,123519,4807.038,N,01131.000,E,1,08,0.9,545.4,M,46.9,M,,*47

            Where:
                    GGA          Global Positioning System Fix Data
                    123519       Fix taken at 12:35:19 UTC
                    4807.038,N   Latitude 48 deg 07.038' N
                    01131.000,E  Longitude 11 deg 31.000' E
                    1            Fix quality: 0 = invalid
                                            1 = GPS fix (SPS)
                                            2 = DGPS fix
                                            3 = PPS fix
                                            4 = Real Time Kinematic
                                            5 = Float RTK
                                            6 = estimated (dead reckoning) (2.3 feature)
                                            7 = Manual input mode
                                            8 = Simulation mode
                    08           Number of satellites being tracked
                    0.9          Horizontal dilution of position
                    545.4,M      Altitude, Meters, above mean sea level
                    46.9,M       Height of geoid (mean sea level) above WGS84
                                    ellipsoid
                    (empty field) time in seconds since last DGPS update
                    (empty field) DGPS station ID number
                    *47          the checksum data, always begins with *
             */
        }

        private static void ProcessGPGSA(String nmeaStatement)
        {
            Debug.Print(nmeaStatement);
            /*
            $GPGSA,A,3,04,05,,09,12,,,24,,,,,2.5,1.3,2.1*39

            Where:
                 GSA      Satellite status
                 A        Auto selection of 2D or 3D fix (M = manual) 
                 3        3D fix - values include: 1 = no fix
                                                   2 = 2D fix
                                                   3 = 3D fix
                 04,05... PRNs of satellites used for fix (space for 12) 
                 2.5      PDOP (dilution of precision) 
                 1.3      Horizontal dilution of precision (HDOP) 
                 2.1      Vertical dilution of precision (VDOP)
                 *39      the checksum data, always begins with *
             */
        }

        private static void ProcessGPGSV(String nmeaStatement)
        {
            Debug.Print(nmeaStatement);
            /*
            $GPGSV,2,1,08,01,40,083,46,02,17,308,41,12,07,344,39,14,22,228,45*75

            Where:
                  GSV          Satellites in view
                  2            Number of sentences for full data
                  1            sentence 1 of 2
                  08           Number of satellites in view

                  01           Satellite PRN number
                  40           Elevation, degrees
                  083          Azimuth, degrees
                  46           SNR - higher is better
                       for up to 4 satellites per sentence
                  *75          the checksum data, always begins with *
             */
        }

        private static void ProcessGPRMC(String nmeaStatement)
        {
            Debug.Print(nmeaStatement);

            /*
            $GPRMC,123519,A,4807.038,N,01131.000,E,022.4,084.4,230394,003.1,W*6A

            Where:
                 RMC          Recommended Minimum sentence C
                 123519       Fix taken at 12:35:19 UTC
                 A            Status A=active or V=Void.
                 4807.038,N   Latitude 48 deg 07.038' N
                 01131.000,E  Longitude 11 deg 31.000' E
                 022.4        Speed over the ground in knots
                 084.4        Track angle in degrees True
                 230394       Date - 23rd of March 1994
                 003.1,W      Magnetic Variation
                 *6A          The checksum data, always begins with *
             */
        }
    }
}
