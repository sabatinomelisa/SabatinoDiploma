using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class ResultadoIntegridadBE
    {
        private string entidad_675MS;
        public string Entidad_675MS
        {
            get { return entidad_675MS; }
            set { entidad_675MS = value; }
        }

        private string identificador_675MS;
        public string Identificador_675MS
        {
            get { return identificador_675MS; }
            set { identificador_675MS = value; }
        }

        private string tipoDigito_675MS;
        public string TipoDigito_675MS
        {
            get { return tipoDigito_675MS; }
            set { tipoDigito_675MS = value; }
        }

        private int valorEsperado_675MS;
        public int ValorEsperado_675MS
        {
            get { return valorEsperado_675MS; }
            set { valorEsperado_675MS = value; }
        }

        private int valorActual_675MS;
        public int ValorActual_675MS
        {
            get { return valorActual_675MS; }
            set { valorActual_675MS = value; }
        }

        private bool correcto_675MS;
        public bool Correcto_675MS
        {
            get { return correcto_675MS; }
            set { correcto_675MS = value; }
        }

        private string mensaje_675MS;
        public string Mensaje_675MS
        {
            get { return mensaje_675MS; }
            set { mensaje_675MS = value; }
        }
    }
}
