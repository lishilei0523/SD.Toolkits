using Newtonsoft.Json;
using System;

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
                JsonSerializerSettings settting = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };
                if (!string.IsNullOrWhiteSpace(dateTimeFormat))
                {
                    settting.DateFormatString = dateTimeFormat!;
                }
                string json = JsonConvert.SerializeObject(instance, Formatting.None, settting);

                return json;
            }
            catch (InvalidOperationException exception)
            {
                throw new InvalidOperationException($"无法将给定实例序列化为JSON，请检查类型后重试！", exception);
            }
        }
        #endregion

        #region # JSON反序列化对象 —— static T AsJsonTo<T>(this string json)
        /// <summary>
        /// JSON反序列化对象
        /// </summary>
        /// <param name="json">JSON文本</param>
        /// <returns>实例</returns>
        public static T AsJsonTo<T>(this string json)
        {
            #region # 验证

            if (string.IsNullOrWhiteSpace(json))
            {
                return default(T);
            }

            #endregion

            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (InvalidOperationException exception)
            {
                throw new InvalidOperationException($"无法将给定JSON反序列化为类型\"{typeof(T).Name}\"，请检查类型后重试！", exception);
            }
        }
        #endregion
    }
}
