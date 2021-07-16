using SD.Toolkits.SerialNumber.Entities;
using SD.Toolkits.SerialNumber.Interfaces;
using SD.Toolkits.Sql;
using SD.Toolkits.Sql.SqlServer;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

// ReSharper disable once CheckNamespace
namespace SD.Toolkits.SerialNumber
{
    /// <summary>
    /// 序列种子提供者SQL Server实现
    /// </summary>
    public sealed class SqlServerSeedProvider : ISerialSeedProvider
    {
        #region # 常量、字段及构造器

        /// <summary>
        /// SQL Helper
        /// </summary>
        private static readonly SqlServerHelper _SqlHelper;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static SqlServerSeedProvider()
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
            _SqlHelper = new SqlServerHelper(connectionString);
        }

        /// <summary>
        /// 构造器
        /// </summary>
        public SqlServerSeedProvider()
        {
            //初始化数据表
            this.InitTable();
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
            sqlBuilder.Append("SELECT * FROM dbo.SerialSeeds ");
            sqlBuilder.Append("WHERE [Name] = @Name ");
            sqlBuilder.Append("AND [Prefix] = @Prefix ");
            sqlBuilder.Append("AND [Timestamp] = @Timestamp ");
            sqlBuilder.Append("AND [SerialLength] = @SerialLength ");

            IDbDataParameter[] parameters =
            {
                new SqlParameter("@Name", seedName.ToDbValue()),
                new SqlParameter("@Prefix", prefix.ToDbValue()),
                new SqlParameter("@Timestamp", timestamp.ToDbValue()),
                new SqlParameter("@SerialLength", serialLength.ToDbValue())
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
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("INSERT INTO dbo.SerialSeeds ");
            sqlBuilder.Append("	([Id], [Name], [Prefix], [Timestamp], [SerialLength], [TodayCount], [Description]) ");
            sqlBuilder.Append("OUTPUT Inserted.Id ");
            sqlBuilder.Append("VALUES ");
            sqlBuilder.Append("	(NEWID(), @Name , @Prefix, @Timestamp, @SerialLength, @TodayCount, @Description) ");

            IDbDataParameter[] parameters =
            {
                new SqlParameter("@Name", serialSeed.Name.ToDbValue()),
                new SqlParameter("@Prefix", serialSeed.Prefix.ToDbValue()),
                new SqlParameter("@Timestamp", serialSeed.Timestamp.ToDbValue()),
                new SqlParameter("@SerialLength", serialSeed.SerialLength.ToDbValue()),
                new SqlParameter("@TodayCount", serialSeed.TodayCount.ToDbValue()),
                new SqlParameter("@Description", serialSeed.Description.ToDbValue())
            };

            object result = _SqlHelper.ExecuteScalar(sqlBuilder.ToString(), parameters);
            Guid serialSeedId = Guid.Parse(result.ToString());
            serialSeed.Id = serialSeedId;
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
            sqlBuilder.Append("UPDATE dbo.SerialSeeds SET ");
            sqlBuilder.Append("	[Name] = @Name, ");
            sqlBuilder.Append("	[Prefix] = @Prefix, ");
            sqlBuilder.Append("	[Timestamp] = @Timestamp, ");
            sqlBuilder.Append("	[SerialLength] = @SerialLength, ");
            sqlBuilder.Append("	[TodayCount] = @TodayCount, ");
            sqlBuilder.Append("	[Description] = @Description ");
            sqlBuilder.Append("WHERE Id = @Id ");

            IDbDataParameter[] parameters =
            {
                new SqlParameter("@Id", serialSeed.Id),
                new SqlParameter("@Name", serialSeed.Name.ToDbValue()),
                new SqlParameter("@Prefix", serialSeed.Prefix.ToDbValue()),
                new SqlParameter("@Timestamp", serialSeed.Timestamp.ToDbValue()),
                new SqlParameter("@SerialLength", serialSeed.SerialLength.ToDbValue()),
                new SqlParameter("@TodayCount", serialSeed.TodayCount.ToDbValue()),
                new SqlParameter("@Description", serialSeed.Description.ToDbValue())
            };

            _SqlHelper.ExecuteNonQuery(sqlBuilder.ToString(), parameters);
        }
        #endregion


        //Private

        #region # 初始化数据表 —— static void InitTable()
        /// <summary>
        /// 初始化数据表
        /// </summary>
        private void InitTable()
        {
            //构造sql语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("IF OBJECT_ID('[dbo].[SerialSeeds]') IS NULL ");
            sqlBuilder.Append("BEGIN ");
            sqlBuilder.Append("CREATE TABLE [dbo].[SerialSeeds] ( ");
            sqlBuilder.Append("	[Id] [uniqueidentifier] PRIMARY KEY NOT NULL, ");
            sqlBuilder.Append("	[Name] [NVARCHAR](MAX) NULL, ");
            sqlBuilder.Append("	[Prefix] [NVARCHAR](MAX) NULL, ");
            sqlBuilder.Append("	[Timestamp] [NVARCHAR](MAX) NULL, ");
            sqlBuilder.Append("	[SerialLength] [INT] NOT NULL, ");
            sqlBuilder.Append("	[TodayCount] [INT] NOT NULL, ");
            sqlBuilder.Append("	[Description] [NVARCHAR](MAX) NULL ");
            sqlBuilder.Append(") ");
            sqlBuilder.Append("END ");

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
                Id = (Guid)reader.ToClsValue("Id"),
                Name = (string)reader.ToClsValue("Name"),
                Prefix = (string)reader.ToClsValue("Prefix"),
                Timestamp = (string)reader.ToClsValue("Timestamp"),
                SerialLength = (int)reader.ToClsValue("SerialLength"),
                TodayCount = (int)reader.ToClsValue("TodayCount"),
                Description = (string)reader.ToClsValue("Description")
            };

            return serialSeed;
        }
        #endregion
    }
}
