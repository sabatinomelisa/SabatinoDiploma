using System;
using BE;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.BE;

namespace DAL
{
    public class ReporteDAL
    {
        public List<ReporteBE> ObtenerTurnosPosterioresVTV_675MS()
        {
            Acceso acceso_675MS = new Acceso();
            List<ReporteBE> reportes_675MS = new List<ReporteBE>();

            try
            {
                acceso_675MS.Conectar_675MS();

                DataTable tabla_675MS = acceso_675MS.Leer_675MS("SP_REPORTE_TURNOS_POSTERIORES_VTV");

                foreach (DataRow fila_675MS in tabla_675MS.Rows)
                {
                    ReporteBE reporte_675MS = new ReporteBE();

                    reporte_675MS.Dominio_675MS = fila_675MS["Dominio"].ToString();
                    reporte_675MS.FechaInspeccion_675MS = Convert.ToDateTime(fila_675MS["FechaInspeccion"]);
                    reporte_675MS.Estado_675MS = fila_675MS["Estado"].ToString();
                    reporte_675MS.TurnosPosteriores_675MS = Convert.ToInt32(fila_675MS["TurnosPosteriores"]);
                    reporte_675MS.MinutosReservados_675MS = fila_675MS["MinutosReservados"] == DBNull.Value ? 0 : Convert.ToInt32(fila_675MS["MinutosReservados"]);

                    reportes_675MS.Add(reporte_675MS);
                }

                return reportes_675MS;
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }
        }

        public List<ReporteBE> ObtenerReinspeccionesPendientes_675MS()
        {
            Acceso acceso_675MS = new Acceso();
            List<ReporteBE> reportes_675MS = new List<ReporteBE>();

            try
            {
                acceso_675MS.Conectar_675MS();

                DataTable tabla_675MS = acceso_675MS.Leer_675MS("SP_REPORTE_REINSPECCIONES_PENDIENTES");

                foreach (DataRow fila_675MS in tabla_675MS.Rows)
                {
                    ReporteBE reporte_675MS = new ReporteBE();

                    reporte_675MS.Dominio_675MS = fila_675MS["Dominio"].ToString();
                    reporte_675MS.FechaInspeccion_675MS = Convert.ToDateTime(fila_675MS["FechaInspeccion"]);
                    reporte_675MS.Estado_675MS = fila_675MS["Estado"].ToString();
                    reporte_675MS.FechaReinspeccion_675MS = fila_675MS["FechaReinspeccion"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(fila_675MS["FechaReinspeccion"]);
                    reporte_675MS.SituacionTurno_675MS = fila_675MS["SituacionTurno"].ToString();
                    reporte_675MS.Recomendacion_675MS = fila_675MS["Recomendacion"].ToString();

                    reportes_675MS.Add(reporte_675MS);
                }

                return reportes_675MS;
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }

        }

        public IndicadoresReporteBE ObtenerIndicadores_675MS()
        {
            Acceso acceso_675MS = new Acceso();

            try
            {
                acceso_675MS.Conectar_675MS();

                System.Data.DataTable tabla_675MS = acceso_675MS.Leer_675MS("SP_REPORTE_INDICADORES");

                IndicadoresReporteBE indicadores_675MS = new BE.IndicadoresReporteBE();

                if (tabla_675MS.Rows.Count > 0)
                {
                    indicadores_675MS.TurnosActivos_675MS = System.Convert.ToInt32(tabla_675MS.Rows[0]["TurnosActivos"]);
                    indicadores_675MS.VTVAprobadas_675MS = System.Convert.ToInt32(tabla_675MS.Rows[0]["VTVAprobadas"]);
                }

                return indicadores_675MS;
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }
        }

    }
}
