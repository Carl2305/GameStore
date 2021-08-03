using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using GameStore_Entidades;

namespace GameStore_AccesoDatos
{
    public class Categoria_D
    {
        public List<tb_Categoria> LoadAllCategories()
        {
            List<tb_Categoria> list = new List<tb_Categoria>();
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(ConexionBD.getConecctionBD(), "LoadCategories");
                while (dr.Read())
                {
                    list.Add(new tb_Categoria()
                    {
                        Id_Categoria=dr.GetString(0),
                        Nombre_Categoria=dr.GetString(1)
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
