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
    public class EmpleadoDAL
    {
        public List<EmpleadoBE> ListarEmpleados_675MS()
        {
            Acceso acceso_675MS = new Acceso();
            List<EmpleadoBE> empleados_675MS = new List<EmpleadoBE>();

            acceso_675MS.Conectar_675MS();

            DataTable tabla_675MS = acceso_675MS.Leer_675MS("LISTAR_EMPLEADOS");

            foreach (DataRow row in tabla_675MS.Rows)
            {
                EmpleadoBE empleadoAuxiliar_675MS = new EmpleadoBE();
                empleadoAuxiliar_675MS.IdEmpleado_675MS = int.Parse(row["IdEmpleado"].ToString());
                empleadoAuxiliar_675MS.DniEmpleado_675MS = int.Parse(row["DNI"].ToString());
                empleadoAuxiliar_675MS.Nombre_675MS = row["Nombre"].ToString();
                empleadoAuxiliar_675MS.Apellido_675MS = row["Apellido"].ToString();
                empleadoAuxiliar_675MS.Mail_675MS = row["Mail"].ToString();
                empleadoAuxiliar_675MS.Domicilio_675MS = row["Domicilio"].ToString();
                empleadoAuxiliar_675MS.Telefono_675MS = int.Parse(row["Telefono"].ToString());
                empleadoAuxiliar_675MS.FechaNacimiento_675MS = Convert.ToDateTime(row["FechaNacimiento"]);
                empleadoAuxiliar_675MS.DigitoVerificadorHorizontal_675MS = Convert.ToInt32(row["DigitoVerificadorHorizontal"]);
                empleadoAuxiliar_675MS.Baja_675MS = Convert.ToInt32(row["BAJA"]);

                empleados_675MS.Add(empleadoAuxiliar_675MS);
            }

            acceso_675MS.Desconectar_675MS();
            return empleados_675MS;
        }

        public int ModificarEmpleadoDAL_675MS(EmpleadoBE empleadoModificado_675MS)
        {
            Acceso acceso_675MS = new Acceso();
            acceso_675MS.Conectar_675MS();

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();

                parametros.Add(acceso_675MS.CrearParametro_675MS("@IdEmpleado", empleadoModificado_675MS.IdEmpleado_675MS));
                parametros.Add(acceso_675MS.CrearParametro_675MS("@dni", empleadoModificado_675MS.DniEmpleado_675MS));
                parametros.Add(acceso_675MS.CrearParametro_675MS("@nombre", empleadoModificado_675MS.Nombre_675MS));
                parametros.Add(acceso_675MS.CrearParametro_675MS("@apellido", empleadoModificado_675MS.Apellido_675MS));
                parametros.Add(acceso_675MS.CrearParametro_675MS("@fechanacimiento", empleadoModificado_675MS.FechaNacimiento_675MS));
                parametros.Add(acceso_675MS.CrearParametro_675MS("@mail", empleadoModificado_675MS.Mail_675MS));
                parametros.Add(acceso_675MS.CrearParametro_675MS("@domicilio", empleadoModificado_675MS.Domicilio_675MS));
                parametros.Add(acceso_675MS.CrearParametro_675MS("@telefono", empleadoModificado_675MS.Telefono_675MS));
                parametros.Add(acceso_675MS.CrearParametro_675MS("@DigitoVerificadorHorizontal", empleadoModificado_675MS.DigitoVerificadorHorizontal_675MS));

                int filasAfectadas_675MS = acceso_675MS.Escribir_675MS("MODIFICAR_EMPLEADO", parametros);

                return filasAfectadas_675MS;
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }
        }
        

        public int RegistrarEmpleadoDAL_675MS(EmpleadoBE empleadoBE_675MS)
        {
            Acceso acceso_675MS = new Acceso();
            acceso_675MS.Conectar_675MS();

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();

                parametros.Add(acceso_675MS.CrearParametro_675MS("@dni", empleadoBE_675MS.DniEmpleado_675MS));
                parametros.Add(acceso_675MS.CrearParametro_675MS("@nombre", empleadoBE_675MS.Nombre_675MS));
                parametros.Add(acceso_675MS.CrearParametro_675MS("@apellido", empleadoBE_675MS.Apellido_675MS));
                parametros.Add(acceso_675MS.CrearParametroDateSolo_675MS("@fechanacimiento", empleadoBE_675MS.FechaNacimiento_675MS));
                parametros.Add(acceso_675MS.CrearParametro_675MS("@mail", empleadoBE_675MS.Mail_675MS));
                parametros.Add(acceso_675MS.CrearParametro_675MS("@domicilio", empleadoBE_675MS.Domicilio_675MS));
                parametros.Add(acceso_675MS.CrearParametro_675MS("@telefono", empleadoBE_675MS.Telefono_675MS));
                parametros.Add(acceso_675MS.CrearParametro_675MS("@DigitoVerificadorHorizontal", empleadoBE_675MS.DigitoVerificadorHorizontal_675MS));

                int filasAfectadas_675MS = acceso_675MS.Escribir_675MS("ALTA_EMPLEADO", parametros);

                return filasAfectadas_675MS;
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }
        }

        public EmpleadoBE ObtenerEmpleadoPorId_675MS(int idEmpleado_675MS)
        {
            Acceso acceso_675MS = new Acceso();
            EmpleadoBE empleado_675MS = null;

            acceso_675MS.Conectar_675MS();

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(acceso_675MS.CrearParametro_675MS("@IdEmpleado", idEmpleado_675MS));

                DataTable tabla_675MS = acceso_675MS.Leer_675MS("OBTENER_EMPLEADO_POR_ID", parametros);

                if (tabla_675MS.Rows.Count > 0)
                {
                    DataRow row = tabla_675MS.Rows[0];
                    empleado_675MS = new EmpleadoBE();

                    empleado_675MS.IdEmpleado_675MS = int.Parse(row["IdEmpleado"].ToString());
                    empleado_675MS.DniEmpleado_675MS = int.Parse(row["DNI"].ToString());
                    empleado_675MS.Nombre_675MS = row["Nombre"].ToString();
                    empleado_675MS.Apellido_675MS = row["Apellido"].ToString();
                    empleado_675MS.Mail_675MS = row["Mail"].ToString();
                    empleado_675MS.Domicilio_675MS = row["Domicilio"].ToString();
                    empleado_675MS.Telefono_675MS = int.Parse(row["Telefono"].ToString());
                    empleado_675MS.FechaNacimiento_675MS = Convert.ToDateTime(row["FechaNacimiento"]);
                    empleado_675MS.DigitoVerificadorHorizontal_675MS = Convert.ToInt32(row["DigitoVerificadorHorizontal"]);
                    empleado_675MS.Baja_675MS = Convert.ToInt32(row["BAJA"]);
                }

                return empleado_675MS;
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }
        }

        public int BajaEmpleadoDAL_675MS(EmpleadoBE empleado_675MS)
        {
            Acceso acceso_675MS = new Acceso();
            acceso_675MS.Conectar_675MS();

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();

                parametros.Add(acceso_675MS.CrearParametro_675MS("@IdEmpleado", empleado_675MS.IdEmpleado_675MS));
                parametros.Add(acceso_675MS.CrearParametro_675MS("@DigitoVerificadorHorizontal", empleado_675MS.DigitoVerificadorHorizontal_675MS));

                int filasAfectadas_675MS = acceso_675MS.Escribir_675MS("BAJA_LOGICA_EMPLEADO", parametros);

                return filasAfectadas_675MS;
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }
        }

        public int ReactivarEmpleadoDAL_675MS(EmpleadoBE empleado_675MS)
        {
            Acceso acceso_675MS = new Acceso();
            acceso_675MS.Conectar_675MS();

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();

                parametros.Add(acceso_675MS.CrearParametro_675MS("@IdEmpleado", empleado_675MS.IdEmpleado_675MS));
                parametros.Add(acceso_675MS.CrearParametro_675MS("@DigitoVerificadorHorizontal", empleado_675MS.DigitoVerificadorHorizontal_675MS));

                int filasAfectadas_675MS = acceso_675MS.Escribir_675MS("ALTA_LOGICA_EMPLEADO", parametros);

                return filasAfectadas_675MS;
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }
        }
    }
}
