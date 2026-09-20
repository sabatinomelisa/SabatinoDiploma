using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Servicios.Observer.IObserverIdioma;

namespace Servicios.Observer
{
    public interface IObserverSubjectIdioma
    {
        void Suscribir_675MS(IOberverIdioma observer_675MS);

        void Desuscribir_675MS(IOberverIdioma observer_675MS);
        void Notificar_675MS();

    }
}
