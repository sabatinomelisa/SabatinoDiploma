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
    public class ClienteDAL
    {
        internal int AltaCliente_675MS(ClienteBE cliente_675MS)
        {
            Acceso acceso_675MS = new Acceso();

            acceso_675MS.Conectar_675MS();

            List<SqlParameter> parametros_675MS = new List<SqlParameter>();

            acceso_675MS.IniciarTx_675MS();

            try
            {
                //Genero parámetros para el alta del cliente
                string sql = "ALTA_CLIENTE";
                parametros_675MS.Clear();
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@dni", cliente_675MS.Dni_675MS));
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@nombre", cliente_675MS.Nombre_675MS));
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@apellido", cliente_675MS.Apellido_675MS));
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@fechaNac", cliente_675MS.FechaNacimiento_675MS));
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@mail", cliente_675MS.Mail_675MS));
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@domicilio", cliente_675MS.Domicilio_675MS));
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@telefono", cliente_675MS.Telefono_675MS));
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@dv",cliente_675MS.DigitoVerificadorHorizontal_675MS));

                int resultado_675MS = acceso_675MS.Escribir_675MS(sql, parametros_675MS);

                if (resultado_675MS != -1)
                {
                    acceso_675MS.ConfirmarTx_675MS();
                    acceso_675MS.Desconectar_675MS();
                    return resultado_675MS;
                }
                else
                {
                    acceso_675MS.Desconectar_675MS();
                    return resultado_675MS;
                }

            }
            catch (Exception ex)
            {
                acceso_675MS.RevertirTx_675MS();
                acceso_675MS.Desconectar_675MS();
                throw new Exception("Error al registrar al usuario");
            }

        }



        public int ActualizarDigitoVerificadorHorizontal_675MS(int dni_675MS, int digitoVerificadorHorizontal_675MS)
        {
            Acceso acceso_675MS = new Acceso();

            acceso_675MS.Conectar_675MS();

            List<SqlParameter> parametros_675MS = new List<SqlParameter>();

            acceso_675MS.IniciarTx_675MS();

            try
            {
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@dni", dni_675MS));

                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@dv", digitoVerificadorHorizontal_675MS));

                int resultado_675MS = acceso_675MS.Escribir_675MS("MODIF_DIGITO", parametros_675MS);

                if (resultado_675MS != -1)
                {
                    acceso_675MS.ConfirmarTx_675MS();
                    return resultado_675MS;
                }

                return resultado_675MS;
            }
            catch
            {
                acceso_675MS.RevertirTx_675MS();
                throw;
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }

        }

        public List<ClienteBE> ListarClientes_675MS()
        {
            Acceso acceso_675MS = new Acceso();
            List<ClienteBE> clientes_675MS = new List<ClienteBE>();

            acceso_675MS.Conectar_675MS();

            DataTable tabla_675MS = acceso_675MS.Leer_675MS("LISTAR_CLIENTES");

            foreach (DataRow row in tabla_675MS.Rows)
            {
                ClienteBE clienteAuxiliar_675MS = new ClienteBE();
                clienteAuxiliar_675MS.Dni_675MS= int.Parse(row["DNI"].ToString());
                clienteAuxiliar_675MS.Nombre_675MS = row["Nombre"].ToString();
                clienteAuxiliar_675MS.Apellido_675MS = row["Apellido"].ToString();
                clienteAuxiliar_675MS.Mail_675MS = row["Mail"].ToString();
                clienteAuxiliar_675MS.Domicilio_675MS = row["Domicilio"].ToString();
                clienteAuxiliar_675MS.Telefono_675MS = int.Parse(row["Telefono"].ToString());
                clienteAuxiliar_675MS.FechaNacimiento_675MS = Convert.ToDateTime(row["FechaNacimiento"]);
                clienteAuxiliar_675MS.DigitoVerificadorHorizontal_675MS = Convert.ToInt32(row["DigitoVerificadorHorizontal"]);

                clientes_675MS.Add(clienteAuxiliar_675MS);
            }

            acceso_675MS.Desconectar_675MS();
            return clientes_675MS;

        }
    }
}
