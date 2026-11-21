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
    public class BitacoraDAL
    {
        public int Registrar_675MS(BitacoraBE bitacora_675MS)
        {
            Acceso acceso_675MS = new Acceso();

            acceso_675MS.Conectar_675MS();

            List<SqlParameter> parametros_675MS = new List<SqlParameter>();

            acceso_675MS.IniciarTx_675MS();

            try
            {
                //Doy de alta el usuario
                string sql = "ALTA_BITACORA";
                parametros_675MS.Clear();
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@usu", bitacora_675MS.Usuario_675MS));
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@accion", bitacora_675MS.Accion_675MS));
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@modulo", bitacora_675MS.Modulo_675MS));
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@descripcion", bitacora_675MS.Descripcion_675MS));


                int resultado_675MS = acceso_675MS.Escribir_675MS(sql, parametros_675MS);

                if (resultado_675MS != -1)
                {
                    acceso_675MS.ConfirmarTx_675MS();
                    acceso_675MS.Desconectar_675MS();
                    return resultado_675MS;
                }
                else
                {
                    acceso_675MS.Desconectar_675MS();
                    return resultado_675MS;
                }

            }
            catch (Exception ex)
            {
                acceso_675MS.RevertirTx_675MS();
                acceso_675MS.Desconectar_675MS();
                throw new Exception("Error al registrar la bitacora");
            }

        }

        public DataTable Consultar_675MS(string usuario_675MS, string modulo_675MS, DateTime? desde_675MS, DateTime? hasta_675MS)
        {
            Acceso acceso_675MS = new Acceso();

            try
            {
                acceso_675MS.Conectar_675MS();

                string sql_675MS = "CONSULTAR_BITACORA";

                List<SqlParameter> parametros_675MS = new List<SqlParameter>();
                parametros_675MS.Add(new SqlParameter("@usuario", SqlDbType.VarChar, 100) { Value = string.IsNullOrWhiteSpace(usuario_675MS) ? (object)DBNull.Value : usuario_675MS.Trim() });
                parametros_675MS.Add(new SqlParameter("@modulo", SqlDbType.VarChar, 100) { Value = string.IsNullOrWhiteSpace(modulo_675MS) ? (object)DBNull.Value : modulo_675MS.Trim() });
                parametros_675MS.Add(new SqlParameter("@desde", SqlDbType.Date) { Value = desde_675MS.HasValue ? (object)desde_675MS.Value.Date : DBNull.Value });
                parametros_675MS.Add(new SqlParameter("@hasta", SqlDbType.Date) { Value = hasta_675MS.HasValue ? (object)hasta_675MS.Value.Date : DBNull.Value });

                return acceso_675MS.Leer_675MS(sql_675MS, parametros_675MS);
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }
        }
    }
}
