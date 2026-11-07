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
    public class VtvDAL
    {

        public int AprobarVtvDAL_675MS(int id_675MS)
        {
            Acceso acceso_675MS = new Acceso();
            acceso_675MS.Conectar_675MS();

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();

                parametros.Add(acceso_675MS.CrearParametro_675MS("@idVTV", id_675MS));

                int filasAfectadas_675MS = acceso_675MS.Escribir_675MS("APROBAR_VTV", parametros);

                return filasAfectadas_675MS;
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }
        }

        public int BorrarVtvDAL_675MS(int vtvEliminar_675MS)
        {
            Acceso acceso_675MS = new Acceso();
            acceso_675MS.Conectar_675MS();

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();

                parametros.Add(acceso_675MS.CrearParametro_675MS("@idVTV", vtvEliminar_675MS));

                int filasAfectadas_675MS = acceso_675MS.Escribir_675MS("BAJA_LOGICA_VTV", parametros);

                return filasAfectadas_675MS;
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }
        }

        public List<VtvBE> ListarVtvs_675MS()
        {
            List<VtvBE> vtvs_675MS = new List<VtvBE>();
            Acceso acceso_675MS = new Acceso();
            DataTable tablaVtvs_675MS = new DataTable();

            try
            {
                acceso_675MS.Conectar_675MS();

                tablaVtvs_675MS = acceso_675MS.Leer_675MS("OBTENER_VTVS");
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
            foreach (DataRow vtvBE_675MS in tablaVtvs_675MS.Rows)
            {
                VtvBE vtvAuxiliar_675MS = new VtvBE();

                vtvAuxiliar_675MS.Id_675MS = Convert.ToInt32(vtvBE_675MS["IdVTV"]);
                vtvAuxiliar_675MS.Dominio_675MS = vtvBE_675MS["Dominio"].ToString();
                vtvAuxiliar_675MS.FechaInspeccion_675MS = Convert.ToDateTime(vtvBE_675MS["FechaInspeccion"]);
                vtvAuxiliar_675MS.Observaciones_675MS = vtvBE_675MS["Observaciones"].ToString();
                vtvAuxiliar_675MS.RequiereInspeccion_675MS = Convert.ToInt32(vtvBE_675MS["RequiereReinspeccion"]);
                if (vtvBE_675MS["FechaReinspeccion"] != DBNull.Value)
                {
                    vtvAuxiliar_675MS.FechaReinspeccion_675MS = Convert.ToDateTime(vtvBE_675MS["FechaReinspeccion"]);
                }
                else
                {
                    vtvAuxiliar_675MS.FechaReinspeccion_675MS = DateTime.MinValue;
                }

                if (vtvBE_675MS["FechaVencimiento"] != DBNull.Value)
                {
                    vtvAuxiliar_675MS.FechaVencimiento_675MS = Convert.ToDateTime(vtvBE_675MS["FechaVencimiento"]);
                }
                else
                {
                    vtvAuxiliar_675MS.FechaVencimiento_675MS = DateTime.MinValue;
                }
                vtvAuxiliar_675MS.IdEmpleado_675MS = Convert.ToInt32(vtvBE_675MS["IdEmpleado"]);
                vtvAuxiliar_675MS.Activo_675MS = Convert.ToInt32(vtvBE_675MS["Activo"]);
                vtvAuxiliar_675MS.Estado_675MS = vtvBE_675MS["Estado"].ToString();
                vtvs_675MS.Add(vtvAuxiliar_675MS);
            }
            return vtvs_675MS;
        }

        public int ModificacionVtvDAL_675MS(VtvBE nuevaVTV_675MS)
        {
            Acceso acceso_675MS = new Acceso();
            acceso_675MS.Conectar_675MS();

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();

                parametros.Add(acceso_675MS.CrearParametro_675MS("@idVTV", nuevaVTV_675MS.Id_675MS));
                parametros.Add(acceso_675MS.CrearParametro_675MS("@dominio", nuevaVTV_675MS.Dominio_675MS));
                parametros.Add(acceso_675MS.CrearParametro_675MS("@fechaInspeccion", nuevaVTV_675MS.FechaInspeccion_675MS));
                parametros.Add(acceso_675MS.CrearParametro_675MS("@observaciones",nuevaVTV_675MS.Observaciones_675MS));
                parametros.Add(acceso_675MS.CrearParametro_675MS("@requiereReinspeccion", nuevaVTV_675MS.RequiereInspeccion_675MS));
                parametros.Add(acceso_675MS.CrearParametroDateSolo_675MS("@fechaReinspeccion", nuevaVTV_675MS.FechaReinspeccion_675MS));
                parametros.Add(acceso_675MS.CrearParametroDateSolo_675MS("@fechaVencimiento", nuevaVTV_675MS.FechaVencimiento_675MS));
                parametros.Add(acceso_675MS.CrearParametro_675MS("@idEmpleado", nuevaVTV_675MS.IdEmpleado_675MS));

                int filasAfectadas_675MS = acceso_675MS.Escribir_675MS("MODIFICAR_VTV", parametros);

                return filasAfectadas_675MS;
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }
        }

        public VtvBE ObtenerPorId_675MS(int idVtv_675MS)
        {
            Acceso acceso_675MS = new Acceso();
            acceso_675MS.Conectar_675MS();
            VtvBE vtvEncontrada_675MS = new VtvBE();

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();

                parametros.Add(acceso_675MS.CrearParametro_675MS("@idVTV", idVtv_675MS));

                DataTable vtvPorId_675MS = acceso_675MS.Leer_675MS("OBTENER_VTV_POR_ID", parametros);


                // Si la consulta devolvió al menos una fila, mapeamos ese único objeto
                if (vtvPorId_675MS.Rows.Count > 0)
                {
                    DataRow fila_675MS = vtvPorId_675MS.Rows[0];
                    vtvEncontrada_675MS.Id_675MS = Convert.ToInt32(fila_675MS["IdVTV"]);
                    vtvEncontrada_675MS.Dominio_675MS = fila_675MS["Dominio"].ToString();
                    vtvEncontrada_675MS.FechaInspeccion_675MS = Convert.ToDateTime(fila_675MS["FechaInspeccion"]);
                    vtvEncontrada_675MS.Observaciones_675MS = fila_675MS["Observaciones"].ToString();
                    vtvEncontrada_675MS.RequiereInspeccion_675MS = Convert.ToInt32(fila_675MS["RequiereReinspeccion"]);
                    // Si FechaReinspeccion tiene fecha la convierte, sino pone DateTime.MinValue
                    vtvEncontrada_675MS.FechaReinspeccion_675MS = fila_675MS["FechaReinspeccion"] != DBNull.Value
                            ? Convert.ToDateTime(fila_675MS["FechaReinspeccion"])
                            : DateTime.MinValue;

                    // Si FechaVencimiento tiene fecha la convierte, sino pone DateTime.MinValue
                    vtvEncontrada_675MS.FechaVencimiento_675MS = fila_675MS["FechaVencimiento"] != DBNull.Value
                        ? Convert.ToDateTime(fila_675MS["FechaVencimiento"])
                        : DateTime.MinValue;
                    vtvEncontrada_675MS.IdEmpleado_675MS = Convert.ToInt32(fila_675MS["IdEmpleado"]);
                    vtvEncontrada_675MS.Activo_675MS = Convert.ToInt32(fila_675MS["Activo"]);
                    vtvEncontrada_675MS.Estado_675MS = fila_675MS["Estado"] != DBNull.Value
                            ? fila_675MS["Estado"].ToString()
                            : string.Empty;
                }
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }

            return vtvEncontrada_675MS;

        }



        public int RegistrarVTVDAL_675MS(VtvBE nuevaVTV_675MS)
        {
            Acceso acceso_675MS = new Acceso();
            acceso_675MS.Conectar_675MS();

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();

                parametros.Add(acceso_675MS.CrearParametro_675MS("@Dominio", nuevaVTV_675MS.Dominio_675MS));
                parametros.Add(acceso_675MS.CrearParametro_675MS("@FechaInspeccion", nuevaVTV_675MS.FechaInspeccion_675MS));
                parametros.Add(acceso_675MS.CrearParametro_675MS("@Observaciones", nuevaVTV_675MS.Observaciones_675MS));
                parametros.Add(acceso_675MS.CrearParametro_675MS("@RequiereReinspeccion", nuevaVTV_675MS.RequiereInspeccion_675MS));
                parametros.Add(acceso_675MS.CrearParametroDateSolo_675MS("@FechaVencimiento", nuevaVTV_675MS.FechaVencimiento_675MS));

                parametros.Add(acceso_675MS.CrearParametroDateSolo_675MS("@FechaReinspeccion", nuevaVTV_675MS.FechaReinspeccion_675MS));

                parametros.Add(acceso_675MS.CrearParametro_675MS("@IdEmpleado", nuevaVTV_675MS.IdEmpleado_675MS));
                parametros.Add(acceso_675MS.CrearParametro_675MS("@Activo", nuevaVTV_675MS.Activo_675MS));
                parametros.Add(acceso_675MS.CrearParametro_675MS("@estado", "PENDIENTE DE APROBAR"));

                int filasAfectadas_675MS = acceso_675MS.Escribir_675MS("ALTA_VTV", parametros);

                return filasAfectadas_675MS;
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }
        } 
    }
}

