using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class ClienteBE
    {
		private int dni_675MS;

		public int Dni_675MS
        {
			get { return dni_675MS; }
			set { dni_675MS = value; }
		}

		private string apellido_675MS;

		public string Apellido_675MS
		{
			get { return apellido_675MS; }
			set { apellido_675MS = value; }
		}

		private string nombre_675MS;

		public string Nombre_675MS
		{
			get { return nombre_675MS; }
			set { nombre_675MS = value; }
		}

		private DateTime fechaNacimiento_675MS;

		public DateTime FechaNacimiento_675MS
        {
			get { return fechaNacimiento_675MS; }
			set { fechaNacimiento_675MS = value; }
		}

		private string mail_675MS;

		public string Mail_675MS
        {
			get { return mail_675MS; }
			set { mail_675MS = value; }
		}

		private string domicilio_675MS;

		public string Domicilio_675MS
        {
			get { return domicilio_675MS; }
			set { domicilio_675MS = value; }
		}

		private int telefono_675MS;

		public int Telefono_675MS
        {
			get { return telefono_675MS; }
			set { telefono_675MS = value; }
		}

        private int digitoVerificadorHorizontal_675MS;
        public int DigitoVerificadorHorizontal_675MS
        {
            get { return digitoVerificadorHorizontal_675MS; }
            set { digitoVerificadorHorizontal_675MS = value; }
        }

    }
}
