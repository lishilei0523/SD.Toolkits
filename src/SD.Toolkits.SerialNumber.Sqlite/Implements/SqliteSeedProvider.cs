using SD.Toolkits.SerialNumber.Entities;
using SD.Toolkits.SerialNumber.Interfaces;
using SD.Toolkits.Sql;
using SD.Toolkits.Sql.SQLite;
using System;
using System.Configuration;
using System.Data;
using System.Text;
#if NET40 || NET45
using System.Data.SQLite;
#endif
#if NET461_OR_GREATER || NETSTANDARD2_0_OR_GREATER
using Microsoft.Data.Sqlite;
#endif

// ReSharper disable once CheckNamespace
namespace SD.Toolkits.SerialNumber
{
    /// <summary>
    /// 序列种子提供者SQLite实现
    /// </summary>
    public sealed class SqliteSeedProvider : ISerialSeedProvider
    {
        #region # 常量、字段及构造器

        /// <summary>
        /// SQL Helper
        /// </summary>
        private static readonly SqliteHelper _SqlHelper;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static SqliteSeedProvider()
        {
            string connectionString = null;

            #region # 验证

            if (!string.IsNullOrWhiteSpace(SerialNumberSection.Setting.ConnectionString.Name))
            {
                connectionString = ConfigurationManager.ConnectionStrings[SerialNumberSection.Setting.ConnectionString.Name]?.ConnectionString;
            }
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                connectionString = SerialNumberSection.Setting.ConnectionString.Value;
            }
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new NullReferenceException("序列号生成器连接字符串未配置！");
            }

            #endregion

            //初始化SQL工具
            _SqlHelper = new SqliteHelper(connectionString);

