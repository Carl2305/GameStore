using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore_Entidades
{
    public class tb_Factura_Det
    {
        public string Id_Factura { get; set; }
        public string Id_Producto { get; set; }
        public int Cantidad_Venta { get; set; }
        public decimal Precio_Detalleventa { get; set; }


        #region Atributos extras para mostrar el detalle de la factura
        public string Imagen_det { get; set; }
        public string Nomb_Product { get; set; }
        public decimal SubTotal_sale { get; set; }
        public string Nomb_Categoria { get; set; }
        #endregion
    }
}
