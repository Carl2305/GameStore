using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using GameStore_Entidades;
using GameStore_AccesoDatos;

namespace GameStore_LogicaNegocio
{
    public class Venta_N
    {
        Venta_D venta_d = new Venta_D();
        public string GenerateCodeInvoice()
        {
            return venta_d.GenerateCodeInvoice();
        }
        public int InsertNewSale(tb_Factura_Cab head, List<tb_Factura_Det> body)
        {
            return venta_d.InsertNewSale(head, body);
        }
        
    }
}
