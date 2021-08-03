using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Web.Mvc;
using GameStore_Entidades;
using GameStore_LogicaNegocio;
using GameStore_Presentacion.Models;

namespace GameStore_Presentacion.Controllers
{
    public class SalesController : Controller
    {
        Producto_N producto_n = new Producto_N();
        Venta_N venta_n = new Venta_N();
        Factura_N factura_n = new Factura_N();
        // POST: DataForSales
        [HttpPost]
        public JsonResult DataForSales()
        {
            return Json(new
            {
                items = Tools.number_items,
                Stotal = Tools.subtotal_sale,
                IGV = Tools.igv,
                total = Tools.total_sale
            });
        }
        // POST: DataCart
        [HttpPost]
        public JsonResult DataCart()
        {
            var json = JsonConvert.SerializeObject(Tools.cart);
            return Json(json);
        }
        // POST: AddElementToCart
        [HttpPost]
        public object AddElementToCart(string code, int cant)
        {
            int answer = 0;
            try
            {
                tb_Producto list = producto_n.ShowDetilProduct(code);
                Boolean exists = false;
                foreach (tb_Factura_Det tb in Tools.cart)
                {
                    if (tb.Id_Producto == code)
                    {
                        Tools.number_items -= tb.Cantidad_Venta;
                        Tools.subtotal_sale -= tb.SubTotal_sale;

                        tb.Cantidad_Venta = tb.Cantidad_Venta + cant;
                        tb.SubTotal_sale = tb.Cantidad_Venta * tb.Precio_Detalleventa;

                        Tools.number_items += tb.Cantidad_Venta;
                        Tools.subtotal_sale += tb.SubTotal_sale;

                        Tools.igv = Convert.ToDecimal(Tools.subtotal_sale) * Convert.ToDecimal(0.18);
                        Tools.total_sale = Convert.ToDecimal(Tools.subtotal_sale) + Convert.ToDecimal(Tools.igv);
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                {
                    tb_Factura_Det d = new tb_Factura_Det();
                    d.Id_Producto = code;
                    d.Imagen_det = list.Imagen_Producto;
                    d.Cantidad_Venta = d.Cantidad_Venta + cant;
                    d.Precio_Detalleventa = list.Precio_Producto;
                    d.SubTotal_sale = list.Precio_Producto * cant;
                    d.Nomb_Product = list.Nombre_Producto;
                    d.Nomb_Categoria = list.Nombre_Categoria;
                    Tools.cart.Add(d);
                    Tools.number_items += d.Cantidad_Venta;
                    Tools.subtotal_sale += d.SubTotal_sale;
                    Tools.igv = Convert.ToDecimal(Tools.subtotal_sale) * Convert.ToDecimal(0.18);
                    Tools.total_sale = Convert.ToDecimal(Tools.subtotal_sale) + Convert.ToDecimal(Tools.igv);
                }
                answer = 1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                answer = 0;
            }
            return answer;
        }

        // POST: RemoveElementToCart
        [HttpPost]
        public object RemoveElementToCart(string code)
        {
            int answer = 0;
            try
            {
                foreach (tb_Factura_Det tb in Tools.cart)
                {
                    if (tb.Id_Producto.Equals(code))
                    {
                        Tools.number_items -= tb.Cantidad_Venta;
                        Tools.subtotal_sale -= tb.SubTotal_sale;
                        Tools.igv = Convert.ToDecimal(Tools.subtotal_sale) * Convert.ToDecimal(0.18);
                        Tools.total_sale = Convert.ToDecimal(Tools.subtotal_sale) + Convert.ToDecimal(Tools.igv);
                        Tools.cart.Remove(tb);
                        if (Tools.number_items <= 0 && Tools.subtotal_sale <= 0)
                        {
                            Tools.number_items = 0;
                            Tools.subtotal_sale = 0;
                            Tools.igv = 0;
                            Tools.total_sale = 0;
                        }
                        break;
                    }
                }
                answer = 1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                answer = 0;
            }
            return answer;
        }
        // GET: FinallySale
        public ActionResult FinallySale()
        {
            return View();
        }
        // POST: FinallyNewSale
        [HttpPost]
        public ActionResult FinallyNewSale()
        {
            List<tb_Factura_Det> tb_Factura_Dets = Tools.cart;
            
            if (tb_Factura_Dets.Count == 0)
            {
                string[] answer = { "2", "Carrito de Compras Vácio", "Agregue un producto al Carrito de Compras.", "error", "OK", "3000" };
                return Json(answer);
            }

            if (Session["Prop_Usua"] == null)
            {
                string[] answer = { "1", "Debe Registrase y/o Iniciar Sesión", "", "warning", "", "" };
                return Json(answer);
            }

            // genera el codigo de la factura
            string codeF = venta_n.GenerateCodeInvoice();
            tb_Factura_Cab tb_Factura_Cabr = new tb_Factura_Cab();
            tb_Factura_Cabr.Id_Factura = codeF;
            tb_Factura_Cabr.Id_Cliente = int.Parse(Session["Id_Cliente"].ToString());     // aquí se obtiene el codigo del cliente desde la session
            tb_Factura_Cabr.Importe_Total = Tools.total_sale;
            tb_Factura_Cabr.Igv = Tools.igv;
            int rpta = venta_n.InsertNewSale(tb_Factura_Cabr, Tools.cart);
            if (rpta == 0)
            {
                string[] answer = { "3", "Ocurrio un Error", "Intentelo de nuevo.", "error", "OK", "3000" };
                return Json(answer);
            }
            else
            {
                // revisa la data de la ultima factura
                tb_Factura_Cab list = factura_n.HeaderInvoiceClient(codeF,tb_Factura_Cabr.Id_Cliente); // los parametros son el codigo del peidfo o factura y el id del cliente
                // envia el email
                Tools.sendMail(Session["Nombre_Cliente"] as string, list.Id_Factura, list.Importe_Total.ToString(), Session["Correo_Cliente"] as string); // auqí se debe poner el nombre del cliente y correo del cliente
                // envia el mensaje de respuesta el cliente 
                string[] answer = { "4", "Compra Exitosa", "En unos momentos recibiras un correo a 'email' con los detalles de tu compra y el QR con el que debes hacer el pago de S/ " + list.Importe_Total + " ", "success", "OK" };
                // resetea las variables del carrito
                Tools.number_items = 0;
                Tools.igv = 0;
                Tools.subtotal_sale = 0;
                Tools.total_sale = 0;
                Tools.cart = new List<tb_Factura_Det>();
                return Json(answer);
            }
        }
        public ActionResult DetailFacClient(string codeF)
        {
            if (Session["Prop_Usua"] != null)
            {
                tb_Factura_Cab list = factura_n.HeaderInvoiceClient(codeF, int.Parse(Session["Id_Cliente"].ToString())); // el id del cliente se debe obtener a travez de la session
                if (list != null)
                {
                    ViewBag.ID_FACTURA = list.Id_Factura;
                    ViewBag.NOMBRE_CLIENTE = list.Nomb_Client;
                    ViewBag.ESTADO = list.Estado == 0 ? "No Pagado" : "Pagado";
                    ViewBag.FECHA_FACTURA = list.Fecha_Factura;
                    ViewBag.IMPORTE_TOTAL = list.Importe_Total;
                    ViewBag.IGV = list.Igv;
                    var listde = factura_n.DetailsInvoiceClient(codeF);
                    return View(listde);
                }
            }
            return View();
        }
    }
}