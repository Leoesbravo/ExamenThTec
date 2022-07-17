using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class Conexion
    {
            public static string GetConnection()
            {
            return "Data Source =localhost; Initial Catalog = ExamenThTec; User ID = leonardo; Password = jodete419"; 
                //"Data Source=localhost;User ID=leonardo;Password=jodete419;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            }
        }
}
