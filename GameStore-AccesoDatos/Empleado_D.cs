using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using GameStore_Entidades;

namespace GameStore_AccesoDatos
{
    public class Empleado_D
    {
        #region Login empleado
        public tb_Empleado loginEmple(string mail, string pass)
        {
            tb_Empleado emple= null;
            try
            {
                SqlDataReader dr=SqlHelper.ExecuteReader(ConexionBD.getConecctionBD(), "LogInEmployee", mail, pass);
                while (dr.Read())
                {
                    emple = new tb_Empleado();
                    emple.Nombre_Empleado = dr.GetString(0);
                    emple.Apellidos_Empleado = dr.GetString(1);
                    emple.Usuario_Empleado = dr.GetString(2);
                    emple.Correo_Empleado = dr.GetString(3);
                    emple.Id_Cargo = dr.GetString(4);
                }
                dr.Close();
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return emple;
        }
        #endregion
        #region crud empleado
        public List<tb_Empleado> GetAllEmployees(string docIden, string user)
        {
            List<tb_Empleado> list = new List<tb_Empleado>();
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(ConexionBD.getConecctionBD(), "GetAllEmployees",docIden, user);
                while (dr.Read())
                {
                    list.Add(new tb_Empleado() {
                        Id_Empleado=dr.GetInt32(0),
                        Nombre_Empleado=dr.GetString(1),
                        Apellidos_Empleado=dr.GetString(2),
                        Correo_Empleado = dr.GetString(3),
                        Telf_Empleado = dr.GetString(4),
                        Fecha_Nacimiento = dr.GetDateTime(5),
                        string_Fecha = dr.GetDateTime(5).ToString("dd/MM/yyyy"),
                        Estado = dr.GetInt32(6),
                        Direccion = dr.GetString(7),
                        Nomb_ubigeo = dr.GetString(8),
                        Ubigeo_Empleado=dr.GetString(9),
                        Num_DocIdent_Empleado = dr.GetString(10),
                        Foto_Empleado = dr.GetString(11)
                    });
                }
                dr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return list;
        }
        public tb_Empleado getDataEmpleado(string user)
        {
            tb_Empleado emple = null;
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(ConexionBD.getConecctionBD(), "getDataEmpleadoIntranet", user);
                while (dr.Read())
                {
                    emple = new tb_Empleado();
                    emple.Id_Empleado = dr.GetInt32(0);
                    emple.Nombre_Empleado = dr.GetString(1);
                    emple.Apellidos_Empleado = dr.GetString(2);
                    emple.Correo_Empleado = dr.GetString(3);
                    emple.Telf_Empleado = dr.GetString(4);
                    emple.Nomb_Cargo = dr.GetString(5);
                    emple.Estado = dr.GetInt32(6);
                    emple.Direccion = dr.GetString(7);
                    emple.Usuario_Empleado = dr.GetString(8);
                    emple.Foto_Empleado = dr.GetString(9);
                }
                dr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return emple;
        }
        public int addNewEmployee(tb_Empleado post)
        {
            int answer = 0;
            try
            {
                answer = SqlHelper.ExecuteNonQuery(ConexionBD.getConecctionBD(), "InsertNewEmployee", post.Nombre_Empleado, post.Apellidos_Empleado, post.string_Fecha, post.Telf_Empleado, 
                                                                                                      post.Codigo_Documento, post.Num_DocIdent_Empleado, post.Ubigeo_Empleado, post.Direccion, 
                                                                                                      post.Id_Cargo, post.Usuario_Empleado, post.Contrasenia_Empleado, post.Foto_Empleado, post.Correo_Empleado);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return answer;
        }
        public tb_Empleado getDataEmployeeForEdit(int idEmple)
        {
            tb_Empleado emple = null;
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(ConexionBD.getConecctionBD(), "getDataEmployeeForEdit", idEmple);
                while (dr.Read())
                {
                    emple = new tb_Empleado();
                    emple.Direccion = dr.GetString(0);
                    emple.Id_Empleado = dr.GetInt32(1);
                    emple.Ubigeo_Empleado = dr.GetString(2);
                    emple.Telf_Empleado = dr.GetString(3);
                    emple.Id_Cargo = dr.GetString(4);
                }
                dr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return emple;
        }
        public int UpdateEmpleadoIntranetA(tb_Empleado post)
        {
            int answer = 0;
            try
            {
                answer = SqlHelper.ExecuteNonQuery(ConexionBD.getConecctionBD(), "UpdateEmpleadoIntranetA", post.Direccion, post.Ubigeo_Empleado, post.Telf_Empleado, post.Id_Cargo, post.Id_Empleado);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return answer;
        }
        public int UpdatePassEmple(string user, string passA, string passN)
        {
            int answer = 0;
            try
            {
                answer = SqlHelper.ExecuteNonQuery(ConexionBD.getConecctionBD(), "UpdatePasswordEmple", user, passA, passN);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return answer;
        }
        public int DeleteEmployee(string Idemple)
        {
            int answer = 0;
            try
            {
                answer = SqlHelper.ExecuteNonQuery(ConexionBD.getConecctionBD(), "DeleteEmployee", Idemple);
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return answer;
        }
        public int UpdateFotoEmpleado(string img, string user)
        {
            int answer = 0;
            try
            {
                answer = SqlHelper.ExecuteNonQuery(ConexionBD.getConecctionBD(), "UpdateFotoEmpleado", img, user);
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                answer = 0;
            }
            return answer;
        }
        #endregion
    }
}
