using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore_AccesoDatos;
using GameStore_Entidades;

namespace GameStore_LogicaNegocio
{
    public class Factura_N
    {
        Factura_D factura_d = new Factura_D();
        #region detalle factura para el cliente
        public tb_Factura_Cab HeaderInvoiceClient(string codeF, int idCli)
        {
            return factura_d.HeaderInvoiceClient(codeF, idCli);
        }
        public List<tb_Factura_Det> DetailsInvoiceClient(string codeF)
        {
            return factura_d.DetailsInvoiceClient(codeF);
        }
        #endregion

        #region panelIntranet
        public int GestionarPagoDePedido(string code)
        {
            return factura_d.GestionarPagoDePedido(code);
        }
        public List<tb_Factura_Cab> LoadInvoicesIntranet(int filter)
        {
            return factura_d.LoadInvoicesIntranet(filter);
        }
        public tb_Factura_Cab HeaderInvoiceForEmployee(string codeF)
        {
            return factura_d.HeaderInvoiceForEmployee(codeF);
        }
        public List<tb_Factura_Det> DetailsInvoiceForEmployee(string codeF)
        {
            return factura_d.DetailsInvoiceForEmployee(codeF);
        }
        #endregion
    }
}
