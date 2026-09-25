using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
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
            integridadBLL_675MS = new IntegridadBLL();
            ValidarCredencialesIngresadas_675MS(username_675MS, password_675MS);
            integridadBLL_675MS.ValidarIntegridadParaLogin_675MS();

            UsuarioBE usuario_675MS = usuarioDAL_675MS.ObtenerPorNombreUsuario_675MS(username_675MS);

            if (usuario_675MS == null)
            {
                bitacoraBLL_675MS.Registrar_675MS("SIN_SESION", "LOGIN_FALLIDO", "Seguridad", "Intento de login con usuario inexistente: " + username_675MS);
                throw new Exception("Usuario inexistente.");
            }

            if (usuario_675MS.Bloqueado_675MS  == "S")
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

            usuarioDAL_675MS.ReiniciarIntentosFallidos_675MS(username_675MS);
            SessionManager.Login_675MS(usuario_675MS);
            bitacoraBLL_675MS.RegistrarLogin_675MS(username_675MS);

            return usuario_675MS;
        }

        public void Logout_675MS()
        {
            string nombreUsuario = "SIN_SESION";
            integridadBLL_675MS = new IntegridadBLL();
            bitacoraBLL_675MS = new BitacoraBLL();

            if (SessionManager.SesionIniciada_675MS)
            {
                nombreUsuario = SessionManager.ObtenerUsuarioActual_675MS().Username_675MS;
                integridadBLL_675MS.ActualizarIntegridadParaLogout_675MS();
                SessionManager.Logout_675MS();
            }

            bitacoraBLL_675MS.RegistrarLogout_675MS(nombreUsuario);
        }

        public int AltaUsuario_675MS(UsuarioBE usuario_675MS)
        {
            usuarioDAL_675MS= new UsuarioDAL();
            integridadBLL_675MS = new IntegridadBLL();
            bitacoraBLL_675MS = new BitacoraBLL();

            ValidarDatosRegistro_675MS(usuario_675MS);
            usuario_675MS.Password_675MS = Seguridad.GenerarHash_675MS(usuario_675MS.Password_675MS);

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

            if (usuario_675MS.Empleado_675MS.DniEmpleado <= 0)
            {
                throw new Exception("Ingresar número de documento válido.");
            }

        
        }

    }
}
