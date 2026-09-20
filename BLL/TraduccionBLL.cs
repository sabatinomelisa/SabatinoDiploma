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
        public List<TraduccionBE> Listar_675MS(int idiomaSel)
        {
            TraduccionDAL tradDAL_675MS = new TraduccionDAL();

            List<TraduccionBE> traducciones_675MS = new List<TraduccionBE>();

            traducciones_675MS = tradDAL_675MS.ListarTraducciones_675MS(idiomaSel);

            return traducciones_675MS;
        }
    }
}
