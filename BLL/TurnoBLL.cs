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
        public List<TurnoBE> ListarTurnosturnos_675MS()
        {
            turnoDAL_675MS = new TurnoDAL();
            return turnoDAL_675MS.ListarTurnosturnos_675MS();
        }

        public List<string> ObtenerHorariosDisponibles_675MS(DateTime fechaSeleccionada_675MS)
        {
            TurnoDAL turnoDAL_675MS = new TurnoDAL();
            List<TurnoBE> turnosOcupados_675MS = turnoDAL_675MS.ObtenerTurnosPorFecha_675MS(fechaSeleccionada_675MS);

            List<string> horariosDisponibles = new List<string>();

            // Reglas de negocio: horario del taller
            TimeSpan horaApertura_675MS = new TimeSpan(8, 0, 0);   // 08:00 AM
            TimeSpan horaCierre_675MS = new TimeSpan(18, 0, 0);    // 18:00 PM
            TimeSpan intervalo_675MS = new TimeSpan(0, 30, 0);     // Turnos cada 30 min

            TimeSpan horaEvaluada_675MS = horaApertura_675MS;

            while (horaEvaluada_675MS < horaCierre_675MS)
            {
                DateTime posibleHorario = fechaSeleccionada_675MS.Add(horaEvaluada_675MS);
                bool horarioLibre = true;

                foreach (TurnoBE turno_675MS in turnosOcupados_675MS)
                {
                    DateTime inicioTurno = Convert.ToDateTime(turno_675MS.FechaHora_675MS);
                    int duracion = Convert.ToInt32(turno_675MS.DuracionEstimada_675MS);
                    DateTime finTurno = inicioTurno.AddMinutes(duracion);

                    if (posibleHorario >= inicioTurno && posibleHorario < finTurno)
                    {
                        horarioLibre = false;
                        break;
                    }
                }

                if (horarioLibre)
                {
                    // Agregamos la hora libre a la lista
                    horariosDisponibles.Add(horaEvaluada_675MS.ToString(@"hh\:mm"));
                }

                horaEvaluada_675MS = horaEvaluada_675MS.Add(intervalo_675MS);
            }

            return horariosDisponibles;
        }


        public void ValidarDatosIngresados_675MS(TurnoBE turno_675MS)
        {
            if(turno_675MS.Dni_675MS <= 0 )
            {
                throw new Exception("Ingresar DNI del cliente");

            }

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

            // Paso los datos a texto
            string dniStr_675MS = turnoBE_675MS.Dni_675MS.ToString();
            string dominioStr_675MS = turnoBE_675MS.Dominio_675MS;
            string fechaStr_675MS = turnoBE_675MS.FechaHora_675MS.ToString("yyyyMMddHHmm");
            string duracionStr_675MS = turnoBE_675MS.DuracionEstimada_675MS.ToString();
            string activoStr_675MS = turnoBE_675MS.Activo_675MS.ToString();

            //Calculo DV
            turnoBE_675MS.DigitoVerificadorHorizontal_675MS = calculadorDV_675MS.CalcularHorizontal_675MS(
                dniStr_675MS,
                dominioStr_675MS,
                fechaStr_675MS,
                duracionStr_675MS,
                activoStr_675MS
            );

            // Intancio la DAL
            TurnoDAL turnoDAL_675MS = new TurnoDAL();

            //Registro en la base de datos
            filasAfectadas_675MS = turnoDAL_675MS.RegistrarTurnoDAL_675MS(turnoBE_675MS);

            return filasAfectadas_675MS;
        }


    }
}
