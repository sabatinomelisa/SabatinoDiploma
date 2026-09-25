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
    }
}
