using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RolBLL
    {
        private RolDAL rolDAL_675MS = new RolDAL();


        public List<RolBE> ListarRoles_675MS()
        {
            rolDAL_675MS = new RolDAL();

            List<RolBE> roles_675MS = rolDAL_675MS.ListarRoles_675MS();

            return roles_675MS;
        }

        public RolBE ObtenerPorId_675MS(int idRol_675MS)
        {
            if (idRol_675MS <= 0)
                return null;

            return rolDAL_675MS.ObtenerComponentesRol(idRol_675MS);
        }

        public bool TienePermiso_675MS(RolBE rol_675MS, string nombrePermiso_675MS)
        {
            if (rol_675MS == null || string.IsNullOrWhiteSpace(nombrePermiso_675MS))
            {
                return false;
            }

            return rol_675MS.TienePermiso_675MS(nombrePermiso_675MS);
        }

        public void ValidarAcceso_675MS(string permiso_675MS)
        {
            UsuarioBE usuario_675MS = Servicios.SessionManager.ObtenerUsuarioActual_675MS();

            if (usuario_675MS == null || usuario_675MS.Rol_675MS == null || !usuario_675MS.Rol_675MS.TienePermiso_675MS(permiso_675MS))
            {
                throw new UnauthorizedAccessException("No tiene permisos para realizar esta operación.");
            }
        }
    }
}
