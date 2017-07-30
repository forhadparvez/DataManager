using System;
using System.Data;
using System.Data.SqlClient;

namespace DataManagerLibrary
{
    public class DataManager
    {
        /// <summary>
        /// ExecuteNonQuery for Insert, Update, Delete In Database
        /// </summary>
        /// <param name="query">Command Text</param>
        /// <param name="connectionString">Sql Connection String</param>
        /// <returns>Return 0 or 1</returns>
        public static int ExecuteNonQuery(string query, string connectionString)
        {
            var connection = new SqlConnection(connectionString);
            var command = new SqlCommand(query, connection);
            connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            return rowAffected;
        }


        /// <summary>
        /// SqlDataReader for Read Data From Database
        /// </summary>
        /// <param name="query">Command Text</param>
        /// <param name="connectionString">Sql Connection String</param>
        /// <returns>Return Database Table Row Data</returns>
        public static SqlDataReader SqlDataReader(string query, string connectionString)
        {
            var connection = new SqlConnection(connectionString);
            var command = new SqlCommand(query, connection);
            connection.Open();
            var reader = command.ExecuteReader();
            return reader;
        }

        /// <summary>
        /// DataTable for Read Data From Database
        /// </summary>
        /// <param name="query">Command Text</param>
        /// <param name="connectionString">Sql Connection String</param>
        /// <param name="tableName">Database Table Name Table Name</param>
        /// <returns></returns>
        public static DataTable DataTable(string query, string connectionString, string tableName)
        {
            var connection = new SqlConnection(connectionString);
            var dataAdapter = new SqlDataAdapter(query, connection);
            var dataSet = new DataSet();
            dataAdapter.Fill(dataSet, tableName);
            dataSet.Tables[0].TableName = tableName;
            return dataSet.Tables[0];
        }

        /// <summary>
        /// Transaction for Insert Row In database and Return A Row Cell Value
        /// </summary>
        /// <param name="connectionString">Sql Connection String</param>
        /// <param name="query1">Query For Insert Row</param>
        /// <param name="query2">Query For Get Cell Value</param>
        /// <returns></returns>
        public static int Transaction(string connectionString, string query1, string query2)
        {
            var connection = new SqlConnection(connectionString);
            SqlTransaction aTransaction;
            connection.Open();
            aTransaction = connection.BeginTransaction();


            var command1 = new SqlCommand(query1, connection, aTransaction);
            command1.ExecuteNonQuery();

            command1 = new SqlCommand(query2, connection, aTransaction);
            int mstId = Convert.ToInt32(command1.ExecuteScalar());

            aTransaction.Commit();
            return mstId;
        }
    }
}
