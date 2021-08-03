using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore_AccesoDatos;
using GameStore_Entidades;

namespace GameStore_LogicaNegocio
{
    public class Empleado_N
    {
        Empleado_D empleado_d = new Empleado_D();
        #region Login
        public tb_Empleado loginEmple(string mail, string pass)
        {
            return empleado_d.loginEmple(mail, pass);
        }
        #endregion
        #region crud empleado
        public List<tb_Empleado> GetAllEmployees(string docIden, string user)
        {
            return empleado_d.GetAllEmployees(docIden, user);
        }
        public tb_Empleado getDataEmpleado(string user)
        {
            return empleado_d.getDataEmpleado(user);
        }
        public int addNewEmployee(tb_Empleado post)
        {
            return empleado_d.addNewEmployee(post);
        }
        public tb_Empleado getDataEmployeeForEdit(int idEmple)
        {
            return empleado_d.getDataEmployeeForEdit(idEmple);
        }
        public int UpdateEmpleadoIntranetA(tb_Empleado post)
        {
            return empleado_d.UpdateEmpleadoIntranetA(post);
        }
        public int UpdatePassEmple(string user, string passA, string passN)
        {
            return empleado_d.UpdatePassEmple(user, passA, passN);
        }
        public int DeleteEmployee(string Idemple)
        {
            return empleado_d.DeleteEmployee(Idemple);
        }
        public int UpdateFotoEmpleado(string img, string user)
        {
            return empleado_d.UpdateFotoEmpleado(img, user);
        }
        #endregion
    }
}
