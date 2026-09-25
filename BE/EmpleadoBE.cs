using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class EmpleadoBE
    {
		private int dniEmpleado;

		public int DniEmpleado
		{
			get { return dniEmpleado; }
			set { dniEmpleado = value; }
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

    }
}
