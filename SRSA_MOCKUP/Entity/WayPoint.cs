using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class WayPoint 
    {
        private int id;

        private Double latitude;

        private Double longitude;

        private Double altitude;

        public int Id
        {
            get { return id; }
            set { id = value; }
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

        public Double Altitude
        {
            get { return altitude; }
            set { altitude = value; }
        }

        public String DescE6()
        {
            return this.id + ";" + this.Latitude * 1000000 + ";" +this.Longitude * 1000000 + ";" + this.Altitude;
        }

    }
}
