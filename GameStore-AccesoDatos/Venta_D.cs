using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using GameStore_Entidades;

namespace GameStore_AccesoDatos
{
    public class Venta_D
    {
        public string GenerateCodeInvoice()
        {
            string code = "FC00000001";
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(ConexionBD.getConecctionBD(), "GenerateCodeInvoice");
                while (dr.Read())
                {
                    int d = Convert.ToInt32(dr.GetString(0));
                    code = String.Format("FC{0:00000000}", d + 1);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return code;
        }
        public int InsertNewSale(tb_Factura_Cab head, List<tb_Factura_Det> body)
        {
            int answer = 0;
            SqlConnection cnx = new SqlConnection(ConexionBD.getConecctionBD());
            cnx.Open();
            SqlTransaction trx = cnx.BeginTransaction(IsolationLevel.Serializable);
            SqlCommand cmd1 = null;
            SqlCommand cmd2 = null;
            try
            {
                cmd1 = new SqlCommand("InsertNewSaleCab", cnx, trx);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@id_Fac", head.Id_Factura);
                cmd1.Parameters.AddWithValue("@id_Client", head.Id_Cliente);
                cmd1.Parameters.AddWithValue("@total", head.Importe_Total);
                cmd1.Parameters.AddWithValue("@igv", head.Igv);
                answer = cmd1.ExecuteNonQuery();

                foreach (tb_Factura_Det d in body)
                {
                    cmd2 = new SqlCommand("InsertNewSaleDet", cnx, trx);
                    cmd2.CommandType = CommandType.StoredProcedure;

                    cmd2.Parameters.AddWithValue("@id_Fac", head.Id_Factura);
                    cmd2.Parameters.AddWithValue("@id_Product", d.Id_Producto);
                    cmd2.Parameters.AddWithValue("@cant", d.Cantidad_Venta);
                    cmd2.Parameters.AddWithValue("@Stotal", d.SubTotal_sale);
                    answer += cmd2.ExecuteNonQuery();
                }
                trx.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                trx.Rollback();
                answer = 0;
            }
            finally
            {
                cnx.Close();
            }
            return answer;
        }
    }
}
