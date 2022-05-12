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



        public List<Group> SelectGroupsRange(int skip, int count)
        {
            var groups = new List<Group>();
            var mySqlDB = MySqlDB.GetDB();
            string query = $"SELECT * FROM `group` g, curator c, specials s WHERE g.curator_id = c.id AND g.special_id = s.id LIMIT { skip},{count}";
            if (mySqlDB.OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(query, mySqlDB.sqlConnection))
                using (MySqlDataReader dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var g = new Group
                        {
                            ID = dr.GetInt32("id"),
                            Title = dr.GetString("title"),
                            Year = dr.GetInt32("year"),
                        };
                        groups.Add(g);
                    }
                }
                mySqlDB.CloseConnection();
            }
            return groups;
        }

        internal List<Curator> SelectCurator()
        {
            var curators = new List<Curator>();
            var mySqlDB = MySqlDB.GetDB();
            string query = $"SELECT * FROM `curator`";
            if (mySqlDB.OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(query, mySqlDB.sqlConnection))
                using (MySqlDataReader dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        curators.Add(new Curator
                        {
                            ID = dr.GetInt32("id"),
                            FirstName = dr.GetString("firstName"),
                            LastName = dr.GetString("lastName"),
                        });
                    }
                }
                mySqlDB.CloseConnection();
            }
            return curators;

        }

        internal List<Student> SelectStudent()
        {
            var students = new List<Student>();
            var mySqlDB = MySqlDB.GetDB();
            string query = $"SELECT * FROM `student`";
            if (mySqlDB.OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(query, mySqlDB.sqlConnection))
                using (MySqlDataReader dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        students.Add(new Student
                        {
                            ID = dr.GetInt32("id"),
                            name = dr.GetString("name"),
                            curator = dr.GetString("patronymicName"),
                            surname = dr.GetString("patronymicName")

                        });
                    }
                }
                mySqlDB.CloseConnection();
            }
            return students;

        }

        internal List<Student> SelectStudentsByGroup(Group selectedGroup)
        {
            int id = selectedGroup?.ID ?? 0;
            var students = new List<Student>();
            var mySqlDB = MySqlDB.GetDB();
            string query = $"SELECT * FROM `student` WHERE group_id = {id}";
            if (mySqlDB.OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(query, mySqlDB.sqlConnection))
                using (MySqlDataReader dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        students.Add(new Student
                        {
                            ID = dr.GetInt32("id"),
                            name = dr.GetString("name"),
                            curator = dr.GetString("curator"),
                            surname = dr.GetString("surname"),
                            GroupId = dr.GetInt32("group_id"),
                        });
                    }
                }
                mySqlDB.CloseConnection();
            }
            return students;
        }

        public void Update<T>(T value) where T : Base
        {
            string table;
            List<(string, object)> values;
            GetMetaData(value, out table, out values);
            var query = CreateUpdateQuery(table, values, value.ID);
            var db = MySqlDB.GetDB();
            db.ExecuteNonQuery(query.Item1, query.Item2);

        }



        private static void GetMetaData<T>(T value, out string table, out List<(string, object)> values)
        {
            var type = value.GetType();
            var tableAtrributes = type.GetCustomAttributes(typeof(TableAttribute), false);
            table = ((TableAttribute)tableAtrributes.First()).Table;
            values = new List<(string, object)>();
            var props = type.GetProperties();
            foreach (var prop in props)
            {
                var columnAttributes = prop.GetCustomAttributes(typeof(ColumnAttribute), false);
                if (columnAttributes.Length > 0)
                {
                    string column = ((ColumnAttribute)columnAttributes.First()).Column;
                    values.Add(new(column, prop.GetValue(value)));
                }
            }
        }

        private static (string, MySqlParameter[]) CreateUpdateQuery(string table, List<(string, object)> values, int id)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"UPDATE `{table}` set ");
            List<MySqlParameter> parameters = InitParameters(values, stringBuilder);
            stringBuilder.Append($" WHERE id = {id}");
            return (stringBuilder.ToString(), parameters.ToArray());
        }

        private static List<MySqlParameter> InitParameters(List<(string, object)> values, StringBuilder stringBuilder)
        {
            var parameters = new List<MySqlParameter>();
            int count = 1;
            var rows = values.Select(s =>
            {
                parameters.Add(new MySqlParameter($"p{count}", s.Item2));
                return $"{s.Item1} = @p{count++}";
            });
            stringBuilder.Append(string.Join(',', rows));
            return parameters;
        }
    }
}

