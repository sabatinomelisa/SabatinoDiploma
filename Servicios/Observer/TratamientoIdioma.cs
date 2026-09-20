using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Servicios.Observer.IObserverIdioma;

namespace Servicios.Observer
{
    public class TratamientoIdioma
    {
        //Utilizamos  un Singleton para gestionar el Observer para que solamente exista un idioma activo a la vez
        private static TratamientoIdioma instancia_675MS;

        private List<IOberverIdioma> observadores_675MS;

        private IdiomaBE idiomaActual_675MS;

        public IdiomaBE IdiomaActual_675MS
        {
            get { return idiomaActual_675MS; }
            set { idiomaActual_675MS = value; }
        }

        private TratamientoIdioma()
        {
            observadores_675MS = new List<IOberverIdioma>();
        }
        public static TratamientoIdioma Instancia
        {
            get
            {
                if (instancia_675MS == null)
                    instancia_675MS = new TratamientoIdioma();
                return instancia_675MS;
            }
        }
        public void Suscribir(IOberverIdioma observer)
        {
            observadores_675MS.Add(observer);
        }

        public void Desuscribir(IOberverIdioma observer)
        {
            observadores_675MS.Remove(observer);
        }

        public void Notificar()
        {
            foreach (var o in observadores_675MS)
            {
                o.ActualizarIdioma_675MS();
            }
        }
    }
}
