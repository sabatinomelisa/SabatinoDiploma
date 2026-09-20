using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class ClienteBE
    {
		private int dni_06675MS;

		public int Dni_06675MS
        {
			get { return dni_06675MS; }
			set { dni_06675MS = value; }
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

		private DateTime fechaNacimiento;

		public DateTime FechaNacimiento
		{
			get { return fechaNacimiento; }
			set { fechaNacimiento = value; }
		}

		private string mail;

		public string Mail
		{
			get { return mail; }
			set { mail = value; }
		}

		private string domicilio;

		public string Domicilio
		{
			get { return domicilio; }
			set { domicilio = value; }
		}

		private int telefono;

		public int Telefono
		{
			get { return telefono; }
			set { telefono = value; }
		}


	}
}
