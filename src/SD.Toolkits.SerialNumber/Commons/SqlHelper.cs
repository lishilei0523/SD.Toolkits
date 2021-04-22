using System;
using System.Data;
using System.Data.SqlClient;

namespace SD.Toolkits.SerialNumber.Commons
{
    /// <summary>
    /// SQL Server数据库访问工具
    /// </summary>
    internal sealed class SqlHelper
    {
        #region # 字段及构造器

        /// <summary>
        /// 连接字符串字段
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        public SqlHelper(string connectionString)
        {
            #region # 验证

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString), @"连接字符串不可为空！");
            }

            #endregion

            this._connectionString = connectionString;
        }

        #endregion


        //Public

        #region # 执行SQL语句命令 —— int ExecuteNonQuery(string sql, params SqlParameter[] args)
        /// <summary>
        /// 执行SQL语句命令
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="args">参数</param>
        /// <returns>受影响的行数</returns>
        public int ExecuteNonQuery(string sql, params SqlParameter[] args)
        {
            return this.ExecuteNonQuery(sql, CommandType.Text, args);
        }
        #endregion

        #region # 执行SQL语句返回首行首列值 —— T ExecuteScalar<T>(string sql, params SqlParameter[] args)
        /// <summary>
        /// 执行SQL语句返回首行首列值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="sql">SQL语句</param>
        /// <param name="args">参数</param>
        /// <returns>类型对象</returns>
        public T ExecuteScalar<T>(string sql, params SqlParameter[] args)
        {
            return this.ExecuteScalar<T>(sql, CommandType.Text, args);
        }
        #endregion

        #region # 执行SQL语句返回DataReader —— SqlDataReader ExecuteReader(string sql, params SqlParameter[] args)
        /// <summary>
        /// 执行SQL语句返回DataReader
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="args">参数</param>
        /// <returns>DataReader对象</returns>
        public SqlDataReader ExecuteReader(string sql, params SqlParameter[] args)
        {
            return this.ExecuteReader(sql, CommandType.Text, args);
        }
        #endregion


        //Private

        #region # 创建连接 —— SqlConnection CreateConnection()
        /// <summary>
        /// 创建连接
        /// </summary>
        /// <returns>连接对象</returns>
        private SqlConnection CreateConnection()
        {
            return new SqlConnection(this._connectionString);
        }
        #endregion

        #region # ExecuteNonQuery方法 —— int ExecuteNonQuery(string sql, CommandType type, params SqlParameter[] args)
        /// <summary>
        /// ExecuteNonQuery方法
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="type">命令类型</param>
        /// <param name="args">参数</param>
        /// <returns>受影响的行数</returns>
        private int ExecuteNonQuery(string sql, CommandType type, params SqlParameter[] args)
        {
            #region # 验证参数

            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException(nameof(sql), @"SQL语句不可为空！");
            }

            #endregion

            int rowCount;
            using (SqlConnection conn = this.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand(sql, conn) { CommandType = type };
                cmd.Parameters.AddRange(args);
                conn.Open();
                rowCount = cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            return rowCount;
        }
        #endregion

        #region # ExecuteScalar —— object ExecuteScalar(string sql, CommandType type, params SqlParameter[] args)
        /// <summary>
        /// ExecuteScalar
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="type">命令类型</param>
        /// <param name="args">参数</param>
        /// <returns>object对象</returns>
        private object ExecuteScalar(string sql, CommandType type, params SqlParameter[] args)
        {
            #region # 验证参数

            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException(nameof(sql), @"SQL语句不可为空！");
            }

            #endregion

            object obj;
            using (SqlConnection conn = this.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand(sql, conn) { CommandType = type };
                cmd.Parameters.AddRange(args);
                conn.Open();
                obj = cmd.ExecuteScalar();
                cmd.Dispose();
            }
            return obj;
        }
        #endregion

        #region # ExecuteScalar —— T ExecuteScalar<T>(string sql, CommandType type, params SqlParameter[] args)
        /// <summary>
        /// ExecuteScalar
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="sql">SQL语句</param>
        /// <param name="type">命令类型</param>
        /// <param name="args">参数</param>
        /// <returns>类型对象</returns>
        private T ExecuteScalar<T>(string sql, CommandType type, params SqlParameter[] args)
        {
            try
            {
                return (T)this.ExecuteScalar(sql, type, args);
            }
            catch (InvalidCastException)
            {
                throw new InvalidCastException("给定类型有误，请重试！");
            }
        }
        #endregion

        #region # ExecuteReader —— SqlDataReader ExecuteReader(string sql, CommandType type, params SqlParameter[] args)
        /// <summary>
        /// ExecuteReader
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="type">命令类型</param>
        /// <param name="args">参数</param>
        /// <returns>DataReader对象</returns>
        private SqlDataReader ExecuteReader(string sql, CommandType type, params SqlParameter[] args)
        {
            #region # 验证参数

            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException(nameof(sql), @"SQL语句不可为空！");
            }

            #endregion

            SqlConnection conn = this.CreateConnection();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn) { CommandType = type };
                cmd.Parameters.AddRange(args);
                conn.Open();
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch
            {
                conn.Dispose();
                throw;
            }
        }
        #endregion
    }
}
