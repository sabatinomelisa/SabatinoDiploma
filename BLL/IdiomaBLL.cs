using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class IdiomaBLL
    {
        public List<IdiomaBE> ListarIdiomas_675MS()
        {
            IdiomaDAL idiomaDAL_675MS = new IdiomaDAL();

            return idiomaDAL_675MS.Listar_675MS();
        }
    }
}
