using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BitacoraBLL
    {

        BitacoraDAL bitacoraBLL_675MS;
        public void Registrar_675MS(string usuario_675MS, string accion_675MS, string modulo_675MS, string descripcion_675MS)
        {
            ValidarEvento_675MS(accion_675MS, modulo_675MS);
            bitacoraBLL_675MS = new BitacoraDAL();

            BitacoraBE bitacora_675MS = new BitacoraBE();
            bitacora_675MS.Usuario_675MS = usuario_675MS;
            bitacora_675MS.Accion_675MS = accion_675MS;
            bitacora_675MS.Modulo_675MS = modulo_675MS;
            bitacora_675MS.Descripcion_675MS = descripcion_675MS;

            bitacoraBLL_675MS.Registrar_675MS(bitacora_675MS);
        }

        public void ValidarEvento_675MS(string accion_675MS, string modulo_675MS)
        {
            if (string.IsNullOrWhiteSpace(accion_675MS))
            {
                throw new ArgumentException("La acción es obligatoria.");
            }

            if (string.IsNullOrWhiteSpace(modulo_675MS))
            {
                throw new ArgumentException("El módulo es obligatorio.");
            }
        }

        public void RegistrarLogin_675MS(string usuario_675MS)
        {
            Registrar_675MS(usuario_675MS, "LOGIN", "Seguridad", "Usuario inició sesión correctamente.");
        }

        public void RegistrarLogout_675MS(string usuario_675MS)
        {
            Registrar_675MS(usuario_675MS, "LOGOUT", "Seguridad", "Usuario cerró sesión.");
        }

        public void RegistrarAlta_675MS(string usuario_675MS, string modulo_675MS, string descripcion_675MS)
        {
            Registrar_675MS(usuario_675MS, "ALTA", modulo_675MS, descripcion_675MS);
        }

        public void RegistrarModificacion_675MS(string usuario_675MS, string modulo_675MS, string descripcion_675MS)
        {
            Registrar_675MS(usuario_675MS, "MODIFICACION", modulo_675MS, descripcion_675MS);
        }

        public void RegistrarBaja(string usuario_675MS, string modulo_675MS, string descripcion_675MS)
        {
            Registrar_675MS(usuario_675MS, "BAJA", modulo_675MS, descripcion_675MS);
        }

        public void RegistrarError(string usuario_675MS, string modulo_675MS, Exception exception)
        {
            string descripcion_675MS = "Error no especificado.";

            if (exception != null)
            {
                descripcion_675MS = exception.Message;
            }

            Registrar_675MS(usuario_675MS, "ERROR", modulo_675MS, descripcion_675MS);
        }

        public DataTable Consultar_675MS(string usuario_675MS, string modulo_675MS, DateTime? desde_675MS, DateTime? hasta_675MS)
        {
            if (desde_675MS.HasValue && hasta_675MS.HasValue && desde_675MS.Value.Date > hasta_675MS.Value.Date)
            {
                throw new ArgumentException("La fecha desde no puede ser posterior a la fecha hasta.");
            }

            BitacoraDAL bitacoraDAL_675MS = new BitacoraDAL();
            return bitacoraDAL_675MS.Consultar_675MS(usuario_675MS, modulo_675MS, desde_675MS, hasta_675MS);
        }

    }
}
