using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using GameStore_Entidades;
using GameStore_LogicaNegocio;
using GameStore_Presentacion.Models;

namespace GameStore_Presentacion.Controllers
{
    public class IntranetController : Controller
    {
        Empleado_N empleado_n = new Empleado_N();
        Ubigeo_N ubigeo_n = new Ubigeo_N();
        DocumentoIdentidad_N documentoIdentidad_n = new DocumentoIdentidad_N();
        Cargo_N cargo_n = new Cargo_N();
        // GET: Intranet
        public ActionResult Login()
        {
            if (Session["Prop_Usu"] == null)
            {
                return View();
            }
            return RedirectToAction("Index", "PanelIntranet");
        }
        // POST: Intranet
        [HttpPost]
        public ActionResult Login(string Usuario, string Contrasenia)
        {
            var answer = empleado_n.loginEmple(Usuario, Tools.Encrypt(Contrasenia));
            if (answer != null)
            {
                Session["Usuario_Empleado"] = answer.Usuario_Empleado;
                Session["Id_Cargo"] = answer.Id_Cargo;
                Session["Nombre_Empleado"]= answer.Nombre_Empleado;
                Session["Prop_Usu"] = true;
                return RedirectToAction("Index", "PanelIntranet");
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Contents.RemoveAll();
            return RedirectToAction("Login","Intranet");
        }

        [HttpPost]
        public Object UpdatePassEmple(string passA, string passN)
        {
            int answer = empleado_n.UpdatePassEmple(Session["Usuario_Empleado"] as string, Tools.Encrypt(passA), Tools.Encrypt(passN));    // aquí se debe poner el nombre de usuario segun la session
            if (answer == 1)
                Logout();
            return answer;
        }
        #region scritp para cargar una imagen
        public string UploadFile(HttpPostedFileBase _file, string ruta, string _archivo, string pagina)
        {
            try
            {
                if (_file != null)
                {
                    string _extencionFile = System.IO.Path.GetExtension(_file.FileName);
                    string mimeType = MimeMapping.GetMimeMapping(_file.FileName);
                    string path = System.Web.HttpContext.Current.Server.MapPath($"~/Content/images/{pagina}/");
                    if (System.IO.Directory.Exists(path) == false)
                    {
                        System.IO.Directory.CreateDirectory(path);
                    }
                    string NombreArchivo = $"{path}{_archivo}{ _extencionFile}";
                    _file.SaveAs(path + System.IO.Path.GetFileName(NombreArchivo));
                    #region eliminar imagen actual
                    if (ruta.Trim() != "")
                    {
                        if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath("~/" + ruta)))
                            System.IO.File.Delete(System.Web.HttpContext.Current.Server.MapPath("~/" + ruta));
                    }
                    #endregion
                    return "ok";
                }
                else
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        #endregion
        #region get data documento de identidad, departamentos, provincias, distritos y cargos
        [HttpPost]
        public Object getDocIdentidads()
        {
            var list = JsonConvert.SerializeObject(documentoIdentidad_n.getDocumentoIdentidads());
            return Json(list);
        }

        [HttpPost]
        public Object getDepartamentos()
        {
            var list = JsonConvert.SerializeObject(ubigeo_n.getDepartamentos());
            return Json(list);
        }

        [HttpPost]
        public Object getProvincias(string codeDep)
        {
            var list = JsonConvert.SerializeObject(ubigeo_n.getProvincias(codeDep));
            return Json(list);
        }

        [HttpPost]
        public Object getDistritos(string codeDep, string codePro)
        {
            var list = JsonConvert.SerializeObject(ubigeo_n.getDistritos(codeDep,codePro));
            return Json(list);
        }
        [HttpPost]
        public object getCargos()
        {
            var list = JsonConvert.SerializeObject(cargo_n.LoadCargos());
            return Json(list);
        }
        #endregion
    }
}