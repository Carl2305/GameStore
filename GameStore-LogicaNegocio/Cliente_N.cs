using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore_AccesoDatos;
using GameStore_Entidades;

namespace GameStore_LogicaNegocio
{
    public class Cliente_N
    {
        Cliente_D cliente_d = new Cliente_D();
        public tb_Cliente loginClient(string mail, string pass)
        {
            return cliente_d.loginClient(mail, pass);
        }
        public int InsertNewClient(tb_Cliente post)
        {
            return cliente_d.InsertNewClient(post);
        }
    }
}
