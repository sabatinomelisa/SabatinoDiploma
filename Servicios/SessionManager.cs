using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace Servicios
{
    public class SessionManager
    {
        private static object _lock = new object();

        private static SessionManager session_675MS;

        UsuarioBE Usuario_675MS { get; set; }
        public DateTime FechaInicio_675MS { get; set; }
        public static SessionManager ObtenerInstancia_675MS()
        {
            if (session_675MS == null)
            {
                throw new InvalidOperationException("Sesión no iniciada.");
            }

            return session_675MS;
        }

        public static void Login_675MS(UsuarioBE usuario_675MS)
        {
            lock (_lock)
            {
                if (session_675MS == null)
                {
                    session_675MS = new SessionManager();
                    session_675MS.Usuario_675MS = usuario_675MS;
                    session_675MS.FechaInicio_675MS = DateTime.Now;

                }
                else
                {
                    throw new Exception("Sesión ya iniciada");
                }
            }

        }

        public static void Logout_675MS()
        {
            lock (_lock)
            {
                if (session_675MS != null)
                {
                    session_675MS = null;
                }
                else
                {
                    throw new Exception("Sesion no iniciada");
                }
            }

        }
        private SessionManager()
        {

        }

        public static bool SesionIniciada_675MS
        {
            get { return session_675MS != null; }
        }

        public static UsuarioBE ObtenerUsuarioActual_675MS()
        {
            return ObtenerInstancia_675MS().Usuario_675MS;

        }

    }
}
