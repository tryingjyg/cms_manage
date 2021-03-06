﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;

namespace LZOpenFaceDAL
{
    /// <summary>
    /// 基于MySQL的数据层基类
    /// </summary>
    /// <remarks>
    /// 参考于MS Petshop 4.0
    /// </remarks>
    public abstract class MySQLHelper
    {

        #region 数据库连接字符串
        public static readonly string DBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["LZBgAuthorityConnStr"].ToString();
        //public static readonly string DBConnectionStringMatch = System.Configuration.ConfigurationManager.ConnectionStrings["mysqlconntionMatch"].ToString();

        //public static readonly string DatabaseName = System.Configuration.ConfigurationManager.AppSettings["DatabaseName"].ToString();
        //public static readonly string DatabaseNameMatch = System.Configuration.ConfigurationManager.AppSettings["DatabaseNameMatch"].ToString();
        #endregion

        #region PrepareCommand
        /// <summary>
        /// Command预处理
        /// </summary>
        /// <param name="conn">MySqlConnection对象</param>
        /// <param name="trans">MySql.Data.MySqlClient.MySqlTransaction对象，可为null</param>
        /// <param name="cmd">MySql.Data.MySqlClient.MySqlCommand对象</param>
        /// <param name="cmdType">CommandType，存储过程或命令行</param>
        /// <param name="cmdText">SQL语句或存储过程名</param>
        /// <param name="cmdParms">MySql.Data.MySqlClient.MySqlCommand参数数组，可为null</param>
        private static void PrepareCommand(MySql.Data.MySqlClient.MySqlConnection conn, MySql.Data.MySqlClient.MySqlTransaction trans, MySql.Data.MySqlClient.MySqlCommand cmd, CommandType cmdType, string cmdText, MySql.Data.MySqlClient.MySqlParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;

            cmd.CommandText = cmdText;

            if (trans != null)

                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (MySql.Data.MySqlClient.MySqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }

        }

        #endregion

        #region ExecuteNonQuery

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="cmdType">命令类型（存储过程或SQL语句）</param>
        /// <param name="cmdText">SQL语句或存储过程名</param>
        /// <param name="cmdParms">MySql.Data.MySqlClient.MySqlCommand参数数组</param>
        /// <returns>返回受引响的记录行数</returns>
        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params MySql.Data.MySqlClient.MySqlParameter[] cmdParms)
        {

            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();
            cmd.CommandTimeout = 600;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {

                PrepareCommand(conn, null, cmd, cmdType, cmdText, cmdParms);

                int val = cmd.ExecuteNonQuery();

                cmd.Parameters.Clear();

                return val;

            }

        }

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="conn">Connection对象</param>
        /// <param name="cmdType">命令类型（存储过程或SQL语句）</param>
        /// <param name="cmdText">SQL语句或存储过程名</param>
        /// <param name="cmdParms">MySql.Data.MySqlClient.MySqlCommand参数数组</param>
        /// <returns>返回受引响的记录行数</returns>
        public static int ExecuteNonQuery(MySqlConnection conn, CommandType cmdType, string cmdText, params MySql.Data.MySqlClient.MySqlParameter[] cmdParms)
        {

            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();
            cmd.CommandTimeout = 600;

            PrepareCommand(conn, null, cmd, cmdType, cmdText, cmdParms);

            int val = cmd.ExecuteNonQuery();

            cmd.Parameters.Clear();

            return val;

        }

        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="trans">MySql.Data.MySqlClient.MySqlTransaction对象</param>
        /// <param name="cmdType">命令类型（存储过程或SQL语句）</param>
        /// <param name="cmdText">SQL语句或存储过程名</param>
        /// <param name="cmdParms">MySql.Data.MySqlClient.MySqlCommand参数数组</param>
        /// <returns>返回受引响的记录行数</returns>
        public static int ExecuteNonQuery(MySql.Data.MySqlClient.MySqlTransaction trans, CommandType cmdType, string cmdText, params MySql.Data.MySqlClient.MySqlParameter[] cmdParms)
        {

            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();
            cmd.CommandTimeout = 600;

            PrepareCommand(trans.Connection, trans, cmd, cmdType, cmdText, cmdParms);

            int val = cmd.ExecuteNonQuery();

            cmd.Parameters.Clear();

            return val;

        }

        #endregion

        #region ExecuteScalar

        /// <summary>
        /// 执行命令，返回第一行第一列的值
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="cmdType">命令类型（存储过程或SQL语句）</param>
        /// <param name="cmdText">SQL语句或存储过程名</param>
        /// <param name="cmdParms">MySql.Data.MySqlClient.MySqlCommand参数数组</param>
        /// <returns>返回Object对象</returns>
        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params MySql.Data.MySqlClient.MySqlParameter[] cmdParms)
        {

            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();
            cmd.CommandTimeout = 600;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {

                PrepareCommand(connection, null, cmd, cmdType, cmdText, cmdParms);

                object val = cmd.ExecuteScalar();

                cmd.Parameters.Clear();

                return val;

            }

        }

        /// <summary>
        /// 执行命令，返回第一行第一列的值
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="cmdType">命令类型（存储过程或SQL语句）</param>
        /// <param name="cmdText">SQL语句或存储过程名</param>
        /// <param name="cmdParms">MySql.Data.MySqlClient.MySqlCommand参数数组</param>
        /// <returns>返回Object对象</returns>
        public static object ExecuteScalar(MySqlConnection conn, CommandType cmdType, string cmdText, params MySql.Data.MySqlClient.MySqlParameter[] cmdParms)
        {

            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();
            cmd.CommandTimeout = 600;

            PrepareCommand(conn, null, cmd, cmdType, cmdText, cmdParms);

            object val = cmd.ExecuteScalar();

            cmd.Parameters.Clear();

            return val;

        }

        #endregion

        #region ExecuteReader

        /// <summary>
        /// 执行命令或存储过程，返回MySqlDataReader对象
        /// 注意MySqlDataReader对象使用完后必须Close以释放MySqlConnection资源
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="cmdType">命令类型（存储过程或SQL语句）</param>
        /// <param name="cmdText">SQL语句或存储过程名</param>
        /// <param name="cmdParms">MySql.Data.MySqlClient.MySqlCommand参数数组</param>
        /// <returns></returns>
        public static MySqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params MySql.Data.MySqlClient.MySqlParameter[] cmdParms)
        {

            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();
            cmd.CommandTimeout = 600;

            MySqlConnection conn = new MySqlConnection(connectionString);

            try
            {

                PrepareCommand(conn, null, cmd, cmdType, cmdText, cmdParms);

                MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                cmd.Parameters.Clear();

                return dr;

            }

            catch
            {

                conn.Close();

                throw;

            }

        }

        #endregion

        #region ExecuteDataSet

        /// <summary>
        /// 执行命令或存储过程，返回DataSet对象
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="cmdType">命令类型(存储过程或SQL语句)</param>
        /// <param name="cmdText">SQL语句或存储过程名</param>
        /// <param name="cmdParms">MySql.Data.MySqlClient.MySqlCommand参数数组(可为null值)</param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(string connectionString, CommandType cmdType, string cmdText, params MySql.Data.MySqlClient.MySqlParameter[] cmdParms)
        {

            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();
            cmd.CommandTimeout = 600;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                PrepareCommand(conn, null, cmd, cmdType, cmdText, cmdParms);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataSet ds = new DataSet();

                da.Fill(ds);

                conn.Close();

                cmd.Parameters.Clear();

                return ds;

            }

        }

        #endregion

    }//end class
}
