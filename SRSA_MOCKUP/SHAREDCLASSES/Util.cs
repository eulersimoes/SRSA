using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Globalization;
using System.Xml;
using Entity;

namespace SHAREDCLASSES
{
    public static class Util
    {
        private static Random random = new Random();

        public static String AddWayHomeCommand(WayPoint wayPoint)
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            String commando = "ADDHOMEPOINT;" + wayPoint.Id + ";" + wayPoint.Latitude + ";" + wayPoint.Longitude + ";" + wayPoint.Altitude;
            Thread.CurrentThread.CurrentCulture = currentCulture;
            return commando;
        }

        public static String AddWayPointCommand(WayPoint wayPoint)
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            String commando = "ADDWAYPOINT;" + wayPoint.Id + ";" + wayPoint.Latitude + ";" + wayPoint.Longitude + ";" + wayPoint.Altitude;
            Thread.CurrentThread.CurrentCulture = currentCulture;
            return commando;
        }

        public static String RemoveWayPointCommand(WayPoint wayPoint)
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            String commando = "REMOVERWAYPOINT;" + wayPoint.Id;
            Thread.CurrentThread.CurrentCulture = currentCulture;
            return commando;
        }

        public static string RemoveWayPointsCommand()
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            String commando = "REMOVERWAYPOINTS";
            Thread.CurrentThread.CurrentCulture = currentCulture;
            return commando;
        }

        public static String MoveServoAlerion(int valor)
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            String commando = "SETSERVOALERION;" + valor;
            Thread.CurrentThread.CurrentCulture = currentCulture;
            return commando;
        }

        public static String MoveServoElevator(int valor)
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            String commando = "SETSERVOELEVATORPOS;" + valor;
            Thread.CurrentThread.CurrentCulture = currentCulture;
            return commando;
        }

        public static String AutoPilotTurnOnOffCommand(Boolean value)
        {
            String command;
            String AutoPilotStatus;
            if (value == true)
            {
                AutoPilotStatus = "ON";
            }
            else
            {
                AutoPilotStatus = "OFF";
            }
            command = "AUTOPILOT;" + AutoPilotStatus;
            return command;
        }

        public static String LigarFarolBoolean(Boolean value)
        {
            String command;
            String FarolStatus;
            if (value == true)
            {
                FarolStatus = "ON";
            }
            else
            {
                FarolStatus = "OFF";
            }
            command = "SETFAROL;" + FarolStatus;
            return command;
        }

        public static String LigarLuzesNav(Boolean value)
        {
            String command;
            String Status;
            if (value == true)
            {
                Status = "ON";
            }
            else
            {
                Status = "OFF";
            }
            command = "SETNAVLIGHT;" + Status;
            return command;
        }

        public static String LigarStrobes(Boolean value)
        {
            String command;
            String Status;
            if (value == true)
            {
                Status = "ON";
            }
            else
            {
                Status = "OFF";
            }
            command = "SETSTROBE;" + Status;
            return command;
        }

        public static String SysncWayPointNetduino()
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            String commando = "WAYPOINTINFO";
            Thread.CurrentThread.CurrentCulture = currentCulture;
            return commando;
        }

        public static List<WayPoint> ListaWayPointSync(String wayPoint)
        {
            List<WayPoint> lista = new List<WayPoint>();
            String[] linha = wayPoint.Split(':');
            for (int i = 0; i < linha.Length; i++) 
            {
                WayPoint w = new WayPoint();
                String[] Campo = linha[i].Split(';');

                w.Id = int.Parse(Campo[0]);
                w.Latitude = Double.Parse(Campo[1]);
                w.Longitude = Double.Parse(Campo[2]);
                w.Altitude = Double.Parse(Campo[3]);
                lista.Add(w);
            }

            return lista;
        }

        public static int GerarIdRandom()
        {
            return random.Next(1000);

        }

        public static Double ConvertLatitudeToGoogleMapsFormat(Double latitude)
        {
            String lat = latitude.ToString();
            lat = lat.Remove(lat.IndexOf("."), 1);
            if (latitude < 0)
            {
                double mmmmm = double.Parse(latitude.ToString().Substring(3, 7));
                mmmmm = mmmmm / 60;
                lat = lat.Substring(0, 3);
                lat = ((mmmmm + Math.Abs(double.Parse(lat))) * -1).ToString();
            }
            else
            {
                double mmmmm = double.Parse(latitude.ToString().Substring(2, 7));
                mmmmm = mmmmm / 60;
                lat = lat.Substring(0, 2);
                lat = ((mmmmm + Math.Abs(double.Parse(lat)))).ToString();
            }
            latitude = Double.Parse(lat);
            return latitude;
        }

        public static Double ConvertLongitudeToGoogleMapsFormat(Double longitude)
        {
            String longi = longitude.ToString();
            longi = longi.Remove(longi.IndexOf("."), 1);
            if (longitude < 0)
            {
                double mmmmm = double.Parse(longitude.ToString().Substring(3, 7));
                mmmmm = mmmmm / 60;
                longi = longi.Substring(0, 3);
                longi = ((mmmmm + Math.Abs(double.Parse(longi))) * -1).ToString();
            }
            else
            {
                double mmmmm = double.Parse(longitude.ToString().Substring(2, 7));
                mmmmm = mmmmm / 60;
                longi = longi.Substring(0, 2);
                longi = ((mmmmm + Math.Abs(double.Parse(longi)))).ToString();
            }
            longitude = Double.Parse(longi);
            return longitude;
        }

        public static Double ConvertoToE6Format(Double valor)
        {
            Double r = valor * 1E6;
            return r;

            //if you having your values like that x° y' z'', then:
            //valueE6 = (int) ((x + y / 60 + z / 3600) * 1000000)
        }

        public static Double ConvertoFromE6Format(Double valor)
        {
            return (Double)valor / 1E6;

            //if you having your values like that x° y' z'', then:
            //valueE6 = (int) ((x + y / 60 + z / 3600) * 1000000)
        }

        public static string Status()
        {
            String command;
            command = "STATUS";
            return command;
        }

        public static String TratarRetornoHtmlWayPoint(String texto)
        {
            String rawWpData = texto;
            rawWpData = rawWpData.Remove(0, 26);
            rawWpData = rawWpData.Replace(" type=hidden>", "");
            rawWpData = rawWpData.Replace("\"", "");
            rawWpData = rawWpData.Replace("\\", "");
            rawWpData = rawWpData.Replace("(", "");
            rawWpData = rawWpData.Replace(")", "");
            return rawWpData;
        }
    }
}
