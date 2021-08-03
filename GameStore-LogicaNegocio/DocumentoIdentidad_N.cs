using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore_AccesoDatos;
using GameStore_Entidades;

namespace GameStore_LogicaNegocio
{
    public class DocumentoIdentidad_N
    {
        DocumentoIdentidad_D documentoIdentidad_d = new DocumentoIdentidad_D();
        public List<tb_DocumentoIdentidad> getDocumentoIdentidads()
        {
            return documentoIdentidad_d.getDocumentoIdentidads();
        }
    }
}
