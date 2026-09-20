using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class TraduccionBE
    {
        private int id_675MS;

        public int Id_675MS
        {
            get { return id_675MS; }
            set { id_675MS = value; }
        }


        private string nombreControl_675MS;

        public string NombreControl_675MS
        {
            get { return nombreControl_675MS; }
            set { nombreControl_675MS = value; }
        }

        private IdiomaBE idioma_675MS;

        public IdiomaBE Idioma_675MS
{
            get { return Idioma_675MS; }
            set { Idioma_675MS = value; }
        }

        private string traduccion_675MS;

        public string Traduccion_675MS
{
            get { return traduccion_675MS; }
            set { traduccion_675MS = value; }
        }

    }
}
