using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class IdiomaDAL
    {
        public int BorrarIdiomaDAL_675MS(int idiomaEliminar_675MS)
        {
            Acceso acceso_675MS = new Acceso();
            acceso_675MS.Conectar_675MS();

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();

                parametros.Add(acceso_675MS.CrearParametro_675MS("@Id", idiomaEliminar_675MS));


                int filasAfectadas_675MS = acceso_675MS.Escribir_675MS("BORRAR_IDIOMA", parametros);

                return filasAfectadas_675MS;
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }
        }

        public List<IdiomaBE> Listar_675MS()
        {
            Acceso acceso_675MS = new Acceso();
            List<IdiomaBE> idiomas_675MS = new List<IdiomaBE>();

            acceso_675MS.Conectar_675MS();

            string sql_675MS = "CONSULTA_IDIOMAS";

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

        public int ModificarIdiomaDAL_675MS(IdiomaBE idiomaModificado_675MS)
        {
            Acceso acceso_675MS = new Acceso();
            acceso_675MS.Conectar_675MS();

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();

                parametros.Add(acceso_675MS.CrearParametro_675MS("@NombreIdioma", idiomaModificado_675MS.Nombre_675MS));
                parametros.Add(acceso_675MS.CrearParametro_675MS("@iD", idiomaModificado_675MS.Id_675MS));


                int filasAfectadas_675MS = acceso_675MS.Escribir_675MS("MODIFICAR_IDIOMA", parametros);

                return filasAfectadas_675MS;
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }
        }

        public int RegistrarIdiomaDAL_675MS(string nuevoIdioma_675MS)
        {
            Acceso acceso_675MS = new Acceso();
            acceso_675MS.Conectar_675MS();

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();

                parametros.Add(acceso_675MS.CrearParametro_675MS("@NombreIdioma", nuevoIdioma_675MS));

                int filasAfectadas_675MS = acceso_675MS.Escribir_675MS("ALTA_IDIOMA", parametros);

                return filasAfectadas_675MS;
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }
        }
    }
}
