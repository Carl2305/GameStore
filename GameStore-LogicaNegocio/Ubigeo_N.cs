using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore_AccesoDatos;
using GameStore_Entidades;

namespace GameStore_LogicaNegocio
{
    public class Ubigeo_N
    {
        Ubigeo_D ubigeo_d = new Ubigeo_D();
        public List<tb_Ubigeo> getDepartamentos()
        {
            return ubigeo_d.getDepartamentos();
        }
        public List<tb_Ubigeo> getProvincias(string codeDep)
        {
            return ubigeo_d.getProvincias(codeDep);
        }
        public List<tb_Ubigeo> getDistritos(string codeDep, string codePro)
        {
            return ubigeo_d.getDistritos(codeDep, codePro);
        }
    }
}
