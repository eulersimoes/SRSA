using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.IO.Ports;
using CaptureCam;
using System.Windows.Forms;
using GEPlugin;
using COCKPIT;
using Entity;
using System.Threading;
using System.Xml;
using BANCO;
using Entity;
using SHAREDCLASSES;

namespace AvionicsInstrumentsControls
{
    public class ModelControle : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        #region Declaração de Variáveis
        DAO dao;
        private Boolean autopilot;
        private String teste;
        private IList<WayPoint> listaWayPoints = new List<WayPoint>();
        private Double pitchAngle = 0;
        private Double rollAngle = 0;

        private int airSpeed = 0;

        private float count = 0;

        private static String serialTxt = String.Empty;
        private static Boolean mensagemRecebida = false;
        private static String latAnterior = String.Empty;
        private static String longAnterior = String.Empty;
        private static int altAnterior = 0;
        private static PlaneInfo lastPlaneInfo = new Entity.PlaneInfo();
        static System.IO.Ports.SerialPort serialPort1;
        System.ComponentModel.IContainer components = new System.ComponentModel.Container();
        CaptureCam.CaptureCam device = new CaptureCam.CaptureCam();
        IGEPlugin m_ge;


        public String Teste
        {
            get { return teste; }
            set
            {
                teste = value;
                OnPropertyChanged("Teste");
            }
        }

        public void Capturar(PictureBox pic)
        {
            try
            {
                device.Capturar(ref pic);
            }
            catch { }
        }
        #endregion
        #region Metódos Publicos
        public ModelControle()
        {
            //arduinoComm = new ArduinoComm();
            //dao = new DAO();
            //NetduinoComm.Start();
        }

        public void SetPluginGmap(IGEPlugin gmapPlugin)
        {
            this.m_ge = gmapPlugin;
            this.m_ge.getLayerRoot().enableLayerById(this.m_ge.LAYER_BORDERS, 1);
            this.m_ge.getLayerRoot().enableLayerById(this.m_ge.LAYER_ROADS, 1);
        }

        public Boolean Autopilot
        {
            get { return autopilot; }
            set { autopilot = value; }
        }

        public Double PitchAngle
        {
            get { return pitchAngle; }
            set
            {
                pitchAngle = value;
                OnPropertyChanged("PitchAngle");
            }
        }

        public Double RollAngle
        {
            get { return rollAngle; }
            set
            {
                rollAngle = value;
                OnPropertyChanged("RollAngle");
            }
        }

        public int AirSpeed
        {
            get { return airSpeed; }
            set { airSpeed = value;
            OnPropertyChanged("AirSpeed");
            }
        }

        public void GenerateMapPoint(String placeMarkName, String placeMarkDesc, Double latitude = 0, Double longitude = 0, Double autitude = 0)
        {

            KmlPointCoClass point = m_ge.createPoint("");
            point.setLatitude(latitude);
            point.setLongitude(longitude);
            point.setAltitude(autitude);

            // create a placemark
            KmlPlacemarkCoClass placemark = m_ge.createPlacemark("");
            placemark.setName(placeMarkName);
            placemark.setDescription(placeMarkDesc);
            placemark.setGeometry(point);
            m_ge.getFeatures().appendChild(placemark);
        }

        public void GeneratePlaneLocation(PlaneInfo planeInfo, Boolean forceGenerate)
        {
            if (lastPlaneInfo.LocalizacaoAtual.Latitude == planeInfo.LocalizacaoAtual.Latitude &&
                lastPlaneInfo.LocalizacaoAtual.Longitude == planeInfo.LocalizacaoAtual.Longitude && !forceGenerate)
            {
                return;
            }
            else
            {
                lastPlaneInfo = planeInfo;
            }

            if (m_ge == null)
            {
                return;
            }

            Double latitude = planeInfo.LocalizacaoAtual.Latitude;
            Double longitude =  planeInfo.LocalizacaoAtual.Longitude;
            int alt = planeInfo.AltitudeMetros;

            if (latAnterior == latitude.ToString() && longAnterior == longitude.ToString() && altAnterior == alt)
            {
                return;
            }

            IGEFeatureContainer features = m_ge.getFeatures();
            KmlObjectListCoClass l = features.getChildNodes();

            for (int i = 0; i < l.getLength(); i++)
            {
                if (l.item(i).getId().Contains("planeMark"))
                {
                    m_ge.getFeatures().removeChild(l.item(i));
                    break;
                }
            }

            KmlScreenOverlayCoClass screenOverlay = m_ge.createScreenOverlay("");

            KmlIconCoClass icon = m_ge.createIcon("");
            icon.setHref("http://www.baixamais.net/resources/banco-de-icones/transportes/aviao5.gif");
            // create a placemark
            KmlStyleCoClass style = m_ge.createStyle("");
            style.getIconStyle().setIcon(icon);

            KmlPointCoClass point = m_ge.createPoint("");
            point.setLatitude(latitude);
            point.setLongitude(longitude);
            point.setAltitude(planeInfo.AltitudeMetros);
            point.setAltitudeMode(m_ge.ALTITUDE_ABSOLUTE);

            KmlPlacemarkCoClass placemark = m_ge.createPlacemark("planeMark" + count);
            placemark.setName("Plane");
            placemark.setDescription(" Lat: " + latitude + " Long: " + longitude + " Alt: " + planeInfo.AltitudeMetros + " Spd: " + planeInfo.Velocidadekmh + " Hdg: " + planeInfo.Hdg + " Data Medicao: " + DateTime.Now );
            placemark.setGeometry(point);
            m_ge.getFeatures().appendChild(placemark);
            placemark.setStyleSelector(style);

            m_ge.getFeatures().appendChild(placemark);
            count++;
        }

