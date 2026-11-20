using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class IndicadoresReporteBE
    {
        private int turnosActivos_675MS;
        private int vtvAprobadas_675MS;

        public int TurnosActivos_675MS
        {
            get { return turnosActivos_675MS; }
            set { turnosActivos_675MS = value; }
        }

        public int VTVAprobadas_675MS
        {
            get { return vtvAprobadas_675MS; }
            set { vtvAprobadas_675MS = value; }
        }
    }
}
