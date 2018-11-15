using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Global
{
    public static class global
    {
        private static string query;
        private static SqlConnection connection;
        private static SqlCommand command = new SqlCommand();
        private static SqlDataReader dataReader;

        public static void EstablishConnection(string server, string DataBase)
        {
            connection = new SqlConnection(string.Format(@"DATA SOURCE = {0}; INITIAL CATALOG = {1};INTEGRATED SECURITY = TRUE", server, DataBase));
            command.Connection = connection;
        }

        public static DataTable SelectFrom(string table)
        {
            if (table == null)
            {
                throw new PrototypeException();
            }
            else
            {
                connection.Open();
                DataTable dataTable = new DataTable();
                command.CommandText = "SELECT * FROM " + table;
                dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    dataTable.Load(dataReader);
                }
                else
                {
                    dataTable = null;
                }
                dataReader.Close();
                connection.Close();
                return dataTable;
            }
        }

        public static DataTable SelectFrom(string table, string[] fields)
        {
            if (table == null)
            {
                throw new PrototypeException();
            }
            else
            {
                connection.Open();
                DataTable dataTable = new DataTable();
                query = "SELECT ";
                foreach (string field in fields)
                {
                    query += field;
                    if (field!=fields.Last())
                    {
                        query += ", ";
                    }
                }
                query += " FROM " + table;
                command.CommandText = query;
                dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    dataTable.Load(dataReader);
                }
                else
                {
                    dataTable = null;
                }
                dataReader.Close();
                connection.Close();
                return dataTable;
            }
        }

        public static DataTable SelectFrom(string table, string where)
        {
            if (table == null || where == null)
            {
                throw new PrototypeException();
            }
            else
            {
                connection.Open();
                DataTable dataTable = new DataTable();
                command.CommandText = "SELECT * FROM " + table + " WHERE " + where;
                dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    dataTable.Load(dataReader);
                }
                else
                {
                    dataTable = null;
                }
                dataReader.Close();
                connection.Close();
                return dataTable;
            }
        }

        public static DataTable SelectFrom(string[] fields, string table, string where)
        {
            if (fields == null || table == null || where == null)
            {
                throw new PrototypeException();
            }
            else
            {
                connection.Open();
                DataTable dataTable = new DataTable();
                query = "SELECT ";
                foreach (string f in fields)
                {
                    query += f;
                    if (f != fields.Last())
                    {
                        query += ", ";
                    }
                }
                query += " FROM " + table + " WHERE " + where;
                command.CommandText = query;
                dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    dataTable.Load(dataReader);
                }
                else
                {
                    dataTable = null;
                }
                dataReader.Close();
                connection.Close();
                return dataTable;
            }
        }

        public static bool InsertInto(string table, string[] values)
        {
            if (table==null||values==null)
            {
                throw new PrototypeException();
            }
            else
            {
                bool isInserted = false;
                connection.Open();
                query = "INSERT INTO " + table + " VALUES (";
                foreach (string value in values)
                {
                    query +="'" +value +"'";
                    if (value != values.Last())
                    {
                        query += ", ";
                    }
                }
                query += ")";
                command.CommandText = query;
                if (command.ExecuteNonQuery() != 0)
                {
                    isInserted = true;
                }
                connection.Close();
                return isInserted;
            }
        }

        public static bool InsertInto(string table,string[] fields ,string[] values)
        {
            if (table == null || fields==null || values == null)
            {
                throw new PrototypeException();
            }
            else
            {
                if (fields.Length!=values.Length)
                {
                    throw new FieldsValuesDifferent();
                }
                else
                {
                    bool isInserted = false;
                    connection.Open();
                    query = "INSERT INTO " + table + " (";
                    foreach (string field in fields)
                    {
                        query += "'" + field + "'";
                        if (field != fields.Last())
                        {
                            query += ", ";
                        }
                    }
                    query += ") VALLUES (";
                    foreach (string value in values)
                    {
                        query += "'" + value + "'";
                        if (value != values.Last())
                        {
                            query += ", ";
                        }
                    }
                    query += ")";
                    command.CommandText = query;
                    if (command.ExecuteNonQuery() != 0)
                    {
                        isInserted = true;
                    }
                    connection.Close();
                    return isInserted;
                }
            }
        }
        public static bool DeleteFrom(string table)
        {
            bool isDeleted = false;
            if (table==null)
            {
                throw new PrototypeException();
            }
            else
            {
                connection.Open();
                query = "DELETE FROM " + table;
                command.CommandText = query;
                if (command.ExecuteNonQuery()!=0)
                {
                    isDeleted = true;
                }
                connection.Close();
                return isDeleted;
            }
        }

        public static bool DeleteFrom(string table,string where)
        {
            bool isDeleted = false;
            if (table == null || where==null)
            {
                throw new PrototypeException();
            }
            else
            {
                connection.Open();
                query = "DELETE FROM " + table + " WHERE " + where;
                command.CommandText = query;
                if (command.ExecuteNonQuery() != 0)
                {
                    isDeleted = true;
                }
                connection.Close();
                return isDeleted;
            }
        }

        public static bool UpdateTable(string table,string[] fields,string[] values,string where)
        {
            if (table==null||fields==null||values==null||where==null)
            {
                throw new PrototypeException();
            }
            else
            {
                if (fields.Length!=values.Length)
                {
                    throw new FieldsValuesDifferent();
                }
                else
                {
                    connection.Open();
                    int iter = 0;
                    bool isUpdated = false;
                    query += "UPDATE " + table + " SET ";
                    foreach (string field in fields)
                    {
                        query += field + "='" + values[iter] + "'";
                        if (field != fields.Last())
                        {
                            query += ", ";
                        }
                        iter++;
                    }
                    query += " WHERE " + where;
                    command.CommandText = query;
                    if (command.ExecuteNonQuery() != 0)
                    {
                        isUpdated = true;
                    }
                    connection.Close();
                    return isUpdated;
                }
            }
        }

        public static bool isTableVoid(string table)
        {
            if (table==null)
            {
                throw new PrototypeException();
            }
            else
            {
                bool isVoid = true;
                DataTable DT = SelectFrom(table);
                if (DT != null)
                {
                    isVoid = false;
                }
                return isVoid;
            }
        }

        public static bool isTableContains(string table, string where)
        {
            if (table == null || where==null)
            {
                throw new PrototypeException();
            }
            else
            {
                bool isContain = false;
                DataTable DT = SelectFrom(table, where);
                if (DT != null)
                {
                    isContain = true;
                }
                return isContain;
            }
            
        }
    }
}
