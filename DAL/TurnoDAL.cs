using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TurnoDAL
    {
        public List<TurnoBE> ListarTurnosturnos_675MS()
        {
            {
                List<TurnoBE> turnos_675MS = new List<TurnoBE>();
                Acceso acceso_675MS = new Acceso();
                DataTable tablaTurnos_675MS = new DataTable();

                try
                {
                    acceso_675MS.Conectar_675MS();

                    tablaTurnos_675MS = acceso_675MS.Leer_675MS("OBTENER_TURNOS_PROXIMOS", null);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    acceso_675MS.Desconectar_675MS();
                }

                //Recorro la tabla y genero la lista para retornar a la capa de negocio
                foreach (DataRow turnoBE in tablaTurnos_675MS.Rows)
                {
                    TurnoBE turnoAuxiliar_675MS = new TurnoBE();
                    turnoAuxiliar_675MS.IdTurno_675MS = Convert.ToInt32(turnoBE["IdTurno"]);
                    turnoAuxiliar_675MS.Dni_675MS = Convert.ToInt32(turnoBE["DNI"]);
                    turnoAuxiliar_675MS.Dominio_675MS = turnoBE["Dominio"].ToString();
                    turnoAuxiliar_675MS.FechaHora_675MS = Convert.ToDateTime(turnoBE["FechaHora"]);
                    turnoAuxiliar_675MS.DuracionEstimada_675MS = Convert.ToInt32(turnoBE["DuracionEstimada"]);
                }
                    return turnos_675MS;
                
            }
        }
    }
}
