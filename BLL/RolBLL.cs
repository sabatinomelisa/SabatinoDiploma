using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
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

        public int AltaRol_675MS(string nombre_675MS)
        {
            if (string.IsNullOrWhiteSpace(nombre_675MS))
            {
                throw new Exception("Debe ingresar un nombre para el rol.");
            }

            nombre_675MS = nombre_675MS.Trim();

            RolDAL rolDAL_675MS = new RolDAL();

            List<RolBE> roles_675MS = rolDAL_675MS.ListarRoles_675MS();

            foreach (RolBE rol_675MS in roles_675MS)
            {
                if (rol_675MS.Nombre_675MS.Equals(nombre_675MS, StringComparison.OrdinalIgnoreCase))
                {
                    throw new Exception("Ya existe un rol con ese nombre.");
                }
            }

            return rolDAL_675MS.AltaRol_675MS(nombre_675MS);
        }

        public int ModificarRol_675MS(int idRol_675MS, string nombre_675MS)
        {
            if (idRol_675MS <= 0)
            {
                throw new Exception("Debe seleccionar un rol válido.");
            }

            if (string.IsNullOrWhiteSpace(nombre_675MS))
            {
                throw new Exception("Debe ingresar un nombre para el rol.");
            }

            nombre_675MS = nombre_675MS.Trim();

            RolDAL rolDAL_675MS = new RolDAL();

            List<RolBE> roles_675MS = rolDAL_675MS.ListarRoles_675MS();

            bool existeRol_675MS = false;

            foreach (RolBE rol_675MS in roles_675MS)
            {
                if (rol_675MS.Id_675MS == idRol_675MS)
                {
                    existeRol_675MS = true;
                }

                if (rol_675MS.Id_675MS != idRol_675MS &&  string.Equals(rol_675MS.Nombre_675MS, nombre_675MS, StringComparison.OrdinalIgnoreCase))
                {
                    throw new Exception("Ya existe otro rol con ese nombre.");
                }
            }

            if (!existeRol_675MS)
            {
                throw new Exception("El rol seleccionado no existe.");
            }

            return rolDAL_675MS.ModificarRol_675MS(idRol_675MS, nombre_675MS);
        }

        public int BajaRol_675MS(int idRol_675MS)
        {
            if (idRol_675MS <= 0)
            {
                throw new Exception("Debe seleccionar un rol válido.");
            }

            RolDAL rolDAL_675MS = new RolDAL();

            List<RolBE> roles_675MS = rolDAL_675MS.ListarRoles_675MS();

            bool existeRol_675MS = false;

            foreach (RolBE rol_675MS in roles_675MS)
            {
                if (rol_675MS.Id_675MS == idRol_675MS)
                {
                    existeRol_675MS = true;
                    break;
                }
            }

            if (!existeRol_675MS)
            {
                throw new Exception("El rol seleccionado no existe.");
            }

            // Proteger los roles principales del sistema
            if (idRol_675MS == 1 || idRol_675MS == 2 || idRol_675MS == 3)
            {
                throw new Exception(
                    "No se pueden eliminar los roles principales del sistema.");
            }

            // Verificar usuarios asignados y relaciones del Composite
            int cantidad_675MS = rolDAL_675MS.VerificarRolEnUso_675MS(idRol_675MS);

            if (cantidad_675MS > 0)
            {
                throw new Exception(
                    "No se puede eliminar el rol porque está asignado a usuarios " +
                    "o tiene relaciones con otros roles y permisos.");
            }

            return rolDAL_675MS.BajaRol_675MS(idRol_675MS);
        }

        public DataTable ListarRolPermisos_675MS()
        {
            return new RolDAL().ListarRolPermisos_675MS();
        }

        private void ValidarRolPermiso_675MS(int idRol_675MS,int idPermiso_675MS, int idRelacionExcluir_675MS = 0)
        {
            RolDAL rolDAL_675MS = new RolDAL();
            PermisoDAL permisoDAL_675MS = new PermisoDAL();

            if (idRol_675MS <= 0 || idPermiso_675MS <= 0)
                throw new Exception("Seleccioná un rol y un permiso válidos.");

            bool existeRol_675MS = rolDAL_675MS.ListarRoles_675MS()
                .Any(r_675MS => r_675MS.Id_675MS == idRol_675MS);

            if (!existeRol_675MS)
                throw new Exception("El rol seleccionado no existe.");

            bool existePermiso_675MS = permisoDAL_675MS.ListarPermisos_675MS().Any(p_675MS => p_675MS.Id_675MS == idPermiso_675MS);

            if (!existePermiso_675MS)
                throw new Exception("El permiso seleccionado no existe.");

            DataTable relaciones_675MS = rolDAL_675MS.ListarRolPermisos_675MS();

            foreach (DataRow fila_675MS in relaciones_675MS.Rows)
            {
                int idRelacion_675MS =  Convert.ToInt32(fila_675MS["IdRolComponente"]);
                int idRolExistente_675MS =Convert.ToInt32(fila_675MS["IdRolPadre"]);
                int idPermisoExistente_675MS = Convert.ToInt32(fila_675MS["IdPermiso"]);

                if (idRelacion_675MS != idRelacionExcluir_675MS && idRolExistente_675MS == idRol_675MS && idPermisoExistente_675MS == idPermiso_675MS)
                {
                    throw new Exception("Este permiso ya está asignado directamente al rol.");
                }
            }
        }

        public int AltaRolPermiso_675MS(int idRol_675MS, int idPermiso_675MS)
        {
            // Proteger rol Admin
            if (idRol_675MS == 1)
                throw new Exception("No se pueden modificar los permisos del rol Administrador.");

            ValidarRolPermiso_675MS(idRol_675MS, idPermiso_675MS);

            return new RolDAL().AltaRolPermiso_675MS(idRol_675MS, idPermiso_675MS);
        }

        public int ModificarRolPermiso_675MS(int idRelacion_675MS,int idRol_675MS, int idPermiso_675MS)
        {
            if (idRelacion_675MS <= 0)
                throw new Exception("Seleccioná una relación válida.");

            RolDAL rolDAL_675MS = new RolDAL();

            DataRow relacion_675MS = rolDAL_675MS.ListarRolPermisos_675MS().AsEnumerable() .FirstOrDefault(f_675MS => Convert.ToInt32(f_675MS["IdRolComponente"]) == idRelacion_675MS);

            if (relacion_675MS == null)
                throw new Exception("La relación seleccionada no existe.");

            int idRolActual_675MS = Convert.ToInt32(relacion_675MS["IdRolPadre"]);

            // Impedir modificar permisos existentes del Admin

            if (idRolActual_675MS == 1 || idRol_675MS == 1)
                throw new Exception("No se pueden modificar los permisos del rol Administrador.");

            ValidarRolPermiso_675MS(idRol_675MS, idPermiso_675MS, idRelacion_675MS);

            return rolDAL_675MS.ModificarRolPermiso_675MS(idRelacion_675MS, idRol_675MS, idPermiso_675MS);
        }

        public int BajaRolPermiso_675MS(int idRelacion_675MS)
        {
            if (idRelacion_675MS <= 0)
                throw new Exception("Seleccioná una relación válida.");

            RolDAL rolDAL_675MS = new RolDAL();

            DataRow relacion_675MS = rolDAL_675MS.ListarRolPermisos_675MS().AsEnumerable().FirstOrDefault(f_675MS => Convert.ToInt32(f_675MS["IdRolComponente"]) == idRelacion_675MS);

            if (relacion_675MS == null)
                throw new Exception("La relación seleccionada no existe.");

            int idRolActual_675MS = Convert.ToInt32(relacion_675MS["IdRolPadre"]);

            // Proteger rol Admin
            if (idRolActual_675MS == 1)
                throw new Exception("No se pueden quitar permisos del rol Administrador.");

            return rolDAL_675MS.BajaRolPermiso_675MS(idRelacion_675MS);
        }


    }
}
