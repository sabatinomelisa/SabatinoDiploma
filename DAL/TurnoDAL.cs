using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TurnoDAL
    {
        public List<TurnoBE> ListarTurnosturnos_675MS()
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
                turnoAuxiliar_675MS.DigitoVerificadorHorizontal_675MS = Convert.ToInt32(turnoBE["DigitoVerificadorHorizontal"]);
                turnoAuxiliar_675MS.Activo_675MS = Convert.ToBoolean(turnoBE["Activo"]);
                turnoAuxiliar_675MS.FechaBaja_675MS = Convert.ToDateTime(turnoBE["FechaHoraBaja"]);
                turnos_675MS.Add(turnoAuxiliar_675MS);
            }
            return turnos_675MS;

        }
        

        public List<TurnoBE> ObtenerTurnosPorFecha_675MS(DateTime fecha_675MS)
        {
            Acceso acceso_675MS = new Acceso();
            acceso_675MS.Conectar_675MS();

            try
            {
                List<SqlParameter> parametros_675MS = new List<SqlParameter>();
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@Fecha", fecha_675MS.Date));

                DataTable tabla_675MS = acceso_675MS.Leer_675MS("OBTENER_TURNOS_POR_FECHA", parametros_675MS);

                // Armamos la lista de objetos
                List<TurnoBE> listaTurnos_675MS = new List<TurnoBE>();

                foreach (DataRow fila_675MS in tabla_675MS.Rows)
                {
                    TurnoBE turno_675MS = new TurnoBE();
                    // Mapeamos todos los datos usando el nombre EXACTO de la columna en SQL
                    turno_675MS.IdTurno_675MS = Convert.ToInt32(fila_675MS["IdTurno"]);
                    turno_675MS.Dni_675MS = Convert.ToInt32(fila_675MS["DNI"]);
                    turno_675MS.Dominio_675MS = fila_675MS["Dominio"].ToString();
                    turno_675MS.FechaHora_675MS = Convert.ToDateTime(fila_675MS["FechaHora"]);
                    turno_675MS.DuracionEstimada_675MS = Convert.ToInt32(fila_675MS["DuracionEstimada"]);
                    turno_675MS.DigitoVerificadorHorizontal_675MS = Convert.ToInt32(fila_675MS["DigitoVerificadorHorizontal"]);
                    turno_675MS.Activo_675MS = Convert.ToBoolean(fila_675MS["Activo"]);
                    turno_675MS.FechaBaja_675MS = Convert.ToDateTime(fila_675MS["FechaHoraBaja"]);
                    listaTurnos_675MS.Add(turno_675MS);
                }

                return listaTurnos_675MS;
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }
        }
        public int BajaTurnoDAL_675MS(int idSeleccionado_675MS)
        {
            Acceso acceso_675MS = new Acceso();
            acceso_675MS.Conectar_675MS();

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();

                // Emvio el ID del turno que queremos dar de baja
                parametros.Add(acceso_675MS.CrearParametro_675MS("@IdTurno", idSeleccionado_675MS));

                return acceso_675MS.Escribir_675MS("BAJA_TURNO", parametros);
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }
        }

        public int ReprogramarTurnoDAL_675MS(int idSeleccionado_675MS, DateTime fechaNueva_675MS)
        {
            Acceso acceso_675MS = new Acceso();
            acceso_675MS.Conectar_675MS();

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();

                // Emvio el ID del turno que queremos dar de baja
                parametros.Add(acceso_675MS.CrearParametro_675MS("@IdTurno", idSeleccionado_675MS));
                parametros.Add(acceso_675MS.CrearParametro_675MS("@fechaNueva", fechaNueva_675MS));

                return acceso_675MS.Escribir_675MS("REPROGRAMAR_TURNO", parametros);
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }
        }

        public int RegistrarTurnoDAL_675MS(TurnoBE turnoBE_675MS)
        {
            Acceso acceso_675MS = new Acceso();
            acceso_675MS.Conectar_675MS();

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();

                // Emvio el ID del turno que queremos dar de baja
                parametros.Add(acceso_675MS.CrearParametro_675MS("@dni", turnoBE_675MS.Dni_675MS));
                parametros.Add(acceso_675MS.CrearParametro_675MS("@dominio", turnoBE_675MS.Dominio_675MS));
                parametros.Add(acceso_675MS.CrearParametro_675MS("@fechahora", turnoBE_675MS.FechaHora_675MS));
                parametros.Add(acceso_675MS.CrearParametro_675MS("@duracionestimada", turnoBE_675MS.DuracionEstimada_675MS));
                parametros.Add(acceso_675MS.CrearParametro_675MS("@dv", turnoBE_675MS.DigitoVerificadorHorizontal_675MS));


                return acceso_675MS.Escribir_675MS("ALTA_TURNO", parametros);
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }
        }
    }
}
