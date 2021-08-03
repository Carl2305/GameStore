using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore_AccesoDatos;
using GameStore_Entidades;

namespace GameStore_LogicaNegocio
{
    public class Producto_N
    {
        Producto_D producto_d = new Producto_D();
        public List<tb_Producto> LoadAllProducts()
        {
            return producto_d.LoadAllProducts();
        }
        public tb_Producto ShowDetilProduct(string code)
        {
            return producto_d.ShowDetilProduct(code);
        }
        public List<tb_Producto> getProductsForCategorie(string code)
        {
            return producto_d.getProductsForCategorie(code);
        }
        public List<tb_Producto> getProductForCategOrNameOrDesc(string q)
        {
            return producto_d.getProductForCategOrNameOrDesc(q);
        }


        #region metodos para el menu productos en intranet
        public List<tb_Producto> LoadAllProductsIntranet(string cadena)
        {
            return producto_d.LoadAllProductsIntranet(cadena);
        }
        public tb_Producto LoadDataProductoIntranet(string code)
        {
            return producto_d.LoadDataProductoIntranet(code);
        }
        public string GenerateCodeProduct()
        {
            return producto_d.GenerateCodeProduct();
        }
            #region crud producto
            public int addNewProduct(tb_Producto p)
            {
                return producto_d.addNewProduct(p);
            }
            public int putProduct(tb_Producto p)
            {
                return producto_d.putProduct(p);
            }
            public int deleteProduct(string code)
            {
                return producto_d.deleteProduct(code);
            }
            #endregion






        #endregion
    }
}
