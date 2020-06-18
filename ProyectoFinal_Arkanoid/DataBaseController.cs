using System.Data;
using Npgsql;

namespace Arkanoid
{
    public static class DataBaseController
    {
        private static readonly string connectionString = "Server=127.0.0.1;" +
                                                          "Port=5432;" +
                                                          "User Id=postgres;" +
                                                          "Password=carlos;" +
                                                          "Database=Arkanoid_BD;";

        public static DataTable ExecuteQuery(string query)
        {
            NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            DataSet ds = new DataSet();
            conn.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
            da.Fill(ds);
            conn.Close();
            return ds.Tables[0];
        }

        public static void ExecuteNonQuery(string query)
        {
            NpgsqlConnection conn = new NpgsqlConnection(connectionString);

            conn.Open();

            NpgsqlCommand comm = new NpgsqlCommand(query, conn);
            comm.ExecuteNonQuery();

            conn.Close();
        }
    }
}


