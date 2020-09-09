using SD.Toolkits.SerialNumber.Commons;
using SD.Toolkits.SerialNumber.Entities;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace SD.Toolkits.SerialNumber.Repositories
{
    /// <summary>
    /// 序列种子仓储实现
    /// </summary>
    internal sealed class SerialSeedRepository
    {
        #region # 常量、字段及构造器

        /// <summary>
        /// 连接字符串名称AppSetting键
        /// </summary>
        private const string ConnectionStringAppSettingKey = "SerialNumberConnection";

        /// <summary>
        /// SQL Helper
        /// </summary>
        private static readonly SqlHelper _SqlHelper;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static SerialSeedRepository()
        {
            //初始化连接字符串
            string defaultConnectionStringName = ConfigurationManager.AppSettings[ConnectionStringAppSettingKey];

            #region # 验证

            if (string.IsNullOrWhiteSpace(defaultConnectionStringName))
            {
                throw new ApplicationException("序列号生成器连接字符串名称未设置！");
            }

            #endregion

            string connectionString = ConfigurationManager.ConnectionStrings[defaultConnectionStringName].ConnectionString;

            #region # 验证

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ApplicationException("序列号生成器连接字符串未设置！");
            }

            #endregion

            //初始化SQL工具
            _SqlHelper = new SqlHelper(connectionString);
        }

        /// <summary>
        /// 构造器
        /// </summary>
        public SerialSeedRepository()
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
        /// <param name="stem">词根</param>
        /// <param name="postfix">后缀</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="serialLength">流水长度</param>
        /// <returns>序列种子</returns>
        /// <remarks>如没有，则返回null</remarks>
        public SerialSeed SingleOrDefault(string seedName, string prefix, string stem, string postfix, string timestamp, int serialLength)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("SELECT * FROM dbo.SerialSeeds ");
            sqlBuilder.Append("WHERE [Name] = @Name ");
            sqlBuilder.Append("AND [Prefix] = @Prefix ");
            sqlBuilder.Append("AND [Stem] = @Stem ");
            sqlBuilder.Append("AND [Postfix] = @Postfix ");
            sqlBuilder.Append("AND [Timestamp] = @Timestamp ");
            sqlBuilder.Append("AND [SerialLength] = @SerialLength ");

            SqlParameter[] parameters =
            {
                new SqlParameter("@Name", this.ToDbValue(seedName)),
                new SqlParameter("@Prefix", this.ToDbValue(prefix)),
                new SqlParameter("@Stem", this.ToDbValue(stem)),
                new SqlParameter("@Postfix", this.ToDbValue(postfix)),
                new SqlParameter("@Timestamp", this.ToDbValue(timestamp)),
                new SqlParameter("@SerialLength", this.ToDbValue(serialLength))
            };

            using (SqlDataReader reader = _SqlHelper.ExecuteReader(sqlBuilder.ToString(), parameters))
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
            sqlBuilder.Append("	([Id], [Name], [Prefix], [Stem], [Postfix], [Timestamp], [SerialLength], [TodayCount], [Description]) ");
            sqlBuilder.Append("OUTPUT Inserted.Id ");
            sqlBuilder.Append("VALUES ");
            sqlBuilder.Append("	(NEWID(), @Name , @Prefix, @Stem, @Postfix, @Timestamp, @SerialLength, @TodayCount, @Description) ");

            SqlParameter[] parameters =
            {
                new SqlParameter("@Name", this.ToDbValue(serialSeed.Name)),
                new SqlParameter("@Prefix", this.ToDbValue(serialSeed.Prefix)),
                new SqlParameter("@Stem", this.ToDbValue(serialSeed.Stem)),
                new SqlParameter("@Postfix", this.ToDbValue(serialSeed.Postfix)),
                new SqlParameter("@Timestamp", this.ToDbValue(serialSeed.Timestamp)),
                new SqlParameter("@SerialLength", this.ToDbValue(serialSeed.SerialLength)),
                new SqlParameter("@TodayCount", this.ToDbValue(serialSeed.TodayCount)),
                new SqlParameter("@Description", this.ToDbValue(serialSeed.Description))
            };

            Guid serialSeedId = _SqlHelper.ExecuteScalar<Guid>(sqlBuilder.ToString(), parameters);
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
            sqlBuilder.Append("	[Stem] = @Stem, ");
            sqlBuilder.Append("	[Postfix] = @Postfix, ");
            sqlBuilder.Append("	[Timestamp] = @Timestamp, ");
            sqlBuilder.Append("	[SerialLength] = @SerialLength, ");
            sqlBuilder.Append("	[TodayCount] = @TodayCount, ");
            sqlBuilder.Append("	[Description] = @Description ");
            sqlBuilder.Append("WHERE Id = @Id ");

            SqlParameter[] parameters =
            {
                new SqlParameter("@Id", serialSeed.Id),
                new SqlParameter("@Name", this.ToDbValue(serialSeed.Name)),
                new SqlParameter("@Prefix", this.ToDbValue(serialSeed.Prefix)),
                new SqlParameter("@Stem", this.ToDbValue(serialSeed.Stem)),
                new SqlParameter("@Postfix", this.ToDbValue(serialSeed.Postfix)),
                new SqlParameter("@Timestamp", this.ToDbValue(serialSeed.Timestamp)),
                new SqlParameter("@SerialLength", this.ToDbValue(serialSeed.SerialLength)),
                new SqlParameter("@TodayCount", this.ToDbValue(serialSeed.TodayCount)),
                new SqlParameter("@Description", this.ToDbValue(serialSeed.Description))
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
            sqlBuilder.Append("	[Stem] [NVARCHAR](MAX) NULL, ");
            sqlBuilder.Append("	[Postfix] [NVARCHAR](MAX) NULL, ");
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
        private SerialSeed ToModel(SqlDataReader reader)
        {
            SerialSeed serialSeed = new SerialSeed
            {
                Id = (Guid)this.ToModelValue(reader, "Id"),
                Name = (string)this.ToModelValue(reader, "Name"),
                Prefix = (string)this.ToModelValue(reader, "Prefix"),
                Stem = (string)this.ToModelValue(reader, "Stem"),
                Postfix = (string)this.ToModelValue(reader, "Postfix"),
                Timestamp = (string)this.ToModelValue(reader, "Timestamp"),
                SerialLength = (int)this.ToModelValue(reader, "SerialLength"),
                TodayCount = (int)this.ToModelValue(reader, "TodayCount"),
                Description = (string)this.ToModelValue(reader, "Description")
            };

            return serialSeed;
        }
        #endregion

        #region # C#值转数据库值空值处理 —— object ToDbValue(object value)
        /// <summary>
        /// C#值转数据库值空值处理
        /// </summary>
        /// <param name="value">C#值</param>
        /// <returns>处理后的数据库值</returns>
        private object ToDbValue(object value)
        {
            return value ?? DBNull.Value;
        }

        #endregion

        #region # 数据库值转C#值空值处理 —— object ToModelValue(SqlDataReader reader, string columnName)
        /// <summary>
        /// 数据库值转C#值空值处理
        /// </summary>
        /// <param name="reader">IDataReader对象</param>
        /// <param name="columnName">列名</param>
        /// <returns>C#值</returns>
        private object ToModelValue(SqlDataReader reader, string columnName)
        {
            if (reader.IsDBNull(reader.GetOrdinal(columnName)))
            {
                return null;
            }

            return reader[columnName];
        }

        #endregion
    }
}
