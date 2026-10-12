using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class RolBE : ComponenteBE
    {
     public List<ComponenteBE> Hijos_675MS { get; set; } = new List<ComponenteBE>();

        public override bool TienePermiso_675MS(string permiso)
        {
            return Hijos_675MS.Any(
                hijo => hijo.TienePermiso_675MS(permiso));
        }

        public void Agregar_675MS(ComponenteBE componente)
        {
            Hijos_675MS.Add(componente);
        }
    }
}
