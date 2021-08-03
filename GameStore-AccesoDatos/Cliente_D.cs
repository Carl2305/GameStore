using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using GameStore_Entidades;

namespace GameStore_AccesoDatos
{
    public class Cliente_D
    {
        #region Login cliente
        public tb_Cliente loginClient(string mail, string pass)
        {
            tb_Cliente client = null;
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(ConexionBD.getConecctionBD(), "LogInClient", mail, pass);
                while (dr.Read())
                {
                    client = new tb_Cliente();
                    client.Nombre_Cliente = dr.GetString(0);
                    client.Apellidos_Cliente = dr.GetString(1);
                    client.Correo_Cliente = dr.GetString(2);
                    client.Id_Cliente = dr.GetInt32(3);
                }
                dr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return client;
        }
        #endregion
        public int InsertNewClient(tb_Cliente post)
        {
            int answer = 0;
            try
            {
                answer = SqlHelper.ExecuteNonQuery(ConexionBD.getConecctionBD(), "InsertNewClient", post.Nombre_Cliente, post.Apellidos_Cliente, post.Ubigeo_Empleado, 
                                                                                                    post.Codigo_Documento, post.Num_DocIdent_Cliente, post.Num_Telefono, 
                                                                                                    post.Correo_Cliente, post.Direccion, post.Contrasenia_Cliente);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return answer;
        }
    }
}
