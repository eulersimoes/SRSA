using System;
using Microsoft.SPOT;

namespace Entity
{
    public class Localizacao
    {
        private Double latitude;
        private Double longitude;
        //Somente usado para calculo de heading*
        private int latitudeE6;
        private int longitudeE6;
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

        public int LatitudeE6
        {
            get
            {
                return (int)(this.latitude * 1000000);
            }
        }

        public int LongitudeE6
        {
            get 
            {
                return (int)(this.longitude * 1000000); 
            }           
        }
    }


}
