using System;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SD.Toolkits.Json
{
    /// <summary>
    /// JSON扩展
    /// </summary>
    public static class JsonExtension
    {
        #region # 序列化JSON —— static string ToJson(this object instance...
        /// <summary>
        /// 序列化JSON
        /// </summary>
        /// <param name="instance">实例</param>
        /// <param name="dateTimeFormat">日期时间格式</param>
        /// <returns>JSON文本</returns>
        public static string ToJson(this object instance, string dateTimeFormat = null)
        {
            #region # 验证

            if (instance == null)
            {
                return null;
            }

            #endregion

            try
            {
                JsonSerializerOptions settting = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };
                if (!string.IsNullOrWhiteSpace(dateTimeFormat))
                {
                    settting.Converters.Add(new DateTimeConverter(dateTimeFormat));
                }
                string json = JsonSerializer.Serialize(instance, settting);

                return json;
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException("无法将给定实例序列化为JSON，请检查类型后重试！", exception);
            }
        }
        #endregion

        #region # JSON反序列化实例 —— static T AsJsonTo<T>(this string json...
        /// <summary>
        /// JSON反序列化实例
        /// </summary>
        /// <param name="json">JSON文本</param>
        /// <param name="dateTimeFormat">日期时间格式</param>
        /// <returns>实例</returns>
        public static T AsJsonTo<T>(this string json, string dateTimeFormat = null)
        {
            #region # 验证

            if (string.IsNullOrWhiteSpace(json))
            {
                return default(T);
            }

            #endregion

            try
            {
                JsonSerializerOptions settting = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };
                if (!string.IsNullOrWhiteSpace(dateTimeFormat))
                {
                    settting.Converters.Add(new DateTimeConverter(dateTimeFormat));
                }
                T instance = JsonSerializer.Deserialize<T>(json, settting);

                return instance;
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException($"无法将给定JSON反序列化为类型\"{typeof(T).Name}\"，请检查类型后重试！", exception);
            }
        }
        #endregion
    }
}
