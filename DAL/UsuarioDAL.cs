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
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@dv", usr_675MS.DigitoVerificadorHorizontal_675MS));
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@idEmp", usr_675MS.IdEmpleado_675MS));
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@idRol", usr_675MS.IdRol_675MS));
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@fechaCreacion", usr_675MS.FechaCreacion_675MS));

                int resultado = acceso_675MS.Escribir_675MS(sql, parametros_675MS);

                if (resultado > 0)
                {
                    acceso_675MS.ConfirmarTx_675MS();
                    acceso_675MS.Desconectar_675MS();
                    return resultado;
                }
                else
                {
                    acceso_675MS.RevertirTx_675MS();
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

        public UsuarioBE DevolverUser_675MS(string usrIngresado, string password = null)
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
                    usrAux.IdEmpleado_675MS = row["IdEmpleado"] != DBNull.Value ? Convert.ToInt32(row["IdEmpleado"]) : 0;

                }

            }

            acceso.Desconectar_675MS();

            return usrAux;
        }

        public int IncrementarIntentosFallidos_675MS(string nombreUsuario_675MS)
        {
            return EjecutarOperacionUsuario_675MS("INCREMENTAR_INTENTOS_FALLIDOS", nombreUsuario_675MS);
        }

        public int ReiniciarIntentosFallidos_675MS(string nombreUsuario_675MS)
        {
            return EjecutarOperacionUsuario_675MS("REINICIAR_INTENTOS_FALLIDOS", nombreUsuario_675MS);
        }

        public int BloquearUsuario_675MS(string nombreUsuario_675MS)
        {
            return EjecutarOperacionUsuario_675MS("BLOQUEAR_USUARIO", nombreUsuario_675MS);
        }

        public int DesbloquearUsuario_675MS(string nombreUsuario_675MS)
        {
            return EjecutarOperacionUsuario_675MS("DESBLOQUEAR_USUARIO", nombreUsuario_675MS);
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

            usuario_675MS.IdUsuario_675MS = Convert.ToInt32(fila_675MS["Id"]);
            usuario_675MS.Username_675MS = fila_675MS["Usuario"].ToString();
            usuario_675MS.Password_675MS = fila_675MS["Contrasena"].ToString();
            usuario_675MS.FechaCreacion_675MS = Convert.ToDateTime(fila_675MS["FechaCreacion"]);
            usuario_675MS.Bloqueado_675MS = fila_675MS["Bloqueado"].ToString();
            usuario_675MS.IntentosFallidos_675MS = Convert.ToInt32(fila_675MS["IntentosFallidos"]);
            usuario_675MS.IdRol_675MS = Convert.ToInt32(fila_675MS["IdRol"]);
            usuario_675MS.Activo_675MS = Convert.ToInt32(fila_675MS["Activo"]);
            usuario_675MS.IdEmpleado_675MS = fila_675MS["IdEmpleado"] != DBNull.Value ? Convert.ToInt32(fila_675MS["IdEmpleado"]) : 0;

            RolDAL rolDAL_675MS = new RolDAL();
            usuario_675MS.Rol_675MS = rolDAL_675MS.ObtenerComponentesRol(usuario_675MS.IdRol_675MS);

            return usuario_675MS;
        }


        public List<UsuarioBloqueadoBE> ListarUsuariosBloqueados_675MS()
        {
            Acceso acceso_675MS = new Acceso();
            List<UsuarioBloqueadoBE> usuariosBloqueados_675MS = new List<UsuarioBloqueadoBE>();

            acceso_675MS.Conectar_675MS();

            try
            {
                DataTable tabla_675MS = acceso_675MS.Leer_675MS("OBTENER_USUARIOS_BLOQUEADOS");

                foreach (DataRow row in tabla_675MS.Rows)
                {
                    UsuarioBloqueadoBE usuarioBloqueadoAuxiliar_675MS = new UsuarioBloqueadoBE();

                    usuarioBloqueadoAuxiliar_675MS.IdUsuario_675MS = Convert.ToInt32(row["IdUsuario"]);
                    usuarioBloqueadoAuxiliar_675MS.Usuario_675MS = row["NombreUsuario"].ToString();
                    usuarioBloqueadoAuxiliar_675MS.Nombre_675MS = row["NombreEmpleado"].ToString();
                    usuarioBloqueadoAuxiliar_675MS.Apellido_675MS = row["ApellidoEmpleado"].ToString();
                    usuarioBloqueadoAuxiliar_675MS.Mail_675MS = row["MailEmpleado"].ToString();
                    usuarioBloqueadoAuxiliar_675MS.DescripcionRol_675MS = row["DescripcionRol"].ToString();

                    usuariosBloqueados_675MS.Add(usuarioBloqueadoAuxiliar_675MS);
                }

                return usuariosBloqueados_675MS;
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }
        }

        private RolBE ObtenerRolConPermisos_675MS(int idRol_675MS)
        {
            RolBE rol_675MS = new RolBE();

            PermisoBE permiso_675MS = new PermisoBE();
            permiso_675MS.Nombre_675MS = "CLIENTES.GESTIONAR";

            rol_675MS.Agregar_675MS(permiso_675MS);

            return rol_675MS;
        }

        public UsuarioBE ObtenerUsuarioPorId_675MS(int idUsuario_675MS)
        {
            Acceso acceso_675MS = new Acceso();
            acceso_675MS.Conectar_675MS();

            try
            {
                List<SqlParameter> parametros_675MS = new List<SqlParameter>();
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@id", idUsuario_675MS));

                DataTable tabla = acceso_675MS.Leer_675MS("OBTENER_USUARIO_POR_ID", parametros_675MS);

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


        public int ActualizarUsuarioConDVH_675MS(UsuarioBE usr_675MS)
        {
            Acceso acceso_675MS = new Acceso();
            bool transaccionActiva_675MS = false;

            try
            {
                acceso_675MS.Conectar_675MS();
                acceso_675MS.IniciarTx_675MS();
                transaccionActiva_675MS = true;

                string sql_675MS = "ACTIVAR_USUARIO";
                List<SqlParameter> parametros_675MS = new List<SqlParameter>();

                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@usu", usr_675MS.Username_675MS));
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@bloqueado", usr_675MS.Bloqueado_675MS));
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@dv", usr_675MS.DigitoVerificadorHorizontal_675MS));

                int resultado_675MS = acceso_675MS.Escribir_675MS(sql_675MS, parametros_675MS);

                if (resultado_675MS <= 0)
                {
                    throw new Exception("No se pudo desbloquear el usuario. Verifique que continúe bloqueado.");
                }

                acceso_675MS.ConfirmarTx_675MS();
                transaccionActiva_675MS = false;

                return resultado_675MS;
            }
            catch
            {
                if (transaccionActiva_675MS)
                {
                    acceso_675MS.RevertirTx_675MS();
                }

                throw;
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }
        }

        public DataTable ListarEmpleadosRoles_675MS()
        {
            Acceso acceso_675MS = new Acceso();
            acceso_675MS.Conectar_675MS();

            try
            {
                List<SqlParameter> parametros_675MS = new List<SqlParameter>();

                DataTable tabla_675MS = acceso_675MS.Leer_675MS("LISTAR_EMPLEADOS_ROLES", parametros_675MS);

                return tabla_675MS;
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }
        }


        public int AsignarRolUsuario_675MS(int idUsuario_675MS, int idRol_675MS, int dvh_675MS)
        {
            Acceso acceso_675MS = new Acceso();
            bool transaccionActiva_675MS = false;

            try
            {
                acceso_675MS.Conectar_675MS();
                acceso_675MS.IniciarTx_675MS();
                transaccionActiva_675MS = true;

                List<SqlParameter> parametros_675MS = new List<SqlParameter>();
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@IdUsuario", idUsuario_675MS));
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@IdRol", idRol_675MS));
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@DVH", dvh_675MS));

                int resultado_675MS = acceso_675MS.Escribir_675MS("ASIGNAR_ROL_USUARIO_CON_DVH", parametros_675MS);

                if (resultado_675MS <= 0) throw new Exception("No se pudo actualizar el rol del usuario.");

                acceso_675MS.ConfirmarTx_675MS();
                transaccionActiva_675MS = false;

                return resultado_675MS;
            }
            catch
            {
                if (transaccionActiva_675MS) acceso_675MS.RevertirTx_675MS();
                throw;
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }
        }



        public bool EmpleadoTieneUsuario_675MS(int idEmpleado_675MS)
        {
            Acceso acceso_675MS = new Acceso();

            try
            {
                acceso_675MS.Conectar_675MS();

                List<SqlParameter> parametros_675MS =  new List<SqlParameter>();

                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@idEmpleado", idEmpleado_675MS));

                int cantidad_675MS = acceso_675MS.DevolverEscalar_675MS( "EMPLEADO_TIENE_USUARIO", parametros_675MS);

                return cantidad_675MS > 0;
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }
        }


        public DataTable ListarUsuarios_675MS()
        {
            Acceso acceso_675MS = new Acceso();

            try
            {
                acceso_675MS.Conectar_675MS();
                DataTable tabla_675MS = acceso_675MS.Leer_675MS("LISTAR_USUARIOS");
                return tabla_675MS;
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }
        }


        public int ActivarCuentaUsuario_675MS(int idUsuario_675MS)
        {
            Acceso acceso_675MS = new Acceso();
            bool transaccionActiva_675MS = false;

            try
            {
                acceso_675MS.Conectar_675MS();
                acceso_675MS.IniciarTx_675MS();
                transaccionActiva_675MS = true;

                List<SqlParameter> parametros_675MS = new List<SqlParameter>();
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@IdUsuario", idUsuario_675MS));

                int resultado_675MS = acceso_675MS.Escribir_675MS("ACTIVAR_CUENTA_USUARIO", parametros_675MS);

                if (resultado_675MS <= 0) throw new Exception("No se pudo activar el usuario. Es posible que ya esté activo.");

                acceso_675MS.ConfirmarTx_675MS();
                transaccionActiva_675MS = false;

                return resultado_675MS;
            }
            catch
            {
                if (transaccionActiva_675MS) acceso_675MS.RevertirTx_675MS();
                throw;
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }
        }


        public int DesactivarCuentaUsuario_675MS(int idUsuario_675MS)
        {
            Acceso acceso_675MS = new Acceso();
            bool transaccionActiva_675MS = false;

            try
            {
                acceso_675MS.Conectar_675MS();
                acceso_675MS.IniciarTx_675MS();
                transaccionActiva_675MS = true;

                List<SqlParameter> parametros_675MS = new List<SqlParameter>();
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@IdUsuario", idUsuario_675MS));

                int resultado_675MS = acceso_675MS.Escribir_675MS("DESACTIVAR_CUENTA_USUARIO", parametros_675MS);

                if (resultado_675MS <= 0) throw new Exception("No se pudo desactivar el usuario. Es posible que ya esté inactivo.");

                acceso_675MS.ConfirmarTx_675MS();
                transaccionActiva_675MS = false;

                return resultado_675MS;
            }
            catch
            {
                if (transaccionActiva_675MS) acceso_675MS.RevertirTx_675MS();
                throw;
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }
        }


    }
}