using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore_AccesoDatos;
using GameStore_Entidades;

namespace GameStore_LogicaNegocio
{
    public class Cargo_N
    {
        Cargo_D cargo_d = new Cargo_D();
        public List<tb_Cargo> LoadCargos()
        {
            return cargo_d.LoadCargos();
        }
    }
}
