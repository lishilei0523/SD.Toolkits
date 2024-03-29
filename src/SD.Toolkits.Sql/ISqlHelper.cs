﻿using System.Data;
using System.Data.Common;

namespace SD.Toolkits.Sql
{
    /// <summary>
    /// SQL数据库访问助手接口
    /// </summary>
    public interface ISqlHelper
    {
        #region # 执行SQL语句命令 —— int ExecuteNonQuery(string sql, params IDbDataParameter[] args)
        /// <summary>
        /// ExecuteNonQuery —— Sql语句
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="args">参数</param>
        /// <returns>受影响的行数</returns>
        int ExecuteNonQuery(string sql, params IDbDataParameter[] args);
        #endregion

        #region # 执行存储过程命令 —— int ExecuteNonQuerySP(string proc, params IDbDataParameter[] args)
        /// <summary>
        /// ExecuteNonQuery —— 存储过程
        /// </summary>
        /// <param name="proc">存储过程名称</param>
        /// <param name="args">参数</param>
        /// <returns>受影响的行数</returns>
        int ExecuteNonQuerySP(string proc, params IDbDataParameter[] args);
        #endregion

        #region # 执行SQL语句返回首行首列值 —— object ExecuteScalar(string sql, params IDbDataParameter[] args)
        /// <summary>
        /// ExecuteScalar —— Sql语句
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="args">参数</param>
        /// <returns>object对象</returns>
        object ExecuteScalar(string sql, params IDbDataParameter[] args);
        #endregion

        #region # 执行存储过程返回首行首列值 —— object ExecuteScalarSP(string proc, params IDbDataParameter[] args)
        /// <summary>
        /// ExecuteScalar —— 存储过程
        /// </summary>
        /// <param name="proc">存储过程名称</param>
        /// <param name="args">参数</param>
        /// <returns>object对象</returns>
        object ExecuteScalarSP(string proc, params IDbDataParameter[] args);
        #endregion

        #region # 执行SQL语句返回DataReader —— IDataReader ExecuteReader(string sql, params IDbDataParameter[] args)
        /// <summary>
        /// ExecuteReader —— Sql语句
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="args">参数</param>
        /// <returns>DataReader对象</returns>
        IDataReader ExecuteReader(string sql, params IDbDataParameter[] args);
        #endregion

        #region # 执行存储过程返回DataReader —— IDataReader ExecuteReaderSP(string proc, params IDbDataParameter[] args)
        /// <summary>
        /// ExecuteReader —— 存储过程
        /// </summary>
        /// <param name="proc">存储过程名称</param>
        /// <param name="args">参数</param>
        /// <returns>DataReader对象</returns>
        IDataReader ExecuteReaderSP(string proc, params IDbDataParameter[] args);
        #endregion

        #region # 执行SQL语句返回DataTable —— DataTable GetDataTable(string sql, params IDbDataParameter[] args)
        /// <summary>
        /// GetDataTable —— Sql语句
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="args">参数</param>
        /// <returns>DataTable对象</returns>
        DataTable GetDataTable(string sql, params IDbDataParameter[] args);
        #endregion

        #region # 执行存储过程返回DataTable —— DataTable GetDataTableSP(string proc, params IDbDataParameter[] args)
        /// <summary>
        /// GetDataTable —— 存储过程
        /// </summary>
        /// <param name="proc">存储过程名称</param>
        /// <param name="args">参数</param>
        /// <returns>DataTable对象</returns>
        DataTable GetDataTableSP(string proc, params IDbDataParameter[] args);
        #endregion

        #region # 批量复制 —— void BulkCopy(DataTable dataTable)
        /// <summary>
        /// 批量复制
        /// </summary>
        /// <param name="dataTable">数据表</param>
        void BulkCopy(DataTable dataTable);
        #endregion

        #region # 批量复制 —— void BulkCopy(DataTable dataTable, DbConnection dbConnection)
        /// <summary>
        /// 批量复制
        /// </summary>
        /// <param name="dataTable">数据表</param>
        /// <param name="dbConnection">数据库连接</param>
        void BulkCopy(DataTable dataTable, DbConnection dbConnection);
        #endregion

        #region # 批量复制 —— void BulkCopy(DataTable dataTable, DbConnection dbConnection, DbTransaction dbTransaction)
        /// <summary>
        /// 批量复制
        /// </summary>
        /// <param name="dataTable">数据表</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        void BulkCopy(DataTable dataTable, DbConnection dbConnection, DbTransaction dbTransaction);
        #endregion

        #region # 批量复制 —— void BulkCopy(DataSet dataSet)
        /// <summary>
        /// 批量复制
        /// </summary>
        /// <param name="dataSet">数据集</param>
        void BulkCopy(DataSet dataSet);
        #endregion

        #region # 批量复制 —— void BulkCopy(DataSet dataSet, DbConnection dbConnection)
        /// <summary>
        /// 批量复制
        /// </summary>
        /// <param name="dataSet">数据集</param>
        /// <param name="dbConnection">数据库连接</param>
        void BulkCopy(DataSet dataSet, DbConnection dbConnection);
        #endregion

        #region # 批量复制 —— void BulkCopy(DataSet dataSet, DbConnection dbConnection, DbTransaction dbTransaction)
        /// <summary>
        /// 批量复制
        /// </summary>
        /// <param name="dataSet">数据集</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        void BulkCopy(DataSet dataSet, DbConnection dbConnection, DbTransaction dbTransaction);
        #endregion
    }
}
