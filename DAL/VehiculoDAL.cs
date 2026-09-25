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
    public class VehiculoDAL
    {
        public List<VehiculoBE> ListarVehiculos_675MS()
        {
            List<VehiculoBE> vehiculos_675MS = new List<VehiculoBE>();
            Acceso acceso_675MS = new Acceso();
            DataTable tablaVehiculos_675MS = new DataTable();

            try
            {
                acceso_675MS.Conectar_675MS();

                tablaVehiculos_675MS = acceso_675MS.Leer_675MS("OBTENER_VEHICULOS");
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
            foreach (DataRow vehiculoBE_675MS in tablaVehiculos_675MS.Rows)
            {
                VehiculoBE vehiculoAuxiliar_675MS = new VehiculoBE();
                vehiculoAuxiliar_675MS.Dominio_675MS = vehiculoBE_675MS["Dominio"].ToString();
                vehiculoAuxiliar_675MS.Dni_675MS = Convert.ToInt32(vehiculoBE_675MS["DNI"]);
                vehiculoAuxiliar_675MS.Modelo_675MS = vehiculoBE_675MS["Modelo"].ToString();
                vehiculoAuxiliar_675MS.Version_675MS = vehiculoBE_675MS["VersionVehiculo"].ToString();
                vehiculoAuxiliar_675MS.Anio_675MS = Convert.ToInt32(vehiculoBE_675MS["Anio"]);
                vehiculoAuxiliar_675MS.NroChasis_675MS = vehiculoBE_675MS["NroChasis"].ToString();
                vehiculoAuxiliar_675MS.UltimaVTV_675MS = Convert.ToDateTime(vehiculoBE_675MS["UltimaVTV"]);
                vehiculoAuxiliar_675MS.DigitoVerificadorHorizontal_675MS = Convert.ToInt32(vehiculoBE_675MS["DigitoVerificadorHorizontal"]);
                vehiculos_675MS.Add(vehiculoAuxiliar_675MS);
            }
            return vehiculos_675MS;
        }

        public int RegistrarVehiculoDAL_675MS(VehiculoBE vehiculoBE_675MS)
        {
            int filasAfectadas_675MS = 0;

            // Instancio la clase Acceso
            Acceso acceso_675MS = new Acceso();

            // Preparo la lista de parámetros para el Stored Procedure
            List<SqlParameter> parametros_675MS = new List<SqlParameter>();

            parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@Dominio", vehiculoBE_675MS.Dominio_675MS));
            parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@DNI", vehiculoBE_675MS.Dni_675MS));
            parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@Modelo", vehiculoBE_675MS.Modelo_675MS));
            parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@VersionVehiculo", vehiculoBE_675MS.Version_675MS));
            parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@Anio", vehiculoBE_675MS.Anio_675MS));
            parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@NroChasis", vehiculoBE_675MS.NroChasis_675MS));
            parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@UltimaVTV", vehiculoBE_675MS.UltimaVTV_675MS));
            parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@DigitoVerificadorHorizontal", vehiculoBE_675MS.DigitoVerificadorHorizontal_675MS));

            //Ejecuto el SP de ALTA
            filasAfectadas_675MS = acceso_675MS.Escribir_675MS("ALTA_VEHICULO", parametros_675MS);

            return filasAfectadas_675MS;
        }

   
    }
}


