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
    public class TraduccionDAL
    {
        public int AltaTraduccionDAL_675MS(TraduccionBE nuevaTraduccion_675MS)
        {
            Acceso acceso_675MS = new Acceso();
            acceso_675MS.Conectar_675MS();

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();

                parametros.Add(acceso_675MS.CrearParametro_675MS("@NombreControl", nuevaTraduccion_675MS.NombreControl_675MS));
                parametros.Add(acceso_675MS.CrearParametro_675MS("@Traduccion", nuevaTraduccion_675MS.Traduccion_675MS));
                parametros.Add(acceso_675MS.CrearParametro_675MS("@IdIdioma", nuevaTraduccion_675MS.IdIdioma_675MS));


                int filasAfectadas_675MS = acceso_675MS.Escribir_675MS("ALTA_TRADUCCION", parametros);

                return filasAfectadas_675MS;
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }
        }

        public int BorrarTraduccionDAL_675MS(int traduccionEliminar_675MS)
        {
            Acceso acceso_675MS = new Acceso();
            acceso_675MS.Conectar_675MS();

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                
                parametros.Add(acceso_675MS.CrearParametro_675MS("@Id", traduccionEliminar_675MS));


                int filasAfectadas_675MS = acceso_675MS.Escribir_675MS("BAJA_TRADUCCION", parametros);

                return filasAfectadas_675MS;
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }
        }

        public List<TraduccionBE> ListarTraducciones_675MS(int idiomaSel_675MS)
        {
            Acceso acceso_675MS = new Acceso();

            List<TraduccionBE> traducciones_675MS = new List<TraduccionBE>();

            acceso_675MS.Conectar_675MS();
            string sql_675MS = "CONSULTA_TRADUCCIONES";
            List<SqlParameter> parametros_675MS = new List<SqlParameter>();
            DataTable tabla = new DataTable();

            parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@id", idiomaSel_675MS));
            tabla = acceso_675MS.Leer_675MS(sql_675MS, parametros_675MS);

            foreach (DataRow row in tabla.Rows)
            {
                TraduccionBE tradAux = new TraduccionBE();
                tradAux.Id_675MS = int.Parse(row["Id"].ToString());
                tradAux.NombreControl_675MS = row["NombreControl"].ToString();
                tradAux.Traduccion_675MS = row["Traduccion"].ToString();
                traducciones_675MS.Add(tradAux);
            }

            acceso_675MS.Desconectar_675MS();

            return traducciones_675MS;
        }

        public int ModificarTraduccionDAL_675MS(TraduccionBE traduccionModificada_675MS)
        {
            Acceso acceso_675MS = new Acceso();
            acceso_675MS.Conectar_675MS();

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();

                parametros.Add(acceso_675MS.CrearParametro_675MS("@Id", traduccionModificada_675MS.Id_675MS));
                parametros.Add(acceso_675MS.CrearParametro_675MS("@nombreControl", traduccionModificada_675MS.NombreControl_675MS));
                parametros.Add(acceso_675MS.CrearParametro_675MS("@traduccion", traduccionModificada_675MS.Traduccion_675MS));

                int filasAfectadas_675MS = acceso_675MS.Escribir_675MS("MODIFICAR_TRADUCCION", parametros);

                return filasAfectadas_675MS;
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }
        }
    }
}
