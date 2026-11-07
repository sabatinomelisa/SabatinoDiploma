using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class IdiomaBLL
    {
        IdiomaDAL idiomaDAL675MS;

        public int AltaIdioma_675MS(string nuevoIdioma_675MS)
        {
            idiomaDAL675MS = new IdiomaDAL();
            return idiomaDAL675MS.RegistrarIdiomaDAL_675MS(nuevoIdioma_675MS);
        }

        public int BorrarIdioma_675MS(int idiomaEliminar_675MS)
        {
            idiomaDAL675MS = new IdiomaDAL();
            return idiomaDAL675MS.BorrarIdiomaDAL_675MS(idiomaEliminar_675MS);
            
        }

        public List<IdiomaBE> ListarIdiomas_675MS()
        {
            IdiomaDAL idiomaDAL_675MS = new IdiomaDAL();

            return idiomaDAL_675MS.Listar_675MS();
        }

        public int ModificarIdioma_675MS(IdiomaBE idiomaModificado_675MS)
        {
   
            idiomaDAL675MS = new IdiomaDAL();
            return idiomaDAL675MS.ModificarIdiomaDAL_675MS(idiomaModificado_675MS);
   
        }
    }
}
