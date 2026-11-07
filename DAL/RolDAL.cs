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
    public class RolDAL
    {
        public List<RolBE> ListarRoles_675MS()
        {
            List<RolBE> roles_675MS = new List<RolBE>();
            Acceso acceso_675MS = new Acceso();
            DataTable tablaRoles_675MS = new DataTable();

            try
            {
                acceso_675MS.Conectar_675MS();

                tablaRoles_675MS = acceso_675MS.Leer_675MS("LISTAR_ROLES");
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
            foreach (DataRow rolBE_675MS in tablaRoles_675MS.Rows)
            {
                RolBE rolAuxiliar_675MS = new RolBE();
                rolAuxiliar_675MS.Nombre_675MS = rolBE_675MS["Nombre"].ToString();
                rolAuxiliar_675MS.Id_675MS = Convert.ToInt32(rolBE_675MS["IdRol"]);
                roles_675MS.Add(rolAuxiliar_675MS);
            }
            return roles_675MS;
        }

        public RolBE ObtenerComponentesRol(int idRol_675MS)
        {
            Acceso acceso_675MS = new Acceso();

            try
            {
                acceso_675MS.Conectar_675MS();

                List<SqlParameter> parametros_675MS = new List<SqlParameter>();

                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@IdRol", idRol_675MS));

                DataTable tabla_675MS = acceso_675MS.Leer_675MS("OBTENER_COMPONENTES_ROL",parametros_675MS);

                if (tabla_675MS.Rows.Count == 0)
                    return null;

                // Diccionario para no crear dos veces el mismo rol
                Dictionary<int, RolBE> roles_675MS = new Dictionary<int, RolBE>();

                // Crear el rol principal.
                RolBE rolPrincipal_675MS = new RolBE();

                rolPrincipal_675MS.Id_675MS = idRol_675MS;
                rolPrincipal_675MS.Nombre_675MS = tabla_675MS.Rows[0]["NombreRolPadre"].ToString();

                roles_675MS.Add(idRol_675MS, rolPrincipal_675MS);

                // Crear los roles que aparecen en la consulta.
                foreach (DataRow fila_675MS in tabla_675MS.Rows)
                {
                    if (fila_675MS["IdRolHijo"] != DBNull.Value)
                    {
                        int idHijo_675MS = Convert.ToInt32(fila_675MS["IdRolHijo"]);

                        if (!roles_675MS.ContainsKey(idHijo_675MS))
                        {
                            RolBE hijo_675MS = new RolBE();

                            hijo_675MS.Id_675MS = idHijo_675MS;
                            hijo_675MS.Nombre_675MS = fila_675MS["NombreRolHijo"].ToString();

                            roles_675MS.Add(idHijo_675MS, hijo_675MS);
                        }
                    }
                }

                // Construir las relaciones y agregar permisos.
                foreach (DataRow fila_675MS in tabla_675MS.Rows)
                {
                    int idPadre_675MS = Convert.ToInt32(fila_675MS["IdRolPadre"]);

                    RolBE padre_675MS = roles_675MS[idPadre_675MS];

                    if (fila_675MS["IdRolHijo"] != DBNull.Value)
                    {
                        int idHijo_675MS =  Convert.ToInt32(fila_675MS["IdRolHijo"]);

                        RolBE hijo_675MS =  roles_675MS[idHijo_675MS];

                        // Evitar agregar dos veces el mismo hijo.
                        if (!padre_675MS.Hijos_675MS.Contains(hijo_675MS))
                        {
                            padre_675MS.Agregar_675MS(hijo_675MS);
                        }
                    }
                    else if (fila_675MS["IdPermiso"] != DBNull.Value)
                    {
                        PermisoBE permiso_675MS = new PermisoBE();

                        permiso_675MS.Id_675MS =   Convert.ToInt32(fila_675MS["IdPermiso"]);

                        permiso_675MS.Nombre_675MS = fila_675MS["NombrePermiso"].ToString();

                        padre_675MS.Agregar_675MS(permiso_675MS);
                    }
                }

                return rolPrincipal_675MS;
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }
        }
    }
}
