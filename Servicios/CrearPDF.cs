using BE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public  class CrearPDF
    {
        public static void CrearPdfVtv(VtvBE vtv_675MS, string rutaDestino_675MS, string empleadoAprobador_675MS)
        {
            if (vtv_675MS == null)
            {
                throw new ArgumentNullException(nameof(vtv_675MS), "Los datos de la VTV no pueden ser nulos.");
            }

            string contenidoComprobante_675MS =
                "========================================\r\n" +
                "       PITBOX - SISTEMA DE VTV          \r\n" +
                "       Comprobante de Inspección        \r\n" +
                "========================================\r\n\r\n" +
                $"Nro. Certificado / ID: {vtv_675MS.Id_675MS}\r\n" +
                $"Dominio (Patente):     {vtv_675MS.Dominio_675MS}\r\n" +
                $"Fecha de Inspección:   {vtv_675MS.FechaInspeccion_675MS:dd/MM/yyyy}\r\n" +
                $"Fecha de Vencimiento:  {(vtv_675MS.FechaVencimiento_675MS == DateTime.MinValue ? "No aplica (Requiere Reinspección)" : vtv_675MS.FechaVencimiento_675MS.ToString("dd/MM/yyyy"))}\r\n" +
                $"Requiere Reinspección: {(vtv_675MS.RequiereInspeccion_675MS == 1 ? "SÍ" : "NO")}\r\n" +
                $"Empleado Aprobador:    {empleadoAprobador_675MS}\r\n\r\n" +
                "Observaciones:\r\n" +
                $"{vtv_675MS.Observaciones_675MS}\r\n\r\n" +
                "========================================\r\n" +
                "  Este documento es un comprobante      \r\n" +
                "  generado por el sistema PitBox.       \r\n" +
                "========================================";

            File.WriteAllText(rutaDestino_675MS, contenidoComprobante_675MS);
        }
    }
    
}
