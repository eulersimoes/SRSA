using System;
using Microsoft.SPOT;
using System.Collections;
using Entity;
using model;

namespace Model
{
    public static class Util
    {
        public static String GerarRawWayPoints(ArrayList listaWayPoint)
        {
            String r= String.Empty;
            //Divisor de campo: ;
            //NovaLinha:#
            foreach (WayPoint wayPoint in listaWayPoint)
            {

                r += wayPoint.Id;
                r += ";";

                r += wayPoint.Latitude;
                r += ";";

                r += wayPoint.Longitude;
                r += ";";

                r += wayPoint.Altitude;
                r += ";";

                r += ":";
            }
            r += " rawWayPoint ";
            r += "\n";
            return r;
        }
    }
}
