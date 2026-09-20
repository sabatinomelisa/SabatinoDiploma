using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

namespace Servicios
{
    public class Seguridad
    {
        public static string GenerarHash_675MS(string password_675MS)
        {
            return BCrypt.Net.BCrypt.HashPassword(password_675MS);
        }

        public static bool VerificarPassword_675MS(string password_675MS, string hash_675MS)
        {
            return BCrypt.Net.BCrypt.Verify(password_675MS, hash_675MS);
        }
    }
}
