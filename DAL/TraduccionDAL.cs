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
        public List<TraduccionBE> ListarTraducciones_675MS(int idiomaSel_675MS)
        {
            Acceso acceso_675MS = new Acceso();

            List<TraduccionBE> traducciones_675MS = new List<TraduccionBE>();

            acceso_675MS.Conectar_675MS();
            string sql_675MS = "ConsultaTraducciones";
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
    }
}
