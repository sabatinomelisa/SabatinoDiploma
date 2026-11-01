using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    // Clase base del patrón Composite
    // Define las propiedades comunes a los roles y permisos
    public abstract class ComponenteBE
    {
        private int id_675MS;

        // Identificador del rol o permiso
        public int Id_675MS
        {
            get { return id_675MS; }
            set { id_675MS = value; }
        }
        // Nombre del rol o código del permiso
        private string nombre_675MS;

        public string Nombre_675MS
        {
            get { return nombre_675MS; }
            set { nombre_675MS = value; }
        }

        // Cada componente define cómo comprobar un permiso
        public abstract bool TienePermiso_675MS(string permiso_675MS);
    }
}
