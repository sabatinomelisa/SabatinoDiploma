
using BE;
using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Servicios
{
    public class CrearPDF
    {
        public static void CrearPdfVtv(VtvBE vtv_675MS, string rutaDestino_675MS, string empleadoAprobador_675MS)
        {
            // Valido que haya recibido los datos de la VTV
            if (vtv_675MS == null)
                throw new ArgumentNullException(nameof(vtv_675MS));

            // Creo el archivo PDF en la ruta indicada
            using (FileStream archivo_675MS = new FileStream(rutaDestino_675MS, FileMode.Create))
            {
                // Configuro el documento en tamaño A4
                Document documento_675MS = new Document(PageSize.A4, 45, 45, 50, 50);
                PdfWriter.GetInstance(documento_675MS, archivo_675MS);
                documento_675MS.Open();

                // Defino los tipos de letra que voy a utilizar
                Font titulo_675MS = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20, BaseColor.BLACK);
                Font subtitulo_675MS = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 13);
                Font normal_675MS = FontFactory.GetFont(FontFactory.HELVETICA, 11);

                // Agrego el nombre del sistema
                Paragraph encabezado_675MS = new Paragraph("PITBOX", titulo_675MS);
                encabezado_675MS.Alignment = Element.ALIGN_CENTER;
                documento_675MS.Add(encabezado_675MS);

                // Agrego el titulo del comprobante
                Paragraph subtituloPdf_675MS = new Paragraph("COMPROBANTE DE INSPECCION VTV", subtitulo_675MS);
                subtituloPdf_675MS.Alignment = Element.ALIGN_CENTER;
                subtituloPdf_675MS.SpacingAfter = 25;
                documento_675MS.Add(subtituloPdf_675MS);

                // Creo una tabla de dos columnas para mostrar los datos
                PdfPTable tabla_675MS = new PdfPTable(2);
                tabla_675MS.WidthPercentage = 100;
                tabla_675MS.SetWidths(new float[] { 1, 2 });

                // Valido si la VTV tiene fecha de vencimiento
                string vencimiento_675MS = vtv_675MS.FechaVencimiento_675MS == DateTime.MinValue ? "No aplica" : vtv_675MS.FechaVencimiento_675MS.ToString("dd/MM/yyyy");

                // Preparo los datos que voy a mostrar en el comprobante
                string[] datos_675MS =
                {
                    "Numero de comprobante", vtv_675MS.Id_675MS.ToString(),
                    "Dominio", vtv_675MS.Dominio_675MS,
                    "Fecha de inspeccion", vtv_675MS.FechaInspeccion_675MS.ToString("dd/MM/yyyy"),
                    "Fecha de vencimiento", vencimiento_675MS,
                    "Estado", vtv_675MS.Estado_675MS ?? "",
                    "Requiere reinspeccion", vtv_675MS.RequiereInspeccion_675MS == 1 ? "SI" : "NO",
                    "Empleado", empleadoAprobador_675MS ?? ""
                };

                // Recorro los datos y los agrego a la tabla
                for (int i_675MS = 0; i_675MS < datos_675MS.Length; i_675MS++)
                {
                    PdfPCell celda_675MS = new PdfPCell(new Phrase(datos_675MS[i_675MS], normal_675MS));
                    celda_675MS.Padding = 9;
                    tabla_675MS.AddCell(celda_675MS);
                }

                // Agrego la tabla al documento
                documento_675MS.Add(tabla_675MS);

                // Agrego las observaciones de la inspeccion
                Paragraph observaciones_675MS = new Paragraph("\nOBSERVACIONES\n" + (vtv_675MS.Observaciones_675MS ?? "Sin observaciones"), normal_675MS);
                observaciones_675MS.SpacingBefore = 15;
                documento_675MS.Add(observaciones_675MS);

                // Agrego un texto al final del comprobante
                Paragraph pie_675MS = new Paragraph("\n\nDocumento generado por el sistema PitBox.", normal_675MS);
                pie_675MS.Alignment = Element.ALIGN_CENTER;
                documento_675MS.Add(pie_675MS);

                // Cierro el documento para terminar de generar el PDF
                documento_675MS.Close();
            }
        }
    }
}
