using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace GameStore_AccesoDatos
{
    public class ConexionBD
    {
        public static string getConecctionBD()
        {
            return ConfigurationManager.ConnectionStrings["cnx_game_store"].ConnectionString;
        }
    }
}
