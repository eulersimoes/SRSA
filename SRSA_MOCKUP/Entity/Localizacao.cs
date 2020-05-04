using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class Localizacao
    {
        private Double latitude;
        private Double longitude;
        //Somente usado para calculo de heading*
        private Double latitudeAbsoluta;
        private Double longitudeAbsoluta;
        //
        private bool norte = false;
        private bool leste = false;

        public bool Leste
        {
            get { return leste; }
            set { leste = value; }
        }

        public bool Norte
        {
            get { return norte; }
            set { norte = value; }
        }

        public Double Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }

        public Double Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }

        public Double LatitudeAbsoluta
        {
            get { return latitudeAbsoluta; }
            set { latitudeAbsoluta = value; }
        }

        public Double LongitudeAbsoluta
        {
            get { return longitudeAbsoluta; }
            set { longitudeAbsoluta = value; }
        }
    }
}
