﻿using System;
using System.Data;
using System.Data.Common;
#if NET40_OR_GREATER
using System.Data.SqlClient;
#endif
#if NETSTANDARD2_0_OR_GREATER || NET8_0_OR_GREATER
using Microsoft.Data.SqlClient;
#endif

namespace SD.Toolkits.Sql.SqlServer
{
    /// <summary>
    /// SQL Server数据库访问助手
    /// </summary>
    public sealed class SqlServerHelper : ISqlHelper
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
        public SqlServerHelper(string connectionString)
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
            #region # 验证

            if (dataTable == null || dataTable.Rows.Count == 0)
            {
                return;
            }

            #endregion

            using (SqlConnection connection = this.CreateConnection())
            {
                connection.Open();

                //开启事务
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        //开启批量复制
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
                        {
                            bulkCopy.BatchSize = dataTable.Rows.Count;
                            bulkCopy.DestinationTableName = dataTable.TableName;

                            //列映射
                            foreach (DataColumn dataColumn in dataTable.Columns)
                            {
                                bulkCopy.ColumnMappings.Add(dataColumn.ColumnName, dataColumn.ColumnName);
                            }

                            //执行批量插入
                            bulkCopy.WriteToServer(dataTable);
                        }

                        //提交事务
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
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
            #region # 验证

            if (dataTable == null || dataTable.Rows.Count == 0)
            {
                return;
            }

            #endregion

            //开启事务
            using (DbTransaction dbTransaction = dbConnection.BeginTransaction())
            {
                try
                {
                    //开启批量复制
                    SqlConnection sqlConnection = (SqlConnection)dbConnection;
                    SqlTransaction sqlTransaction = (SqlTransaction)dbTransaction;
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.Default, sqlTransaction))
                    {
                        bulkCopy.BatchSize = dataTable.Rows.Count;
                        bulkCopy.DestinationTableName = dataTable.TableName;

                        //列映射
                        foreach (DataColumn dataColumn in dataTable.Columns)
                        {
                            bulkCopy.ColumnMappings.Add(dataColumn.ColumnName, dataColumn.ColumnName);
                        }

                        //执行批量插入
                        bulkCopy.WriteToServer(dataTable);
                    }

                    //提交事务
                    dbTransaction.Commit();
                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }
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
            #region # 验证

            if (dataTable == null || dataTable.Rows.Count == 0)
            {
                return;
            }

            #endregion

            try
            {
                //开启批量复制
                SqlConnection sqlConnection = (SqlConnection)dbConnection;
                SqlTransaction sqlTransaction = (SqlTransaction)dbTransaction;
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.Default, sqlTransaction))
                {
                    bulkCopy.BatchSize = dataTable.Rows.Count;
                    bulkCopy.DestinationTableName = dataTable.TableName;

                    //列映射
                    foreach (DataColumn dataColumn in dataTable.Columns)
                    {
                        bulkCopy.ColumnMappings.Add(dataColumn.ColumnName, dataColumn.ColumnName);
                    }

                    //执行批量插入
                    bulkCopy.WriteToServer(dataTable);
                }
            }
            catch
            {
                dbTransaction.Rollback();
                throw;
            }
        }
        #endregion

        #region # 批量复制 —— void BulkCopy(DataSet dataSet)
        /// <summary>
        /// 批量复制
        /// </summary>
        /// <param name="dataSet">数据集</param>
        public void BulkCopy(DataSet dataSet)
        {
            #region # 验证

            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                return;
            }

            #endregion

            using (SqlConnection connection = this.CreateConnection())
            {
                connection.Open();

                //开启事务
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        foreach (DataTable dataTable in dataSet.Tables)
                        {
                            if (dataTable.Rows.Count > 0)
                            {
                                //开启批量复制
                                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
                                {
                                    bulkCopy.BatchSize = dataTable.Rows.Count;
                                    bulkCopy.DestinationTableName = dataTable.TableName;

                                    //列映射
                                    foreach (DataColumn dataColumn in dataTable.Columns)
                                    {
                                        bulkCopy.ColumnMappings.Add(dataColumn.ColumnName, dataColumn.ColumnName);
                                    }

                                    //执行批量插入
                                    bulkCopy.WriteToServer(dataTable);
                                }
                            }
                        }

                        //提交事务
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
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
            #region # 验证

            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                return;
            }

            #endregion

            //开启事务
            using (DbTransaction dbTransaction = dbConnection.BeginTransaction())
            {
                try
                {
                    SqlConnection sqlConnection = (SqlConnection)dbConnection;
                    SqlTransaction sqlTransaction = (SqlTransaction)dbTransaction;
                    foreach (DataTable dataTable in dataSet.Tables)
                    {
                        if (dataTable.Rows.Count > 0)
                        {
                            //开启批量复制
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.Default, sqlTransaction))
                            {
                                bulkCopy.BatchSize = dataTable.Rows.Count;
                                bulkCopy.DestinationTableName = dataTable.TableName;

                                //列映射
                                foreach (DataColumn dataColumn in dataTable.Columns)
                                {
                                    bulkCopy.ColumnMappings.Add(dataColumn.ColumnName, dataColumn.ColumnName);
                                }

                                //执行批量插入
                                bulkCopy.WriteToServer(dataTable);
                            }
                        }
                    }

                    //提交事务
                    dbTransaction.Commit();
                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }
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
            #region # 验证

            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                return;
            }

            #endregion

            try
            {
                SqlConnection sqlConnection = (SqlConnection)dbConnection;
                SqlTransaction sqlTransaction = (SqlTransaction)dbTransaction;
                foreach (DataTable dataTable in dataSet.Tables)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        //开启批量复制
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.Default, sqlTransaction))
                        {
                            bulkCopy.BatchSize = dataTable.Rows.Count;
                            bulkCopy.DestinationTableName = dataTable.TableName;

                            //列映射
                            foreach (DataColumn dataColumn in dataTable.Columns)
                            {
                                bulkCopy.ColumnMappings.Add(dataColumn.ColumnName, dataColumn.ColumnName);
                            }

                            //执行批量插入
                            bulkCopy.WriteToServer(dataTable);
                        }
                    }
                }
            }
            catch
            {
                dbTransaction.Rollback();
                throw;
            }
        }
        #endregion


        //Private

        #region # 创建连接方法 —— SqlConnection CreateConnection()
        /// <summary>
        /// 创建连接方法
        /// </summary>
        /// <returns>连接对象</returns>
        private SqlConnection CreateConnection()
        {
            return new SqlConnection(this._connectionString);
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
            using (SqlConnection conn = this.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand(sql, conn)
                {
                    CommandType = type,
                    CommandTimeout = 600
                };

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
            using (SqlConnection conn = this.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand(sql, conn)
                {
                    CommandType = type,
                    CommandTimeout = 600
                };
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

            SqlConnection conn = this.CreateConnection();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn)
                {
                    CommandType = type,
                    CommandTimeout = 600
                };
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
            using (SqlConnection conn = this.CreateConnection())
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, conn) { SelectCommand = { CommandType = type } })
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
