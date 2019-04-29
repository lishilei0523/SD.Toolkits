using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SD.Common.PoweredByLee
{
    /// <summary>
    /// SQL Server数据库访问助手类
    /// </summary>
    public sealed class SqlHelper
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
            #region # 验证参数

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException("connectionString", @"连接字符串不可为空！");
            }

            #endregion

            this._connectionString = connectionString;
        }

        #endregion


        //Public

        #region # 执行SQL语句命令 —— int ExecuteNonQuery(string sql, params SqlParameter[] args)
        /// <summary>
        /// ExecuteNonQuery —— Sql语句
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="args">参数</param>
        /// <returns>受影响的行数</returns>
        public int ExecuteNonQuery(string sql, params SqlParameter[] args)
        {
            return this.ExecuteNonQuery(sql, CommandType.Text, args);
        }
        #endregion

        #region # 执行存储过程命令 —— int ExecuteNonQuerySP(string proc, params SqlParameter[] args)
        /// <summary>
        /// ExecuteNonQuery —— 存储过程
        /// </summary>
        /// <param name="proc">存储过程名称</param>
        /// <param name="args">参数</param>
        /// <returns>受影响的行数</returns>
        public int ExecuteNonQuerySP(string proc, params SqlParameter[] args)
        {
            return this.ExecuteNonQuery(proc, CommandType.StoredProcedure, args);
        }
        #endregion

        #region # 执行SQL语句返回首行首列值 —— object ExecuteScalar(string sql, params SqlParameter[] args)
        /// <summary>
        /// ExecuteScalar —— Sql语句
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="args">参数</param>
        /// <returns>object对象</returns>
        public object ExecuteScalar(string sql, params SqlParameter[] args)
        {
            return this.ExecuteScalar(sql, CommandType.Text, args);
        }
        #endregion

        #region # 执行SQL语句返回首行首列值（泛型） —— T ExecuteScalar<T>(string sql, params SqlParameter[] args)
        /// <summary>
        /// ExecuteScalar —— Sql语句
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="sql">Sql语句</param>
        /// <param name="args">参数</param>
        /// <returns>类型对象</returns>
        public T ExecuteScalar<T>(string sql, params SqlParameter[] args)
        {
            return this.ExecuteScalar<T>(sql, CommandType.Text, args);
        }
        #endregion

        #region # 执行存储过程返回首行首列值 —— object ExecuteScalarSP(string proc, params SqlParameter[] args)
        /// <summary>
        /// ExecuteScalar —— 存储过程
        /// </summary>
        /// <param name="proc">存储过程名称</param>
        /// <param name="args">参数</param>
        /// <returns>object对象</returns>
        public object ExecuteScalarSP(string proc, params SqlParameter[] args)
        {
            return this.ExecuteScalar(proc, CommandType.StoredProcedure, args);
        }
        #endregion

        #region # 执行存储过程返回首行首列值（泛型） —— T ExecuteScalarSP<T>(string proc, params SqlParameter[] args)
        /// <summary>
        /// ExecuteScalar —— 存储过程
        /// </summary>
        /// <param name="proc">存储过程名称</param>
        /// <param name="args">参数</param>
        /// <returns>类型对象</returns>
        public T ExecuteScalarSP<T>(string proc, params SqlParameter[] args)
        {
            return this.ExecuteScalar<T>(proc, CommandType.StoredProcedure, args);
        }
        #endregion

        #region # 执行SQL语句返回DataReader —— SqlDataReader ExecuteReader(string sql, params SqlParameter[] args)
        /// <summary>
        /// ExecuteReader —— Sql语句
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="args">参数</param>
        /// <returns>DataReader对象</returns>
        public SqlDataReader ExecuteReader(string sql, params SqlParameter[] args)
        {
            return this.ExecuteReader(sql, CommandType.Text, args);
        }
        #endregion

        #region # 执行存储过程返回DataReader —— SqlDataReader ExecuteReaderSP(string proc, params SqlParameter[] args)
        /// <summary>
        /// ExecuteReader —— 存储过程
        /// </summary>
        /// <param name="proc">存储过程名称</param>
        /// <param name="args">参数</param>
        /// <returns>DataReader对象</returns>
        public SqlDataReader ExecuteReaderSP(string proc, params SqlParameter[] args)
        {
            return this.ExecuteReader(proc, CommandType.StoredProcedure, args);
        }
        #endregion

        #region # 执行SQL语句返回DataTable —— DataTable GetDataTable(string sql, params SqlParameter[] args)
        /// <summary>
        /// GetDataTable —— Sql语句
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="args">参数</param>
        /// <returns>DataTable对象</returns>
        public DataTable GetDataTable(string sql, params SqlParameter[] args)
        {
            return this.GetDataTable(sql, CommandType.Text, args);
        }
        #endregion

        #region # 执行存储过程返回DataTable —— DataTable GetDataTableSP(string proc, params SqlParameter[] args)
        /// <summary>
        /// GetDataTable —— 存储过程
        /// </summary>
        /// <param name="proc">存储过程名称</param>
        /// <param name="args">参数</param>
        /// <returns>DataTable对象</returns>
        public DataTable GetDataTableSP(string proc, params SqlParameter[] args)
        {
            return this.GetDataTable(proc, CommandType.StoredProcedure, args);
        }
        #endregion

        #region # 执行SQL语句返回DataSet —— DataSet GetDataSet(string sql, params SqlParameter[] args)
        /// <summary>
        /// GetDataSet —— Sql语句
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="args">参数</param>
        /// <returns>DataSet对象</returns>
        public DataSet GetDataSet(string sql, params SqlParameter[] args)
        {
            return this.GetDataSet(sql, CommandType.Text, args);
        }
        #endregion

        #region # 执行存储过程返回DataSet —— DataSet GetDataSetSP(string proc, params SqlParameter[] args)
        /// <summary>
        /// GetDataSet —— 存储过程
        /// </summary>
        /// <param name="proc">存储过程名称</param>
        /// <param name="args">参数</param>
        /// <returns>DataSet对象</returns>
        public DataSet GetDataSetSP(string proc, params SqlParameter[] args)
        {
            return this.GetDataSet(proc, CommandType.StoredProcedure, args);
        }
        #endregion

        #region # 执行SQL语句返回泛型集合 —— IList<T> GetList<T>(string sql...
        /// <summary>
        /// GetDataTable —— Sql语句
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="args">参数</param>
        /// <returns>泛型集合</returns>
        public IList<T> GetList<T>(string sql, params SqlParameter[] args)
        {
            return this.GetDataTable(sql, CommandType.Text, args).ToList<T>();
        }
        #endregion

        #region # 执行存储过程返回泛型集合 —— IList<T> GetListSP<T>(string proc...
        /// <summary>
        /// GetDataTable —— 存储过程
        /// </summary>
        /// <param name="proc">存储过程名称</param>
        /// <param name="args">参数</param>
        /// <returns>泛型集合</returns>
        public IList<T> GetListSP<T>(string proc, params SqlParameter[] args)
        {
            return this.GetDataTable(proc, CommandType.StoredProcedure, args).ToList<T>();
        }
        #endregion

        #region # 分页执行SQL语句返回泛型集合 —— IList<T> GetListByPage<T>(string sql...
        /// <summary>
        /// 分页执行SQL语句返回泛型集合
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="fieldSelectors">字段选择器</param>
        /// <param name="keySelectors">排序键选择器</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="rowCount">总记录条数</param>
        /// <param name="pageCount">总页数</param>
        /// <returns>泛型集合</returns>
        public IList<T> GetListByPage<T>(string sql, IList<string> fieldSelectors, IDictionary<string, bool> keySelectors, int pageIndex, int pageSize, out int rowCount, out int pageCount) where T : class
        {
            #region # 验证

            fieldSelectors = fieldSelectors ?? new List<string>();
            keySelectors = keySelectors ?? new Dictionary<string, bool>();

            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException(nameof(sql), "SQL语句不可为空！");
            }
            if (!keySelectors.Any())
            {
                throw new ArgumentNullException(nameof(keySelectors), "排序键选择器不可为空！");
            }
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }
            if (pageSize < 1)
            {
                pageSize = 1;
            }

            #endregion

            const string totalFields = "*";
            const string separator = ",";

            IEnumerable<string> sortSelectors =
                from keySelector in keySelectors
                let sortMode = keySelector.Value ? "ASC" : "DESC"
                select $"{keySelector.Key} {sortMode}";

            string fields = fieldSelectors.Any() ? string.Join(separator, fieldSelectors) : totalFields;
            string sorts = string.Join(separator, sortSelectors);

            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = (pageIndex - 1) * pageSize + pageSize;

            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append($"WITH Buffer AS ");
            sqlBuilder.Append($"( ");
            sqlBuilder.Append($"SELECT ROW_NUMBER() OVER (ORDER BY {sorts}) AS RowIndex, {fields} FROM ({sql}) ");
            sqlBuilder.Append($") ");
            sqlBuilder.Append($"SELECT * FROM Buffer ");
            sqlBuilder.Append($"WHERE RowIndex BETWEEN {startIndex} AND {endIndex} ");

            rowCount = this.ExecuteScalar<int>($"SELECT COUNT(*) FROM ({sql}) ");
            pageCount = (int)Math.Ceiling(rowCount * 1.0 / pageSize);

            IList<T> list = this.GetList<T>(sqlBuilder.ToString());

            return list;
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

        #region # ExecuteNonQuery方法 —— int ExecuteNonQuery(string sql, CommandType type, params SqlParameter[] args)
        /// <summary>
        /// ExecuteNonQuery方法
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="type">命令类型</param>
        /// <param name="args">参数</param>
        /// <returns>受影响的行数</returns>
        private int ExecuteNonQuery(string sql, CommandType type, params SqlParameter[] args)
        {
            #region # 验证参数

            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException("sql", @"SQL语句不可为空！");
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

        #region # ExecuteScalar方法 —— object ExecuteScalar(string sql, CommandType type, params SqlParameter[] args)
        /// <summary>
        /// ExecuteScalar方法
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="type">命令类型</param>
        /// <param name="args">参数</param>
        /// <returns>object对象</returns>
        private object ExecuteScalar(string sql, CommandType type, params SqlParameter[] args)
        {
            #region # 验证参数

            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException("sql", @"SQL语句不可为空！");
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

        #region # ExecuteScalar方法 —— T ExecuteScalar<T>(string sql, CommandType type, params SqlParameter[] args)
        /// <summary>
        /// ExecuteScalar方法
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="sql">Sql语句</param>
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
                throw new InvalidCastException(string.Format("返回值不可转换为给定类型{0}，请检查程序！", typeof(T).Name));
            }
        }
        #endregion

        #region # ExecuteReader方法 —— SqlDataReader ExecuteReader(string sql, CommandType type, params SqlParameter[] args)
        /// <summary>
        /// ExecuteReader方法
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="type">命令类型</param>
        /// <param name="args">参数</param>
        /// <returns>DataReader对象</returns>
        private SqlDataReader ExecuteReader(string sql, CommandType type, params SqlParameter[] args)
        {
            #region # 验证参数

            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException("sql", @"SQL语句不可为空！");
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

        #region # 返回DataTable方法 —— DataTable GetDataTable(string sql, CommandType type, params SqlParameter[] args)
        /// <summary>
        /// 返回DataTable方法
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="type">命令类型</param>
        /// <param name="args">参数</param>
        /// <returns>DataTable对象</returns>
        private DataTable GetDataTable(string sql, CommandType type, params SqlParameter[] args)
        {
            #region # 验证参数

            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException("sql", @"SQL语句不可为空！");
            }

            #endregion

            DataTable dataTable = new DataTable();
            using (SqlConnection conn = this.CreateConnection())
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn) { SelectCommand = { CommandType = type } };
                adapter.SelectCommand.Parameters.AddRange(args);
                conn.Open();
                adapter.Fill(dataTable);
            }
            return dataTable;
        }
        #endregion

        #region # 返回DataSet方法 —— DataSet GetDataSet(string sql, CommandType type, params SqlParameter[] args)
        /// <summary>
        /// 返回DataSet方法
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="type">命令类型</param>
        /// <param name="args">参数</param>
        /// <returns>DataSet对象</returns>
        private DataSet GetDataSet(string sql, CommandType type, params SqlParameter[] args)
        {
            #region # 验证参数

            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException("sql", @"SQL语句不可为空！");
            }

            #endregion

            using (SqlConnection conn = this.CreateConnection())
            {
                DataSet dataSet = new DataSet();
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, conn))
                {
                    adapter.SelectCommand.Parameters.AddRange(args);
                    adapter.SelectCommand.CommandType = type;
                    conn.Open();
                    adapter.Fill(dataSet);
                }
                return dataSet;
            }
        }
        #endregion
    }
}
