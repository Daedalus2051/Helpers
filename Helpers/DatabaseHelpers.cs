using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Helpers
{

    public class DatabaseHelpers
    {
        /// <summary>
        /// Builds a connection string based on the parameters specified.
        /// </summary>
        /// <param name="serverName">Name of the database server.</param>
        /// <param name="databaseName">Name of the initial database to connect to.</param>
        /// <param name="integratedSecurity">Use Windows Authentication with connection.</param>
        /// <param name="userName">UserId of the SQL account, integratedSecurity must be set to false.</param>
        /// <param name="password">Password for the SQL account, integratedSecurity must be set to false.</param>
        /// <returns>Returns standard Sql connection string based on the parameters provided.</returns>
        public static string ConnectionStringBuilder(string serverName, string databaseName, bool integratedSecurity = true, string userName = "", string password = "")
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = serverName,
                InitialCatalog = databaseName,
                IntegratedSecurity = integratedSecurity
            };

            // No integrated security, plus username and password are not empty
            if ((integratedSecurity == false) && (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password)))
            {
                builder.UserID = userName;
                builder.Password = password;
            }
            
            return builder.ConnectionString;
        }

        /// <summary>
        /// Runs a stored procedure on the database specified in the connection string.
        /// </summary>
        /// <param name="sqlConnection">Connection against which the stored procedure will be run.</param>
        /// <param name="procedureName">Name of the stored procedure.</param>
        /// <param name="parameterCollection">Collection of SqlParameters to be passed into the stored procedure (can be null for none).</param>
        /// <returns>Returns the results in a System.Data.DataTable.</returns>
        public static DataTable ExecuteStoredProcedure(SqlConnection sqlConnection, string procedureName, List<SqlParameter> parameterCollection = null)
        {
            using (SqlConnection procConnection = sqlConnection)
            {
                procConnection.Open();
                SqlCommand command = new SqlCommand
                {
                    Connection = procConnection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = procedureName
                };

                if (parameterCollection != null)
                {
                    foreach (SqlParameter parm in parameterCollection)
                    {
                        command.Parameters.Add(parm);
                    }
                }

                var reader = command.ExecuteReader();
                var datTable = new DataTable();
                datTable.Load(reader);

                return datTable;
            }
        }
        /// <summary>
        /// Runs a stored procedure on the database specified in the connection string.
        /// </summary>
        /// <param name="connectionString">Standard Sql connection string used to connect to the database.</param>
        /// <param name="procedureName">Name of the stored procedure.</param>
        /// <param name="sqlParameters">Collection of SqlParameters to be passed into the stored procedure (can be null for none).</param>
        /// <returns>Returns the results in a System.Data.DataTable.</returns>
        public static DataTable ExecuteStoredProcedure(string connectionString, string procedureName, List<SqlParameter> sqlParameters = null)
        {
            return ExecuteStoredProcedure(new SqlConnection(connectionString), procedureName, sqlParameters);
        }
        /// <summary>
        /// Runs a stored procedure on the database specified in the connection string.
        /// </summary>
        /// <param name="connectionString">Standard Sql connection string used to connect to the database.</param>
        /// <param name="procedureName">Name of the stored procedure.</param>
        /// <param name="sqlParameter">Single Sql parameter to be passed to the stored procedure.</param>
        /// <returns></returns>
        public static DataTable ExecuteStoredProcedure(string connectionString, string procedureName, SqlParameter sqlParameter = null)
        {
            List<SqlParameter> temp = new List<SqlParameter> { sqlParameter };
            return ExecuteStoredProcedure(connectionString, procedureName, temp);
        }

        /// <summary>
        /// Executes a single SQL statement.
        /// </summary>
        /// <param name="sqlCommand">The SqlCommand object to be used in the execution.</param>
        /// <returns>Returns the number of rows affected, or -1 if the execution failed.</returns>
        public static int ExecuteSql(SqlCommand sqlCommand)
        {
            int results = -1;
            using (sqlCommand.Connection)
            {
                sqlCommand.Connection.Open();
                results = sqlCommand.ExecuteNonQuery();
            }
            return results;
        }
        /// <summary>
        /// Executes a single SQL statement.
        /// </summary>
        /// <param name="connectionString">Standard SQL connection string.</param>
        /// <param name="sqlCommand">The SqlCommand object to be used in the execution.</param>
        /// <returns>Returns the number of rows affected, or -1 if the execution failed.</returns>
        /// <returns></returns>
        public static int ExecuteSql(string connectionString, SqlCommand sqlCommand)
        {
            sqlCommand.Connection = new SqlConnection(connectionString);
            return ExecuteSql(sqlCommand);
        }

        /// <summary>
        /// Runs a SQL Command and returns a DataTable.
        /// </summary>
        /// <param name="sqlCommand">SQL Command to be executed.</param>
        /// <returns></returns>
        public static DataTable GetQueryResult(SqlCommand sqlCommand)
        {
            DataTable resultTable = new DataTable();

            using (sqlCommand.Connection)
            {
                sqlCommand.Connection.Open();
                resultTable.Load(sqlCommand.ExecuteReader());
            }
            return resultTable;
        }
    }
}
