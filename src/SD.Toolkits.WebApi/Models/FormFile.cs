using System;

namespace SD.Toolkits.WebApi.Models
{
    /// <summary>
    /// Http请求文件
    /// </summary>
    public sealed class FormFile : IFormFile
    {
        /// <summary>
        /// 表单名称
        /// </summary>
        public string FormName { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 内容类型
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// 内容长度
        /// </summary>
        public long ContentLength { get; set; }

        /// <summary>
        /// 文件数据
        /// </summary>
        public byte[] Datas { get; set; }

        /// <summary>
        /// 文件地址集
        /// </summary>
        public Uri[] Uris { get; set; }
    }
}
