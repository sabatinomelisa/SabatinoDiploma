using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class UsuarioBE
    {
        private string username_675MS;

        public string Username_675MS
        {
            get { return username_675MS; }
            set { username_675MS = value; }
        }

        private string password_675MS;

        public string Password_675MS
        {
            get { return password_675MS; }
            set { password_675MS = value; }
        }

        private DateTime fechaCreacion_675MS;

        public DateTime FechaCreacion_675MS
        {
            get { return fechaCreacion_675MS; }
            set { fechaCreacion_675MS = value; }
        }

        private string bloqueado_675MS;

        public string Bloqueado_675MS
        {
            get { return bloqueado_675MS; }
            set { bloqueado_675MS = value; }
        }


        private int intentosFallidos_675MS;

        public int IntentosFallidos_675MS
        {
            get { return intentosFallidos_675MS; }
            set { intentosFallidos_675MS = value; }
        }

        private bool solicitudDesbloqueo_675MS;

        public bool SolicitudDesbloqueo_675MS
        {
            get { return solicitudDesbloqueo_675MS; }
            set { solicitudDesbloqueo_675MS = value; }
        }


    }
}
