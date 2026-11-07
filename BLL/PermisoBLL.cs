using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PermisoBLL
    {
        PermisoDAL permisoDAL_675MS;
        public List<PermisoBE> ListarPermisos_675MS()
        {
            permisoDAL_675MS = new PermisoDAL();

            List<PermisoBE> permisos_675MS = permisoDAL_675MS.ListarPermisos_675MS();

            return permisos_675MS;
        }
    }
}
