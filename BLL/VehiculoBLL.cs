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
        public List<string> ListarPatentes()
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

        public int RegistrarVehiculo_675MS(VehiculoBE nuevoVehiculo_675MS)
        {
            //Instancio Calculadora de Digito Verificador
            Servicios.CalculadorDigitoVerificador calculadorDV_675MS = new Servicios.CalculadorDigitoVerificador();

            // Preparo los datos del vehículo pasándolos a texto
            string dominioStr_675MS = nuevoVehiculo_675MS.Dominio_675MS ?? "";
            string dniStr_675MS = nuevoVehiculo_675MS.Dni_675MS.ToString();
            string modeloStr_675MS = nuevoVehiculo_675MS.Modelo_675MS ?? "";
            string versionStr_675MS = nuevoVehiculo_675MS.Version_675MS ?? "";
            string anioStr_675MS = nuevoVehiculo_675MS.Anio_675MS.ToString();
            string chasisStr_675MS = nuevoVehiculo_675MS.NroChasis_675MS ?? "";
            string vtvStr_675MS = nuevoVehiculo_675MS.UltimaVTV_675MS.ToString("yyyyMMdd");

            // Calculamos el DVH
            nuevoVehiculo_675MS.DigitoVerificadorHorizontal_675MS = calculadorDV_675MS.CalcularHorizontal_675MS(
                dominioStr_675MS,
                dniStr_675MS,
                modeloStr_675MS,
                versionStr_675MS,
                anioStr_675MS,
                chasisStr_675MS,
                vtvStr_675MS
            );

            VehiculoDAL vehiculoDAL_675MS = new VehiculoDAL();
            return vehiculoDAL_675MS.RegistrarVehiculoDAL_675MS(nuevoVehiculo_675MS);
        }
    }
}
