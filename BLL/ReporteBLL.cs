using BE;
using BE.BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BLL
{
    public class ReporteBLL
    {
        ReporteDAL reporteDAL_675MS = new ReporteDAL();

        public List<ReporteBE> ObtenerTurnosPosterioresVTV_675MS()
        {
            return reporteDAL_675MS.ObtenerTurnosPosterioresVTV_675MS();
        }

        public List<ReporteBE> ObtenerReinspeccionesPendientes_675MS()
        {
            return reporteDAL_675MS.ObtenerReinspeccionesPendientes_675MS();
        }

        public IndicadoresReporteBE ObtenerIndicadores_675MS()
        {
            return reporteDAL_675MS.ObtenerIndicadores_675MS();
        }
        public string GenerarConclusiones_675MS(List<ReporteBE> turnos_675MS, List<ReporteBE> reinspecciones_675MS)
        {
            if (turnos_675MS == null || reinspecciones_675MS == null) throw new ArgumentNullException("Los datos del reporte no pueden ser nulos.");

            StringBuilder conclusiones_675MS = new StringBuilder();

            conclusiones_675MS.AppendLine("REPORTE INTELIGENTE DE PITBOX");
            conclusiones_675MS.AppendLine("Análisis de turnos y revisiones técnicas");
            conclusiones_675MS.AppendLine();

            int sinTurno_675MS = 0;
            int conTurno_675MS = 0;

            foreach (ReporteBE reporte_675MS in reinspecciones_675MS)
            {
                if (reporte_675MS.SituacionTurno_675MS == "SIN TURNO FUTURO") sinTurno_675MS++;
                else if (reporte_675MS.SituacionTurno_675MS == "TIENE TURNO FUTURO") conTurno_675MS++;
            }

            conclusiones_675MS.AppendLine("REINSPECCIONES");
            conclusiones_675MS.AppendLine("Vehículos que requieren reinspección: " + reinspecciones_675MS.Count);
            conclusiones_675MS.AppendLine("Sin turno futuro: " + sinTurno_675MS);
            conclusiones_675MS.AppendLine("Con turno futuro: " + conTurno_675MS);
            conclusiones_675MS.AppendLine();

            if (sinTurno_675MS > 0)
            {
                conclusiones_675MS.AppendLine("RECOMENDACIÓN: Priorizar la asignación de turnos para los vehículos con reinspección pendiente.");
            }

            if (conTurno_675MS > 0)
            {
                conclusiones_675MS.AppendLine("RECOMENDACIÓN: Verificar si los turnos futuros corresponden a reinspecciones.");
            }

            if (reinspecciones_675MS.Count == 0)
            {
                conclusiones_675MS.AppendLine("No se detectaron vehículos cuya última VTV requiera reinspección.");
            }

            conclusiones_675MS.AppendLine();
            conclusiones_675MS.AppendLine("PLANIFICACIÓN DE TURNOS");
            conclusiones_675MS.AppendLine("Registros de VTV aprobadas con turnos posteriores: " + turnos_675MS.Count);

            int minutos_675MS = 0;

            foreach (ReporteBE reporte_675MS in turnos_675MS)
            {
                minutos_675MS += reporte_675MS.MinutosReservados_675MS;
            }

            conclusiones_675MS.AppendLine("Minutos reservados asociados: " + minutos_675MS);

            if (turnos_675MS.Count > 0)
            {
                conclusiones_675MS.AppendLine("RECOMENDACIÓN: Revisar el motivo de los turnos posteriores para evaluar la planificación del taller.");
            }

            return conclusiones_675MS.ToString();
        }
    }
}
