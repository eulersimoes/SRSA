using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BANCO;

namespace WebServicePlaneInfo
{
    public class Model
    {
        DAO dao = new DAO();

        public String GetInfo(int flight)
        {
            String retorno;
            FLIGHT_DATA fd = dao.GetTopFlightData(flight); 
            retorno = fd.ALTITUDE + ";" + fd.AOA + ";" + fd.RA + ";" + fd.BATTERY_LEVEL + ";" + fd.DATE_REG;
            return retorno;
        }
    }
}