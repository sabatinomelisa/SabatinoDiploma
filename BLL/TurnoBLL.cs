using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class TurnoBLL
    {
        TurnoDAL turnoDAL_675MS;
        public List<TurnoBE> ListarTurnosturnos_675MS()
        {
            turnoDAL_675MS = new TurnoDAL();
            return turnoDAL_675MS.ListarTurnosturnos_675MS();
        }
    }
}
