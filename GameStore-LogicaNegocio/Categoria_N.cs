using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore_Entidades;
using GameStore_AccesoDatos;

namespace GameStore_LogicaNegocio
{
    public class Categoria_N
    {
        Categoria_D categoria_d = new Categoria_D();
        public List<tb_Categoria> LoadAllCategories()
        {
            return categoria_d.LoadAllCategories();
        }
    }
}
