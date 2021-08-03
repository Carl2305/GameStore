using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore_Entidades
{
    public class tb_Cliente
    {
		public int Id_Cliente { get; set; }
		public string Nombre_Cliente { get; set; }
		public string Apellidos_Cliente { get; set; }
		public string Ubigeo_Empleado { get; set; }
		public string Codigo_Documento { get; set; }
		public string Num_DocIdent_Cliente { get; set; }
		public string Num_Telefono { get; set; }
		public string Correo_Cliente { get; set; }
		public string Contrasenia_Cliente { get; set; }
		public DateTime Fecha_Registro { get; set; }
        public string Direccion { get; set; }
    }
}
