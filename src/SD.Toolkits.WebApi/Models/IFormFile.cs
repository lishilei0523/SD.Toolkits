using System;

namespace SD.Toolkits.WebApi.Models
{
    /// <summary>
    /// Http请求文件接口
    /// </summary>
    public interface IFormFile
    {
        /// <summary>
        /// 表单名称
        /// </summary>
        string FormName { get; }

        /// <summary>
        /// 文件名称
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// 内容类型
        /// </summary>
        string ContentType { get; }

        /// <summary>
        /// 内容长度
        /// </summary>
        long? ContentLength { get; }

        /// <summary>
        /// 文件数据
        /// </summary>
        byte[] Datas { get; set; }

        /// <summary>
        /// 文件地址集
        /// </summary>
        Uri[] Uris { get; set; }
    }
}
