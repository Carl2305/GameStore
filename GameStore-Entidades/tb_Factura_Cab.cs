using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore_Entidades
{
    public class tb_Factura_Cab
    {
        public string Id_Factura { get; set; }
        public int Id_Empleado { get; set; }
        public int Id_Cliente { get; set; }
        public DateTime Fecha_Factura { get; set; }
        public decimal Importe_Total { get; set; }
        public int Estado { get; set; }
        public decimal Igv { get; set; }


        #region Atributos extras para mostrar el detalle de la factura
        public string Nomb_Client { get; set; }
        public string tipo_doc { get; set; }
        public string Num_DocIdentC { get; set; }
        public string string_Fecha { get; set; }
        #endregion
    }
}
