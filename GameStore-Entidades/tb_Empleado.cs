using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore_Entidades
{
    public class tb_Empleado
    {
        public int Id_Empleado { get; set; }
        public string Nombre_Empleado { get; set; }
        public string Apellidos_Empleado { get; set; }
        public string Correo_Empleado { get; set; }
        public string Telf_Empleado { get; set; }
        public string Id_Cargo { get; set; }
        public DateTime Fecha_Nacimiento { get; set; }
        public int Estado { get; set; }
        public string Direccion { get; set; }
        public string Usuario_Empleado { get; set; }
        public string Contrasenia_Empleado { get; set; }
        public string Ubigeo_Empleado { get; set; }
        public string Codigo_Documento { get; set; }
        public string Num_DocIdent_Empleado { get; set; }
        public string Foto_Empleado { get; set; }

        #region atributos extras
        public string Nomb_Cargo { get; set; }
        public string Nomb_ubigeo { get; set; }
        public string string_Fecha { get; set; }

        //atributo para actualizar la imagen
        public int modImagen { get; set; }
        #endregion
    }
}
