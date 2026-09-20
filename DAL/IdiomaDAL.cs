using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class IdiomaDAL
    {
        public List<IdiomaBE> Listar_675MS()
        {
            Acceso acceso_675MS = new Acceso();
            List<IdiomaBE> idiomas_675MS = new List<IdiomaBE>();

            acceso_675MS.Conectar_675MS();

            string sql_675MS = "ConsultaIdiomas";

            DataTable respuesta_675MS = new DataTable();

            respuesta_675MS = acceso_675MS.Leer_675MS(sql_675MS);

            foreach (DataRow row in respuesta_675MS.Rows)
            {
                IdiomaBE idioma_675MS = new IdiomaBE();
                idioma_675MS.Id_675MS = int.Parse(row["Id"].ToString());
                idioma_675MS.Nombre_675MS = row["NombreIdioma"].ToString();
                idiomas_675MS.Add(idioma_675MS);

            }

            acceso_675MS.Desconectar_675MS();

            return idiomas_675MS;
        }
    }
}
