using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore_LogicaNegocio;
using GameStore_Presentacion.Models;

namespace GameStore_Presentacion.Controllers
{
    public class HomeController : Controller
    {
        Producto_N producto_n = new Producto_N();
        Cliente_N cliente_n = new Cliente_N();
        public ActionResult Index()
        {
            return View(producto_n.LoadAllProducts());
        }
        [HttpPost]
        public Object Login(string mail, string pass)
        {
            var answer = cliente_n.loginClient(mail,Tools.Encrypt(pass));
            if (answer != null)
            {
                Session["Correo_Cliente"]=answer.Correo_Cliente;
                Session["Nombre_Cliente"] =answer.Nombre_Cliente;
                Session["Apellidos_Cliente"] =answer.Apellidos_Cliente;
                Session["Id_Cliente"] = answer.Id_Cliente;
                Session["Prop_Usua"] = true;
                return 1;
            }
            return 0;
        }
        public ActionResult Logout()
        {
            Session.Contents.RemoveAll();
            return RedirectToAction("Index", "Home");
        }
    }
}