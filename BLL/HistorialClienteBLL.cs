
using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class HistorialClienteBLL
    {
        private HistorialClienteDAL historialClienteDAL_675MS;

        public HistorialClienteBLL()
        {
            historialClienteDAL_675MS = new HistorialClienteDAL();
        }

        private void ValidarPermiso_675MS()
        {
            // Verificar que exista una sesión iniciada
            if (!SessionManager.SesionIniciada_675MS)
            {
                throw new UnauthorizedAccessException("Debe iniciar sesión para consultar el historial.");
            }

            UsuarioBE usuario_675MS =  SessionManager.ObtenerUsuarioActual_675MS();

            if (usuario_675MS == null || usuario_675MS.Rol_675MS == null)
            {
                throw new UnauthorizedAccessException("El usuario no tiene un rol asignado.");
            }

            // Validar permisos mediante Composite
            RolBLL rolBLL_675MS = new RolBLL();

            if (!rolBLL_675MS.TienePermiso_675MS( usuario_675MS.Rol_675MS, "GestionIntegridad"))
            {
                throw new UnauthorizedAccessException("No tiene permiso para consultar el historial.");
            }
        }

        public List<HistorialClienteBE> ListarHistorial_675MS()
        {
            ValidarPermiso_675MS();

            return historialClienteDAL_675MS.ListarHistorial_675MS();
        }

        public List<HistorialClienteBE> ObtenerHistorialPorDNI_675MS(int dni_675MS)
        {
            ValidarPermiso_675MS();

            if (dni_675MS <= 0)
            {
                throw new ArgumentException("Debe ingresar un DNI válido.");
            }

            return historialClienteDAL_675MS.ObtenerHistorialPorDNI_675MS(dni_675MS);
        }


        public HistorialClienteBE ValidarVersionParaRecuperar_675MS(int idCambio_675MS, int dni_675MS)
        {
            List<HistorialClienteBE> historial_675MS = ObtenerHistorialPorDNI_675MS(dni_675MS);

            HistorialClienteBE version_675MS = historial_675MS.Find(h => h.IdHistorial_675MS == idCambio_675MS);

            if (version_675MS == null)
                throw new Exception("La versión histórica seleccionada ya no existe.");

            if (version_675MS.Act_675MS)
                throw new Exception("La versión seleccionada ya está activa.");

            return version_675MS;
        }

    }
}
