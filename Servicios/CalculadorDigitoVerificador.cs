using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class CalculadorDigitoVerificador
    {
        private const int ModuloCalculo_675MS = 1000000007;

        public int CalcularHorizontal_675MS(params string[] valoresAtributos_675MS)
        {
            if (valoresAtributos_675MS == null)
            {
                return 0;
            }

            long acumulador_675MS = 0;

            for (int indiceAtributo_675MS = 0; indiceAtributo_675MS < valoresAtributos_675MS.Length; indiceAtributo_675MS++)
            {
                string valorAtributo_675MS = valoresAtributos_675MS[indiceAtributo_675MS] ?? string.Empty;

                for (int indiceCaracter_675MS = 0; indiceCaracter_675MS < valorAtributo_675MS.Length; indiceCaracter_675MS++)
                {
                    int posicionAtributo = indiceAtributo_675MS + 1;
                    int posicionCaracter = indiceCaracter_675MS + 1;
                    int valorCaracter = Convert.ToInt32(valorAtributo_675MS[indiceCaracter_675MS]);

                    acumulador_675MS += posicionAtributo * posicionCaracter * valorCaracter;
                }
            }

            return Convert.ToInt32(acumulador_675MS % ModuloCalculo_675MS);
        }

        public Dictionary<string, int> CalcularVertical(List<Dictionary<string, string>> filas_675MS)
        {
            Dictionary<string, int> resultados_675MS = new Dictionary<string, int>();

            if (filas_675MS == null)
            {
                return resultados_675MS;
            }

            for (int indiceFila = 0; indiceFila < filas_675MS.Count; indiceFila++)
            {
                Dictionary<string, string> fila_675MS = filas_675MS[indiceFila];

                foreach (KeyValuePair<string, string> atributo_675MS in fila_675MS.OrderBy(item_675MS => item_675MS.Key))
                {
                    int valorCalculado = CalcularHorizontal_675MS(indiceFila.ToString(), atributo_675MS.Key, atributo_675MS.Value);

                    if (!resultados_675MS.ContainsKey(atributo_675MS.Key))
                    {
                        resultados_675MS.Add(atributo_675MS.Key, 0);
                    }

                    resultados_675MS[atributo_675MS.Key] = Convert.ToInt32((resultados_675MS[atributo_675MS.Key] + valorCalculado) % ModuloCalculo_675MS);
                }
            }

            return resultados_675MS;
        }

    }
}
