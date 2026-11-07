using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class VtvBLL
    {
        VtvDAL VtvDAL_675MS;

        public int AltaVtv_675MS(VtvBE nuevaVtv_675MS)
        {
            VtvDAL_675MS = new VtvDAL();
            return VtvDAL_675MS.RegistrarVTVDAL_675MS(nuevaVtv_675MS);
        }

        public int ModificarVtv_675MS(VtvBE vtvModificada_675MS)
        {
            VtvDAL_675MS = new VtvDAL();
            return VtvDAL_675MS.ModificacionVtvDAL_675MS(vtvModificada_675MS);
        }
        public int BorrarVtv_675MS(int vtvEliminar_675MS)
        {
            VtvDAL_675MS = new VtvDAL();
            return VtvDAL_675MS.BorrarVtvDAL_675MS(vtvEliminar_675MS);
        }


        public List<VtvBE> ListarVtvs_675MS()
        {
            //Instancio la DAL
            VtvDAL_675MS = new VtvDAL();

            List<VtvBE> vtvs_675MS = VtvDAL_675MS.ListarVtvs_675MS();

            return vtvs_675MS;
        }

        public int BajaVTV_675MS(int idSeleccionado_675MS)
        {
            int filasAfectadas_675MS = 0;

            VtvDAL_675MS = new VtvDAL();

            filasAfectadas_675MS = VtvDAL_675MS.BorrarVtvDAL_675MS(idSeleccionado_675MS);

            return filasAfectadas_675MS;
        }

        public int RegistrarVTV_675MS(VtvBE nuevaVTV_675MS)
        {
            int filasAfectadas_675MS = 0;

            //Registro la VTV
            VtvDAL_675MS = new VtvDAL();
            filasAfectadas_675MS = VtvDAL_675MS.RegistrarVTVDAL_675MS(nuevaVTV_675MS);

            return filasAfectadas_675MS;
        }

        public int ModificarVTV_675MS(VtvBE nuevaVTV_675MS)
        {
            int filasAfectadas_675MS = 0;

            //Registro la VTV 
            VtvDAL_675MS = new VtvDAL();
            filasAfectadas_675MS = VtvDAL_675MS.ModificacionVtvDAL_675MS(nuevaVTV_675MS);

            return filasAfectadas_675MS;
        }

        public int AprobarVTV_675MS(int id_675MS)
        {
            VtvDAL_675MS = new VtvDAL();
            return VtvDAL_675MS.AprobarVtvDAL_675MS(id_675MS);
        }

        public void GenerarComprobanteVTV(int idVtv_675MS, string rutaCompleta_675MS, string empleadoAprobador_675MS)
        {
            //Instancio la DAL
            VtvDAL_675MS = new VtvDAL();

            // Busco los datos completos de la VTV
            VtvBE vtv_675MS = VtvDAL_675MS.ObtenerPorId_675MS(idVtv_675MS);

            if (vtv_675MS == null)
            {
                throw new Exception("No se encontró el registro de la VTV para generar el comprobante.");
            }

            // Ejecuto el generador de PDF
            Servicios.CrearPDF.CrearPdfVtv(vtv_675MS, rutaCompleta_675MS,empleadoAprobador_675MS);
        }
    }
    
}

