using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class TraduccionBLL
    {
        TraduccionDAL traduccionDAL_675MS;

        public int AltaTraduccion_675MS(TraduccionBE nuevaTraduccion_675MS)
        {
            traduccionDAL_675MS = new TraduccionDAL();
            return traduccionDAL_675MS.AltaTraduccionDAL_675MS(nuevaTraduccion_675MS);
        }

        public int BorrarTraduccion_675MS(int traduccionEliminar_675MS)
        {
            traduccionDAL_675MS = new TraduccionDAL();
            return traduccionDAL_675MS.BorrarTraduccionDAL_675MS(traduccionEliminar_675MS);
        }

        public List<TraduccionBE> Listar_675MS(int idiomaSel)
        {
            traduccionDAL_675MS = new TraduccionDAL();

            List<TraduccionBE> traducciones_675MS = new List<TraduccionBE>();

            traducciones_675MS = traduccionDAL_675MS.ListarTraducciones_675MS(idiomaSel);

            return traducciones_675MS;
        }

        public int ModificarTraduccion_675MS(TraduccionBE traduccionModificada_675MS)
        {
            traduccionDAL_675MS = new TraduccionDAL();
            return traduccionDAL_675MS.ModificarTraduccionDAL_675MS(traduccionModificada_675MS);
        }
    }
}
