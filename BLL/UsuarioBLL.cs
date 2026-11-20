using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL
{
    public class UsuarioBLL
    {
        private const int CantidadMaximaIntentosFallidos_675MS = 3;
        private  UsuarioDAL usuarioDAL_675MS;
        private  BitacoraBLL bitacoraBLL_675MS;
        private  IntegridadBLL integridadBLL_675MS;

        public void ValidarCredencialesIngresadas_675MS(string username_675MS, string password_675MS)
        {
            if (string.IsNullOrWhiteSpace(username_675MS))
            {
                throw new Exception("Ingresar nombre de usuario.");
            }

            if (string.IsNullOrWhiteSpace(password_675MS))
            {
                throw new Exception("Ingresar contraseña.");
            }else
            {
                if (!ValidarPass_675MS(password_675MS))
                    {
                    throw new Exception("La contraseña debe tener como mínimo 8 caracteres, al menos una letra y al menos un número.");
                    }
            }
        }

        public bool ValidarPass_675MS(string password_675MS)
        {
            if (string.IsNullOrWhiteSpace(password_675MS))
            {
                return false;
            }

            string patron_675MS = @"^(?=.*[A-Za-z])(?=.*\d).{8,}$";
            return Regex.IsMatch(password_675MS, patron_675MS);
        }

        public UsuarioBE Login_675MS(string username_675MS, string password_675MS)
        {
            usuarioDAL_675MS = new UsuarioDAL();
            bitacoraBLL_675MS = new BitacoraBLL();
            integridadBLL_675MS = new IntegridadBLL();

            ValidarCredencialesIngresadas_675MS(username_675MS, password_675MS);

            UsuarioBE usuario_675MS = usuarioDAL_675MS.ObtenerPorNombreUsuario_675MS(username_675MS);

            if (usuario_675MS == null)
            {
                bitacoraBLL_675MS.Registrar_675MS("SIN_SESION", "LOGIN_FALLIDO", "Seguridad", "Intento de login con usuario inexistente: " + username_675MS);
                throw new Exception("Usuario inexistente.");

            }
            if (usuario_675MS.Activo_675MS == 0)
            {
                bitacoraBLL_675MS.Registrar_675MS(username_675MS, "LOGIN_FALLIDO", "Seguridad", "Intento de ingreso con una cuenta inactiva.");
                throw new Exception("Tu cuenta está inactiva. Contactá al administrador para solicitar su activación.");
            }

            if (usuario_675MS.Bloqueado_675MS  == "1")
            {
                bitacoraBLL_675MS.Registrar_675MS(username_675MS, "LOGIN_FALLIDO", "Seguridad", "El usuario está bloqueado.");
                throw new Exception("El usuario está bloqueado.");
            }

            bool passwordValida = Seguridad.VerificarPassword_675MS(password_675MS, usuario_675MS.Password_675MS);

            if (!passwordValida)
            {
                usuarioDAL_675MS.IncrementarIntentosFallidos_675MS(username_675MS);
                UsuarioBE usuarioActualizado = usuarioDAL_675MS.ObtenerPorNombreUsuario_675MS(username_675MS);

                if (usuarioActualizado != null && usuarioActualizado.IntentosFallidos_675MS >= CantidadMaximaIntentosFallidos_675MS)
                {
                    usuarioDAL_675MS.BloquearUsuario_675MS(username_675MS);
                    bitacoraBLL_675MS.Registrar_675MS(username_675MS, "BLOQUEO", "Seguridad", "Usuario bloqueado por superar la cantidad máxima de intentos fallidos.");
                    throw new Exception("Contraseña incorrecta. La cuenta fue bloqueada por superar los 3 intentos fallidos.");
                }

                bitacoraBLL_675MS.Registrar_675MS(username_675MS, "LOGIN_FALLIDO", "Seguridad", "Contraseña incorrecta.");
                throw new Exception("Contraseña incorrecta.");
            }
            // Primero verificamos que el usuario exista, que no esté bloqueado y que su contraseña sea correcta.

            bool esAdministrador_675MS = usuario_675MS.IdRol_675MS == 1;

            if (!esAdministrador_675MS)
            {
                // Los usuarios comunes no pueden ingresar
                // si existe alguna inconsistencia de integridad.
                integridadBLL_675MS.ValidarIntegridadParaLogin_675MS();
            }
            else
            {
                // El administrador puede ingresar para reparar
                // las inconsistencias, pero debe quedar registrado.
                try
                {
                    integridadBLL_675MS.ValidarIntegridadParaLogin_675MS();
                }
                catch (Exception ex)
                {
                    bitacoraBLL_675MS.Registrar_675MS(
                        username_675MS,
                        "INTEGRIDAD_DETECTADA",
                        "Seguridad",
                        "El administrador ingresó con una inconsistencia de integridad: "
                        + ex.Message);
                }
            }


            usuarioDAL_675MS.ReiniciarIntentosFallidos_675MS(username_675MS);
            SessionManager.Login_675MS(usuario_675MS);
            bitacoraBLL_675MS.RegistrarLogin_675MS(username_675MS);

            return usuario_675MS;
        }


        public void Logout_675MS()
        {
            string nombreUsuario = "SIN_SESION";

            bitacoraBLL_675MS = new BitacoraBLL();

            if (SessionManager.SesionIniciada_675MS)
            {
                nombreUsuario =
                    SessionManager.ObtenerUsuarioActual_675MS().Username_675MS;
            }

            // Cerrar la sesión independientemente del estado de integridad.
            try
            {
                bitacoraBLL_675MS.RegistrarLogout_675MS(nombreUsuario);
            }
            finally
            {
                if (SessionManager.SesionIniciada_675MS)
                {
                    SessionManager.Logout_675MS();
                }
            }
        }


        public int AltaUsuario_675MS(UsuarioBE usuario_675MS)
        {
            usuarioDAL_675MS= new UsuarioDAL();
            integridadBLL_675MS = new IntegridadBLL();
            bitacoraBLL_675MS = new BitacoraBLL();

            ValidarDatosRegistro_675MS(usuario_675MS);
            usuario_675MS.Password_675MS = Seguridad.GenerarHash_675MS(usuario_675MS.Password_675MS);
            // Calculo y asigno el dígito verificador horizontal al cliente
            usuario_675MS.DigitoVerificadorHorizontal_675MS = CalcularDVHUsuario_675MS(usuario_675MS);

            int resultado_675MS = usuarioDAL_675MS.AltaUsuario_675MS(usuario_675MS);

            if (resultado_675MS > 0)
            {
                //integridadBLL_675MS.RecalcularIntegridad_675MS();
                bitacoraBLL_675MS.RegistrarAlta_675MS(usuario_675MS.Username_675MS , "Usuarios", "Usuario registrado correctamente.");
            }

            return resultado_675MS;
        }

        private void ValidarDatosRegistro_675MS(UsuarioBE usuario_675MS)
        {
            if (usuario_675MS == null)
            {
                throw new Exception("Ingresar datos para el registro.");
            }

            if (string.IsNullOrWhiteSpace(usuario_675MS.Password_675MS))
            {
                throw new Exception("Ingresar Contraseña.");
            }

        
        }

        public List<UsuarioBloqueadoBE> ObtenerUsuariosBloqueados()
        {
            usuarioDAL_675MS = new UsuarioDAL();

            List<UsuarioBloqueadoBE> usuariosBloqueados_675MS = usuarioDAL_675MS.ListarUsuariosBloqueados_675MS();

            return usuariosBloqueados_675MS;
        }


        private int CalcularDVHUsuario_675MS(UsuarioBE usuario_675MS)
        {
            CalculadorDigitoVerificador calculador_675MS =  new CalculadorDigitoVerificador();

            return calculador_675MS.CalcularHorizontal_675MS(
                usuario_675MS.Username_675MS,
                usuario_675MS.Password_675MS,
                usuario_675MS.FechaCreacion_675MS.ToString("yyyyMMdd"),
                usuario_675MS.Bloqueado_675MS,
                usuario_675MS.IntentosFallidos_675MS.ToString(),
                usuario_675MS.IdRol_675MS.ToString(),
                usuario_675MS.IdEmpleado_675MS.ToString()
            );
        }


        public int ActivarUsuario(int idUsuario_675MS)
        {
            usuarioDAL_675MS = new UsuarioDAL();

            UsuarioBE usuario_675MS = usuarioDAL_675MS.ObtenerUsuarioPorId_675MS(idUsuario_675MS);

            if (usuario_675MS == null)
            {
                throw new Exception("No se encontró el usuario.");
            }

            if (usuario_675MS.Bloqueado_675MS != "1")
            {
                throw new Exception("El usuario seleccionado no está bloqueado.");
            }

            // Desbloquear y reiniciar intentos fallidos
            usuario_675MS.Bloqueado_675MS = "0";
            usuario_675MS.IntentosFallidos_675MS = 0;

            // Recalcular DVH con los valores nuevos
            usuario_675MS.DigitoVerificadorHorizontal_675MS = CalcularDVHUsuario_675MS(usuario_675MS);

            // Guardar mediante DAL y procedimiento almacenado
            return usuarioDAL_675MS.ActualizarUsuarioConDVH_675MS(usuario_675MS);
        }


        public int ActivarCuentaUsuario_675MS(int idUsuario_675MS)
        {
            if (idUsuario_675MS <= 0) throw new Exception("Debe seleccionar un usuario.");

            if (!SessionManager.SesionIniciada_675MS) throw new Exception("Debe iniciar sesión para activar usuarios.");

            UsuarioBE administrador_675MS = SessionManager.ObtenerUsuarioActual_675MS();

            if (administrador_675MS.IdRol_675MS != 1) throw new Exception("Solo un administrador puede activar usuarios.");

            UsuarioDAL usuarioDAL_675MS = new UsuarioDAL();
            UsuarioBE usuario_675MS = usuarioDAL_675MS.ObtenerUsuarioPorId_675MS(idUsuario_675MS);

            if (usuario_675MS == null) throw new Exception("El usuario seleccionado no existe.");

            int resultado_675MS = usuarioDAL_675MS.ActivarCuentaUsuario_675MS(idUsuario_675MS);

            BitacoraBLL bitacoraBLL_675MS = new BitacoraBLL();
            bitacoraBLL_675MS.Registrar_675MS(administrador_675MS.Username_675MS, "ACTIVAR_USUARIO", "Usuarios", "Se activó la cuenta del usuario " + usuario_675MS.Username_675MS);

            return resultado_675MS;
        }

        public int DesactivarCuentaUsuario_675MS(int idUsuario_675MS)
        {
            if (idUsuario_675MS <= 0) throw new Exception("Debe seleccionar un usuario.");

            if (!SessionManager.SesionIniciada_675MS) throw new Exception("Debe iniciar sesión para desactivar usuarios.");

            UsuarioBE administrador_675MS = SessionManager.ObtenerUsuarioActual_675MS();

            if (administrador_675MS.IdRol_675MS != 1) throw new Exception("Solo un administrador puede desactivar usuarios.");

            if (administrador_675MS.IdUsuario_675MS == idUsuario_675MS) throw new Exception("No puede desactivar su propia cuenta.");

            UsuarioDAL usuarioDAL_675MS = new UsuarioDAL();
            UsuarioBE usuario_675MS = usuarioDAL_675MS.ObtenerUsuarioPorId_675MS(idUsuario_675MS);

            if (usuario_675MS == null) throw new Exception("El usuario seleccionado no existe.");

            int resultado_675MS = usuarioDAL_675MS.DesactivarCuentaUsuario_675MS(idUsuario_675MS);

            BitacoraBLL bitacoraBLL_675MS = new BitacoraBLL();
            bitacoraBLL_675MS.Registrar_675MS(administrador_675MS.Username_675MS, "DESACTIVAR_USUARIO", "Usuarios", "Se desactivó la cuenta del usuario " + usuario_675MS.Username_675MS);

            return resultado_675MS;
        }


        public DataTable ListarEmpleadosRoles_675MS()
        {
            UsuarioDAL usuarioDAL_675MS = new UsuarioDAL();

            return usuarioDAL_675MS.ListarEmpleadosRoles_675MS();
        }


        public DataTable ListarUsuarios_675MS()
        {
            UsuarioDAL usuarioDAL_675MS = new UsuarioDAL();
            return usuarioDAL_675MS.ListarUsuarios_675MS();
        }


        public int AsignarRolUsuario_675MS(int idUsuario_675MS, int idRol_675MS)
        {
            if (idUsuario_675MS <= 0) throw new Exception("Debe seleccionar un usuario.");
            if (idRol_675MS <= 0) throw new Exception("Debe seleccionar un rol válido.");

            UsuarioDAL usuarioDAL_675MS = new UsuarioDAL();
            RolDAL rolDAL_675MS = new RolDAL();

            UsuarioBE usuario_675MS = usuarioDAL_675MS.ObtenerUsuarioPorId_675MS(idUsuario_675MS);

            if (usuario_675MS == null) throw new Exception("El usuario seleccionado no existe.");

            List<RolBE> roles_675MS = rolDAL_675MS.ListarRoles_675MS();
            bool existeRol_675MS = false;

            foreach (RolBE rol_675MS in roles_675MS)
            {
                if (rol_675MS.Id_675MS == idRol_675MS)
                {
                    existeRol_675MS = true;
                    break;
                }
            }

            if (!existeRol_675MS) throw new Exception("El rol seleccionado no existe.");
            if (usuario_675MS.IdRol_675MS == idRol_675MS) throw new Exception("El usuario ya tiene asignado ese rol.");

            if (!SessionManager.SesionIniciada_675MS) throw new Exception("Debe iniciar sesión para modificar roles.");

            UsuarioBE usuarioActual_675MS = SessionManager.ObtenerUsuarioActual_675MS();

            if (usuarioActual_675MS.IdRol_675MS != 1) throw new Exception("Solo un administrador puede modificar roles.");

            if (usuarioActual_675MS.IdUsuario_675MS == idUsuario_675MS) throw new Exception("No puede modificar su propio rol.");


            // Modificar el rol en memoria antes de calcular el DVH
            usuario_675MS.IdRol_675MS = idRol_675MS;
            usuario_675MS.DigitoVerificadorHorizontal_675MS = CalcularDVHUsuario_675MS(usuario_675MS);


            int resultado_675MS = usuarioDAL_675MS.AsignarRolUsuario_675MS(idUsuario_675MS, idRol_675MS, usuario_675MS.DigitoVerificadorHorizontal_675MS);

            BitacoraBLL bitacoraBLL_675MS = new BitacoraBLL();
            bitacoraBLL_675MS.Registrar_675MS(usuarioActual_675MS.Username_675MS, "CAMBIO_ROL", "Usuarios", "Se modificó el rol del usuario " + usuario_675MS.Username_675MS + " al rol ID " + idRol_675MS);

            return resultado_675MS;

        }


        public bool EmpleadoTieneUsuario_675MS(int idEmpleado_675MS)
        {
            UsuarioDAL usuarioDAL_675MS = new UsuarioDAL();

            return usuarioDAL_675MS.EmpleadoTieneUsuario_675MS(idEmpleado_675MS);
        }
    }
}
