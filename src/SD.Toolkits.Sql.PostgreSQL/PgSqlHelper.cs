using Npgsql;
using System;
using System.Data;
using System.Data.Common;

namespace SD.Toolkits.Sql.PostgreSQL
{
    /// <summary>
    /// PostgreSQL数据库访问助手
    /// </summary>
    public sealed class PgSqlHelper : ISqlHelper
    {
        #region # 字段及构造器

        /// <summary>
        /// 连接字符串
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        public PgSqlHelper(string connectionString)
        {
            #region # 验证

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString), "连接字符串不可为空！");
            }

            #endregion

            this._connectionString = connectionString;
        }

        #endregion


        //Public

        #region # 执行SQL语句命令 —— int ExecuteNonQuery(string sql, params IDbDataParameter[] args)
        /// <summary>
        /// ExecuteNonQuery —— Sql语句
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="args">参数</param>
        /// <returns>受影响的行数</returns>
        public int ExecuteNonQuery(string sql, params IDbDataParameter[] args)
        {
            return this.ExecuteNonQuery(sql, CommandType.Text, args);
        }
        #endregion

        #region # 执行存储过程命令 —— int ExecuteNonQuerySP(string proc, params IDbDataParameter[] args)
        /// <summary>
        /// ExecuteNonQuery —— 存储过程
        /// </summary>
        /// <param name="proc">存储过程名称</param>
        /// <param name="args">参数</param>
        /// <returns>受影响的行数</returns>
        public int ExecuteNonQuerySP(string proc, params IDbDataParameter[] args)
        {
            return this.ExecuteNonQuery(proc, CommandType.StoredProcedure, args);
        }
        #endregion

        #region # 执行SQL语句返回首行首列值 —— object ExecuteScalar(string sql, params IDbDataParameter[] args)
        /// <summary>
        /// ExecuteScalar —— Sql语句
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="args">参数</param>
        /// <returns>object对象</returns>
        public object ExecuteScalar(string sql, params IDbDataParameter[] args)
        {
            return this.ExecuteScalar(sql, CommandType.Text, args);
        }
        #endregion

        #region # 执行存储过程返回首行首列值 —— object ExecuteScalarSP(string proc, params IDbDataParameter[] args)
        /// <summary>
        /// ExecuteScalar —— 存储过程
        /// </summary>
        /// <param name="proc">存储过程名称</param>
        /// <param name="args">参数</param>
        /// <returns>object对象</returns>
        public object ExecuteScalarSP(string proc, params IDbDataParameter[] args)
        {
            return this.ExecuteScalar(proc, CommandType.StoredProcedure, args);
        }
        #endregion

        #region # 执行SQL语句返回DataReader —— IDataReader ExecuteReader(string sql, params IDbDataParameter[] args)
        /// <summary>
        /// ExecuteReader —— Sql语句
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="args">参数</param>
        /// <returns>DataReader对象</returns>
        public IDataReader ExecuteReader(string sql, params IDbDataParameter[] args)
        {
            return this.ExecuteReader(sql, CommandType.Text, args);
        }
        #endregion

        #region # 执行存储过程返回DataReader —— IDataReader ExecuteReaderSP(string proc, params IDbDataParameter[] args)
        /// <summary>
        /// ExecuteReader —— 存储过程
        /// </summary>
        /// <param name="proc">存储过程名称</param>
        /// <param name="args">参数</param>
        /// <returns>DataReader对象</returns>
        public IDataReader ExecuteReaderSP(string proc, params IDbDataParameter[] args)
        {
            return this.ExecuteReader(proc, CommandType.StoredProcedure, args);
        }
        #endregion

        #region # 执行SQL语句返回DataTable —— DataTable GetDataTable(string sql, params IDbDataParameter[] args)
        /// <summary>
        /// GetDataTable —— Sql语句
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="args">参数</param>
        /// <returns>DataTable对象</returns>
        public DataTable GetDataTable(string sql, params IDbDataParameter[] args)
        {
            return this.GetDataTable(sql, CommandType.Text, args);
        }
        #endregion

        #region # 执行存储过程返回DataTable —— DataTable GetDataTableSP(string proc, params IDbDataParameter[] args)
        /// <summary>
        /// GetDataTable —— 存储过程
        /// </summary>
        /// <param name="proc">存储过程名称</param>
        /// <param name="args">参数</param>
        /// <returns>DataTable对象</returns>
        public DataTable GetDataTableSP(string proc, params IDbDataParameter[] args)
        {
            return this.GetDataTable(proc, CommandType.StoredProcedure, args);
        }
        #endregion

        #region # 批量复制 —— void BulkCopy(DataTable dataTable)
        /// <summary>
        /// 批量复制
        /// </summary>
        /// <param name="dataTable">数据表</param>
        public void BulkCopy(DataTable dataTable)
        {
            throw new NotImplementedException("暂时未实现");
        }
        #endregion

        #region # 批量复制 —— void BulkCopy(DataTable dataTable, DbConnection dbConnection)
        /// <summary>
        /// 批量复制
        /// </summary>
        /// <param name="dataTable">数据表</param>
        /// <param name="dbConnection">数据库连接</param>
        public void BulkCopy(DataTable dataTable, DbConnection dbConnection)
        {
            throw new NotImplementedException("暂时未实现");
        }
        #endregion

        #region # 批量复制 —— void BulkCopy(DataTable dataTable, DbConnection dbConnection, DbTransaction dbTransaction)
        /// <summary>
        /// 批量复制
        /// </summary>
        /// <param name="dataTable">数据表</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        public void BulkCopy(DataTable dataTable, DbConnection dbConnection, DbTransaction dbTransaction)
        {
            throw new NotImplementedException("暂时未实现");
        }
        #endregion

        #region # 批量复制 —— void BulkCopy(DataSet dataSet)
        /// <summary>
        /// 批量复制
        /// </summary>
        /// <param name="dataSet">数据集</param>
        public void BulkCopy(DataSet dataSet)
        {
            throw new NotImplementedException("暂时未实现");
        }
        #endregion

        #region # 批量复制 —— void BulkCopy(DataSet dataSet, DbConnection dbConnection)
        /// <summary>
        /// 批量复制
        /// </summary>
        /// <param name="dataSet">数据集</param>
        /// <param name="dbConnection">数据库连接</param>
        public void BulkCopy(DataSet dataSet, DbConnection dbConnection)
        {
            throw new NotImplementedException("暂时未实现");
        }
        #endregion

        #region # 批量复制 —— void BulkCopy(DataSet dataSet, DbConnection dbConnection, DbTransaction dbTransaction)
        /// <summary>
        /// 批量复制
        /// </summary>
        /// <param name="dataSet">数据集</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        public void BulkCopy(DataSet dataSet, DbConnection dbConnection, DbTransaction dbTransaction)
        {
            throw new NotImplementedException("暂时未实现");
        }
        #endregion


        //Private

        #region # 创建连接方法 —— NpgsqlConnection CreateConnection()
        /// <summary>
        /// 创建连接方法
        /// </summary>
        /// <returns>连接对象</returns>
        private NpgsqlConnection CreateConnection()
        {
            return new NpgsqlConnection(this._connectionString);
        }
        #endregion

        #region # ExecuteNonQuery方法 —— int ExecuteNonQuery(string sql, CommandType type, params IDbDataParameter[] args)
        /// <summary>
        /// ExecuteNonQuery方法
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="type">命令类型</param>
        /// <param name="args">参数</param>
        /// <returns>受影响的行数</returns>
        private int ExecuteNonQuery(string sql, CommandType type, params IDbDataParameter[] args)
        {
            #region # 验证参数

            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException(nameof(sql), @"SQL语句不可为空！");
            }

            #endregion

            int rowCount;
            using (NpgsqlConnection conn = this.CreateConnection())
            {
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn) { CommandType = type };
                cmd.Parameters.AddRange(args);
                conn.Open();
                rowCount = cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            return rowCount;
        }
        #endregion

        #region # ExecuteScalar方法 —— object ExecuteScalar(string sql, CommandType type, params IDbDataParameter[] args)
        /// <summary>
        /// ExecuteScalar方法
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="type">命令类型</param>
        /// <param name="args">参数</param>
        /// <returns>object对象</returns>
        private object ExecuteScalar(string sql, CommandType type, params IDbDataParameter[] args)
        {
            #region # 验证参数

            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException(nameof(sql), @"SQL语句不可为空！");
            }

            #endregion

            object obj;
            using (NpgsqlConnection conn = this.CreateConnection())
            {
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn) { CommandType = type };
                cmd.Parameters.AddRange(args);
                conn.Open();
                obj = cmd.ExecuteScalar();
                cmd.Dispose();
            }
            return obj;
        }
        #endregion

        #region # ExecuteReader方法 —— IDataReader ExecuteReader(string sql, CommandType type, params IDbDataParameter[] args)
        /// <summary>
        /// ExecuteReader方法
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="type">命令类型</param>
        /// <param name="args">参数</param>
        /// <returns>DataReader对象</returns>
        private IDataReader ExecuteReader(string sql, CommandType type, params IDbDataParameter[] args)
        {
            #region # 验证参数

            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException(nameof(sql), @"SQL语句不可为空！");
            }

            #endregion

            NpgsqlConnection conn = this.CreateConnection();
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn) { CommandType = type };
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

        #region # 返回DataTable方法 —— DataTable GetDataTable(string sql, CommandType type, params IDbDataParameter[] args)
        /// <summary>
        /// 返回DataTable方法
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="type">命令类型</param>
        /// <param name="args">参数</param>
        /// <returns>DataTable对象</returns>
        private DataTable GetDataTable(string sql, CommandType type, params IDbDataParameter[] args)
        {
            #region # 验证参数

            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException(nameof(sql), @"SQL语句不可为空！");
            }

            #endregion

            DataTable dataTable = new DataTable();
            using (NpgsqlConnection conn = this.CreateConnection())
            {
                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql, conn) { SelectCommand = { CommandType = type } })
                {
                    adapter.SelectCommand.Parameters.AddRange(args);
                    conn.Open();
                    adapter.Fill(dataTable);
                }
            }

            return dataTable;
        }
        #endregion
    }
}
