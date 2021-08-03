using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore_Entidades;
using GameStore_LogicaNegocio;
using GameStore_Presentacion.Models;

namespace GameStore_Presentacion.Controllers
{
    public class ClientsController : Controller
    {
        Cliente_N cliente_n = new Cliente_N();
        // GET: Clients
        public ActionResult MyProfile()
        {
            if (Session["Prop_Usua"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // GET: RegisterClient
        public ActionResult RegisterClient()
        {
            return View();
        }

        // POST: RegisterNewClient
        [HttpPost]
        public Object RegisterNewClient(tb_Cliente post)
        {
            int answer = 0;
            try
            {
                string pass = $"#${post.Num_DocIdent_Cliente.Substring(0, 4)}{post.Codigo_Documento.Substring(0, 2)}&GM{post.Num_DocIdent_Cliente.Substring(4, 3)}{post.Num_Telefono.Substring(3, 2)}!";
                Tools.sendMailRegisterClient(post.Nombre_Cliente, post.Apellidos_Cliente, pass, post.Correo_Cliente); 
                post.Contrasenia_Cliente = Tools.Encrypt(pass);
                answer = cliente_n.InsertNewClient(post);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
            return answer;
        }
    }
}