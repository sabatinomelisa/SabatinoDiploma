using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class VehiculoBE
    {
        private string dominio_675MS;

        public string Dominio_675MS
        {
            get { return dominio_675MS; }
            set { dominio_675MS = value; }
        }

        private int dni_675MS;

        public int Dni_675MS
        {
            get { return dni_675MS; }
            set { dni_675MS = value; }
        }

        private string modelo_675MS;

        public string Modelo_675MS
        {
            get { return modelo_675MS; }
            set { modelo_675MS = value; }
        }

        private string version_675MS;

        public string Version_675MS
        {
            get { return version_675MS; }
            set { version_675MS = value; }
        }


        private int anio_675MS;

        public int Anio_675MS
        {
            get { return anio_675MS; }
            set { anio_675MS = value; }
        }

        private string nroChasis_675MS;

        public string NroChasis_675MS
        {
            get { return nroChasis_675MS; }
            set { nroChasis_675MS = value; }
        }

        private DateTime ultimaVTV_675MS;

        public DateTime UltimaVTV_675MS
        {
            get { return ultimaVTV_675MS; }
            set { ultimaVTV_675MS = value; }
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
