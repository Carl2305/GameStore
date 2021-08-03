using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using GameStore_Entidades;

namespace GameStore_AccesoDatos
{
    public class DocumentoIdentidad_D
    {
        public List<tb_DocumentoIdentidad> getDocumentoIdentidads()
        {
            List<tb_DocumentoIdentidad> list = new List<tb_DocumentoIdentidad>();
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(ConexionBD.getConecctionBD(), "LoadTipoDocumentos");
                while (dr.Read())
                {
                    list.Add(new tb_DocumentoIdentidad()
                    {
                        Codigo_Documento = dr.GetString(0),
                        Descripcion_Corta = dr.GetString(1)
                    });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return list;
        }
    }
}
