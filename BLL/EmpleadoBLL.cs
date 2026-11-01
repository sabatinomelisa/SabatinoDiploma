using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class EmpleadoBLL
    {
        EmpleadoDAL empleadoDAL675MS; 
        EmpleadoBE empleadoBE_675MS = new EmpleadoBE();

        public int BajaEmpleado_675MS(int idEmpleadoBaja_675MS)
        {
            empleadoDAL675MS = new EmpleadoDAL();

            EmpleadoBE empleado_675MS = empleadoDAL675MS.ObtenerEmpleadoPorId_675MS(idEmpleadoBaja_675MS);

            if (empleado_675MS == null)
            {
                return -1; 
            }

            // Cambio el campo BAJA  a 1 --> EMPLEADO INACTIVO
            empleado_675MS.Baja_675MS = 1;

            Servicios.CalculadorDigitoVerificador calculadorDV_675MS = new Servicios.CalculadorDigitoVerificador();

            empleado_675MS.DigitoVerificadorHorizontal_675MS = calculadorDV_675MS.CalcularHorizontal_675MS(
                empleado_675MS.DniEmpleado_675MS.ToString(),
                empleado_675MS.Nombre_675MS,
                empleado_675MS.Apellido_675MS,
                empleado_675MS.FechaNacimiento_675MS.ToString("yyyy-MM-dd"),
                empleado_675MS.Mail_675MS,
                empleado_675MS.Domicilio_675MS,
                empleado_675MS.Telefono_675MS.ToString(),
                empleado_675MS.Baja_675MS.ToString()
            );

            return empleadoDAL675MS.BajaEmpleadoDAL_675MS(empleado_675MS);
        }

        public List<EmpleadoBE> ListarEmpleados_675MS()
        {
            //Instancio la DAL
            empleadoDAL675MS = new EmpleadoDAL();

            List<EmpleadoBE> empleados_675MS = empleadoDAL675MS.ListarEmpleados_675MS();

            return empleados_675MS;
        }

        public int ModificarEmpleado_675MS(EmpleadoBE empleadoModificado_675MS)
        {
            // Recalculo el DVH al modificar los datos del cliente
            Servicios.CalculadorDigitoVerificador calculadorDV_675MS = new Servicios.CalculadorDigitoVerificador();

            empleadoModificado_675MS.DigitoVerificadorHorizontal_675MS = calculadorDV_675MS.CalcularHorizontal_675MS(
                    empleadoModificado_675MS.DniEmpleado_675MS.ToString(),
                    empleadoModificado_675MS.Nombre_675MS,
                    empleadoModificado_675MS.Apellido_675MS,
                    empleadoModificado_675MS.FechaNacimiento_675MS.ToString("yyyy-MM-dd"),
                    empleadoModificado_675MS.Mail_675MS,
                    empleadoModificado_675MS.Domicilio_675MS,
                    empleadoModificado_675MS.Telefono_675MS.ToString(),
                    empleadoModificado_675MS.Baja_675MS.ToString()

                );
            empleadoDAL675MS = new EmpleadoDAL();
            return empleadoDAL675MS.ModificarEmpleadoDAL_675MS(empleadoModificado_675MS);
        }

        public int ReactivarEmpleado_675MS(int idEmpleadoBaja_675MS)
        {
            empleadoDAL675MS = new EmpleadoDAL();

            EmpleadoBE empleado_675MS = empleadoDAL675MS.ObtenerEmpleadoPorId_675MS(idEmpleadoBaja_675MS);

            if (empleado_675MS == null)
            {
                return -1;
            }

            // Cambio el campo BAJA  a 0 --> EMPLEADO ACTIVO
            empleado_675MS.Baja_675MS = 1;

            Servicios.CalculadorDigitoVerificador calculadorDV_675MS = new Servicios.CalculadorDigitoVerificador();

            empleado_675MS.DigitoVerificadorHorizontal_675MS = calculadorDV_675MS.CalcularHorizontal_675MS(
                empleado_675MS.DniEmpleado_675MS.ToString(),
                empleado_675MS.Nombre_675MS,
                empleado_675MS.Apellido_675MS,
                empleado_675MS.FechaNacimiento_675MS.ToString("yyyy-MM-dd"),
                empleado_675MS.Mail_675MS,
                empleado_675MS.Domicilio_675MS,
                empleado_675MS.Telefono_675MS.ToString(),
                empleado_675MS.Baja_675MS.ToString()
            );

            return empleadoDAL675MS.ReactivarEmpleadoDAL_675MS(empleado_675MS);
        }

        public int RegistrarEmpleado_675MS(EmpleadoBE nuevoEmpleado_675MS)
        {
            int filasAfectadas_675MS = 0;

            // Instancio servicio para calcular dígito verificador horizontal (DVH)
            Servicios.CalculadorDigitoVerificador calculadorDV_675MS = new Servicios.CalculadorDigitoVerificador();

            // Calculo y asigno el dígito verificador horizontal al cliente
            empleadoBE_675MS.DigitoVerificadorHorizontal_675MS = calculadorDV_675MS.CalcularHorizontal_675MS(
                    empleadoBE_675MS.DniEmpleado_675MS.ToString(),
                    empleadoBE_675MS.Nombre_675MS,
                    empleadoBE_675MS.Apellido_675MS,
                    empleadoBE_675MS.FechaNacimiento_675MS.ToString("yyyy-MM-dd"),
                    empleadoBE_675MS.Mail_675MS,
                    empleadoBE_675MS.Domicilio_675MS,
                    empleadoBE_675MS.Telefono_675MS.ToString(),
                    empleadoBE_675MS.Baja_675MS.ToString()
                );

            // Valido que haya una sesión iniciada en el sistema
            if (Servicios.SessionManager.SesionIniciada_675MS)
            {
               EmpleadoDAL empleadoDAL_675MS = new EmpleadoDAL();
                filasAfectadas_675MS = empleadoDAL675MS.RegistrarEmpleadoDAL_675MS(nuevoEmpleado_675MS);
            }
            else
            {
                throw new Exception("No hay ningún empleado logueado en el sistema.");
            }

            return filasAfectadas_675MS;

        }
    }
}
