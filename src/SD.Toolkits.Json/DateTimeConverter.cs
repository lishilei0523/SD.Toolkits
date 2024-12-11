using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SD.Toolkits.Json
{
    /// <summary>
    /// 日期时间转换器
    /// </summary>
    internal class DateTimeConverter : JsonConverter<DateTime>
    {
        /// <summary>
        /// 日期时间格式
        /// </summary>
        private readonly string _dateTimeFormat;

        /// <summary>
        /// 创建日期时间转换器构造器
        /// </summary>
        /// <param name="dateTimeFormat">日期时间格式</param>
        internal DateTimeConverter(string dateTimeFormat)
        {
            if (string.IsNullOrWhiteSpace(dateTimeFormat))
            {
                throw new ArgumentNullException(nameof(dateTimeFormat), "日期时间格式不可为空！");
            }

            this._dateTimeFormat = dateTimeFormat;
        }

        /// <summary>
        /// 序列化日期时间
        /// </summary>
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(this._dateTimeFormat));
        }

        /// <summary>
        /// 反序列化日期时间
        /// </summary>
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                if (DateTime.TryParse(reader.GetString(), out DateTime dateTime))
                {
                    return dateTime;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(reader), "字符串格式不正确！");
                }
            }
            else
            {
                return reader.GetDateTime();
            }
        }
    }
}
