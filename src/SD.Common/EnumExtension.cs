﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace SD.Common
{
    /// <summary>
    /// 枚举扩展
    /// </summary>
    public static class EnumExtension
    {
        #region # 常量

        /// <summary>
        /// 枚举值字段
        /// </summary>
        private const string EnumValueField = "value__";

        #endregion

        #region # 获取枚举成员名称 —— static string GetEnumMember(this Enum @enum)
        /// <summary>
        /// 获取枚举成员名称
        /// </summary>
        /// <param name="enum">枚举值</param>
        /// <returns>枚举成员名称</returns>
        public static string GetEnumMember(this Enum @enum)
        {
            Type type = @enum.GetType();
            FieldInfo field = type.GetField(@enum.ToString());
            DescriptionAttribute enumMember = field.GetCustomAttribute<DescriptionAttribute>();
            string enumMemberDescription = enumMember == null
                ? @enum.ToString()
                : string.IsNullOrWhiteSpace(enumMember.Description)
                    ? @enum.ToString()
                    : enumMember.Description;

            return enumMemberDescription;
        }
        #endregion

        #region # 获取枚举成员名称字典 —— static IDictionary<string, string> GetEnumMembers(...
        /// <summary>
        /// 获取枚举成员名称字典
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns>枚举成员名称字典</returns>
        /// <remarks>IDictionary[string, string]，[枚举名，枚举描述]</remarks>
        public static IDictionary<string, string> GetEnumMembers(this Type enumType)
        {
            #region # 验证

            if (!enumType.IsSubclassOf(typeof(Enum)))
            {
                throw new ArgumentOutOfRangeException($"类型\"{enumType.Name}\"不是枚举类型！");
            }

            #endregion

            FieldInfo[] fields = enumType.GetFields();
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (FieldInfo field in fields.Where(x => x.Name != EnumValueField))
            {
                DescriptionAttribute enumMember = field.GetCustomAttribute<DescriptionAttribute>();
                string enumMemberDescription = enumMember == null
                    ? field.Name
                    : string.IsNullOrEmpty(enumMember.Description)
                        ? field.Name
                        : enumMember.Description;
                dictionary.Add(field.Name, enumMemberDescription);
            }

            return dictionary;
        }
        #endregion

        #region # 获取枚举值/描述字典 —— static IDictionary<int, string> GetEnumDictionary(...
        /// <summary>
        /// 获取枚举值/描述字典
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns>枚举值、描述字典</returns>
        /// <remarks>IDictionary[int, string]，[枚举int值，枚举描述]</remarks>
        public static IDictionary<int, string> GetEnumDictionary(this Type enumType)
        {
            IEnumerable<Tuple<int, string, string>> tuples = GetEnumMemberInfos(enumType);
            IDictionary<int, string> dictionary = new Dictionary<int, string>();
            foreach (Tuple<int, string, string> tuple in tuples)
            {
                dictionary.Add(tuple.Item1, tuple.Item3);
            }

            return dictionary;
        }
        #endregion

        #region # 获取枚举类型完整信息 —— static ICollection<Tuple<int, string, string>> GetEnumMemberInfos(...
        /// <summary>
        /// 获取枚举类型完整信息
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns>完整信息</returns>
        /// <remarks>Tuple[int, string, string]，[枚举int值，枚举名，枚举描述]</remarks>
        public static ICollection<Tuple<int, string, string>> GetEnumMemberInfos(this Type enumType)
        {
            #region # 验证

            if (!enumType.IsSubclassOf(typeof(Enum)))
            {
                throw new ArgumentOutOfRangeException($"类型\"{enumType.Name}\"不是枚举类型！");
            }

            #endregion

            FieldInfo[] fields = enumType.GetFields();

            ICollection<Tuple<int, string, string>> enumInfos = new HashSet<Tuple<int, string, string>>();
            foreach (FieldInfo field in fields.Where(x => x.Name != EnumValueField))
            {
                DescriptionAttribute enumMember = field.GetCustomAttribute<DescriptionAttribute>();
                int value = Convert.ToInt32(field.GetValue(Activator.CreateInstance(enumType)));
                enumInfos.Add(new Tuple<int, string, string>(value, field.Name, enumMember == null ? field.Name : string.IsNullOrEmpty(enumMember.Description) ? field.Name : enumMember.Description));
            }

            return enumInfos;
        }
        #endregion

        #region # 字符串转换枚举值 —— static T AsEnumTo<T>(this string enumText)
        /// <summary>
        /// 字符串转换枚举值
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="enumText">枚举字符串</param>
        /// <returns>枚举值</returns>
        public static T AsEnumTo<T>(this string enumText) where T : struct
        {
            #region # 验证

            if (string.IsNullOrWhiteSpace(enumText))
            {
                throw new ArgumentNullException(nameof(enumText), "枚举字符串不可为空！");
            }
            if (typeof(T).IsSubclassOf(typeof(Enum)))
            {
                throw new ArgumentOutOfRangeException(nameof(T), $"类型\"{typeof(T).Name}\"不是枚举！");
            }

            #endregion

            if (!Enum.TryParse(enumText, out T @enum))
            {
                throw new InvalidCastException($"无法将给定字符串\"{enumText}\"转换为枚举\"{typeof(T).Name}\"！");
            }

            return @enum;
        }
        #endregion
    }
}
