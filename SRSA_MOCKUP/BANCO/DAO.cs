using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BANCO
{
    public class DAO : IAcessoDB<FLIGHT_DATA>
    {
        private planeinfoEntities entities;
        public DAO()
        {
            entities = new planeinfoEntities();
        }

        public FLIGHT_DATA GetTopFlightData(int Flight)
        {
            try
            {
                IQueryable<FLIGHT_DATA> consultaFlightData = entities.FLIGHT_DATA.AsQueryable<FLIGHT_DATA>();
                consultaFlightData = consultaFlightData.Where(c => c.ID_FLIGHT == Flight);
                return consultaFlightData.ToList().Last();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void SaveRegistro(FLIGHT_DATA registro)
        {
            try
            {
                entities.AddToFLIGHT_DATA(registro);
                entities.SaveChanges();
            }
            catch (Exception ex)
            {
                
            }
        }

        public void UpdateRegistro(FLIGHT_DATA registro)
        {
            EntityKey key;
            object originalItem;

            using (entities)
            {
                key = entities.CreateEntityKey("Clientes", registro);

                if (entities.TryGetObjectByKey(key, out originalItem))
                {
                    entities.ApplyCurrentValues(key.EntitySetName, registro);
                }
                entities.SaveChanges();
            }
        }
    }
}
