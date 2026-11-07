using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PermisoDAL
    {
        public List<PermisoBE> ListarPermisos_675MS()
        {
            List<PermisoBE>  permisos_675MS = new List<PermisoBE>();
            Acceso acceso_675MS = new Acceso();
            DataTable tablaPermisos_675MS = new DataTable();

            try
            {
                acceso_675MS.Conectar_675MS();

                tablaPermisos_675MS = acceso_675MS.Leer_675MS("LISTAR_PERMISOS");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }

            //Recorro la tabla y genero la lista para retornar a la capa de negocio
            foreach (DataRow permisoBE_675MS in tablaPermisos_675MS.Rows)
            {
                PermisoBE permisoAuxiliar_675MS = new PermisoBE();
                permisoAuxiliar_675MS.Nombre_675MS = permisoBE_675MS["Nombre"].ToString();
                permisoAuxiliar_675MS.Id_675MS = Convert.ToInt32(permisoBE_675MS["IdPermiso"]);
                permisos_675MS.Add(permisoAuxiliar_675MS);
            }
            return permisos_675MS;
        }
    }
}
