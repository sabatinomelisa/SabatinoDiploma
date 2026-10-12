using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public abstract class ComponenteBE
    {
        private int id_675MS;

        public int Id_675MS
        {
            get { return id_675MS; }
            set { id_675MS = value; }
        }

        private string nombre_675MS;

        public string Nombre_675MS
        {
            get { return nombre_675MS; }
            set { nombre_675MS = value; }
        }


        public abstract bool TienePermiso_675MS(string permiso);
    }
}
