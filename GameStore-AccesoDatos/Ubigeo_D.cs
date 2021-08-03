using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using GameStore_Entidades;

namespace GameStore_AccesoDatos
{
    public class Ubigeo_D
    {
        public List<tb_Ubigeo> getDepartamentos()
        {
            List<tb_Ubigeo> list = new List<tb_Ubigeo>();
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(ConexionBD.getConecctionBD(), "LoadDepatamentos");
                while (dr.Read())
                {
                    list.Add(new tb_Ubigeo()
                    {
                        CodDep=dr.GetString(0),
                        NombreUbigeo=dr.GetString(1)
                    });
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return list;
        }
        public List<tb_Ubigeo> getProvincias(string codeDep)
        {
            List<tb_Ubigeo> list = new List<tb_Ubigeo>();
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(ConexionBD.getConecctionBD(), "LoadProvincias", codeDep);
                while (dr.Read())
                {
                    list.Add(new tb_Ubigeo()
                    {
                        CodProv = dr.GetString(0),
                        NombreUbigeo = dr.GetString(1)
                    });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return list;
        }
        public List<tb_Ubigeo> getDistritos(string codeDep, string codePro)
        {
            List<tb_Ubigeo> list = new List<tb_Ubigeo>();
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(ConexionBD.getConecctionBD(), "LoadDistrito", codeDep, codePro);
                while (dr.Read())
                {
                    list.Add(new tb_Ubigeo()
                    {
                        CodDist = dr.GetString(0),
                        NombreUbigeo = dr.GetString(1)
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
