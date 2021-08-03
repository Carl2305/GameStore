using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using GameStore_Entidades;

namespace GameStore_AccesoDatos
{
    public class Factura_D
    {
        #region detalle factura para el cliente
        public tb_Factura_Cab HeaderInvoiceClient(string codeF, int idCli)
        {
            tb_Factura_Cab facture = null;
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(ConexionBD.getConecctionBD(), "HeaderInvoiceClient", codeF, idCli);
                while (dr.Read())
                {
                    facture = new tb_Factura_Cab();
                    facture.Id_Factura = dr.GetString(0);
                    facture.Nomb_Client = dr.GetString(1);
                    facture.Estado = dr.GetInt32(2);
                    facture.Fecha_Factura = dr.GetDateTime(3);
                    facture.Importe_Total = dr.GetDecimal(4);
                    facture.Igv = dr.GetDecimal(5);
                }
                dr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return facture;
        }
        public List<tb_Factura_Det> DetailsInvoiceClient(string codeF)
        {
            List<tb_Factura_Det> list = new List<tb_Factura_Det>();
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(ConexionBD.getConecctionBD(), "DetailsInvoiceClient", codeF);
                while (dr.Read())
                {
                    list.Add(new tb_Factura_Det()
                    {
                        Cantidad_Venta = dr.GetInt32(0),
                        Nomb_Product = dr.GetString(1),
                        Precio_Detalleventa = dr.GetDecimal(2),
                        SubTotal_sale = dr.GetDecimal(3),
                        Nomb_Categoria = dr.GetString(4)
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
        #endregion


        #region panelIntranet
        public int GestionarPagoDePedido(string code)
        {
            int answer = 0;
            try
            {
                answer = SqlHelper.ExecuteNonQuery(ConexionBD.getConecctionBD(), "GestionarPagoDePedido", code);
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return answer;
        }
        public List<tb_Factura_Cab> LoadInvoicesIntranet(int filter)
        {
            List<tb_Factura_Cab> list = new List<tb_Factura_Cab>();
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(ConexionBD.getConecctionBD(), "LoadInvoicesIntranet", filter);
                while (dr.Read())
                {
                    list.Add(new tb_Factura_Cab() { 
                        Id_Factura=dr.GetString(0),
                        string_Fecha= dr.GetDateTime(1).ToString(),
                        Nomb_Client =dr.GetString(2),
                        tipo_doc=dr.GetString(3),
                        Num_DocIdentC=dr.GetString(4),
                        Importe_Total=dr.GetDecimal(5),
                        Estado=dr.GetInt32(6)
                    });
                }
                dr.Close();
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return list;
        }
        public tb_Factura_Cab HeaderInvoiceForEmployee(string codeF)
        {
            tb_Factura_Cab facture = null;
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(ConexionBD.getConecctionBD(), "HeaderInvoiceForEmployee", codeF);
                while (dr.Read())
                {
                    facture = new tb_Factura_Cab();
                    facture.Id_Factura = dr.GetString(0);
                    facture.Nomb_Client = dr.GetString(1);
                    facture.Estado = dr.GetInt32(2);
                    facture.Fecha_Factura = dr.GetDateTime(3);
                    facture.Importe_Total = dr.GetDecimal(4);
                    facture.Igv = dr.GetDecimal(5);
                }
                dr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return facture;
        }
        public List<tb_Factura_Det> DetailsInvoiceForEmployee(string codeF)
        {
            List<tb_Factura_Det> list = new List<tb_Factura_Det>();
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(ConexionBD.getConecctionBD(), "DetailsInvoiceForEmployee", codeF);
                while (dr.Read())
                {
                    list.Add(new tb_Factura_Det()
                    {
                        Cantidad_Venta = dr.GetInt32(0),
                        Nomb_Product = dr.GetString(1),
                        Precio_Detalleventa = dr.GetDecimal(2),
                        SubTotal_sale = dr.GetDecimal(3),
                        Nomb_Categoria = dr.GetString(4)
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
        #endregion
    }
}
