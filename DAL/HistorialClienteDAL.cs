
using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class HistorialClienteDAL
    {
        public List<HistorialClienteBE> ListarHistorial_675MS()
        {
            Acceso acceso_675MS = new Acceso();

            try
            {
                acceso_675MS.Conectar_675MS();

                DataTable tabla_675MS = acceso_675MS.Leer_675MS("LISTAR_HISTORIAL_CLIENTE");

                return ConvertirHistorial_675MS(tabla_675MS);
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }
        }

        public List<HistorialClienteBE> ObtenerHistorialPorDNI_675MS(int dni_675MS)
        {
            Acceso acceso_675MS = new Acceso();

            try
            {
                acceso_675MS.Conectar_675MS();

                List<SqlParameter> parametros_675MS =  new List<SqlParameter>();

                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@DNI", dni_675MS));

                DataTable tabla_675MS = acceso_675MS.Leer_675MS("OBTENER_HISTORIAL_CLIENTE_POR_DNI", parametros_675MS);

                return ConvertirHistorial_675MS(tabla_675MS);
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }
        }

        private List<HistorialClienteBE> ConvertirHistorial_675MS(DataTable tabla_675MS)
        {
            List<HistorialClienteBE> historial_675MS = new List<HistorialClienteBE>();

            foreach (DataRow fila_675MS in tabla_675MS.Rows)
            {
                HistorialClienteBE registro_675MS =  new HistorialClienteBE();

                registro_675MS.IdHistorial_675MS = Convert.ToInt32(fila_675MS["IdCambio"]);
                registro_675MS.DNI_675MS = Convert.ToInt32(fila_675MS["DNI"]);
                registro_675MS.Nombre_675MS =  fila_675MS["Nombre"].ToString();
                registro_675MS.Apellido_675MS = fila_675MS["Apellido"].ToString();
                registro_675MS.FechaNacimiento_675MS = fila_675MS["FechaNacimiento"] == DBNull.Value ? DateTime.MinValue: Convert.ToDateTime(fila_675MS["FechaNacimiento"]);
                registro_675MS.Mail_675MS =  fila_675MS["Mail"].ToString();
                registro_675MS.Domicilio_675MS = fila_675MS["Domicilio"].ToString();
                registro_675MS.Telefono_675MS = fila_675MS["Telefono"] == DBNull.Value ? (int?)null : Convert.ToInt32(fila_675MS["Telefono"]);
                registro_675MS.DigitoVerificadorHorizontal_675MS = fila_675MS["DigitoVerificadorHorizontal"] == DBNull.Value ? (int?)null: Convert.ToInt32( fila_675MS["DigitoVerificadorHorizontal"]);
                registro_675MS.FechaCambio_675MS =  Convert.ToDateTime(fila_675MS["FechaCambio"]);
                registro_675MS.Operacion_675MS =fila_675MS["Operacion"].ToString();
                registro_675MS.Act_675MS =   Convert.ToBoolean(fila_675MS["Act"]);
                registro_675MS.Activo_675MS = fila_675MS["Activo"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(fila_675MS["Activo"]);

                historial_675MS.Add(registro_675MS);
            }

            return historial_675MS;
        }
    }
}
