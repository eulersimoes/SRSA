using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Entity;
using System.Globalization;
using System.Threading;

namespace WebServicePlaneInfo
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class PlaneInfo : System.Web.Services.WebService
    {

        [WebMethod]
        public string OpenSerialPort(String CommPort)
        {
            return Model.StartSerialPort();
        }

        [WebMethod]
        public string CloseSerialPort(String Request)
        {
           return Model.CloseSerialPort();
        }

        [WebMethod]
        public string GetPlaneInfo(int Flight)
        {
            return Model.GetInfo(Flight);
        }

        [WebMethod]
        public void StartAutomaticGetPlaneInfo(String Request)
        {
            Model.GetAutoInfo();
        }

        [WebMethod]
        public string LigarStrobes(Boolean Valor)
        {
            return Model.LigarStrobes(Valor);
        }

        [WebMethod]
        public string LigarLuzesNav(Boolean Valor)
        {
            return Model.LigarLuzesNav(Valor);
        }

        [WebMethod]
        public string LigarFarol(Boolean Valor)
        {
            return Model.LigarFarol(Valor);
        }

        [WebMethod]
        public string AddHomePoint(int Id, int LatitudeE6, double LongitudeE6, int Altitude)
        {
            return Model.AddHomePoint(Id, LatitudeE6, LongitudeE6, Altitude);
        }

        [WebMethod]
        public string AddWayPoint(int Id, int LatitudeE6, double LongitudeE6, int Altitude)
        {
            return Model.AddWayPoint(Id, LatitudeE6, LongitudeE6, Altitude);
        }

        [WebMethod]
        public string RemoverWayPoint(int id)
        {
            return Model.RemoveWayPoint(id);
        }

        [WebMethod]
        public string RemoverWayPoints(String Request)
        {
            return Model.RemoveWayPoints();
        }

        [WebMethod]
        public String ListarWayPointsStringE6(String Request)
        {
            return Model.ListarWayPointsStringE6();
        }

        [WebMethod]
        public String ListarWayPointsStringE6Sync(String Request)
        {
            return Model.ListarWayPointsStringE6Sync();
        }

        [WebMethod]
        public List<WayPoint> ListarWayPoints(String Request)
        {
            return Model.ListarWayPoints().ToList();
        }

        [WebMethod]
        public string SyncPoints(String Sync)
        {
            return Model.SyncPoints();
        }

        [WebMethod]
        public Byte[] GetCamPic()
        {
            return Model.GetCamPic();
        }

        [WebMethod]
        public void IniciarCamera()
        {
            Model.IniciarCamera();
        }

        [WebMethod]
        public void FinalizarCamera()
        {
            Model.FinalizarCamera();
        }

    }
}