        public void GenerateWayPointLocation(String wayPointName, String wayPointDesc, Double latitude = 0, Double longitude = 0, Double autitude = 0)
        {
            KmlScreenOverlayCoClass screenOverlay = m_ge.createScreenOverlay("");
            KmlIconCoClass icon = m_ge.createIcon("");
            icon.setHref("http://www.chaucerbusinesspark.co.uk/images/google_maps_icon.png");
            // create a placemark
            KmlStyleCoClass style = m_ge.createStyle("");
            style.getIconStyle().setIcon(icon);

            KmlPointCoClass point = m_ge.createPoint("");
            point.setLatitude(latitude);
            point.setLongitude(longitude);
            point.setAltitude(autitude);
            point.setAltitudeMode(m_ge.ALTITUDE_ABSOLUTE);

            KmlPlacemarkCoClass placemark = m_ge.createPlacemark(wayPointName);
            placemark.setName(wayPointName);
            placemark.setDescription(wayPointDesc);
            placemark.setGeometry(point);
            m_ge.getFeatures().appendChild(placemark);
            placemark.setStyleSelector(style);

            m_ge.getFeatures().appendChild(placemark);
        }

        private void RemoveWayPointLocation(String Id)
        {
            IGEFeatureContainer features = m_ge.getFeatures();
            KmlObjectListCoClass l = features.getChildNodes();

            for (int i = 0; i < l.getLength(); i++)
            {
                if (l.item(i).getId() == Id)
                {
                    m_ge.getFeatures().removeChild(l.item(i));
                }
            }

        }

        public IList<WayPoint> ListaWayPoints
        {
            get { return listaWayPoints; }
            set
            {
                listaWayPoints = value;
                foreach (WayPoint wayPoint in listaWayPoints)
                {
                    this.GenerateWayPointLocation("WayPoint" + wayPoint.Id, "WayPoint" + wayPoint.Id, wayPoint.Latitude, wayPoint.Longitude, wayPoint.Altitude);
                }
                OnPropertyChanged("ListaWayPoints");
            }
        }

        public void AddWayPoint(WayPoint wayPoint)
        {
            wayPoint.Id = ListaWayPoints.Count + 1;
            ListaWayPoints.Add(wayPoint);
            //Service.planeInfoService.AddWayPoint(wayPoint.Id, Util.ConvertoToE6Format(wayPoint.Latitude), Util.ConvertoToE6Format(wayPoint.Longitude), (int)wayPoint.Altitude);
            OnPropertyChanged("ListaWayPoints");
            this.GenerateWayPointLocation("WayPoint" + wayPoint.Id, "WayPoint" + wayPoint.Id, wayPoint.Latitude, wayPoint.Longitude, wayPoint.Altitude);
        }

        public void RemoveWayPoint(int WayPId)
        {
            Service.planeInfoService.RemoverWayPoint(WayPId);
            this.RemoveWayPointLocation("WayPoint" + WayPId);

            for (int i = 0; i < listaWayPoints.Count; i++)
            {
                if (listaWayPoints[i].Id == WayPId)
                {
                    listaWayPoints.RemoveAt(i);
                    break;
                }
            }
            
            OnPropertyChanged("ListaWayPoints");
        }

        public void TurnAutoPilot(Boolean Status)
        {
            String command = Util.AutoPilotTurnOnOffCommand(Status);
            //arduinoComm.EnviarComando(command, false, true);
            //NetduinoComm.AddMensage(command, true);
        }

        public void TurnFarol(Boolean Status)
        {
            Service.planeInfoService.LigarFarol(Status);
        }

        public void TurnStrobe(Boolean Status)
        {
            Service.planeInfoService.LigarStrobes(Status);
        }

        public void TurnNavLight(Boolean Status)
        {
            Service.planeInfoService.LigarLuzesNav(Status);
        }
        

        public void CentralizarMapa()
        {
            try
            {
                KmlLookAtCoClass look = m_ge.createLookAt("");
                look.set(-19.81924, -43.17567, 1000, m_ge.ALTITUDE_ABSOLUTE, 0, 0, 0);
                m_ge.getView().setAbstractView(look);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update()
        {
            OnPropertyChanged("model");
        }

        public void WayPointInfo()
        {
          ListaWayPoints.Clear();
          foreach (COCKPIT.ServicePlane.WayPoint wp in Service.planeInfoService.ListarWayPoints(""))
          {
              WayPoint w = new Entity.WayPoint();
              w.Id = wp.Id;
              w.Latitude = wp.Latitude;
              w.Longitude = wp.Longitude;
              w.Altitude = wp.Altitude;
              this.AddWayPoint(w);
          }
        }

        public void SetAlerionServoPos(int valor)
        {
            //arduinoComm.EnviarComando(Util.MoveServoAlerion(valor), false, false);
            //NetduinoComm.AddMensage(Util.MoveServoAlerion(valor), false);
        }

        public void SetElevatorServoPos(int valor)
        {
            //arduinoComm.EnviarComando(Util.MoveServoElevator(valor), false, false);
        }

        #endregion


        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public void AutoGetPlaneInfo()
        {
                Service.planeInfoService.StartAutomaticGetPlaneInfo(" ");
        }

        public void CentralizarAviao()
        {
            try
            {
                KmlLookAtCoClass look = m_ge.createLookAt("");
                look.set(lastPlaneInfo.LocalizacaoAtual.Latitude, lastPlaneInfo.LocalizacaoAtual.Longitude, 1000, m_ge.ALTITUDE_ABSOLUTE, 0, 0, 0);
                m_ge.getView().setAbstractView(look);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
