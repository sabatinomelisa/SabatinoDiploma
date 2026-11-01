using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    // Hoja del patrón Composite: representa un permiso individual
    // Hereda de ComponenteBE para poder tratarlo igual que un rol
    public class PermisoBE : ComponenteBE
    {
        public override bool TienePermiso_675MS(string permiso_675MS)
        {
            return Nombre_675MS == permiso_675MS;
        }
    }
}
