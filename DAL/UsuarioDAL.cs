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
    public class UsuarioDAL
    {
        public int ActPass_675MS(UsuarioBE usr_675MS)
        {
            Acceso acceso_675MS = new Acceso();

            acceso_675MS.Conectar_675MS();

            List<SqlParameter> parametros_675MS = new List<SqlParameter>();

            acceso_675MS.IniciarTx_675MS();

            try
            {
                string sql = "ActualizaPass";
                parametros_675MS.Clear();
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@usu", usr_675MS.Username_675MS));
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@nuevapass", usr_675MS.Password_675MS));
                int resultado_675MS = acceso_675MS.Escribir_675MS(sql, parametros_675MS);
                acceso_675MS.ConfirmarTx_675MS();
                acceso_675MS.Desconectar_675MS();
                return resultado_675MS;

            }
            catch (Exception ex)
            {
                acceso_675MS.RevertirTx_675MS();
                acceso_675MS.Desconectar_675MS();
                throw new Exception("Error al actualizar contraseña");
            }
        }

        public int AltaUsuario_675MS(UsuarioBE usr_675MS)
        {
            Acceso acceso_675MS = new Acceso();

            acceso_675MS.Conectar_675MS();

            List<SqlParameter> parametros_675MS = new List<SqlParameter>();

            acceso_675MS.IniciarTx_675MS();

            try
            {
                //Doy de alta el usuario
                 string sql = "RegistrarUsuario";
                 parametros_675MS.Clear();
                 parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@usuario", usr_675MS.Username_675MS));
                 parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@password", usr_675MS.Password_675MS));
                 parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@fechaCreacion", usr_675MS.FechaCreacion_675MS));


                 int resultado = acceso_675MS.Escribir_675MS(sql, parametros_675MS);

                if (resultado != -1)
                {
                    acceso_675MS.ConfirmarTx_675MS();
                    acceso_675MS.Desconectar_675MS();
                    return resultado;
                }
                else
                {
                    acceso_675MS.Desconectar_675MS();
                    return resultado;
                }

            }
            catch (Exception ex)
            {
                acceso_675MS.RevertirTx_675MS();
                acceso_675MS.Desconectar_675MS();
                throw new Exception("Error al registrar al usuario");
            }

        }

        public UsuarioBE DevolverUser(string usrIngresado, string password = null)
        {
            Acceso acceso = new Acceso();
            UsuarioBE usrAux = new UsuarioBE();
            string sql;

            acceso.Conectar_675MS();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Clear();
            parametros.Add(acceso.CrearParametro_675MS("@usu", usrIngresado));

            if (password == null)
            {
                //Consulto si el usuario ya existe
                sql = "ConsultaUsuario";
                string usrConsultado = acceso.DevolverEscalarString_675MS(sql, parametros);
                usrAux.Username_675MS = usrConsultado;
            }
            else
            {
                sql = "ConsultaUsrPass";
                parametros.Add(acceso.CrearParametro_675MS("@pass", password));
                DataTable tabla = new DataTable();
                tabla = acceso.Leer_675MS(sql, parametros);

                foreach (DataRow row in tabla.Rows)
                {
                    usrAux.Username_675MS = row["NombreUsuario"].ToString();
                    usrAux.Password_675MS = row["Contraseña"].ToString();

                }

            }

            acceso.Desconectar_675MS();

            return usrAux;
        }
    }
}
