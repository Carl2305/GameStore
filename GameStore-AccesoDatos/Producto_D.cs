using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using GameStore_Entidades;

namespace GameStore_AccesoDatos
{
    public class Producto_D
    {
        #region acciones de listado de productos para el cliente
        public List<tb_Producto> LoadAllProducts()
        {
            List<tb_Producto> list = new List<tb_Producto>();
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(ConexionBD.getConecctionBD(), "getProductos");
                while (dr.Read())
                {
                    list.Add(new tb_Producto() { 
                        Id_Producto=dr.GetString(0),
                        Nombre_Producto=dr.GetString(1),
                        //Descripcion_Producto=dr.GetString(2),
                        Imagen_Producto=dr.GetString(2),
                        Precio_Producto=dr.GetDecimal(3),
                        Nombre_Categoria=dr.GetString(4)
                        //Stock_Producto=dr.GetInt32(4)
                    });
                }
                dr.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return list;
        }
        public tb_Producto ShowDetilProduct(string code)
        {
            tb_Producto product = null;
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(ConexionBD.getConecctionBD(), "getDetailProduct",code);
                while (dr.Read())
                {
                    product = new tb_Producto();
                    product.Id_Producto = dr.GetString(0);
                    product.Nombre_Producto = dr.GetString(1);
                    //product.Descripcion_Producto=dr.GetString(2);
                    product.Imagen_Producto = dr.GetString(2);
                    product.Precio_Producto = dr.GetDecimal(3);
                    product.Nombre_Categoria = dr.GetString(4);
                }
                dr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return product;
        }
        public List<tb_Producto> getProductsForCategorie(string code)
        {
            List<tb_Producto> list = new List<tb_Producto>();
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(ConexionBD.getConecctionBD(), "getProductsForCategorie", code);
                while (dr.Read())
                {
                    list.Add(new tb_Producto()
                    {
                        Id_Producto = dr.GetString(0),
                        Nombre_Producto = dr.GetString(1),
                        //Descripcion_Producto=dr.GetString(2),
                        Imagen_Producto = dr.GetString(2),
                        Precio_Producto = dr.GetDecimal(3),
                        Nombre_Categoria=dr.GetString(4)
                        //Stock_Producto = dr.GetInt32(4)
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
        public List<tb_Producto> getProductForCategOrNameOrDesc(string q)
        {
            List<tb_Producto> list = new List<tb_Producto>();
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(ConexionBD.getConecctionBD(), "getProductForCategOrNameOrDesc", q);
                while (dr.Read())
                {
                    list.Add(new tb_Producto()
                    {
                        Id_Producto = dr.GetString(0),
                        Nombre_Producto = dr.GetString(1),
                        //Descripcion_Producto=dr.GetString(2),
                        Imagen_Producto = dr.GetString(2),
                        Precio_Producto = dr.GetDecimal(3),
                        Nombre_Categoria=dr.GetString(4)
                        //Stock_Producto = dr.GetInt32(4)
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

        #region crud de productos para el controller PanelIntranet
        public List<tb_Producto> LoadAllProductsIntranet(string cadena)
        {
            List<tb_Producto> list = new List<tb_Producto>();
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(ConexionBD.getConecctionBD(), "LoadAllProductsIntranet", cadena);
                while (dr.Read())
                {
                    list.Add(new tb_Producto()
                    {
                        Id_Producto = dr.GetString(0),
                        Nombre_Producto = dr.GetString(1),
                        Imagen_Producto = dr.GetString(2),
                        Precio_Producto = dr.GetDecimal(3),
                        Stock_Producto = dr.GetInt32(4),
                        Estado = dr.GetInt32(5),
                        Nombre_Categoria=dr.GetString(6)
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

        public tb_Producto LoadDataProductoIntranet(string code)
        {
            tb_Producto product = null;
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(ConexionBD.getConecctionBD(), "getDataProducto", code);
                while (dr.Read())
                {
                    product = new tb_Producto();
                    product.Id_Producto = dr.GetString(0);
                    product.Nombre_Producto = dr.GetString(1);
                    product.Descripcion_Producto=dr.GetString(2);
                    product.Imagen_Producto = dr.GetString(3);
                    product.Precio_Producto = dr.GetDecimal(4);
                    product.Stock_Producto = dr.GetInt32(5);
                    product.Id_Categoria = dr.GetString(6);
                    product.Estado = dr.GetInt32(7);
                }
                dr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return product;
        }
        public string GenerateCodeProduct()
        {
            string code = "P0001";
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(ConexionBD.getConecctionBD(), "GenerateCodeProduct");
                while (dr.Read())
                {
                    int d = Convert.ToInt32(dr.GetString(0));
                    code = String.Format("P{0:0000}", d + 1);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return code;
        }
        public int addNewProduct(tb_Producto p)
        {
            int answer = 0;
            try
            {
                answer = SqlHelper.ExecuteNonQuery(ConexionBD.getConecctionBD(), "InsertProduct", p.Id_Producto, p.Nombre_Producto, 
                    p.Descripcion_Producto, p.Imagen_Producto, p.Precio_Producto, p.Stock_Producto, p.Id_Categoria);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return answer;
        }
        public int putProduct(tb_Producto p)
        {
            int answer = 0;
            try
            {
                answer = SqlHelper.ExecuteNonQuery(ConexionBD.getConecctionBD(), "UpdateProduct", p.Id_Producto, p.Nombre_Producto,
                    p.Descripcion_Producto, p.Imagen_Producto, p.Precio_Producto, p.Stock_Producto, p.Id_Categoria, p.modImagen);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return answer;
        }
        public int deleteProduct(string code)
        {
            int answer = 0;
            try
            {
                answer = SqlHelper.ExecuteNonQuery(ConexionBD.getConecctionBD(), "DeleteProduct", code);
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return answer;
        }

        #endregion
    }
}
