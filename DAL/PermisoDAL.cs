using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        public int AltaPermiso_675MS(string nombre_675MS)
        {
            Acceso acceso_675MS = new Acceso();
            acceso_675MS.Conectar_675MS();

            try
            {
                List<SqlParameter> parametros_675MS = new List<SqlParameter>();

                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@Nombre", nombre_675MS));

                return acceso_675MS.Escribir_675MS("ALTA_PERMISO", parametros_675MS);
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }
        }

        public int ModificarPermiso_675MS(int idPermiso_675MS, string nombre_675MS)
        {
            Acceso acceso_675MS = new Acceso();
            acceso_675MS.Conectar_675MS();

            try
            {
                List<SqlParameter> parametros_675MS = new List<SqlParameter>();

                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@IdPermiso", idPermiso_675MS));

                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@Nombre", nombre_675MS));

                return acceso_675MS.Escribir_675MS("MODIFICAR_PERMISO", parametros_675MS);
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }
        }

        public int BajaPermiso_675MS(int idPermiso_675MS)
        {
            Acceso acceso_675MS = new Acceso();
            acceso_675MS.Conectar_675MS();

            try
            {
                List<SqlParameter> parametros_675MS = new List<SqlParameter>();

                parametros_675MS.Add(
                    acceso_675MS.CrearParametro_675MS("@IdPermiso", idPermiso_675MS));

                return acceso_675MS.Escribir_675MS("BAJA_PERMISO", parametros_675MS);
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }
        }

        public int VerificarPermisoEnUso_675MS(int idPermiso_675MS)
        {
            Acceso acceso_675MS = new Acceso();
            acceso_675MS.Conectar_675MS();

            try
            {
                List<SqlParameter> parametros_675MS = new List<SqlParameter>();

                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@IdPermiso", idPermiso_675MS));

                DataTable tabla_675MS = acceso_675MS.Leer_675MS("VERIFICAR_PERMISO_EN_USO", parametros_675MS);

                return Convert.ToInt32(tabla_675MS.Rows[0]["Cantidad"]);
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }
        }
    }
}
