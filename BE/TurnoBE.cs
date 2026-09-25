using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class TurnoBE
    {
		//Defino los datos del turno del cliente
		private int idTurno_675MS;

		public int IdTurno_675MS
		{
			get { return idTurno_675MS; }
			set { idTurno_675MS = value; }
		}

		private int dni_675MS;

		public int Dni_675MS
		{
			get { return dni_675MS; }
			set { dni_675MS = value; }
		}


		private string dominio_675MS;

		public string Dominio_675MS
		{
			get { return dominio_675MS; }
			set { dominio_675MS = value; }
		}

		private DateTime fechaHora_675MS;

		public DateTime FechaHora_675MS
		{
			get { return fechaHora_675MS; }
			set { fechaHora_675MS = value; }
		}

		private int duracionEstimada_675MS;

		public int DuracionEstimada_675MS
		{
			get { return duracionEstimada_675MS; }
			set { duracionEstimada_675MS = value; }
		}

        private int digitoVerificadorHorizontal_675MS;

        public int DigitoVerificadorHorizontal_675MS
        {
            get
            {
                return digitoVerificadorHorizontal_675MS;
            }
            set
            {
                digitoVerificadorHorizontal_675MS = value;
            }
        }

		private bool activo_675MS;

		public bool Activo_675MS
		{
			get { return activo_675MS; }
			set { activo_675MS = value; }
		}

		private DateTime fechaBaja_675MS;

		public DateTime FechaBaja_675MS
		{
			get { return fechaBaja_675MS; }
			set { fechaBaja_675MS = value; }
		}

	}
}
