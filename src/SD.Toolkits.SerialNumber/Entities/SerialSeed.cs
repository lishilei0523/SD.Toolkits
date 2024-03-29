﻿using System;

namespace SD.Toolkits.SerialNumber.Entities
{
    /// <summary>
    /// 序列种子
    /// </summary>
    public class SerialSeed
    {
        #region # 构造器

        #region 01.无参构造器
        /// <summary>
        /// 无参构造器
        /// </summary>
        public SerialSeed()
        {
            //默认值
            this.TodayCount = 1;
        }
        #endregion

        #region 01.创建序列种子构造器
        /// <summary>
        /// 创建序列种子构造器
        /// </summary>
        /// <param name="seedName">种子名称</param>
        /// <param name="prefix">前缀</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="serialLength">流水长度</param>
        /// <param name="description">描述</param>
        public SerialSeed(string seedName, string prefix, string timestamp, int serialLength, string description)
            : this()
        {
            this.Name = string.IsNullOrWhiteSpace(seedName) ? string.Empty : seedName;
            this.Prefix = string.IsNullOrWhiteSpace(prefix) ? string.Empty : prefix;
            this.Timestamp = string.IsNullOrWhiteSpace(timestamp) ? string.Empty : timestamp;
            this.SerialLength = serialLength < 1 ? 1 : serialLength;
            this.Description = description;
        }
        #endregion

        #endregion

        #region # 属性

        #region 标识Id —— Guid Id
        /// <summary>
        /// 标识Id
        /// </summary>
        public Guid Id { get; set; }
        #endregion

        #region 种子名称 —— string Name
        /// <summary>
        /// 种子名称
        /// </summary>
        public string Name { get; set; }
        #endregion

        #region 前缀 —— string Prefix
        /// <summary>
        /// 前缀
        /// </summary>
        public string Prefix { get; set; }
        #endregion

        #region 时间戳 —— string Timestamp
        /// <summary>
        /// 时间戳
        /// </summary>
        public string Timestamp { get; set; }
        #endregion

        #region 流水长度 —— int SerialLength
        /// <summary>
        /// 流水长度
        /// </summary>
        public int SerialLength { get; set; }
        #endregion

        #region 当天流水数 —— int TodayCount
        /// <summary>
        /// 当天流水数
        /// </summary>
        public int TodayCount { get; set; }
        #endregion

        #region 描述 —— string Description
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        #endregion

        #endregion

        #region # 方法

        #region 更新序列种子 —— void UpdateInfo(int todayCount)
        /// <summary>
        /// 更新序列种子
        /// </summary>
        /// <param name="todayCount">当天流水数</param>
        public void UpdateInfo(int todayCount)
        {
            this.TodayCount = todayCount;
        }
        #endregion

        #endregion
    }
}
