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
    public class TurnoBLL
    {
        TurnoDAL turnoDAL_675MS;
        public List<TurnoBE> ListarTurnost_6755MS()
        {
            turnoDAL_675MS = new TurnoDAL();
            return turnoDAL_675MS.ListarTurnosturnos_675MS();
        }


        public List<string> ObtenerHorariosDisponibles_675MS(DateTime fechaSeleccionada_675MS, int duracionSolicitada_675MS = 60, int idTurnoExcluir_675MS = 0)
        {
            List<string> horariosDisponibles_675MS = new List<string>();

            if (duracionSolicitada_675MS <= 0)
                return horariosDisponibles_675MS;

            TurnoDAL turnoDAL_675MS = new TurnoDAL();

            List<TurnoBE> turnosOcupados_675MS = turnoDAL_675MS.ObtenerTurnosPorFecha_675MS(fechaSeleccionada_675MS.Date);

            DateTime apertura_675MS = fechaSeleccionada_675MS.Date.AddHours(8);

            DateTime cierre_675MS = fechaSeleccionada_675MS.Date.AddHours(18);

            for (DateTime inicio_675MS = apertura_675MS; inicio_675MS.AddMinutes(duracionSolicitada_675MS) <= cierre_675MS; inicio_675MS = inicio_675MS.AddMinutes(30))
            {
                DateTime fin_675MS = inicio_675MS.AddMinutes(duracionSolicitada_675MS);

                bool ocupado_675MS = false;

                foreach (TurnoBE turno_675MS in turnosOcupados_675MS)
                {
                    if (!turno_675MS.Activo_675MS || turno_675MS.IdTurno_675MS == idTurnoExcluir_675MS)
                        continue;

                    DateTime inicioExistente_675MS = turno_675MS.FechaHora_675MS;

                    DateTime finExistente_675MS = inicioExistente_675MS.AddMinutes(turno_675MS.DuracionEstimada_675MS);

                    if (inicio_675MS < finExistente_675MS && fin_675MS > inicioExistente_675MS)
                    {
                        ocupado_675MS = true;
                        break;
                    }
                }

                if (!ocupado_675MS)
                    horariosDisponibles_675MS.Add(inicio_675MS.ToString("HH:mm"));
            }

            return horariosDisponibles_675MS;
        }

        public void ValidarDatosIngresados_675MS(TurnoBE turno_675MS)
        {

            if (turno_675MS.Dominio_675MS== string.Empty)
            {
                throw new Exception("Ingresar Dominio del Vehículo");

            }

            if (turno_675MS.DuracionEstimada_675MS <= 0)
            {
                throw new Exception("Ingresar duración del turno");

            }

            if (turno_675MS.FechaHora_675MS==null)
            {
                throw new Exception("Ingresar fecha y hora del turno");

            }
        }

        public int BajaTurno_675MS(int idSeleccionado_675MS)
        {
            int filasAfectadas_675MS = 0;

            turnoDAL_675MS = new TurnoDAL();

            filasAfectadas_675MS = turnoDAL_675MS.BajaTurnoDAL_675MS(idSeleccionado_675MS);

            return filasAfectadas_675MS;
        }

        public int ReprogramarTurno_675MS(int idSeleccionado_675MS, DateTime fechaNueva_675MS)
        {
            int filasAfectadas_675MS = 0;

            turnoDAL_675MS = new TurnoDAL();

            filasAfectadas_675MS = turnoDAL_675MS.ReprogramarTurnoDAL_675MS(idSeleccionado_675MS, fechaNueva_675MS);

            return filasAfectadas_675MS;
        }

        public int RegistrarTurno_675MS(TurnoBE turnoBE_675MS)
        {
            int filasAfectadas_675MS = 0;

            // Instancio servicio para calcular digito verificador
            Servicios.CalculadorDigitoVerificador calculadorDV_675MS = new Servicios.CalculadorDigitoVerificador();
            // Buesco el Id Empleado
            if (Servicios.SessionManager.SesionIniciada_675MS)
            {
                // Obterngo el usuario que está logueado en este momento a través de la clase de Servicios SessionManager
                BE.UsuarioBE usuarioLogueado = Servicios.SessionManager.ObtenerUsuarioActual_675MS();

                //Guardo el ID de ese empleado a la entidad TurnoBE
                turnoBE_675MS.IdEmpleado_675MS = usuarioLogueado.IdEmpleado_675MS;

                //Registro el turno 
                TurnoDAL turnoDal = new TurnoDAL();
                filasAfectadas_675MS = turnoDal.RegistrarTurnoDAL_675MS(turnoBE_675MS);
            }
            else
            {
                throw new Exception("No hay ningún empleado logueado en el sistema.");
            }

            return filasAfectadas_675MS;
        }


    }
}
