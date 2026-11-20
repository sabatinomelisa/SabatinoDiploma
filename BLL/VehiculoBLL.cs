using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class VehiculoBLL
    {
        VehiculoDAL vehiculoDAL_675MS;
        public List<string> ListarPatentes_675MS()
        {
            vehiculoDAL_675MS = new VehiculoDAL();

            List<string> patentes_675MS = new List<string>();

            List<VehiculoBE> vehiculos_675MS = vehiculoDAL_675MS.ListarVehiculos_675MS();

            foreach(VehiculoBE vehiculo_675MS in vehiculos_675MS )
            {
                patentes_675MS.Add(vehiculo_675MS.Dominio_675MS);
            }

            return patentes_675MS;
        }

        public List<VehiculoBE> ListarVehiculos_675MS()
        {
            //Instancio la DAL
            vehiculoDAL_675MS = new VehiculoDAL();

            List<VehiculoBE> vehiculos_675MS = vehiculoDAL_675MS.ListarVehiculos_675MS();

            return vehiculos_675MS;
        }

        public int ModificarVehiculo_675MS(VehiculoBE vehiculoModificado_675MS)
        {
            // Recalculo el DVH al modificar los datos del vehiculo
            Servicios.CalculadorDigitoVerificador calculadorDV_675MS = new Servicios.CalculadorDigitoVerificador();

            vehiculoModificado_675MS.DigitoVerificadorHorizontal_675MS = calculadorDV_675MS.CalcularHorizontal_675MS(
                        vehiculoModificado_675MS.Dominio_675MS,
                        vehiculoModificado_675MS.Dni_675MS.ToString(),
                        vehiculoModificado_675MS.Modelo_675MS,
                        vehiculoModificado_675MS.Version_675MS,
                        vehiculoModificado_675MS.Anio_675MS.ToString(),
                        vehiculoModificado_675MS.NroChasis_675MS,
                        vehiculoModificado_675MS.UltimaVTV_675MS.ToString("yyyy-MM-dd")
                    );

            vehiculoDAL_675MS = new VehiculoDAL();
            return vehiculoDAL_675MS.ModificarVehiculoDAL_675MS(vehiculoModificado_675MS);
        }


        public int RegistrarVehiculo_675MS(VehiculoBE nuevoVehiculo_675MS)
        {
            VehiculoDAL vehiculoDAL_675MS = new VehiculoDAL();

            // Validar que el dominio no esté vacío
            if (nuevoVehiculo_675MS == null || string.IsNullOrWhiteSpace(nuevoVehiculo_675MS.Dominio_675MS))
            {
                throw new Exception("Debe ingresar un dominio válido.");
            }

            // Validar si el dominio ya existe
            string dominio_675MS = nuevoVehiculo_675MS.Dominio_675MS.Trim().ToUpperInvariant();

            nuevoVehiculo_675MS.Dominio_675MS = dominio_675MS;

            // Buscar en los vehículos existentes
            var vehiculos_675MS = vehiculoDAL_675MS.ListarVehiculos_675MS();

            if (vehiculos_675MS.Any(v => string.Equals(v.Dominio_675MS, dominio_675MS, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException("Ya existe un vehículo registrado con ese dominio.");
            }

            // Calcular DVH
            Servicios.CalculadorDigitoVerificador calculadorDV_675MS = new Servicios.CalculadorDigitoVerificador();

            nuevoVehiculo_675MS.DigitoVerificadorHorizontal_675MS =
                calculadorDV_675MS.CalcularHorizontal_675MS(
                    nuevoVehiculo_675MS.Dominio_675MS ?? "",
                    nuevoVehiculo_675MS.Dni_675MS.ToString(),
                    nuevoVehiculo_675MS.Modelo_675MS ?? "",
                    nuevoVehiculo_675MS.Version_675MS ?? "",
                    nuevoVehiculo_675MS.Anio_675MS.ToString(),
                    nuevoVehiculo_675MS.NroChasis_675MS ?? "",
                    nuevoVehiculo_675MS.UltimaVTV_675MS.ToString("yyyyMMdd")
                );

            return vehiculoDAL_675MS.RegistrarVehiculoDAL_675MS(nuevoVehiculo_675MS);
        }
    }
}
