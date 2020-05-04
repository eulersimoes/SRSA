using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using GEPlugin;
using Entity;
using AvionicsInstrumentsControls;
using System.Xml.XPath;
using System.Xml;
using System.Threading;
using Entity;
using SHAREDCLASSES;
using BANCO;

namespace COCKPIT
{
    public class ModelLeituraDados : INotifyPropertyChanged
    {
        #region Declaração de Var
        private int quantFuel;
        private int hdg;
        private IList<WayPoint> listaWayPointsRemoto = new List<WayPoint>();

        private IGEPlugin m_ge;
        private PlaneInfo planeInfo = new PlaneInfo();
        private static String serialTxt = String.Empty;
        private ArduinoComm arduinoComm;
        private List<WayPoint> listaWayPoint = new List<WayPoint>();
        private Boolean atualizarListaWayPoint = false;

        private static DAO dao = new DAO();
        private ServicePlane.PlaneInfoSoapClient planeInfoService = new ServicePlane.PlaneInfoSoapClient("PlaneInfoSoap");
        
        #endregion

        #region Contrutor
        public ModelLeituraDados()
        {

        }

        #endregion

        #region Metodos Publicos
        public event PropertyChangedEventHandler PropertyChanged;

        public Boolean AtualizarListaWayPoint
        {
            get { return atualizarListaWayPoint; }
            set { atualizarListaWayPoint = value; }
        }

        public IList<WayPoint> ListaWayPointsRemoto
        {
            get { return listaWayPointsRemoto; }
            set { listaWayPointsRemoto = value; }
        }

        public int QuantFuel
        {
            get { return quantFuel; }
            set { quantFuel = value; }
        }

        public int Hdg
        {
            get { return hdg; }
            set { hdg = value; }
        }

        public PlaneInfo PlaneInfo
        {
            get { return planeInfo; }
            set { planeInfo = value; }
        }

        public List<WayPoint> ListaWayPoint
        {
            get { return listaWayPoint; }
            set { listaWayPoint = value; }
        }
        
        public void AtualizarPlaneInfo()
        {
            try
            {
                //Esse metodo vai gravar os dados no banco, nao esta pronto
                /*
                FLIGHT_DATA fd = dao.GetTopFlightData(1);

                this.PlaneInfo.RollAngle = Double.Parse(fd.RA.ToString());
                this.PlaneInfo.PitchAngle = Double.Parse(fd.AOA.ToString());
                this.PlaneInfo.AutoPilot = Boolean.Parse(fd.AUTOPILOT);

                this.PlaneInfo.LocalizacaoAtual.Latitude = Double.Parse(fd.LATITUDE.ToString());
                //this.PlaneInfo.LocalizacaoAtual.Latitude = Util.ConvertLatitudeToGoogleMapsFormat(this.PlaneInfo.LocalizacaoAtual.Latitude);

                this.PlaneInfo.LocalizacaoAtual.Longitude = Double.Parse(fd.LONGITUDE.ToString());
                //this.PlaneInfo.LocalizacaoAtual.Longitude = Util.ConvertLongitudeToGoogleMapsFormat(this.PlaneInfo.LocalizacaoAtual.Longitude);


                this.PlaneInfo.AltitudeMetros = (int)Double.Parse(fd.ALTITUDE.ToString());
                this.PlaneInfo.Velocidadekmh = (int)Double.Parse(fd.VELOCIDADE.ToString());
                this.PlaneInfo.PercentualBateria = (int)Double.Parse(fd.BATTERY_LEVEL.ToString());
                this.PlaneInfo.PercentualCombustivel = (int)Double.Parse(fd.FUEL_LEVEL.ToString());
                this.PlaneInfo.Hdg = int.Parse(fd.HDG.ToString());

                /*
                this.PlaneInfo.RollAngle = Double.Parse(strArr[1]);
                this.PlaneInfo.PitchAngle = Double.Parse(strArr[2]);
                this.PlaneInfo.AutoPilot = Boolean.Parse(strArr[11]);
           
                this.PlaneInfo.LocalizacaoAtual.Latitude = Util.ConvertoFromE6Format(Double.Parse(strArr[6]));
                //this.PlaneInfo.LocalizacaoAtual.Latitude = Util.ConvertLatitudeToGoogleMapsFormat(this.PlaneInfo.LocalizacaoAtual.Latitude);
           
                this.PlaneInfo.LocalizacaoAtual.Longitude = Util.ConvertoFromE6Format(Double.Parse(strArr[7]));
                //this.PlaneInfo.LocalizacaoAtual.Longitude = Util.ConvertLongitudeToGoogleMapsFormat(this.PlaneInfo.LocalizacaoAtual.Longitude);
           

                this.PlaneInfo.AltitudeMetros = (int)Double.Parse(strArr[3]);
                this.PlaneInfo.Velocidadekmh = (int)Double.Parse(strArr[5]);
                this.PlaneInfo.PercentualBateria = (int)Double.Parse(strArr[10]);
                this.PlaneInfo.PercentualCombustivel = (int)Double.Parse(strArr[9]);
                this.PlaneInfo.Hdg = int.Parse(strArr[4]);
                 * */
            }
            catch (Exception ex) { }
        }

        public void GerenciarMensagensRecebidas()
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
                            PlaneInfo.RollAngle = Double.Parse(strArr[2]) *-1 ;
                            PlaneInfo.PitchAngle = Double.Parse(strArr[3])* -1;
                            PlaneInfo.PercentualBateria = int.Parse(strArr[11]);
                            PlaneInfo.AutoPilot = Boolean.Parse(strArr[12]);
                            PlaneInfo.LocalizacaoAtual.Latitude = Double.Parse(strArr[7]);
                            PlaneInfo.LocalizacaoAtual.Longitude = Double.Parse(strArr[8]);
                            PlaneInfo.AltitudeMetros = int.Parse(strArr[4]);
                            PlaneInfo.Hdg = int.Parse(strArr[5]);
                            //AtualizarPlaneInfo(planeInfo);

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
                            //listaWayPointsNetDuino = lista;
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
            //getDao().SaveRegistro(fd);
        }

        #endregion

        #region Metodos Privados

        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion
    }
}
