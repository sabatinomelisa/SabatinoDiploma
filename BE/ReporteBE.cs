using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{

    using System;

    namespace BE
    {
        public class ReporteBE
        {
            private string dominio_675MS;
            private DateTime fechaInspeccion_675MS;
            private string estado_675MS;
            private int turnosPosteriores_675MS;
            private int minutosReservados_675MS;
            private DateTime? fechaReinspeccion_675MS;
            private string situacionTurno_675MS;
            private string recomendacion_675MS;

            public string Dominio_675MS
            {
                get { return dominio_675MS; }
                set { dominio_675MS = value; }
            }

            public DateTime FechaInspeccion_675MS
            {
                get { return fechaInspeccion_675MS; }
                set { fechaInspeccion_675MS = value; }
            }

            public string Estado_675MS
            {
                get { return estado_675MS; }
                set { estado_675MS = value; }
            }

            public int TurnosPosteriores_675MS
            {
                get { return turnosPosteriores_675MS; }
                set { turnosPosteriores_675MS = value; }
            }

            public int MinutosReservados_675MS
            {
                get { return minutosReservados_675MS; }
                set { minutosReservados_675MS = value; }
            }

            public DateTime? FechaReinspeccion_675MS
            {
                get { return fechaReinspeccion_675MS; }
                set { fechaReinspeccion_675MS = value; }
            }

            public string SituacionTurno_675MS
            {
                get { return situacionTurno_675MS; }
                set { situacionTurno_675MS = value; }
            }

            public string Recomendacion_675MS
            {
                get { return recomendacion_675MS; }
                set { recomendacion_675MS = value; }
            }
        }
    }

}
