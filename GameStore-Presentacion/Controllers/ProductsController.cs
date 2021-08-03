using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore_LogicaNegocio;
using GameStore_Entidades;

namespace GameStore_Presentacion.Controllers
{
    public class ProductsController : Controller
    {
        Producto_N producto_n = new Producto_N();
        // GET: Products
        public ActionResult Products(string ctg = "")
        {
            return View(producto_n.getProductsForCategorie(ctg));
        }
        // POST: Products
        [HttpPost]
        public ActionResult Products(int ctg = 0, string q = "")
        {
            List<tb_Producto> list = producto_n.getProductForCategOrNameOrDesc(q);
            if (list.Count() != 0)
            {
                return View(list);
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult ProductDetail(string code = "")
        {
            tb_Producto list = producto_n.ShowDetilProduct(code);
            if (list != null)
            {
                return View(list);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}