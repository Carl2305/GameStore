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
    public class PanelIntranetController : Controller
    {
        Producto_N producto_n = new Producto_N();
        Categoria_N categoria_n = new Categoria_N();
        Factura_N factura_n = new Factura_N();
        Empleado_N empleado_n = new Empleado_N();
        // GET: PanelIntranet
        public ActionResult Index()
        {
            if (Session["Prop_Usu"] == null)
            {
                return RedirectToAction("Login", "Intranet");
            }
            return View();
        }

        #region crud productos
        // GET: Products
        public ActionResult Products()
        {
            if (Session["Prop_Usu"] == null)
            {
                return RedirectToAction("Login", "Intranet");
            }
            ViewBag.DROPDOWN_CATEGORIAS =new SelectList(categoria_n.LoadAllCategories(), "Id_Categoria", "Nombre_Categoria");
            return View();
        }
        // POST: Products
        [HttpPost]
        public ActionResult Products(string cadena="")
        {
            var list = JsonConvert.SerializeObject(producto_n.LoadAllProductsIntranet(cadena));
            return Json(list);
        }
        // POST: getDataProduct
        [HttpPost]
        public JsonResult getDataProduct(string code)
        {
            var list = JsonConvert.SerializeObject(producto_n.LoadDataProductoIntranet(code));
            return Json(list);
        }
        [HttpPost]
        public Object addNewProduct(tb_Producto post)
        {
            post.Id_Producto = producto_n.GenerateCodeProduct();
            decimal x = post.Precio_Producto;
            HttpFileCollectionBase files = null;
            HttpPostedFileBase file = null;
            string path = null;
            string nombreArchivo = "";
            string _archivo = "";

            if (Request.Files.Count > 0)
            {
                files = Request.Files;
                file = files[0];
                string _extencionFile = System.IO.Path.GetExtension(file.FileName);
                _archivo = string.Format("{0:ddMMyyyy_hhmmss}", DateTime.Now);
                nombreArchivo = $"{_archivo}{_extencionFile}";
                if(_extencionFile == ".png" || _extencionFile == ".jpeg" || _extencionFile == ".jpg")
                {
                    path = $"Content/images/Products/{nombreArchivo}";
                    new IntranetController().UploadFile(file,"Content/images/Products/",_archivo, "Products");
                    post.Imagen_Producto = path;
                }
            }
            int answer = producto_n.addNewProduct(post);
            if (answer == 0)
            {
                if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath("~/" + path)))
                    System.IO.File.Delete(System.Web.HttpContext.Current.Server.MapPath("~/" + path));
            }
            return answer;
        }
        [HttpPost]
        public Object putProducto(tb_Producto post)
        {
            string path = null;
            if (post.modImagen != 0)
            {
                HttpFileCollectionBase files = null;
                HttpPostedFileBase file = null;
                string nombreArchivo = "";
                string _archivo = "";

                if (Request.Files.Count > 0)
                {
                    files = Request.Files;
                    file = files[0];
                    string _extencionFile = System.IO.Path.GetExtension(file.FileName);
                    _archivo = string.Format("{0:ddMMyyyy_hhmmss}", DateTime.Now);
                    nombreArchivo = $"{_archivo}{_extencionFile}";
                    if (_extencionFile == ".png" || _extencionFile == ".jpeg" || _extencionFile == ".jpg")
                    {
                        path = $"Content/images/Products/{nombreArchivo}";
                        new IntranetController().UploadFile(file, "Content/images/Products/", _archivo, "Products");
                        post.Imagen_Producto = path;
                    }
                }
            }
            else
            {
                post.Imagen_Producto = "NULL";
            }
            int answer = producto_n.putProduct(post);
            if (post.modImagen != 0 && answer == 0)
            {
                if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath("~/" + path)))
                    System.IO.File.Delete(System.Web.HttpContext.Current.Server.MapPath("~/" + path));
            }
            return answer;
        }
        [HttpPost]
        public Object deleteProducto(string code)
        {
            int answer = producto_n.deleteProduct(code);
            return answer;
        }
        #endregion

        #region crud ventas
        // GET: Sales
        public ActionResult Sales()
        {
            if (Session["Prop_Usu"] == null)
            {
                return RedirectToAction("Login", "Intranet");
            }
            return View();
        }

        // POST: Sales
        [HttpPost]
        public JsonResult Sales(string fil="0")
        {
            int filter = int.Parse(fil);
            var list = JsonConvert.SerializeObject(factura_n.LoadInvoicesIntranet(filter));
            return Json(list);
        }

        // POST: GestionarPagoDePedido
        [HttpPost]
        public Object GestionarPagoDePedido(string code)
        {
            return factura_n.GestionarPagoDePedido(code.Trim());
        }

        // POST: DetailsOrdenSale
        [HttpPost]
        public ActionResult DetailSale(string code)
        {
            tb_Factura_Cab list = factura_n.HeaderInvoiceForEmployee(code);
            if (list != null)
            {
                ViewBag.ID_FACTURA = list.Id_Factura;
                ViewBag.NOMBRE_CLIENTE = list.Nomb_Client;
                ViewBag.ESTADO = list.Estado;
                ViewBag.FECHA_FACTURA = list.Fecha_Factura;
                ViewBag.IMPORTE_TOTAL = list.Importe_Total;
                ViewBag.IGV = list.Igv;
                var listde = factura_n.DetailsInvoiceClient(code);
                return View(listde);
            }
            return RedirectToAction("Sales", "PanelIntranet");
        }


        #endregion

        #region MyPerfil
        public ActionResult MyProfile()
        {
            if (Session["Prop_Usu"] == null)
            {
                return RedirectToAction("Login", "Intranet");
            }
            return View();
        }
        // POST: MyProfile
        [HttpPost]
        public ActionResult MyProfileUpdateFotoEmpleado(tb_Empleado post)
        {
            HttpFileCollectionBase files = null;
            HttpPostedFileBase file = null;
            string path = null;
            string nombreArchivo = "";
            string _archivo = "";

            if (Request.Files.Count > 0)
            {
                files = Request.Files;
                file = files[0];
                string _extencionFile = System.IO.Path.GetExtension(file.FileName);
                _archivo = string.Format("{0:ddMMyyyy_hhmmss}", DateTime.Now);
                nombreArchivo = $"{_archivo}{_extencionFile}";
                if (_extencionFile == ".png" || _extencionFile == ".jpeg" || _extencionFile == ".jpg")
                {
                    path = $"Content/images/Employees/{nombreArchivo}";
                    new IntranetController().UploadFile(file, "Content/images/Employees/", _archivo, "Employees");
                    post.Foto_Empleado = path;
                }
            }
            int answer = empleado_n.UpdateFotoEmpleado(post.Foto_Empleado,Session["Usuario_Empleado"]as string); // aquí se debe enviar el nombre de usuario de la session
            if (answer == 0)
            {
                if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath("~/" + path)))
                    System.IO.File.Delete(System.Web.HttpContext.Current.Server.MapPath("~/" + path));
            }
            return Json(new { data = answer });
        }
        #endregion

        #region crud empleado mantenimiento
        // GET: Employees
        public ActionResult Employees()
        {
            if (Session["Prop_Usu"] == null)
            {
                return RedirectToAction("Login", "Intranet");
            }
            return View();
        }
        // POST: getAllEmployees
        [HttpPost]
        public JsonResult getAllEmployees(string docIden="")
        {
            var list = JsonConvert.SerializeObject(empleado_n.GetAllEmployees(docIden, Session["Usuario_Empleado"] as string));    // aquí se debe enviar el nombre de usario de la session
            return Json(list);
        }
        
        // POST: addNewEmployee
        public Object addNewEmployee(tb_Empleado post)
        {
            int answer = 0;
            try
            {
                HttpFileCollectionBase files = null;
                HttpPostedFileBase file = null;
                string path = null;
                string nombreArchivo = "";
                string _archivo = "";

                if (Request.Files.Count > 0)
                {
                    files = Request.Files;
                    file = files[0];
                    string _extencionFile = System.IO.Path.GetExtension(file.FileName);
                    _archivo = string.Format("{0:ddMMyyyy_hhmmss}", DateTime.Now);
                    nombreArchivo = $"{_archivo}{_extencionFile}";
                    if (_extencionFile == ".png" || _extencionFile == ".jpeg" || _extencionFile == ".jpg")
                    {
                        path = $"Content/images/Employees/{nombreArchivo}";
                        new IntranetController().UploadFile(file, "Content/images/Employees/", _archivo, "Employees");
                        post.Foto_Empleado = path;
                    }
                }

                string useuario = $"GS{string.Format("{0:yyyyhhss}", DateTime.Now)}{post.Num_DocIdent_Empleado.Substring(1, 2)}";
                string pass = $"{post.Num_DocIdent_Empleado.Substring(0, 4)}&-GM{post.Num_DocIdent_Empleado.Substring(4, 3)}{string.Format("{0:yyyy_ST!MMhhdd}", DateTime.Now)}{post.Id_Cargo}!";
                post.Usuario_Empleado = useuario;
                post.Contrasenia_Empleado = Tools.Encrypt(pass);
                try
                {
                    Tools.sendMailRegister(post.Nombre_Empleado, post.Apellidos_Empleado, post.Usuario_Empleado, post.Contrasenia_Empleado, post.Correo_Empleado);
                }catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath("~/" + path)))
                        System.IO.File.Delete(System.Web.HttpContext.Current.Server.MapPath("~/" + path));
                    return 0;
                }
                answer= empleado_n.addNewEmployee(post);
                if (answer == 0)
                {
                    if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath("~/" + path)))
                        System.IO.File.Delete(System.Web.HttpContext.Current.Server.MapPath("~/" + path));
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return answer;
        }
        
        // POST: PutEmployee
        [HttpPost]
        public Object PutEmployee(tb_Empleado post)
        {
            return empleado_n.UpdateEmpleadoIntranetA(post);
        }

        // POST:DeleteEmployee
        [HttpPost]
        public Object DeleteEmployee(string Idemple)
        {
            return empleado_n.DeleteEmployee(Idemple);
        }







        // POST: EmployeeData este es para listar los datos del empleado que accedió al sistema
        [HttpPost]
        public JsonResult EmployeeData()
        {
            var list = JsonConvert.SerializeObject(empleado_n.getDataEmpleado(Session["Usuario_Empleado"] as string)); // aquí se debe enviar le nombre de usuario según la session
            return Json(list);
        }
        //public tb_Empleado getDataEmployeeForEdit(int idEmple)
        // POST: Employee este es para listar los datos del empleado que accedió al sistema
        [HttpPost]
        public JsonResult EmployeeDataEdit(string cod)
        {
            int idEmple = int.Parse(cod);
            var list = JsonConvert.SerializeObject(empleado_n.getDataEmployeeForEdit(idEmple));
            return Json(list);
        }
        #endregion






    }
}