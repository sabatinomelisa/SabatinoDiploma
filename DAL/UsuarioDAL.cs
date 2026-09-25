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
                string sql = "ALTA_USUARIO";
                parametros_675MS.Clear();
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@usuario", usr_675MS.Username_675MS));
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@pass", usr_675MS.Password_675MS));
                //consultar por dni al empleado
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@idEmp", 1));


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

        public int IncrementarIntentosFallidos_675MS(string nombreUsuario_675MS)
        {
            return EjecutarOperacionUsuario_675MS("IncrementarIntentosFallidos", nombreUsuario_675MS);
        }

        public int ReiniciarIntentosFallidos_675MS(string nombreUsuario_675MS)
        {
            return EjecutarOperacionUsuario_675MS("ReiniciarIntentosFallidos", nombreUsuario_675MS);
        }

        public int BloquearUsuario_675MS(string nombreUsuario_675MS)
        {
            return EjecutarOperacionUsuario_675MS("BloquearUsuario", nombreUsuario_675MS);
        }

        public int DesbloquearUsuario_675MS(string nombreUsuario_675MS)
        {
            return EjecutarOperacionUsuario_675MS("DesbloquearUsuario", nombreUsuario_675MS);
        }

        public int ActivarUsuario_675MS(string nombreUsuario_675MS)
        {
            return EjecutarOperacionUsuario_675MS("ActivarUsuario", nombreUsuario_675MS);
        }

        public int DesactivarUsuario(string nombreUsuario_675MS)
        {
            return EjecutarOperacionUsuario_675MS("DesactivarUsuario", nombreUsuario_675MS);
        }

        private int EjecutarOperacionUsuario_675MS(string procedimiento_675MS, string nombreUsuario_675MS)
        {
            Acceso acceso_675MS = new Acceso();
            acceso_675MS.Conectar_675MS();
            acceso_675MS.IniciarTx_675MS();

            try
            {
                List<SqlParameter> parametros_675MS = new List<SqlParameter>();
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@usu", nombreUsuario_675MS));

                int resultado_675MS = acceso_675MS.Escribir_675MS(procedimiento_675MS, parametros_675MS);
                acceso_675MS.ConfirmarTx_675MS();
                return resultado_675MS;
            }
            catch
            {
                acceso_675MS.RevertirTx_675MS();
                throw;
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }
        }

        public UsuarioBE ObtenerPorNombreUsuario_675MS(string usuarioIngresado_675MS)
        {
            if (string.IsNullOrWhiteSpace(usuarioIngresado_675MS))
            {
                return null;
            }

            Acceso acceso_675MS = new Acceso();
            acceso_675MS.Conectar_675MS();

            try
            {
                List<SqlParameter> parametros_675MS = new List<SqlParameter>();
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@usu", usuarioIngresado_675MS));

                DataTable tabla = acceso_675MS.Leer_675MS("OBTENER_USUARIO_NOMBRE", parametros_675MS);

                if (tabla.Rows.Count == 0)
                {
                    return null;
                }

                return MapearUsuario_675MS(tabla.Rows[0]);
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }
        }

        private UsuarioBE MapearUsuario_675MS(DataRow fila_675MS)
        {
            UsuarioBE usuario_675MS = new UsuarioBE();
            usuario_675MS.Username_675MS = fila_675MS["Usuario"].ToString();
            usuario_675MS.Password_675MS = fila_675MS["Contrasena"].ToString();
            usuario_675MS.FechaCreacion_675MS = Convert.ToDateTime(fila_675MS["FechaCreacion"]);
            usuario_675MS.Bloqueado_675MS = fila_675MS["Bloqueado"].ToString();
            usuario_675MS.IntentosFallidos_675MS = Convert.ToInt32(fila_675MS["IntentosFallidos"]);
            usuario_675MS.IdRol_675MS = Convert.ToInt32(fila_675MS["IdRol"].ToString());
  
            return usuario_675MS;
        }

    }
}
