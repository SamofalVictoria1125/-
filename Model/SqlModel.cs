using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Курсовая.Tools;
using Курсовая.NewFolder1;
using MySql.Data.MySqlClient;

namespace Курсовая.Model
{

    //тут тоже я пока занята другим!!
    public class SqlModel
    {
        private SqlModel() { }
        static SqlModel sqlModel;
        public static SqlModel GetInstance()
        {
            if (sqlModel == null)
                sqlModel = new SqlModel();
            return sqlModel;
        }

        public bool OpenConnection()
        {
            var connection = DBMySqlUtils.GetDBConnection();
            if (connection.State == System.Data.ConnectionState.Open)
                return true;
            try
            {
                connection.Open();
                return true;
            }
            catch (Exception e)
            { 
            
            }
            return false;
        }

        public void CloseConnection()
        {
            var connection = DBMySqlUtils.GetDBConnection();
            if (connection.State == System.Data.ConnectionState.Closed)
                return;
            try
            { 
                connection.Close();
            }
            catch (Exception e)
            {

            }
        }

        internal List<Group> SelectAllGroups()
        {
            List<Group> groups = new List<Group>();
            string query = "select * from `group`";
            var mySqlDB = MySqlDB.GetDB();
            if (mySqlDB.OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(query, mySqlDB.sqlConnection))
                using (MySqlDataReader dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        groups.Add(new Group
                        {
                            ID = dr.GetInt32("id"),
                            Title = dr.GetString("title"),
                            Year = dr.GetInt32("year"),
                            curator_id = dr.GetInt32("curator_id")
                        });
                    }
                }
                mySqlDB.CloseConnection();
            }
            return groups;
        }


        public int GetNumRows(Type type)
        {
            string table = GetTableName(type);
            return MySqlDB.GetDB().GetRowsCount(table);
        }

        private static string GetTableName(Type type)
        {
            var tableAtrributes = type.GetCustomAttributes(typeof(TableAttribute), false);
            return ((TableAttribute)tableAtrributes.First()).Table;
        }

    }
    
}
