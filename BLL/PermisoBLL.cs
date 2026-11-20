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

        public int AltaPermiso_675MS(string nombre_675MS)
        {
            if (string.IsNullOrWhiteSpace(nombre_675MS))
            {
                throw new Exception("Debe ingresar un nombre para el permiso.");
            }

            nombre_675MS = nombre_675MS.Trim();

            PermisoDAL permisoDAL_675MS = new PermisoDAL();

            List<PermisoBE> permisos_675MS = permisoDAL_675MS.ListarPermisos_675MS();

            foreach (PermisoBE permiso_675MS in permisos_675MS)
            {
                if (string.Equals(permiso_675MS.Nombre_675MS, nombre_675MS,StringComparison.OrdinalIgnoreCase))
                {
                    throw new Exception("Ya existe un permiso con ese nombre.");
                }
            }

            return permisoDAL_675MS.AltaPermiso_675MS(nombre_675MS);
        }

        public int ModificarPermiso_675MS(int idPermiso_675MS, string nombre_675MS)
        {
            if (idPermiso_675MS <= 0)
            {
                throw new Exception("Debe seleccionar un permiso válido.");
            }

            if (string.IsNullOrWhiteSpace(nombre_675MS))
            {
                throw new Exception("Debe ingresar un nombre para el permiso.");
            }

            nombre_675MS = nombre_675MS.Trim();

            PermisoDAL permisoDAL_675MS = new PermisoDAL();

            List<PermisoBE> permisos_675MS = permisoDAL_675MS.ListarPermisos_675MS();

            bool existePermiso_675MS = false;

            foreach (PermisoBE permiso_675MS in permisos_675MS)
            {
                if (permiso_675MS.Id_675MS == idPermiso_675MS)
                {
                    existePermiso_675MS = true;
                }

                if (permiso_675MS.Id_675MS != idPermiso_675MS &&
                    string.Equals(permiso_675MS.Nombre_675MS, nombre_675MS, StringComparison.OrdinalIgnoreCase))
                {
                    throw new Exception("Ya existe otro permiso con ese nombre.");
                }
            }

            if (!existePermiso_675MS)
            {
                throw new Exception("El permiso seleccionado no existe.");
            }

            return permisoDAL_675MS.ModificarPermiso_675MS(idPermiso_675MS, nombre_675MS);
        }

        public int BajaPermiso_675MS(int idPermiso_675MS)
        {
            if (idPermiso_675MS <= 0)
            {
                throw new Exception("Debe seleccionar un permiso válido.");
            }

            PermisoDAL permisoDAL_675MS = new PermisoDAL();

            List<PermisoBE> permisos_675MS = permisoDAL_675MS.ListarPermisos_675MS();

            bool existePermiso_675MS = false;

            foreach (PermisoBE permiso_675MS in permisos_675MS)
            {
                if (permiso_675MS.Id_675MS == idPermiso_675MS)
                {
                    existePermiso_675MS = true;
                    break;
                }
            }

            if (!existePermiso_675MS)
            {
                throw new Exception("El permiso seleccionado no existe.");
            }

            // Proteger los seis permisos principales del sistema
            if (idPermiso_675MS >= 1 && idPermiso_675MS <= 6)
            {
                throw new Exception(
                    "No se pueden eliminar los permisos principales del sistema.");
            }

            // Verificar que no esté asociado a ningún rol
            int cantidad_675MS =
                permisoDAL_675MS.VerificarPermisoEnUso_675MS(idPermiso_675MS);

            if (cantidad_675MS > 0)
            {
                throw new Exception(
                    "No se puede eliminar el permiso porque está asignado a uno o más roles.");
            }

            return permisoDAL_675MS.BajaPermiso_675MS(idPermiso_675MS);
        }
    }
}
