using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BANCO;
using System.ComponentModel;
using System.Threading;
using Entity;
using Entity;
using SHAREDCLASSES;
using System.Diagnostics;
using System.Globalization;

namespace WebServicePlaneInfo
{
    public static class Model
    {
        #region Variáveis
        private static IContainer components = new System.ComponentModel.Container();
        private static ThreadStart tsReceiveMsg = new ThreadStart(ReceberMensagem);
        private static Thread threadReceiveMsg = new Thread(tsReceiveMsg);
        private static Entity.PlaneInfo planeInfo = new Entity.PlaneInfo();
        private static IList<WayPoint> listaWayPoints = new List<WayPoint>();
        private static IList<WayPoint> listaWayPointsNetDuino = new List<WayPoint>();

        private static Boolean rodarThreadGetInfoAuto = false;
        #endregion

        #region Metodos Publicos

        public static String StartSerialPort()
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            string commPort = System.Configuration.ConfigurationSettings.AppSettings["ComPort"];
            try
            {
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                if (SerialPortHolder.SerialPort1 != null && SerialPortHolder.SerialPort1.IsOpen)
                {
                    CloseSerialPort();
                }
                SerialPortHolder.Start(components, commPort);
                threadReceiveMsg = new Thread(tsReceiveMsg);
                threadReceiveMsg.Start();
                return "Comando executado com sucesso !";
            }
            catch (Exception ex)
            {
                if (threadReceiveMsg.IsAlive)
                {
                    threadReceiveMsg.Suspend();
                }
                throw ex;
            }
        }

        public static String CloseSerialPort()
        {
            try
            {
                SerialPortHolder.SerialPort1.Close();
                return "Comando executado com sucesso !";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Boolean IsSerialPortOpen()
        {
            return SerialPortHolder.SerialPort1.IsOpen;
        }

        #endregion

        #region Metodos Privados

        private static void ReceberMensagem()
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            NetduinoComm.Start();
            while (true)
            {
                try
                {
                    for (int i = 0; i < NetduinoComm.ListaMensagemReceber.Count; i++)
                    {
                        NetduinoMessage msg = NetduinoComm.ListaMensagemReceber[i];
                        String retorno = msg.Msg;
                        if (retorno.ToUpper().Contains("PLANEINFO"))
                        {
                            string[] strArr = null;
                            strArr = retorno.Split(';');
                            planeInfo.RollAngle = Double.Parse(strArr[2]);
                            planeInfo.PitchAngle = Double.Parse(strArr[3]);
                            planeInfo.AutoPilot = Boolean.Parse(strArr[12]);
                            planeInfo.LocalizacaoAtual.Latitude = Double.Parse(strArr[7]);
                            planeInfo.LocalizacaoAtual.Longitude = Double.Parse(strArr[8]);
                            planeInfo.AltitudeMetros = int.Parse(strArr[4]);
                            planeInfo.Hdg = int.Parse(strArr[5]);
                            AtualizarPlaneInfo(planeInfo);

                        }
                        else if (retorno.ToUpper().Contains("COMANDORECEBIDO"))
                        {
                            string[] strArr = null;
                            strArr = retorno.Split(';');
                            int idComandoRecebido = int.Parse(strArr[1]);
                        }
                        else if (retorno.ToUpper().Contains("RAWWAYPOINT"))
                        {
                            List<WayPoint> lista = new List<WayPoint>();
                            lista = Util.ListaWayPointSync(retorno);
                            listaWayPointsNetDuino = lista;
                        }
                    }

                    NetduinoComm.ListaMensagemReceber.Clear();
                }
                catch (Exception ex)
                {
                    NetduinoComm.ListaMensagemReceber.Clear();
                }

                Thread.Sleep(100);
            }
        }

        private static void AutoGetInfo()
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            while (true)
            {
                if (rodarThreadGetInfoAuto)
                {
                    NetduinoComm.AddMensage(Util.Status(), true);
                    Thread.Sleep(200);
                }
            }
        }

        private static void AtualizarPlaneInfo(Entity.PlaneInfo planeInfo)
        {
            Random r = new Random();
            FLIGHT_DATA fd = new FLIGHT_DATA();
            fd.ALTITUDE = planeInfo.AltitudeMetros;
            fd.AOA = int.Parse(planeInfo.PitchAngle.ToString());
            fd.RA = int.Parse(planeInfo.RollAngle.ToString());
            fd.VELOCIDADE = planeInfo.Velocidadekmh;
            fd.LATITUDE = Decimal.Parse(planeInfo.LocalizacaoAtual.Latitude.ToString());
            fd.LONGITUDE = Decimal.Parse(planeInfo.LocalizacaoAtual.Longitude.ToString());
            fd.BATTERY_LEVEL = planeInfo.PercentualBateria;
            fd.ID_FLIGHT = 1;
            fd.DATE_REG = DateTime.Now;
            fd.HDG = planeInfo.Hdg;
            fd.AUTOPILOT = planeInfo.AutoPilot.ToString();
            fd.FUEL_LEVEL = planeInfo.PercentualCombustivel;
            getDao().SaveRegistro(fd);
        }

        #endregion

        #region Consultas

