using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore_Entidades
{
    public class tb_DocumentoIdentidad
    {
        public string Codigo_Documento { get; set; }
        public string Descripcion { get; set; }
        public string Descripcion_Corta { get; set; }
        public DateTime Fecha_Registro { get; set; }
    }
}
