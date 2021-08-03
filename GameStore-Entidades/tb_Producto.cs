using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore_Entidades
{
    public class tb_Producto
    {
		public string Id_Producto { get; set; }
		public string Nombre_Producto { get; set; }
		public string Descripcion_Producto { get; set; }
		public string Imagen_Producto { get; set; }
		public decimal Precio_Producto { get; set; }
		public int Stock_Producto { get; set; }
		public string Id_Categoria { get; set; }
		public int Estado { get; set; }
		public DateTime Fecha_Registro { get; set; }
		public DateTime Fecha_Modificacion { get; set; }


        #region atributos extras
        public string Nombre_Categoria { get; set; }

        //atributo para actualizar la imagen
        public int modImagen { get; set; }
        #endregion
    }
}