        public static String GetInfo(int flight)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            String retorno;
            FLIGHT_DATA fd = getDao().GetTopFlightData(flight);
            retorno = fd.DATE_REG + ";" + fd.RA + ";" + fd.AOA + ";" + fd.ALTITUDE + ";" + fd.HDG + ";" + fd.VELOCIDADE + ";" + Util.ConvertoToE6Format(Double.Parse(fd.LATITUDE.ToString())) + ";" + Util.ConvertoToE6Format(Double.Parse(fd.LONGITUDE.ToString())) + ";0;" + fd.FUEL_LEVEL + ";" + fd.BATTERY_LEVEL + ";" + fd.AUTOPILOT + ";" + fd.PROXIMO_WAY_POINT +";" + Convert.ToInt32(fd.ALTITUDE);
            return retorno;
        }

        #endregion

        #region Comandos

        public static String LigarFarol(Boolean Valor)
        {
            try
            {
                NetduinoComm.AddMensage(Util.LigarFarolBoolean(Valor), true);
                return "Comando executado com Sucesso !";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string LigarLuzesNav(bool Valor)
        {
            try
            {
                NetduinoComm.AddMensage(Util.LigarLuzesNav(Valor), true);
                return "Comando executado com Sucesso !";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string LigarStrobes(bool Valor)
        {
            try
            {
                NetduinoComm.AddMensage(Util.LigarStrobes(Valor), true);
                return "Comando executado com Sucesso !";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string AddHomePoint(int Id, double LatitudeE6, double LongitudeE6, int Altitude)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                WayPoint wayPoint = new WayPoint();
                wayPoint.Latitude = LatitudeE6;
                wayPoint.Longitude = LongitudeE6;
                wayPoint.Altitude = Altitude;
                wayPoint.Id = Id;
                listaWayPoints.Add(wayPoint);
                NetduinoComm.AddMensage(Util.AddWayHomeCommand(wayPoint), true);
                return "Comando executado com Sucesso!";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string AddWayPoint(int id, double latitudeE6, double longitudeE6, int altitude)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                WayPoint wayPoint = new WayPoint();
                wayPoint.Latitude = Util.ConvertoFromE6Format(latitudeE6);
                wayPoint.Longitude = Util.ConvertoFromE6Format(longitudeE6);
                wayPoint.Altitude = altitude;
                wayPoint.Id = id;

                if (wayPoint.Id == 0)
                {
                    WayPoint wayPointExcluir = null;
                    foreach (WayPoint w in listaWayPoints)
                    {
                        if (w.Id == 0)
                        {
                            wayPointExcluir = w;
                            break;
                        }
                    }
                    if (wayPointExcluir != null) { listaWayPoints.Remove(wayPointExcluir); }
                    NetduinoComm.AddMensage(Util.AddWayHomeCommand(wayPoint), true);
                } 
                else
                {
                     NetduinoComm.AddMensage(Util.AddWayPointCommand(wayPoint), true);
                }
                listaWayPoints.Add(wayPoint);
               
                return "Comando executado com Sucesso!";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string RemoveWayPoint(int Id)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                WayPoint wayPoint = new WayPoint();
                wayPoint.Id = Id;
                NetduinoComm.AddMensage(Util.RemoveWayPointCommand(wayPoint), true);
                listaWayPoints.Remove(wayPoint);
                return "Comando executado com Sucesso!";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static string RemoveWayPoints()
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                NetduinoComm.AddMensage(Util.RemoveWayPointsCommand(), true);
                return "Comando executado com Sucesso!";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static String SyncPoints()
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                NetduinoComm.AddMensage(Util.SysncWayPointNetduino(), true);
                return "Comando executado com Sucesso!";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static IList<WayPoint> ListarWayPoints()
        {
            return listaWayPoints;
        }

        public static IList<WayPoint> ListaWayPoints
        {
            get { return listaWayPoints; }
            set
            {
                listaWayPoints = value;
            }
        }

        public static void IniciarCamera()
        {

            try
            {
                FinalizarCamera();
                Process cam = new Process();
                cam.StartInfo.FileName = System.Configuration.ConfigurationSettings.AppSettings["CaminhoCamApp"];
                cam.Start();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static void GetAutoInfo()
        {
            NetduinoComm.AddMensage(Util.Status(), true);
        }



        public static void GetAutoInfoStop()
        {
            rodarThreadGetInfoAuto = false;
        }

        public static void FinalizarCamera()
        {
            try
            {
                Process[] processes = Process.GetProcessesByName("WebCamC");

                foreach (Process process in processes)
                {
                    process.Kill();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        public static byte[] GetCamPic()
        {
            byte[] _Buffer = null;
            string _FileName = "c:\\temp\\teste.bmp";
            try
            {
                // Open file for reading
                System.IO.FileStream _FileStream = new System.IO.FileStream(_FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                // attach filestream to binary reader
                System.IO.BinaryReader _BinaryReader = new System.IO.BinaryReader(_FileStream);
                // get total byte length of the file
                long _TotalBytes = new System.IO.FileInfo(_FileName).Length;
                // read entire file into buffer
                _Buffer = _BinaryReader.ReadBytes((Int32)_TotalBytes);
                // close file reader
                _FileStream.Close();
                _FileStream.Dispose();
                _BinaryReader.Close();
            }

            catch (Exception _Exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
            }
            return _Buffer;
        }

        public static string ListarWayPointsStringE6()
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                String retorno = "";
                foreach (WayPoint w in Model.ListarWayPoints())
                {
                    retorno += w.DescE6() + "/n";
                }

                return retorno;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }


        public static String ListarWayPointsStringE6Sync()
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                String retorno = "";
                foreach (WayPoint w in Model.listaWayPointsNetDuino)
                {
                    retorno += w.DescE6() + "/n";
                }

                return retorno;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        private static DAO getDao()
        {
            DAO dao = new DAO();
            return dao;
        }

        #endregion

        #region Private

        #endregion
    }
}