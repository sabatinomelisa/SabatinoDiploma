using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class RolBE : ComponenteBE
    {
        // Lista de permisos que tiene asignados este rol.
        public List<ComponenteBE> Hijos_675MS { get; set; } = new List<ComponenteBE>();

        public override bool TienePermiso_675MS(string permiso_675MS)
        {
            // El rol tiene un permiso si alguno de sus hijos lo tiene.

            return Hijos_675MS.Any(hijo => hijo.TienePermiso_675MS(permiso_675MS));
        }

        public void Agregar_675MS(ComponenteBE componente_675MS)
        {
            Hijos_675MS.Add(componente_675MS);
        }
    }
}