            //初始化数据表
            InitTable();
        }

        #endregion


        //Internal

        #region # 获取唯一序列种子 —— SerialSeed SingleOrDefault(string seedName...
        /// <summary>
        /// 获取唯一序列种子，
        /// </summary>
        /// <param name="seedName">种子名称</param>
        /// <param name="prefix">前缀</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="serialLength">流水长度</param>
        /// <returns>序列种子</returns>
        /// <remarks>如没有，则返回null</remarks>
        public SerialSeed SingleOrDefault(string seedName, string prefix, string timestamp, int serialLength)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("SELECT * FROM SerialSeeds ");
            sqlBuilder.Append("WHERE Name = @Name ");
            sqlBuilder.Append("AND Prefix = @Prefix ");
            sqlBuilder.Append("AND Timestamp = @Timestamp ");
            sqlBuilder.Append("AND SerialLength = @SerialLength ");

            IDbDataParameter[] parameters =
            {
#if NET40 || NET45
                new SQLiteParameter("@Name", seedName.ToDbValue()),
                new SQLiteParameter("@Prefix", prefix.ToDbValue()),
                new SQLiteParameter("@Timestamp", timestamp.ToDbValue()),
                new SQLiteParameter("@SerialLength", serialLength.ToDbValue())
#endif
#if NET461_OR_GREATER || NETSTANDARD2_0_OR_GREATER
                new SqliteParameter("@Name", seedName.ToDbValue()),
                new SqliteParameter("@Prefix", prefix.ToDbValue()),
                new SqliteParameter("@Timestamp", timestamp.ToDbValue()),
                new SqliteParameter("@SerialLength", serialLength.ToDbValue())
#endif
            };

            using (IDataReader reader = _SqlHelper.ExecuteReader(sqlBuilder.ToString(), parameters))
            {
                return reader.Read() ? this.ToModel(reader) : null;
            }
        }
        #endregion

        #region # 创建序列种子 —— void Create(SerialSeed serialSeed)
        /// <summary>
        /// 创建序列种子
        /// </summary>
        /// <param name="serialSeed">序列种子</param>
        public void Create(SerialSeed serialSeed)
        {
            serialSeed.Id = Guid.NewGuid();
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("INSERT INTO SerialSeeds ");
            sqlBuilder.Append("	(Id, Name, Prefix, Timestamp, SerialLength, TodayCount, Description) ");
            sqlBuilder.Append("VALUES ");
            sqlBuilder.Append(" (@Id, @Name, @Prefix, @Timestamp, @SerialLength, @TodayCount, @Description) ");

            IDbDataParameter[] parameters =
            {
#if NET40 || NET45
                new SQLiteParameter("@Id", serialSeed.Id.ToDbValue()),
                new SQLiteParameter("@Name", serialSeed.Name.ToDbValue()),
                new SQLiteParameter("@Prefix", serialSeed.Prefix.ToDbValue()),
                new SQLiteParameter("@Timestamp", serialSeed.Timestamp.ToDbValue()),
                new SQLiteParameter("@SerialLength", serialSeed.SerialLength.ToDbValue()),
                new SQLiteParameter("@TodayCount", serialSeed.TodayCount.ToDbValue()),
                new SQLiteParameter("@Description", serialSeed.Description.ToDbValue())
#endif
#if NET461_OR_GREATER || NETSTANDARD2_0_OR_GREATER
                new SqliteParameter("@Id", serialSeed.Id.ToDbValue()),
                new SqliteParameter("@Name", serialSeed.Name.ToDbValue()),
                new SqliteParameter("@Prefix", serialSeed.Prefix.ToDbValue()),
                new SqliteParameter("@Timestamp", serialSeed.Timestamp.ToDbValue()),
                new SqliteParameter("@SerialLength", serialSeed.SerialLength.ToDbValue()),
                new SqliteParameter("@TodayCount", serialSeed.TodayCount.ToDbValue()),
                new SqliteParameter("@Description", serialSeed.Description.ToDbValue())
#endif
            };

            _SqlHelper.ExecuteNonQuery(sqlBuilder.ToString(), parameters);
        }
        #endregion

        #region # 保存序列种子 —— void Save(SerialSeed serialSeed)
        /// <summary>
        /// 保存序列种子
        /// </summary>
        /// <param name="serialSeed">序列种子</param>
        /// <returns>受影响的行数</returns>
        public void Save(SerialSeed serialSeed)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("UPDATE SerialSeeds SET ");
            sqlBuilder.Append("	Name = @Name, ");
            sqlBuilder.Append("	Prefix = @Prefix, ");
            sqlBuilder.Append("	Timestamp = @Timestamp, ");
            sqlBuilder.Append("	SerialLength = @SerialLength, ");
            sqlBuilder.Append("	TodayCount = @TodayCount, ");
            sqlBuilder.Append("	Description = @Description ");
            sqlBuilder.Append("WHERE Id = @Id ");

            IDbDataParameter[] parameters =
            {
#if NET40 || NET45
                new SQLiteParameter("@Id", serialSeed.Id),
                new SQLiteParameter("@Name", serialSeed.Name.ToDbValue()),
                new SQLiteParameter("@Prefix", serialSeed.Prefix.ToDbValue()),
                new SQLiteParameter("@Timestamp", serialSeed.Timestamp.ToDbValue()),
                new SQLiteParameter("@SerialLength", serialSeed.SerialLength.ToDbValue()),
                new SQLiteParameter("@TodayCount", serialSeed.TodayCount.ToDbValue()),
                new SQLiteParameter("@Description", serialSeed.Description.ToDbValue())
#endif
#if NET461_OR_GREATER || NETSTANDARD2_0_OR_GREATER
                new SqliteParameter("@Id", serialSeed.Id),
                new SqliteParameter("@Name", serialSeed.Name.ToDbValue()),
                new SqliteParameter("@Prefix", serialSeed.Prefix.ToDbValue()),
                new SqliteParameter("@Timestamp", serialSeed.Timestamp.ToDbValue()),
                new SqliteParameter("@SerialLength", serialSeed.SerialLength.ToDbValue()),
                new SqliteParameter("@TodayCount", serialSeed.TodayCount.ToDbValue()),
                new SqliteParameter("@Description", serialSeed.Description.ToDbValue())
#endif
            };

            _SqlHelper.ExecuteNonQuery(sqlBuilder.ToString(), parameters);
        }
        #endregion


        //Private

        #region # 初始化数据表 —— static void InitTable()
        /// <summary>
        /// 初始化数据表
        /// </summary>
        private static void InitTable()
        {
            //构造sql语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("CREATE TABLE IF NOT EXISTS SerialSeeds ( ");
            sqlBuilder.Append("	Id TEXT PRIMARY KEY NOT NULL, ");
            sqlBuilder.Append(" Name TEXT default NULL, ");
            sqlBuilder.Append(" Prefix TEXT default NULL, ");
            sqlBuilder.Append(" Timestamp TEXT default NULL, ");
            sqlBuilder.Append("	SerialLength INT NOT NULL, ");
            sqlBuilder.Append("	TodayCount INT NOT NULL, ");
            sqlBuilder.Append("	Description TEXT default NULL ");
            sqlBuilder.Append("); ");

            //执行创建表
            _SqlHelper.ExecuteNonQuery(sqlBuilder.ToString());
        }
        #endregion

        #region # 根据SqlDataReader返回对象 —— SerialSeed ToModel(SqlDataReader reader)
        /// <summary>
        /// 根据SqlDataReader返回对象
        /// </summary>
        /// <param name="reader">SqlDataReader对象</param>
        /// <returns>实体对象</returns>
        private SerialSeed ToModel(IDataReader reader)
        {
            SerialSeed serialSeed = new SerialSeed
            {
                Id = Guid.Parse(reader.ToClsValue("Id").ToString()),
                Name = (string)reader.ToClsValue("Name"),
                Prefix = (string)reader.ToClsValue("Prefix"),
                Timestamp = (string)reader.ToClsValue("Timestamp"),
                SerialLength = Convert.ToInt32(reader.ToClsValue("SerialLength")),
                TodayCount = Convert.ToInt32(reader.ToClsValue("TodayCount")),
                Description = (string)reader.ToClsValue("Description")
            };

            return serialSeed;
        }
        #endregion
    }
}
