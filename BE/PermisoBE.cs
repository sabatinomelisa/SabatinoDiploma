using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class PermisoBE : ComponenteBE
    {
        public override bool TienePermiso_675MS(string permiso)
        {
            return Nombre_675MS == permiso;
        }
    }
}
