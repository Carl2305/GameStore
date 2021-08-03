using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using GameStore_Entidades;

namespace GameStore_AccesoDatos
{
    public class Cargo_D
    {
        public List<tb_Cargo> LoadCargos()
        {
            List<tb_Cargo> list = new List<tb_Cargo>();
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(ConexionBD.getConecctionBD(), "LoadCargos");
                while (dr.Read())
                {
                    list.Add(new tb_Cargo()
                    {
                        Id_Cargo = dr.GetString(0),
                        Nombre_Cargo = dr.GetString(1)
                    });
                }
                dr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return list;
        }
    }
}
