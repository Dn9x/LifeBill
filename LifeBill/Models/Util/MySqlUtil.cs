using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;

namespace LifeBill.Models.Util
{
    public class MySqlUtil
    {
        public DataSet GetDataSet(string connString, CommandType commandType, string commandText)
        {
            //pass through the call providing null for the set of OracleParameters//create & open an OracleConnection, and dispose of it after we are done.
            using (MySqlConnection cn = new MySqlConnection(connString))
            {
                cn.Open();

                //create a command and prepare it for execution
                MySqlCommand cmd = new MySqlCommand();

                PrepareCommand(cmd, cn, (MySqlTransaction)null, commandType, commandText, (MySqlParameter[])null);

                //create the DataAdapter & DataSet
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataSet ds = new DataSet();

                //fill the DataSet using default values for DataTable names, etc.
                da.Fill(ds);

                //return the dataset
                return ds;
            }
        }

        private static void PrepareCommand(MySqlCommand command, MySqlConnection connection, MySqlTransaction transaction, CommandType commandType, string commandText, MySqlParameter[] commandParameters)
        {
            //if the provided connection is not open, we will open it
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //associate the connection with the command
            command.Connection = connection;

            //set the command text (stored procedure name or Oracle statement)
            command.CommandText = commandText;

            //if we were provided a transaction, assign it.
            if (transaction != null)
            {
                command.Transaction = transaction;
            }

            //set the command type
            command.CommandType = commandType;

            //attach the command parameters if they are provided
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }

            return;
        }

        private static void AttachParameters(MySqlCommand command, MySqlParameter[] commandParameters)
        {
            foreach (MySqlParameter p in commandParameters)
            {
                //check for derived output value with no value assigned
                if ((p.Direction == ParameterDirection.InputOutput) && (p.Value == null))
                {
                    p.Value = DBNull.Value;
                }

                command.Parameters.Add(p);
            }
        }

        public string ExecuteSql(string conn, List<string> sqls)
        {
            MySqlConnection oraCon = new MySqlConnection(conn);

            MySqlCommand oraCmd = new MySqlCommand();

            oraCon.Open();

            MySqlTransaction oraTact = oraCon.BeginTransaction(IsolationLevel.ReadCommitted);

            oraCmd.Connection = oraCon;

            oraCmd.Transaction = oraTact;

            string result = String.Empty;

            try
            {
                for (int i = 0; i < sqls.Count; i++)
                {
                    //执行的sql
                    oraCmd.CommandText = sqls[i];

                    //执行操作
                    oraCmd.ExecuteNonQuery();
                }

                //提交事务
                oraTact.Commit();
                result = "1";
            }
            catch (Exception ex)
            {
                oraTact.Rollback();

                result = ex.Message;
            }
            finally
            {
                oraCon.Close();
            }

            return result;
        }
    
    }
}