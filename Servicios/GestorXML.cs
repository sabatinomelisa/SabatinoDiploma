using BE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Servicios
{
    public class GestorXML
    {
        public void Serializar_675MS(List<ClienteBE> lista, string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<ClienteBE>));
                serializer.Serialize(fs, lista);
            }
        }

        public List<ClienteBE> Deserializar_675MS(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<ClienteBE>));
                return (List<ClienteBE>)serializer.Deserialize(fs);
            }
        }
    }
}
