using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BANCO
{
    public interface IAcessoDB<T>
    {
        void SaveRegistro(T registro);
        void UpdateRegistro(T registro);
        FLIGHT_DATA GetTopFlightData(int Flight);
    }
}
