using System;
using Microsoft.SPOT;

namespace Entity
{
    public class WayPoint : Localizacao
    {
        private int id;

        private int altitude;

        public WayPoint()
        {

        }

        public WayPoint(int Id, Double Lat, Double Long, int Alt)
        {
            this.Id = Id;
            this.Latitude = Lat;
            this.Longitude = Long;
            this.Altitude = Alt;
        }

        public int Altitude
        {
            get { return altitude; }
            set { altitude = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

    }
}
