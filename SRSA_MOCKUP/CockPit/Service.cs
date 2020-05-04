using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COCKPIT
{
   public static class Service
    {
       public static ServicePlane.PlaneInfoSoapClient planeInfoService = new ServicePlane.PlaneInfoSoapClient("PlaneInfoSoap");
    }
}
