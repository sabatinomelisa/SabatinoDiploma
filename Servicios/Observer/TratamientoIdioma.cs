using BE;
using System.Collections.Generic;

namespace Servicios.Observer
{
    public class TratamientoIdioma
    {
        private static TratamientoIdioma instancia_675MS;

        private List<IObserverIdioma> observadores_675MS;

        private IdiomaBE idiomaActual_675MS;

        public IdiomaBE IdiomaActual_675MS
        {
            get { return idiomaActual_675MS; }
            set { idiomaActual_675MS = value; }
        }

        private TratamientoIdioma()
        {
            observadores_675MS = new List<IObserverIdioma>();
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

        public void Suscribir(IObserverIdioma observer)
        {
            if (!observadores_675MS.Contains(observer))
                observadores_675MS.Add(observer);
        }

        public void Desuscribir(IObserverIdioma observer)
        {
            observadores_675MS.Remove(observer);
        }

        public void Notificar()
        {
            foreach (IObserverIdioma observer in observadores_675MS.ToArray())
            {
                observer.ActualizarIdioma_675MS();
            }
        }
    }
}