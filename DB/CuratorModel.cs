using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Курсовая.NewFolder1;

namespace Курсовая.DB
{
    class CuratorModel
    {
        public List<Curator> GetCuratorByFIO(string search)
        {
            List<Curator> array = new List<Curator>();
            string query = "select * from curator WHERE FirstName like @p1 or LastName like @p1";
            using (MySqlCommand mc = new MySqlCommand(query, DBMySqlUtils.GetDBConnection()))
            {
                mc.Parameters.AddWithValue("p1", '%' + search + '%');
                using (MySqlDataReader dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        array.Add(
                            new Curator { 
                                FirstName = dr.GetString("FirstName"),
                                LastName = dr.GetString("LastName"),
                                ID = dr.GetInt32("CuratorID") });
                    }
                }
            }
            return array;
        }
    }
}
