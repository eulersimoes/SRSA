using System;
using Microsoft.SPOT;

namespace Entity
{
    public class PlaneInfo
    {
        private Localizacao localizacaoAtual;
        private Double distanciaParaCasa =0;
        private Double velocidadeknots = 0;
        private Double altitudeMetros =0;
        private Double hdg =0;
        private int percentualCombustivel =0;
        private int percentualBateria =0;
        private DateTime dataHoraMedicao;
        private int pitchAngle;
        private int rollAngle;
        private Boolean autopilot;
        private int throttle = 0;

        public int Throttle
        {
            get { return throttle; }
            set { throttle = value; }
        }

        public Boolean Autopilot
        {
            get { return autopilot; }
            set { autopilot = value; }
        }

        public int RollAngle
        {
            get { return rollAngle; }
            set { rollAngle = value; }
        }

        public int PitchAngle
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

        public Double Velocidadeknots
        {
            get { return velocidadeknots; }
            set { velocidadeknots = value; }
        }

        public Double AltitudeMetros
        {
            get { return altitudeMetros; }
            set { altitudeMetros = value; }
        }

        public Localizacao LocalizacaoAtual
        {
            get { return localizacaoAtual; }
            set { localizacaoAtual = value; }
        }

        public Double Hdg
        {
            get { return hdg; }
            set { hdg = value; }
        }

        public String getRawData()
        {
            String info = String.Empty;
            info += "planeinfo;";
            info += DateTime.Now.ToString() + ";";
            info += this.RollAngle.ToString() + ";";
            info += this.PitchAngle.ToString() + ";";
            info += System.Math.Round(this.AltitudeMetros) + ";";
            info +=  System.Math.Round(this.Hdg) + ";";
            info +=  System.Math.Round(this.Velocidadeknots) + ";";
            info += this.LocalizacaoAtual.Latitude.ToString() + ";";
            info += this.LocalizacaoAtual.Longitude.ToString() + ";";
            info += this.Throttle.ToString() + ";";
            info += this.PercentualCombustivel.ToString() + ";";
            info += this.PercentualBateria.ToString() + ";";
            info += this.Autopilot.ToString();
            info += "\n";
            return info;
        }
    }
}
