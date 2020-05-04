using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class PlaneInfo
    {
        private Localizacao localizacaoAtual;
        private int velocidadekmh = 0;
        private int altitudeMetros = 0;
        private int hdg = 0;
        private int percentualCombustivel = 0;
        private int percentualBateria = 0;
        private int distanciaCasa = 0;
        private int proximoWayPoint = 0;
        private DateTime dataHoraMedicao;
        private Double pitchAngle;
        private Double rollAngle;
        private Boolean autoPilot;
        private Double throttle = 0;
        private String autoPilotMode = null;

        //OFF,WAYPOINT,ESTAB


        public int ProximoWayPoint
        {
            get { return proximoWayPoint; }
            set { proximoWayPoint = value; }
        }

        public Double Throttle
        {
            get { return throttle; }
            set { throttle = value; }
        }

        public String AutoPilotMode
        {
            get { return autoPilotMode; }
            set { autoPilotMode = value; }
        }

        public Boolean AutoPilot
        {
            get { return autoPilot; }
            set { autoPilot = value; }
        }

        public int DistanciaCasa
        {
            get { return distanciaCasa; }
            set { distanciaCasa = value; }
        }

        public String StatusAutoPilot()
        {
            if (AutoPilot == true)
            {
                return "ON";
            }
            else
            {
                return "OFF";
            }
        }

        public Double RollAngle
        {
            get { return rollAngle; }
            set { rollAngle = value; }
        }

        public Double PitchAngle
        {
            get { return pitchAngle; }
            set { pitchAngle = value; }
        }

        public DateTime DataHoraMedicao
        {
            get { return dataHoraMedicao; }
            set { dataHoraMedicao = value; }
        }

        public PlaneInfo()
        {
            localizacaoAtual = new Localizacao();
        }

        public int PercentualBateria
        {
            get { return percentualBateria; }
            set { percentualBateria = value; }
        }

        public int PercentualCombustivel
        {
            get { return percentualCombustivel; }
            set { percentualCombustivel = value; }
        }

        public int Velocidadekmh
        {
            get { return velocidadekmh; }
            set { velocidadekmh = value; }
        }

        public int AltitudeMetros
        {
            get { return altitudeMetros; }
            set { altitudeMetros = value; }
        }

        public Localizacao LocalizacaoAtual
        {
            get { return localizacaoAtual; }
            set { localizacaoAtual = value; }
        }

        public int Hdg
        {
            get { return hdg; }
            set { hdg = value; }
        }

        public String getXml()
        {
            String info = String.Empty;
            info = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
            info += " <planeInfo> ";
            info += " <pitchAngle> ";
            info += this.pitchAngle;
            info += " </pitchangle> ";

            info += " <rollangle> ";
            info += this.rollAngle;
            info += " </rollAngle> ";

            info += " <fuel> ";
            info += this.PercentualCombustivel;
            info += " </fuel> ";

            info += " <latitude> ";
            info += this.LocalizacaoAtual.Latitude;
            info += " </latitude> ";

            info += " <longitude> ";
            info += this.LocalizacaoAtual.Longitude;
            info += " </longitude> ";

            info += " <altitude> ";
            info += this.AltitudeMetros;
            info += " </altitude> ";

            info += " <speed> ";
            info += this.Velocidadekmh;
            info += " </speed> ";

            info += " <hdg> ";
            info += this.Hdg;
            info += " </hdg> ";

            info += " </planeInfo> ";
            info += "\n";
            return info;
        }
    }
}
